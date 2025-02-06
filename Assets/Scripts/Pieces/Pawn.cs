using System.Collections.Generic;
using UnityEngine;

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

        public override List<Vector2Int> AvailableMovements(Piece[,] pieces, Vector2Int position, bool firstCall)
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
                        
                        if (!pieces[i, position.y])
                        {
                            movements.Add(new Vector2Int(i, position.y));
                        }
                    }
                }
                else
                {
                    if (!pieces[position.x + 1, position.y])
                    {
                        movements.Add(new Vector2Int(position.x + 1, position.y));
                    }
                }

                foreach (Vector2Int direction in _directionsBlackEat)
                {
                    Vector2Int testDirection = position + direction;

                    if ((uint)testDirection.x > 7 || (uint)testDirection.y > 7) continue;

                    if (pieces[testDirection.x, testDirection.y] && pieces[testDirection.x, testDirection.y].IsWhite != IsWhite)
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
                        
                        if (!pieces[i, position.y])
                        {
                            movements.Add(new Vector2Int(i, position.y));
                        }
                    }
                }
                else
                {
                    if (!pieces[position.x - 1, position.y])
                    {
                        movements.Add(new Vector2Int(position.x - 1, position.y));
                    }
                }

                foreach (Vector2Int direction in _directionsWhiteEat)
                {
                    Vector2Int testDirection = position + direction;

                    if ((uint)testDirection.x > 7 || (uint)testDirection.y > 7) continue;

                    if (pieces[testDirection.x, testDirection.y] && pieces[testDirection.x, testDirection.y].IsWhite != IsWhite)
                    {
                        movements.Add(new Vector2Int(testDirection.x, testDirection.y));
                    }
                }
            }
            
            if (firstCall)
            {
                movements.RemoveAll(movement => !CanPlayThisMovement(pieces, this, position, movement));
            }

            return movements;
        }
    }
}