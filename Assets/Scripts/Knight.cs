using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Knight : MonoBehaviour
    {
        [SerializeField] private bool isWhite; // Set this to true if it's a white knight

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
            // Calculate possible legal moves for the knight
            int currentRow = playerPlacementHandler.row;
            int currentColumn = playerPlacementHandler.column;

            // Example logic for calculating possible moves
            boardPlacementHandler.ClearHighlights();

            // Check for possible moves in L-shape patterns
            CheckKnightMove(currentRow + 2, currentColumn + 1);
            CheckKnightMove(currentRow + 2, currentColumn - 1);
            CheckKnightMove(currentRow - 2, currentColumn + 1);
            CheckKnightMove(currentRow - 2, currentColumn - 1);
            CheckKnightMove(currentRow + 1, currentColumn + 2);
            CheckKnightMove(currentRow + 1, currentColumn - 2);
            CheckKnightMove(currentRow - 1, currentColumn + 2);
            CheckKnightMove(currentRow - 1, currentColumn - 2);
        }

        private void CheckKnightMove(int row, int column)
        {
            if (row >= 0 && row < 8 && column >= 0 && column < 8)
            {
                ChessPlayerPlacementHandler handler = GetChessPlayerPlacementHandler(row, column);
                if (handler == null)
                {
                    boardPlacementHandler.Highlight(row, column);
                }
                else if (handler.IsWhite != isWhite)
                {
                    boardPlacementHandler.Highlight(row, column);
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
    }
}