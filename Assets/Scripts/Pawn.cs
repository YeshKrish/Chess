using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Pawn : MonoBehaviour
    {
        [SerializeField] private bool isWhite; 

        private ChessBoardPlacementHandler boardPlacementHandler;
        private ChessPlayerPlacementHandler playerPlacementHandler;
        private bool isFirstMove;
        private Vector2 startingPosition;
        [SerializeField] private GameObject _highlightPrefab;

        private void Start()
        {
            boardPlacementHandler = ChessBoardPlacementHandler.Instance;
            playerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();

            // starting position of the pawn
            startingPosition = new Vector2(playerPlacementHandler.column, playerPlacementHandler.row);

        }

        private void OnMouseDown()
        {
            int currentRow = playerPlacementHandler.row;
            int currentColumn = playerPlacementHandler.column;

            GameManager.instance.SelectedChessPiece(gameObject);

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
      
                if (currentRow < 7)
                {
                    ChessPlayerPlacementHandler forwardHandler = GameManager.instance.GetChessPlayerPlacementHandler(currentRow + 1, currentColumn);
                    if (forwardHandler == null)
                    {
                        GameManager.instance.CreateHighlight(currentRow + 1, currentColumn);
                        if (!isFirstMove && currentColumn == startingPosition.x)
                        {
                            ChessPlayerPlacementHandler doubleForwardHandler = GameManager.instance.GetChessPlayerPlacementHandler(currentRow + 2, currentColumn);
                            if (doubleForwardHandler == null)
                            {
                                GameManager.instance.CreateHighlight(currentRow + 2, currentColumn);
                            }
                            else if(doubleForwardHandler != null && doubleForwardHandler.IsWhite != isWhite)
                            {
                                GameManager.instance.CreateHighlight(currentRow + 2, currentColumn);
                            }
                        }
                    }
                    else if(forwardHandler != null && forwardHandler.IsWhite != isWhite)
                    {
                        GameManager.instance.CreateHighlight(currentRow + 1, currentColumn);
                    }
                }

      
                if (currentRow < 7 && currentColumn > 0)
                {
                    ChessPlayerPlacementHandler captureLeftHandler = GameManager.instance.GetChessPlayerPlacementHandler(currentRow + 1, currentColumn - 1);
                    if (captureLeftHandler != null && captureLeftHandler.IsWhite != isWhite)
                    {
                        GameManager.instance.CreateHighlight(currentRow + 1, currentColumn - 1);
                    }
                }

                if (currentRow < 7 && currentColumn < 7)
                {
                    ChessPlayerPlacementHandler captureRightHandler = GameManager.instance.GetChessPlayerPlacementHandler(currentRow + 1, currentColumn + 1);
                    if (captureRightHandler != null && captureRightHandler.IsWhite != isWhite)
                    {
                        GameManager.instance.CreateHighlight(currentRow + 1, currentColumn + 1);
                    }
                }
            }
            else
            {
            
                if (currentRow > 0)
                {
                    ChessPlayerPlacementHandler forwardHandler = GameManager.instance.GetChessPlayerPlacementHandler(currentRow - 1, currentColumn);
                    if (forwardHandler == null)
                    {
                        GameManager.instance.CreateHighlight(currentRow - 1, currentColumn);
                        if (!isFirstMove && currentColumn == startingPosition.x)
                        {
                            ChessPlayerPlacementHandler doubleForwardHandler = GameManager.instance.GetChessPlayerPlacementHandler(currentRow - 2, currentColumn);
                            if (doubleForwardHandler == null)
                            {
                                GameManager.instance.CreateHighlight(currentRow - 2, currentColumn);
                            }
                            else if (doubleForwardHandler != null && doubleForwardHandler.IsWhite != isWhite)
                            {
                                GameManager.instance.CreateHighlight(currentRow - 2, currentColumn);
                            }
                        }
                    }
                    else if (forwardHandler != null && forwardHandler.IsWhite != isWhite)
                    {
                        GameManager.instance.CreateHighlight(currentRow - 1, currentColumn);
                    }
                }

               
                if (currentRow > 0 && currentColumn > 0)
                {
                    ChessPlayerPlacementHandler captureLeftHandler = GameManager.instance.GetChessPlayerPlacementHandler(currentRow - 1, currentColumn - 1);
                    if (captureLeftHandler != null && captureLeftHandler.IsWhite != isWhite)
                    {
                        GameManager.instance.CreateHighlight(currentRow - 1, currentColumn - 1);
                    }
                }

                if (currentRow > 0 && currentColumn < 7)
                {
                    ChessPlayerPlacementHandler captureRightHandler = GameManager.instance.GetChessPlayerPlacementHandler(currentRow - 1, currentColumn + 1);
                    if (captureRightHandler != null && captureRightHandler.IsWhite != isWhite)
                    {
                        GameManager.instance.CreateHighlight(currentRow - 1, currentColumn + 1);
                    }
                }
            }
        }
    }
}