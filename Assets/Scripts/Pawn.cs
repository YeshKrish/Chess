using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Pawn : MonoBehaviour
    {
        [SerializeField] private bool isWhite; // Set this to true if it's a white pawn


        private ChessBoardPlacementHandler boardPlacementHandler;
        private ChessPlayerPlacementHandler playerPlacementHandler;

        private void Start()
        {
            boardPlacementHandler = ChessBoardPlacementHandler.Instance;
            playerPlacementHandler = GetComponent<Chess.Scripts.Core.ChessPlayerPlacementHandler>();
        }

        private void OnMouseDown()
        {
            int currentRow = playerPlacementHandler.row;
            int currentColumn = playerPlacementHandler.column;

            Debug.Log("Current Row" + currentRow + "Current Column" + currentColumn);

            boardPlacementHandler.ClearHighlights();

            if (playerPlacementHandler.IsWhite)
            {
                Debug.Log("white");
                // Calculate possible legal moves for a white pawn
                // Move forward
                if (currentRow < 7 && boardPlacementHandler.GetTile(currentRow + 1, currentColumn) != null)
                {
                    Debug.Log("I am inside");
                    boardPlacementHandler.Highlight(currentRow + 1, currentColumn);

                    // Move two steps forward on the first move
                    if (currentRow == 1 && boardPlacementHandler.GetTile(currentRow + 2, currentColumn) != null)
                    {
                        boardPlacementHandler.Highlight(currentRow + 2, currentColumn);
                    }
                }
                if (currentRow < 7 && currentColumn > 0)
                {
                    ChessPlayerPlacementHandler leftDiagonalHandler = GetChessPlayerPlacementHandler(currentRow + 1, currentColumn - 1);
                    if (leftDiagonalHandler != null && leftDiagonalHandler.IsWhite != isWhite)
                    {
                        // Enemy piece in the left diagonal position
                        boardPlacementHandler.Highlight(currentRow + 1, currentColumn - 1);
                    }
                }

                if (currentRow < 7 && currentColumn < 7)
                {
                    ChessPlayerPlacementHandler rightDiagonalHandler = GetChessPlayerPlacementHandler(currentRow + 1, currentColumn + 1);
                    if (rightDiagonalHandler != null && rightDiagonalHandler.IsWhite != isWhite)
                    {
                        // Enemy piece in the right diagonal position
                        boardPlacementHandler.Highlight(currentRow + 1, currentColumn + 1);
                    }
                }
            }
        
            else
            {
                // Calculate possible legal moves for a black pawn
                // Move forward
                if (currentRow > 0 && boardPlacementHandler.GetTile(currentRow - 1, currentColumn) != null)
                {
                    boardPlacementHandler.Highlight(currentRow - 1, currentColumn);

                    // Move two steps forward on the first move
                    if (currentRow == 6 && boardPlacementHandler.GetTile(currentRow - 2, currentColumn) != null)
                    {
                        boardPlacementHandler.Highlight(currentRow - 2, currentColumn);
                    }
                }

                // Capture diagonally if there is an enemy piece
                if (currentRow > 0 && currentColumn > 0)
                {
                    ChessPlayerPlacementHandler leftDiagonalHandler = GetChessPlayerPlacementHandler(currentRow - 1, currentColumn - 1);
                    if (leftDiagonalHandler != null && leftDiagonalHandler.IsWhite != isWhite)
                    {
                        // Enemy piece in the left diagonal position
                        boardPlacementHandler.Highlight(currentRow - 1, currentColumn - 1);
                    }
                }
                if (currentRow > 0 && currentColumn < 7)
                {
                    ChessPlayerPlacementHandler rightDiagonalHandler = GetChessPlayerPlacementHandler(currentRow - 1, currentColumn + 1);
                    if (rightDiagonalHandler != null && rightDiagonalHandler.IsWhite != isWhite)
                    {
                        // Enemy piece in the right diagonal position
                        boardPlacementHandler.Highlight(currentRow - 1, currentColumn + 1);
                    }
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