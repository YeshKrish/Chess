using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Pieces
{
    public class Knight : MonoBehaviour
    {
        private ChessBoardPlacementHandler boardPlacementHandler;
        private Chess.Scripts.Core.ChessPlayerPlacementHandler playerPlacementHandler;

        private void Start()
        {
            boardPlacementHandler = ChessBoardPlacementHandler.Instance;
            playerPlacementHandler = GetComponent<Chess.Scripts.Core.ChessPlayerPlacementHandler>();
        }

        private void OnMouseDown()
        {
            // Calculate possible legal moves for the knight
            int currentRow = playerPlacementHandler.row;
            int currentColumn = playerPlacementHandler.column;

            // Example logic for calculating possible moves
            boardPlacementHandler.ClearHighlights();
            boardPlacementHandler.Highlight(currentRow + 2, currentColumn + 1);
            boardPlacementHandler.Highlight(currentRow + 2, currentColumn - 1);
            boardPlacementHandler.Highlight(currentRow - 2, currentColumn + 1);
            boardPlacementHandler.Highlight(currentRow - 2, currentColumn - 1);
            boardPlacementHandler.Highlight(currentRow + 1, currentColumn + 2);
            boardPlacementHandler.Highlight(currentRow + 1, currentColumn - 2);
            boardPlacementHandler.Highlight(currentRow - 1, currentColumn + 2);
            boardPlacementHandler.Highlight(currentRow - 1, currentColumn - 2);
        }
    }
}
