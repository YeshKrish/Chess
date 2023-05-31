using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Bishop : MonoBehaviour
    {
        [SerializeField] private bool isWhite; 

        private ChessBoardPlacementHandler boardPlacementHandler;
        private ChessPlayerPlacementHandler playerPlacementHandler;

        private void Start()
        {
            boardPlacementHandler = ChessBoardPlacementHandler.Instance;
            playerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();
        }

        private void OnMouseDown()
        {
            
            int currentRow = playerPlacementHandler.row;
            int currentColumn = playerPlacementHandler.column;

           
            boardPlacementHandler.ClearHighlights();

            

            // Top-Left diagonal
            for (int i = 1; i < 8; i++)
            {
                int row = currentRow + i;
                int col = currentColumn - i;
                
                if (row < 8 && col >= 0)
                {
                   
                    GameObject tile = boardPlacementHandler.GetTile(row, col);
                    if (tile != null)
                    {
                       
                        ChessPlayerPlacementHandler handler = GameManager.instance.GetChessPlayerPlacementHandler(row, col);
                        if (handler == null)
                        {
                           
                            GameManager.instance.CreateHighlight(row, col);
                        }
                        else
                        {
                           
                            if (handler.IsWhite != isWhite)
                            {
                                
                                GameManager.instance.CreateHighlight(row, col);
                            }
                          
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
                
                if (row < 8 && col < 8)
                {
                
                    GameObject tile = boardPlacementHandler.GetTile(row, col);
                    if (tile != null)
                    {
                       
                        ChessPlayerPlacementHandler handler = GameManager.instance.GetChessPlayerPlacementHandler(row, col);
                        if (handler == null)
                        {
                            
                            GameManager.instance.CreateHighlight(row, col);
                        }
                        else
                        {
                            
                            if (handler.IsWhite != isWhite)
                            {
                               
                                GameManager.instance.CreateHighlight(row, col);
                            }

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
 
                if (row >= 0 && col >= 0)
                {

                    GameObject tile = boardPlacementHandler.GetTile(row, col);
                    if (tile != null)
                    {

                        ChessPlayerPlacementHandler handler = GameManager.instance.GetChessPlayerPlacementHandler(row, col);
                        if (handler == null)
                        {
 
                            GameManager.instance.CreateHighlight(row, col);
                        }
                        else
                        {

                            if (handler.IsWhite != isWhite)
                            {

                                GameManager.instance.CreateHighlight(row, col);
                            }

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

                if (row >= 0 && col < 8)
                {

                    GameObject tile = boardPlacementHandler.GetTile(row, col);
                    if (tile != null)
                    {

                        ChessPlayerPlacementHandler handler = GameManager.instance.GetChessPlayerPlacementHandler(row, col);
                        if (handler == null)
                        {

                            GameManager.instance.CreateHighlight(row, col);
                        }
                        else
                        {

                            if (handler.IsWhite != isWhite)
                            {

                                GameManager.instance.CreateHighlight(row, col);
                            }

                            break;
                        }
                    }
                }
            }
        }
    }
}