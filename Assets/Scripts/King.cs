using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Pieces
{
    public class King : MonoBehaviour
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
            // Calculate possible legal moves for the king
            int currentRow = playerPlacementHandler.row;
            int currentColumn = playerPlacementHandler.column;

            // Example logic for calculating possible moves
            boardPlacementHandler.ClearHighlights();
            boardPlacementHandler.Highlight(currentRow + 1, currentColumn);
            boardPlacementHandler.Highlight(currentRow - 1, currentColumn);
            boardPlacementHandler.Highlight(currentRow, currentColumn + 1);
            boardPlacementHandler.Highlight(currentRow, currentColumn - 1);
            boardPlacementHandler.Highlight(currentRow + 1, currentColumn + 1);
            boardPlacementHandler.Highlight(currentRow + 1, currentColumn - 1);
            boardPlacementHandler.Highlight(currentRow - 1, currentColumn + 1);
            boardPlacementHandler.Highlight(currentRow - 1, currentColumn - 1);
        }
    }
}
