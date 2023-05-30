using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Queen : MonoBehaviour
    {
        [SerializeField] private bool isWhite; // Set this to true if it's a white queen

        private ChessBoardPlacementHandler boardPlacementHandler;
        private ChessPlayerPlacementHandler playerPlacementHandler;
        [SerializeField] private GameObject _highlightPrefab;
        private Highlighter highlighter;
        private void Start()
        {
            boardPlacementHandler = ChessBoardPlacementHandler.Instance;
            playerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();
        }

        private void OnMouseDown()
        {
            // Calculate possible legal moves for the queen
            int currentRow = playerPlacementHandler.row;
            int currentColumn = playerPlacementHandler.column;

            // Clear previous highlights
            boardPlacementHandler.ClearHighlights();

            // Check for possible moves in the same row (forward and backward)
            for (int i = currentColumn + 1; i < 8; i++)
            {
                if (!CheckMove(currentRow, i))
                    break;
            }
            for (int i = currentColumn - 1; i >= 0; i--)
            {
                if (!CheckMove(currentRow, i))
                    break;
            }

            // Check for possible moves in the same column (upward and downward)
            for (int i = currentRow + 1; i < 8; i++)
            {
                if (!CheckMove(i, currentColumn))
                    break;
            }
            for (int i = currentRow - 1; i >= 0; i--)
            {
                if (!CheckMove(i, currentColumn))
                    break;
            }

            // Check for possible moves in the top-left diagonal
            int row = currentRow + 1;
            int col = currentColumn - 1;
            while (row < 8 && col >= 0)
            {
                if (!CheckMove(row, col))
                    break;
                row++;
                col--;
            }

            // Check for possible moves in the top-right diagonal
            row = currentRow + 1;
            col = currentColumn + 1;
            while (row < 8 && col < 8)
            {
                if (!CheckMove(row, col))
                    break;
                row++;
                col++;
            }

            // Check for possible moves in the bottom-left diagonal
            row = currentRow - 1;
            col = currentColumn - 1;
            while (row >= 0 && col >= 0)
            {
                if (!CheckMove(row, col))
                    break;
                row--;
                col--;
            }

            // Check for possible moves in the bottom-right diagonal
            row = currentRow - 1;
            col = currentColumn + 1;
            while (row >= 0 && col < 8)
            {
                if (!CheckMove(row, col))
                    break;
                row--;
                col++;
            }
        }

        private bool CheckMove(int row, int column)
        {
            GameObject tile = boardPlacementHandler.GetTile(row, column);
            if (tile != null)
            {
                ChessPlayerPlacementHandler handler = GetChessPlayerPlacementHandler(row, column);
                if (handler == null)
                {
                    // If the tile is empty, create a highlight
                    CreateHighlight(row, column);
                    return true;
                }
                else
                {
                    // If there is a piece, check if it is an enemy piece
                    if (handler.IsWhite != isWhite)
                    {
                        // If it is an enemy piece, create a highlight and stop further movement
                        CreateHighlight(row, column);
                    }
                    return false;
                }
            }
            return false;
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

        private void CreateHighlight(int row, int column)
        {
            GameObject highlight = Instantiate(_highlightPrefab, boardPlacementHandler.GetTile(row, column).transform.position, Quaternion.identity, boardPlacementHandler.GetTile(row, column).transform);
            highlighter = highlight.GetComponent<Highlighter>();
            highlighter.OnHighlightCollision += OnHighlightCollision;
        }

        private void OnHighlightCollision(bool isHitBlack)
        {
            if (isHitBlack)
            {
                // Stop creating highlights
                highlighter.OnHighlightCollision -= OnHighlightCollision;
            }
        }
    }
}