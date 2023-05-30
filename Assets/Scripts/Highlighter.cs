using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Highlighter : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        public event System.Action<bool> OnHighlightCollision;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
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
    }
}