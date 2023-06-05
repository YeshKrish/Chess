using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Highlighter : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private ChessBoardPlacementHandler boardPlacementHandler;
        private ChessPlayerPlacementHandler playerPlacementHandler;

        public event System.Action<bool> OnHighlightCollision;

        public int newRow;
        public int newColumn;


        private void Start()
        {
            boardPlacementHandler = ChessBoardPlacementHandler.Instance;
            playerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnMouseDown()
        {
            if(GameManager.instance.selectedChessPiece != null)
            {
                MoveChessPiece(GameManager.instance.selectedChessPiece, transform.position, newRow, newColumn);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("BlackPiece"))
            {
                bool isWhite = collision.GetComponent<ChessPlayerPlacementHandler>().IsWhite;
                if (!isWhite)
                {
                    spriteRenderer.color = Color.red;
                }
                OnHighlightCollision?.Invoke(!isWhite);
            }
            else if (collision.gameObject.CompareTag("WhitePiece"))
            {
                bool isWhite = collision.GetComponent<ChessPlayerPlacementHandler>().IsWhite;
                if (isWhite)
                {
                    spriteRenderer.color = Color.red;
                }
            }
        }


        private void MoveChessPiece(GameObject chessPiece, Vector3 targetPosition, int row, int column)
        {
            boardPlacementHandler.ClearHighlights();

            chessPiece.GetComponent<ChessPlayerPlacementHandler>().row = row;
            chessPiece.GetComponent<ChessPlayerPlacementHandler>().column = column;
            
            chessPiece.transform.position = targetPosition;
        }
    }
}