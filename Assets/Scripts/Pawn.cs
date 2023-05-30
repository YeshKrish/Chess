using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Pawn : MonoBehaviour
    {
        private ChessBoardPlacementHandler boardPlacementHandler;
        private ChessPlayerPlacementHandler playerPlacementHandler;

        private void Start()
        {
            boardPlacementHandler = ChessBoardPlacementHandler.Instance;
            playerPlacementHandler = GetComponent<Chess.Scripts.Core.ChessPlayerPlacementHandler>();
        }

        private void OnMouseDown()
        {
            // Calculate possible legal moves for the pawn
            int currentRow = playerPlacementHandler.row;
            int currentColumn = playerPlacementHandler.column;

            // Example logic for calculating possible moves
            boardPlacementHandler.ClearHighlights();
            boardPlacementHandler.Highlight(currentRow + 1, currentColumn); // Move one step forward
            boardPlacementHandler.Highlight(currentRow + 2, currentColumn); // Move two steps forward (only on the first move)
            boardPlacementHandler.Highlight(currentRow + 1, currentColumn + 1); // Capture diagonally to the right
            boardPlacementHandler.Highlight(currentRow + 1, currentColumn - 1); // Capture diagonally to the left
        }
    }
}