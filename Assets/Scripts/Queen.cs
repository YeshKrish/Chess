using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Queen : MonoBehaviour
    {
        [SerializeField] private bool isWhite; // Set this to true if it's a white queen

        private ChessBoardPlacementHandler boardPlacementHandler;
        private ChessPlayerPlacementHandler playerPlacementHandler;
        [SerializeField] private GameObject _highlightPrefab;

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
                    else
                    {
                        if (handler.IsWhite != isWhite)
                        {
                            CreateHighlight(currentRow, i);
                        }
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
                    else
                    {
                        if (handler.IsWhite != isWhite)
                        {
                            CreateHighlight(i, currentColumn);
                        }
                        break;
                    }
                }
            }

            // Check for possible moves in the diagonals
            for (int i = 1; i < 8; i++)
            {
                // Top-Left diagonal
                int row = currentRow + i;
                int col = currentColumn - i;
                if (row < 8 && col >= 0)
                {
                    GameObject tile = boardPlacementHandler.GetTile(row, col);
                    if (tile != null)
                    {
                        ChessPlayerPlacementHandler handler = GetChessPlayerPlacementHandler(row, col);
                        if (handler == null)
                        {
                            CreateHighlight(row, col);
                        }
                        else
                        {
                            if (handler.IsWhite != isWhite)
                            {
                                CreateHighlight(row, col);
                            }
                            break;
                        }
                    }
                }

                row = currentRow + i;
                col = currentColumn + i;
                if (row < 8 && col < 8)
                {
                    GameObject tile = boardPlacementHandler.GetTile(row, col);
                    if (tile != null)
                    {
                        ChessPlayerPlacementHandler handler = GetChessPlayerPlacementHandler(row, col);
                        if (handler == null)
                        {
                            CreateHighlight(row, col);
                        }
                        else
                        {
                            if (handler.IsWhite != isWhite)
                            {
                                CreateHighlight(row, col);
                            }
                            break;
                        }
                    }
                }

                row = currentRow - i;
                col = currentColumn - i;
                if (row >= 0 && col >= 0)
                {
                    GameObject tile = boardPlacementHandler.GetTile(row, col);
                    if (tile != null)
                    {
                        ChessPlayerPlacementHandler handler = GetChessPlayerPlacementHandler(row, col);
                        if (handler == null)
                        {
                            CreateHighlight(row, col);
                        }
                        else
                        {
                            if (handler.IsWhite != isWhite)
                            {
                                CreateHighlight(row, col);
                            }
                            break;
                        }
                    }
                }

                row = currentRow - i;
                col = currentColumn + i;
                if (row >= 0 && col < 8)
                {
                    GameObject tile = boardPlacementHandler.GetTile(row, col);
                    if (tile != null)
                    {
                        ChessPlayerPlacementHandler handler = GetChessPlayerPlacementHandler(row, col);
                        if (handler == null)
                        {
                            CreateHighlight(row, col);
                        }
                        else
                        {
                            if (handler.IsWhite != isWhite)
                            {
                                CreateHighlight(row, col);
                            }
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
            Highlighter highlighter = highlight.GetComponent<Highlighter>();
            highlighter.OnHighlightCollision += OnHighlightCollision;
        }

        private void OnHighlightCollision(bool isHitBlack)
        {
            if (isHitBlack)
            {
                // Stop creating highlights or perform other actions
                Debug.Log("Highlight collision detected with black piece.");
            }
        }
    }
}