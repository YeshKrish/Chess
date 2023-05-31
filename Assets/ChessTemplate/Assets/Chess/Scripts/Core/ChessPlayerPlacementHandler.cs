using System;
using UnityEngine;


namespace Chess.Scripts.Core
{
    public class ChessPlayerPlacementHandler : MonoBehaviour
    {
        [SerializeField] public int row, column;

        private bool isWhite; 

        private void Start()
        {
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
            isWhite = gameObject.CompareTag("WhitePiece");
        }

        public bool IsWhite
        {
            get { return isWhite; }
        }
    }
}