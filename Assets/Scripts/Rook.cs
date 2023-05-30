using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Rook : MonoBehaviour
    {
        [SerializeField] private bool isWhite; // Set this to true if it's a white rook

        private ChessBoardPlacementHandler boardPlacementHandler;
        private ChessPlayerPlacementHandler playerPlacementHandler;
        [SerializeField] private GameObject _highlightPrefab;
        private Highlighter currentHighlighter; // Reference to the current highlighter

        private void Start()
        {
            boardPlacementHandler = ChessBoardPlacementHandler.Instance;
            playerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();
        }

        private void OnMouseDown()
        {
            // Calculate possible legal moves for the rook
            int currentRow = playerPlacementHandler.row;
            int currentColumn = playerPlacementHandler.column;

            // Example logic for calculating possible moves
            boardPlacementHandler.ClearHighlights();

            // Check for possible moves in the same row
            for (int i = currentColumn + 1; i < 8; i++)
            {
                GameObject tile = boardPlacementHandler.GetTile(currentRow, i);
                if (tile != null)
                {
                    ChessPlayerPlacementHandler handler = GetChessPlayerPlacementHandler(currentRow, i);
                    if (handler == null)
                    {
                        CreateHighlight(currentRow, i);
                    }
                    else if (handler.IsWhite == isWhite)
                    {
                        break;
                    }
                    else
                    {
                        CreateHighlight(currentRow, i);
                        break;
                    }
                }
            }

            // Check for possible moves in the same column
            for (int i = currentRow + 1; i < 8; i++)
            {
                GameObject tile = boardPlacementHandler.GetTile(i, currentColumn);
                if (tile != null)
                {
                    ChessPlayerPlacementHandler handler = GetChessPlayerPlacementHandler(i, currentColumn);
                    if (handler == null)
                    {
                        CreateHighlight(i, currentColumn);
                    }
                    else if (handler.IsWhite == isWhite)
                    {
                        break;
                    }
                    else
                    {
                        CreateHighlight(i, currentColumn);
                        break;
                    }
                }
            }
        }

        private void CreateHighlight(int row, int column)
        {
            GameObject highlight = Instantiate(_highlightPrefab, boardPlacementHandler.GetTile(row, column).transform.position, Quaternion.identity, boardPlacementHandler.GetTile(row, column).transform);
            currentHighlighter = highlight.GetComponent<Highlighter>();
            currentHighlighter.OnHighlightCollision += OnHighlightCollision;
        }

        private void OnHighlightCollision(bool isHitBlack)
        {
            if (isHitBlack)
            {
                // Stop creating highlights
                currentHighlighter.OnHighlightCollision -= OnHighlightCollision;
            }
        }

        private ChessPlayerPlacementHandler GetChessPlayerPlacementHandler(int row, int column)
        {
            Collider2D[] colliders = Physics2D.OverlapPointAll(boardPlacementHandler.GetTile(row, column).transform.position);
            foreach (Collider2D collider in colliders)
            {
                ChessPlayerPlacementHandler handler = collider.GetComponent<ChessPlayerPlacementHandler>();
                if (handler != null)
                {
                    return handler;
                }
            }
            return null;
        }
    }
}
