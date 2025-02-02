using System.Collections.Generic;
using Game;
using Handlers;
using UnityEngine;
using Utils;

namespace Pieces
{
    public abstract class Piece : ScriptableObject
    {
        public Sprite Sprite;
        public bool IsWhite;
        public int BaseValue;

        public abstract List<Vector2Int> AvailableMovements(Piece[,] pieces, Vector2Int position, bool firstCall);

        public bool CanPlayThisMovement(Piece[,] pieces, Piece piece, Vector2Int oldPosition, Vector2Int newPosition)
        {
            bool canPlayThisMovement = true;
            
            // Vérifier si pour ce mouvement le roi est toujours en echec
            if (IsWhite)
            {
                Piece[,] testPieces = (Piece[,]) BoardsHandler.Instance.Pieces.Clone();
                testPieces[newPosition.x, newPosition.y] = piece;
                testPieces[oldPosition.x, oldPosition.y] = null;

                for (int i = 0; i < testPieces.GetLength(0); i++)
                {
                    for (int j = 0; j < testPieces.GetLength(1); j++)
                    {
                        if (testPieces[i,j])
                        {
                            if (testPieces[i,j].name == "WhiteKing")
                            {
                                if (Rules.IsCheck(testPieces, testPieces[i,j], new Vector2Int(i, j)))
                                { 
                                    canPlayThisMovement = false;
                                }
                            }
                        }
                    }
                }
            }

            if (!IsWhite)
            {
                Piece[,] testPieces = (Piece[,]) BoardsHandler.Instance.Pieces.Clone();
                testPieces[newPosition.x, newPosition.y] = piece;
                testPieces[oldPosition.x, oldPosition.y] = null;

                for (int i = 0; i < testPieces.GetLength(0); i++)
                {
                    for (int j = 0; j < testPieces.GetLength(1); j++)
                    {
                        if (testPieces[i,j])
                        { 
                            if (testPieces[i,j].name == "BlackKing")
                            { 
                                if (Rules.IsCheck(testPieces, testPieces[i,j], new Vector2Int(i, j)))
                                {
                                    canPlayThisMovement = false;
                                }
                            }
                        }
                    }
                }
            }
            
            return canPlayThisMovement;
        }
    }
}