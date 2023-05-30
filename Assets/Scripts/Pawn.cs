using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Pawn : MonoBehaviour
    {
        [SerializeField] private bool isWhite; // Set this to true if it's a white pawn

        private ChessBoardPlacementHandler boardPlacementHandler;
        private ChessPlayerPlacementHandler playerPlacementHandler;
        [SerializeField] private GameObject _highlightPrefab;
        private Highlighter highlighter;
        private bool isFirstMove;
        private Vector2 startingPosition;

        private void Start()
        {
            boardPlacementHandler = ChessBoardPlacementHandler.Instance;
            playerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();

            // Store the starting position of the pawn
            startingPosition = new Vector2(playerPlacementHandler.column, playerPlacementHandler.row);

        }

        private void OnMouseDown()
        {
            int currentRow = playerPlacementHandler.row;
            int currentColumn = playerPlacementHandler.column;

            Debug.Log("isWhite" + (playerPlacementHandler.row > 1) + "Pler" + playerPlacementHandler.row);

            if (isWhite)
            {
                isFirstMove = playerPlacementHandler.row > 1;
            }
            else
            {
                isFirstMove = playerPlacementHandler.row < 6;
            }

            boardPlacementHandler.ClearHighlights();

            if (isWhite)
            {
                // Check for possible move forward
                if (currentRow < 7)
                {
                    ChessPlayerPlacementHandler forwardHandler = GetChessPlayerPlacementHandler(currentRow + 1, currentColumn);
                    if (forwardHandler == null)
                    {
                        CreateHighlight(currentRow + 1, currentColumn);
                        if (!isFirstMove && currentColumn == startingPosition.x)
                        {
                            ChessPlayerPlacementHandler doubleForwardHandler = GetChessPlayerPlacementHandler(currentRow + 2, currentColumn);
                            if (doubleForwardHandler == null)
                            {
                                CreateHighlight(currentRow + 2, currentColumn);
                            }
                            else if(doubleForwardHandler != null && doubleForwardHandler.IsWhite != isWhite)
                            {
                                CreateHighlight(currentRow + 2, currentColumn);
                            }
                        }
                    }
                    else if(forwardHandler != null && forwardHandler.IsWhite != isWhite)
                    {
                        CreateHighlight(currentRow + 1, currentColumn);
                    }
                }

                // Check for possible capture moves
                if (currentRow < 7 && currentColumn > 0)
                {
                    ChessPlayerPlacementHandler captureLeftHandler = GetChessPlayerPlacementHandler(currentRow + 1, currentColumn - 1);
                    if (captureLeftHandler != null && captureLeftHandler.IsWhite != isWhite)
                    {
                        CreateHighlight(currentRow + 1, currentColumn - 1);
                    }
                }

                if (currentRow < 7 && currentColumn < 7)
                {
                    ChessPlayerPlacementHandler captureRightHandler = GetChessPlayerPlacementHandler(currentRow + 1, currentColumn + 1);
                    if (captureRightHandler != null && captureRightHandler.IsWhite != isWhite)
                    {
                        CreateHighlight(currentRow + 1, currentColumn + 1);
                    }
                }
            }
            else
            {
                // Check for possible move forward
                if (currentRow > 0)
                {
                    ChessPlayerPlacementHandler forwardHandler = GetChessPlayerPlacementHandler(currentRow - 1, currentColumn);
                    if (forwardHandler == null)
                    {
                        CreateHighlight(currentRow - 1, currentColumn);
                        if (!isFirstMove && currentColumn == startingPosition.x)
                        {
                            ChessPlayerPlacementHandler doubleForwardHandler = GetChessPlayerPlacementHandler(currentRow - 2, currentColumn);
                            if (doubleForwardHandler == null)
                            {
                                CreateHighlight(currentRow - 2, currentColumn);
                            }
                            else if (doubleForwardHandler != null && doubleForwardHandler.IsWhite != isWhite)
                            {
                                CreateHighlight(currentRow - 2, currentColumn);
                            }
                        }
                    }
                    else if (forwardHandler != null && forwardHandler.IsWhite != isWhite)
                    {
                        CreateHighlight(currentRow - 1, currentColumn);
                    }
                }

                // Check for possible capture moves
                if (currentRow > 0 && currentColumn > 0)
                {
                    ChessPlayerPlacementHandler captureLeftHandler = GetChessPlayerPlacementHandler(currentRow - 1, currentColumn - 1);
                    if (captureLeftHandler != null && captureLeftHandler.IsWhite != isWhite)
                    {
                        CreateHighlight(currentRow - 1, currentColumn - 1);
                    }
                }

                if (currentRow > 0 && currentColumn < 7)
                {
                    ChessPlayerPlacementHandler captureRightHandler = GetChessPlayerPlacementHandler(currentRow - 1, currentColumn + 1);
                    if (captureRightHandler != null && captureRightHandler.IsWhite != isWhite)
                    {
                        CreateHighlight(currentRow - 1, currentColumn + 1);
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