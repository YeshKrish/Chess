using UnityEngine;

namespace Chess.Scripts.Core
{
    public class King : MonoBehaviour
    {
        [SerializeField] private bool isWhite; // Set this to true if it's a white king

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
            // Calculate possible legal moves for the king
            int currentRow = playerPlacementHandler.row;
            int currentColumn = playerPlacementHandler.column;

            // Example logic for calculating possible moves
            boardPlacementHandler.ClearHighlights();

            // Check for possible moves in all adjacent tiles
            for (int i = currentRow - 1; i <= currentRow + 1; i++)
            {
                for (int j = currentColumn - 1; j <= currentColumn + 1; j++)
                {
                    if (i >= 0 && i < 8 && j >= 0 && j < 8)
                    {
                        ChessPlayerPlacementHandler handler = GetChessPlayerPlacementHandler(i, j);
                        if (handler == null)
                        {
                            CreateHighlight(i, j);
                        }
                        else if (handler.IsWhite != isWhite)
                        {
                            CreateHighlight(i, j);
                        }
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