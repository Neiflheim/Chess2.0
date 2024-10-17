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
        private Piece _piece;
        private Image _image;
        private Vector2Int _position;
        private bool _isMovement;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void Setup(Piece piece, Vector2Int position)
        {
            _piece = piece;
            _image.sprite = piece.sprite;
            _position = position;
        }

        public void SetupTransparent(Vector2Int position)
        {
            _position = position;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isMovement == false)
            {
                Debug.Log(_isMovement + " and " +_position + " not ");

                GameManager.Instance.lastClickGameObject = gameObject;
            
                List<Vector2Int> availableMovements = _piece.AvailableMovements(_position);
                Debug.Log(" Available movements : " + availableMovements.Count);
            
                foreach (Vector2Int availableMovement in availableMovements)
                {
                    Debug.Log(availableMovement);
                    BoardsHandler.Instance.PiecesDisplay[availableMovement.x, availableMovement.y].GetComponent<PieceHandler>()._isMovement = true;
                    BoardsHandler.Instance.PiecesDisplay[availableMovement.x, availableMovement.y].GetComponent<Image>().color = new Color(1, 1, 0, 0.5f);
                }
            }
            else
            {
                Debug.Log(_isMovement + " and " +_position + " is ");

                if (GameManager.Instance == null)
                {
                    Debug.Log(" Game Manager is null ");
                }

                if (GameManager.Instance.lastClickGameObject == null)
                {
                    Debug.Log(" lastClickGameObject is null ");
                }

                if (GameManager.Instance.lastClickGameObject.GetComponent<PieceHandler>() == null)
                {
                    Debug.Log(" PieceHandler is null ");
                }
                
                Vector2Int lastPosition = GameManager.Instance.lastClickGameObject.GetComponent<PieceHandler>()._position;
                BoardsHandler.Instance.Pieces[lastPosition.x, lastPosition.y] = null;
                BoardsHandler.Instance.Pieces[_position.x, _position.y] = GameManager.Instance.lastClickGameObject.GetComponent<PieceHandler>()._piece;
                
                BoardsHandler.Instance.ResetMatrix();
                BoardsHandler.Instance.DisplayMatrix();
            }
        }
    }
}
