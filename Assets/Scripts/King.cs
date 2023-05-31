using UnityEngine;

namespace Chess.Scripts.Core
{
    public class King : MonoBehaviour
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

            // adjacent tiles
            for (int i = currentRow - 1; i <= currentRow + 1; i++)
            {
                for (int j = currentColumn - 1; j <= currentColumn + 1; j++)
                {
                    if (i >= 0 && i < 8 && j >= 0 && j < 8)
                    {
                        ChessPlayerPlacementHandler handler = GameManager.instance.GetChessPlayerPlacementHandler(i, j);
                        if (handler == null)
                        {
                            GameManager.instance.CreateHighlight(i, j);
                        }
                        else if (handler.IsWhite != isWhite)
                        {
                            GameManager.instance.CreateHighlight(i, j);
                        }
                    }
                }
            }
        }
    }
}