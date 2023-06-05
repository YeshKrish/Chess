using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Queen : MonoBehaviour
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

            // Clear previous highlights
            boardPlacementHandler.ClearHighlights();


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


            int row = currentRow + 1;
            int col = currentColumn - 1;
            while (row < 8 && col >= 0)
            {
                if (!CheckMove(row, col))
                    break;
                row++;
                col--;
            }


            row = currentRow + 1;
            col = currentColumn + 1;
            while (row < 8 && col < 8)
            {
                if (!CheckMove(row, col))
                    break;
                row++;
                col++;
            }


            row = currentRow - 1;
            col = currentColumn - 1;
            while (row >= 0 && col >= 0)
            {
                if (!CheckMove(row, col))
                    break;
                row--;
                col--;
            }


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
                ChessPlayerPlacementHandler handler = GameManager.instance.GetChessPlayerPlacementHandler(row, column);
                if (handler == null)
                {

                    GameManager.instance.CreateHighlight(row, column);
                    return true;
                }
                else
                {

                    if (handler.IsWhite != isWhite)
                    {

                        GameManager.instance.CreateHighlight(row, column);
                    }
                    return false;
                }
            }
            return false;
        }
    }
}