using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Pieces
{
    public class Bishop : MonoBehaviour
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
            // Calculate possible legal moves for the bishop
            int currentRow = playerPlacementHandler.row;
            int currentColumn = playerPlacementHandler.column;

            // Example logic for calculating possible moves
            boardPlacementHandler.ClearHighlights();

            // Highlight all tiles on the diagonal
            for (int i = 1; i < 8; i++)
            {
                boardPlacementHandler.Highlight(currentRow + i, currentColumn + i); // Up-right diagonal
                boardPlacementHandler.Highlight(currentRow + i, currentColumn - i); // Up-left diagonal
                boardPlacementHandler.Highlight(currentRow - i, currentColumn + i); // Down-right diagonal
                boardPlacementHandler.Highlight(currentRow - i, currentColumn - i); // Down-left diagonal
            }
        }
    }
}