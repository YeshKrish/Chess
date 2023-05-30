using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Highlighter : MonoBehaviour
    {
        public delegate void HighlightCollisionEventHandler(bool isHitBlack);
        public event HighlightCollisionEventHandler OnHighlightCollision;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("BlackPiece"))
            {
                OnHighlightCollision?.Invoke(true);
            }
        }
    }
}