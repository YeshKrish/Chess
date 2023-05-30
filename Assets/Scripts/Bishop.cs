using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Bishop : MonoBehaviour
    {
        [SerializeField] private bool isWhite; // Set this to true if it's a white bishop

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
            // Calculate possible legal moves for the bishop
            int currentRow = playerPlacementHandler.row;
            int currentColumn = playerPlacementHandler.column;

            // Clear previous highlights
            boardPlacementHandler.ClearHighlights();

            // Check for possible moves in the diagonal directions

            // Top-Left diagonal
            for (int i = 1; i < 8; i++)
            {
                int row = currentRow + i;
                int col = currentColumn - i;
                // Check if the row and column are within bounds
                if (row < 8 && col >= 0)
                {
                    // Check the tile at the current row and column
                    GameObject tile = boardPlacementHandler.GetTile(row, col);
                    if (tile != null)
                    {
                        // Check if there is a piece at the current tile
                        ChessPlayerPlacementHandler handler = GetChessPlayerPlacementHandler(row, col);
                        if (handler == null)
                        {
                            // If the tile is empty, create a highlight
                            CreateHighlight(row, col);
                        }
                        else
                        {
                            // If there is a piece, check if it is an enemy piece
                            if (handler.IsWhite != isWhite)
                            {
                                // If it is an enemy piece, create a highlight
                                CreateHighlight(row, col);
                            }
                            // Stop highlighting if there is a piece blocking the way
                            break;
                        }
                    }
                }
            }

            // Top-Right diagonal
            for (int i = 1; i < 8; i++)
            {
                int row = currentRow + i;
                int col = currentColumn + i;
                // Check if the row and column are within bounds
                if (row < 8 && col < 8)
                {
                    // Check the tile at the current row and column
                    GameObject tile = boardPlacementHandler.GetTile(row, col);
                    if (tile != null)
                    {
                        // Check if there is a piece at the current tile
                        ChessPlayerPlacementHandler handler = GetChessPlayerPlacementHandler(row, col);
                        if (handler == null)
                        {
                            // If the tile is empty, create a highlight
                            CreateHighlight(row, col);
                        }
                        else
                        {
                            // If there is a piece, check if it is an enemy piece
                            if (handler.IsWhite != isWhite)
                            {
                                // If it is an enemy piece, create a highlight
                                CreateHighlight(row, col);
                            }
                            // Stop highlighting if there is a piece blocking the way
                            break;
                        }
                    }
                }
            }

            // Bottom-Left diagonal
            for (int i = 1; i < 8; i++)
            {
                int row = currentRow - i;
                int col = currentColumn - i;
                // Check if the row and column are within bounds
                if (row >= 0 && col >= 0)
                {
                    // Check the tile at the current row and column
                    GameObject tile = boardPlacementHandler.GetTile(row, col);
                    if (tile != null)
                    {
                        // Check if there is a piece at the current tile
                        ChessPlayerPlacementHandler handler = GetChessPlayerPlacementHandler(row, col);
                        if (handler == null)
                        {
                            // If the tile is empty, create a highlight
                            CreateHighlight(row, col);
                        }
                        else
                        {
                            // If there is a piece, check if it is an enemy piece
                            if (handler.IsWhite != isWhite)
                            {
                                // If it is an enemy piece, create a highlight
                                CreateHighlight(row, col);
                            }
                            // Stop highlighting if there is a piece blocking the way
                            break;
                        }
                    }
                }
            }

            // Bottom-Right diagonal
            for (int i = 1; i < 8; i++)
            {
                int row = currentRow - i;
                int col = currentColumn + i;
                // Check if the row and column are within bounds
                if (row >= 0 && col < 8)
                {
                    // Check the tile at the current row and column
                    GameObject tile = boardPlacementHandler.GetTile(row, col);
                    if (tile != null)
                    {
                        // Check if there is a piece at the current tile
                        ChessPlayerPlacementHandler handler = GetChessPlayerPlacementHandler(row, col);
                        if (handler == null)
                        {
                            // If the tile is empty, create a highlight
                            CreateHighlight(row, col);
                        }
                        else
                        {
                            // If there is a piece, check if it is an enemy piece
                            if (handler.IsWhite != isWhite)
                            {
                                // If it is an enemy piece, create a highlight
                                CreateHighlight(row, col);
                            }
                            // Stop highlighting if there is a piece blocking the way
                            break;
                        }
                    }
                }
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