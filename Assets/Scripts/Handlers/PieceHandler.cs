using System.Collections.Generic;
using Game;
using Pieces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Handlers
{
    public class PieceHandler : MonoBehaviour, IPointerClickHandler
    {
        public Piece Piece;
        public Vector2Int Position;
        private Image _image;
        private bool _isMovement;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void Setup(Piece piece, Vector2Int position)
        {
            Piece = piece;
            _image.sprite = piece.Sprite;
            Position = position;
        }

        public void SetupTransparent(Vector2Int position)
        {
            Position = position;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Piece != null && GameManager.Instance.IsWhiteTurn == Piece.IsWhite)
            {
                if (_isMovement == false)
                {
                    GameManager.Instance.LastClickGameObject = gameObject.GetComponent<PieceHandler>();
                
                    BoardsHandler.Instance.ResetMatrix();
                    BoardsHandler.Instance.DisplayMatrix(false);
            
                    List<Vector2Int> availableMovements = Piece.AvailableMovements(BoardsHandler.Instance.Pieces, Position, true);
            
                    foreach (Vector2Int availableMovement in availableMovements)
                    {
                        BoardsHandler.Instance.PiecesDisplay[availableMovement.x, availableMovement.y].GetComponent<PieceHandler>()._isMovement = true;
                        BoardsHandler.Instance.PiecesDisplay[availableMovement.x, availableMovement.y].GetComponent<Image>().color = new Color(1, 1, 0, 0.5f);
                    }
                
                    availableMovements.Clear();
                }
            }
            
            if (_isMovement)
            {
                // Movement
                Vector2Int lastPosition = GameManager.Instance.LastClickGameObject.Position;
                BoardsHandler.Instance.Pieces[lastPosition.x, lastPosition.y] = null;
                BoardsHandler.Instance.Pieces[Position.x, Position.y] = GameManager.Instance.LastClickGameObject.Piece;
                
                BoardsHandler.Instance.ResetMatrix();
                BoardsHandler.Instance.DisplayMatrix(true);

                GameManager.Instance.IsWhiteTurn = !GameManager.Instance.IsWhiteTurn;
            }
        }
    }
}