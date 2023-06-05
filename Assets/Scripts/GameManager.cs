using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        private Highlighter highlighter;
        [SerializeField] private GameObject _highlightPrefab;

        private ChessBoardPlacementHandler boardPlacementHandler;
        private ChessPlayerPlacementHandler playerPlacementHandler;

        public GameObject selectedChessPiece;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            boardPlacementHandler = ChessBoardPlacementHandler.Instance;
            playerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();
        }
        public ChessPlayerPlacementHandler GetChessPlayerPlacementHandler(int row, int column)
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
        public void CreateHighlight(int row, int column)
        {
            GameObject highlight = Instantiate(_highlightPrefab, boardPlacementHandler.GetTile(row, column).transform.position, Quaternion.identity, boardPlacementHandler.GetTile(row, column).transform);
            highlighter = highlight.GetComponent<Highlighter>();
            highlighter.newRow = row;
            highlighter.newColumn = column;
            highlighter.OnHighlightCollision += OnHighlightCollision;
        }

        public void OnHighlightCollision(bool isHitBlack)
        {
            if (isHitBlack)
            {
                // Stop creating highlights
                highlighter.OnHighlightCollision -= OnHighlightCollision;
            }
        }
        public GameObject SelectedChessPiece(GameObject selectedPiece)
        {
            selectedChessPiece = selectedPiece;
            return selectedChessPiece;
        }
    }

}
