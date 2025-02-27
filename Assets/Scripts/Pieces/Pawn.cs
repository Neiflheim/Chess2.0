using System.Collections.Generic;
using Handlers;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Pawn", menuName = "Piece/Pawn")]
    public class Pawn : Piece
    {
        private List<Vector2Int> _directionsBlackEat = new List<Vector2Int>
        {
            new Vector2Int(1, 1), new Vector2Int(1, -1)
        };
        
        private List<Vector2Int> _directionsWhiteEat = new List<Vector2Int>
        {
            new Vector2Int(-1, 1), new Vector2Int(-1, -1)
        };

        public override List<Vector2Int> AvailableMovements(int[,] board, Vector2Int position, bool verifyKingIsCheck)
        {
            List<Vector2Int> movements = new List<Vector2Int>();

            int i;

            if (!IsWhite)
            {
                if (position.x == 1)
                {
                    for (i = position.x + 1; i <= position.x + 2; i++)
                    {
                        if (i < 0 || i > 7) continue;
                        
                        if (board[i, position.y] == 0)
                        {
                            movements.Add(new Vector2Int(i, position.y));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if (board[position.x + 1, position.y] == 0)
                    {
                        if (position.x + 1 >= 0 && position.x + 1 <= 7)
                        {
                            movements.Add(new Vector2Int(position.x + 1, position.y));
                        }
                    }
                }

                foreach (Vector2Int direction in _directionsBlackEat)
                {
                    Vector2Int testDirection = position + direction;

                    if ((uint)testDirection.x > 7 || (uint)testDirection.y > 7) continue;

                    if (Rules.AreDifferentColors(board[position.x, position.y], board[testDirection.x, testDirection.y], false))
                    {
                        movements.Add(new Vector2Int(testDirection.x, testDirection.y));
                    }
                }
            }
            else
            {
                if (position.x == 6)
                {
                    for (i = position.x - 1; i >= position.x - 2; i--)
                    {
                        if (i < 0 || i > 7) continue;
                        
                        if (board[i, position.y] == 0)
                        {
                            movements.Add(new Vector2Int(i, position.y));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if (board[position.x - 1, position.y] == 0)
                    {
                        if (position.x - 1 >= 0 && position.x - 1 <= 7)
                        {
                            movements.Add(new Vector2Int(position.x - 1, position.y));
                        }
                    }
                }

                foreach (Vector2Int direction in _directionsWhiteEat)
                {
                    Vector2Int testDirection = position + direction;

                    if ((uint)testDirection.x > 7 || (uint)testDirection.y > 7) continue;

                    if (Rules.AreDifferentColors(board[position.x, position.y], board[testDirection.x, testDirection.y], false))
                    {
                        movements.Add(new Vector2Int(testDirection.x, testDirection.y));
                    }
                }
            }
            
            if (verifyKingIsCheck)
            {
                movements.RemoveAll(movement => !CanPlayThisMovement(board, position, movement));
            }

            return movements;
        }
    }
}