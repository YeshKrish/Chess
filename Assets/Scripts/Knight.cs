using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Knight : MonoBehaviour
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

            GameManager.instance.SelectedChessPiece(gameObject);

            boardPlacementHandler.ClearHighlights();


            int[,] knightMoves = new int[,]
            {
                { 1, 2 }, { -1, 2 }, { 1, -2 }, { -1, -2 },
                { 2, 1 }, { -2, 1 }, { 2, -1 }, { -2, -1 }
            };


            for (int i = 0; i < knightMoves.GetLength(0); i++)
            {
                int row = currentRow + knightMoves[i, 0];
                int col = currentColumn + knightMoves[i, 1];


                if (row >= 0 && row < 8 && col >= 0 && col < 8)
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
                        }
                    }
                }
            }
        }
    }
}