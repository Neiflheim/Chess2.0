using System.Collections.Generic;
using Game;
using MinMax;
using Pieces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

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
                
                // Update matrix and change player
                BoardsHandler.Instance.ResetMatrix();
                BoardsHandler.Instance.DisplayMatrix(true);
                GameManager.Instance.IsWhiteTurn = !GameManager.Instance.IsWhiteTurn;
                
                // Rules verifications
                // PawnPromotion
                Rules.PawnPromotion(BoardsHandler.Instance.Pieces, BoardsHandler.Instance.WhiteQueen, BoardsHandler.Instance.BlackQueen);
                
                // IsEndGame
                Node currentBoard = new Node(BoardsHandler.Instance.Pieces, GameManager.Instance.IsWhiteTurn, GameManager.Instance.IsWhiteTurn);
                List<Node> children = currentBoard.Children();
                int whiteCount = 0;
                int blackCount = 0;
                
                foreach (Node child in children)
                {
                    for (int i = 0; i < child.Pieces.GetLength(0); i++)
                    {
                        for (int j = 0; j < child.Pieces.GetLength(1); j++)
                        {
                            if (child.Pieces[i,j])
                            {
                                if (child.Pieces[i,j].IsWhite)
                                {
                                    if (child.Pieces[i,j].name == "WhiteKing" && Rules.IsCheckMate(child.Pieces, child.Pieces[i,j], new Vector2Int(i,j)))
                                    {
                                        whiteCount++;
                                    }
                                }
                                else
                                {
                                    if (child.Pieces[i,j].name == "BlackKing" && Rules.IsCheckMate(child.Pieces, child.Pieces[i,j], new Vector2Int(i,j)))
                                    {
                                        blackCount++;
                                    }
                                }
                            }
                        }
                    }
                }
                // Debug.Log("White count : " + whiteCount + " / Black count : " + blackCount + " / Chlidren count : " + children.Count);

                if (whiteCount == children.Count)
                {
                    Rules.IsGameOver(true);
                }
                else if (blackCount == children.Count)
                {
                    Rules.IsGameOver(false);
                }
            }
        }
    }
}