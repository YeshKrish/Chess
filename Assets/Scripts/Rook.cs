using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Rook : MonoBehaviour
    {
        [SerializeField] private bool isWhite; // Set this to true if it's a white rook

        private ChessBoardPlacementHandler boardPlacementHandler;
        private ChessPlayerPlacementHandler playerPlacementHandler;
        [SerializeField] private GameObject _highlightPrefab;
        private Highlighter currentHighlighter;

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
    }
}