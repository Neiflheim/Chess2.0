using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Pawn", menuName = "Piece/Pawn")]
    public class Pawn : Piece
    {
        List<Vector2Int> _directionsBlackEat = new List<Vector2Int>
        {
            new Vector2Int(1, 1), new Vector2Int(1, -1)
        };
        
        List<Vector2Int> _directionsWhiteEat = new List<Vector2Int>
        {
            new Vector2Int(-1, 1), new Vector2Int(-1, -1)
        };

        public override List<Vector2Int> AvailableMovements(Piece[,] pieces, Vector2Int position)
        {
            List<Vector2Int> movements = new List<Vector2Int>();

            int i;

            if (IsWhite == false)
            {
                if (position.x == 1)
                {
                    for (i = position.x + 1; i <= position.x + 2; i++)
                    {
                        if (i < 0 || i > 7)
                        {
                            continue;
                        }
                        if (BoardsHandler.Instance.Pieces[i, position.y] == null)
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
                    for (i = position.x + 1; i <= position.x + 1; i++)
                    {
                        if (i < 0 || i > 7)
                        {
                            continue;
                        }
                        if (BoardsHandler.Instance.Pieces[i, position.y] == null)
                        {
                            movements.Add(new Vector2Int(i, position.y));
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                foreach (Vector2Int direction in _directionsBlackEat)
                {
                    Vector2Int testDirection = position + direction;

                    if (testDirection.x < 0 || testDirection.x > 7 || testDirection.y < 0 || testDirection.y > 7)
                    {
                        continue;
                    }

                    if (BoardsHandler.Instance.Pieces[testDirection.x, testDirection.y] == null)
                    {
                        continue;
                    }

                    if (BoardsHandler.Instance.Pieces[testDirection.x, testDirection.y].IsWhite != IsWhite)
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
                        if (i < 0 || i > 7)
                        {
                            continue;
                        }
                        if (BoardsHandler.Instance.Pieces[i, position.y] == null)
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
                    for (i = position.x - 1; i >= position.x - 1; i--)
                    {
                        if (i < 0 || i > 7)
                        {
                            continue;
                        }
                        if (BoardsHandler.Instance.Pieces[i, position.y] == null)
                        {
                            movements.Add(new Vector2Int(i, position.y));
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                foreach (Vector2Int direction in _directionsWhiteEat)
                {
                    Vector2Int testDirection = position + direction;

                    if (testDirection.x < 0 || testDirection.x > 7 || testDirection.y < 0 || testDirection.y > 7)
                    {
                        continue;
                    }

                    if (BoardsHandler.Instance.Pieces[testDirection.x, testDirection.y] == null)
                    {
                        continue;
                    }

                    if (BoardsHandler.Instance.Pieces[testDirection.x, testDirection.y].IsWhite != IsWhite)
                    {
                        movements.Add(new Vector2Int(testDirection.x, testDirection.y));
                    }
                }
            }

            return movements;
        }
    }
}