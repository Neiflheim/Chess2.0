using System.Collections.Generic;
using Handlers;
using UnityEngine;
using Utils;

namespace Pieces
{
    public abstract class Piece : ScriptableObject
    {
        public int Id;
        public Sprite Sprite;
        public bool IsWhite;
        public int BaseValue;

        public abstract List<Vector2Int> AvailableMovements(int[,] board, Vector2Int position, bool firstCall);

        public bool CanPlayThisMovement(int[,] pieces, Piece piece, Vector2Int oldPosition, Vector2Int newPosition)
        {
            bool canPlayThisMovement = true;
            
            int[,] testBoard = (int[,]) pieces.Clone();
            testBoard[newPosition.x, newPosition.y] = testBoard[oldPosition.x, oldPosition.y];
            testBoard[oldPosition.x, oldPosition.y] = 0;
            
            // Vérifier si pour ce mouvement le roi est toujours en echec
            if (IsWhite)
            {
                for (int i = 0; i < testBoard.GetLength(0); i++)
                {
                    for (int j = 0; j < testBoard.GetLength(1); j++)
                    {
                        if (testBoard[i,j] == 6)
                        {
                            if (Rules.IsCheck(testBoard, new Vector2Int(i, j)))
                            { 
                                canPlayThisMovement = false;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < testBoard.GetLength(0); i++)
                {
                    for (int j = 0; j < testBoard.GetLength(1); j++)
                    {
                        if (testBoard[i,j] == 12)
                        { 
                            if (Rules.IsCheck(testBoard, new Vector2Int(i, j)))
                            {
                                canPlayThisMovement = false;
                            }
                        }
                    }
                }
            }
            
            return canPlayThisMovement;
        }
    }
}