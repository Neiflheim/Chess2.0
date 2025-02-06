using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Queen", menuName = "Piece/Queen")]
    public class Queen : Piece
    {
        private List<Vector2Int> _directions = new List<Vector2Int>
        {
            new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1)
        };
        
        public override List<Vector2Int> AvailableMovements(Piece[,] pieces, Vector2Int position, bool firstCall)
        {
            List<Vector2Int> movements = new List<Vector2Int>();
            
            // Rook movements
            int i;
            
            for (i = position.x + 1; i <= 7; i++) 
            { 
                if (!pieces[i, position.y])
                { 
                    movements.Add(new Vector2Int(i, position.y));
                }
                else
                {
                    if (pieces[i, position.y].IsWhite != IsWhite)
                    { 
                        movements.Add(new Vector2Int(i, position.y)); 
                    }
                    break;
                }
            } 
            
            for (i = position.x - 1; i >= 0; i--) 
            { 
                if (!pieces[i, position.y]) 
                { 
                    movements.Add(new Vector2Int(i, position.y));
                }
                else
                {
                    if (pieces[i, position.y].IsWhite != IsWhite) 
                    { 
                        movements.Add(new Vector2Int(i, position.y)); 
                    }
                    break;
                }
            }
            
            for (i = position.y + 1; i <= 7; i++) 
            { 
                if (!pieces[position.x, i])
                { 
                    movements.Add(new Vector2Int(position.x, i));
                }
                else
                {
                    if (pieces[position.x, i].IsWhite != IsWhite) 
                    { 
                        movements.Add(new Vector2Int(position.x, i));
                    }
                    break;
                }
            } 
            
            for (i = position.y - 1; i >= 0; i--) 
            { 
                if (!pieces[position.x, i]) 
                { 
                    movements.Add(new Vector2Int(position.x, i));
                }
                else
                {
                    if (pieces[position.x, i].IsWhite != IsWhite) 
                    { 
                        movements.Add(new Vector2Int(position.x, i));
                    }
                    break;
                }
            }
            
            // Bishop movements
            foreach (Vector2Int direction in _directions)
            {
                Vector2Int testDirection = position;

                while (true)
                {
                    testDirection += direction;

                    if ((uint)testDirection.x > 7 || (uint)testDirection.y > 7) break;

                    if (!pieces[testDirection.x, testDirection.y])
                    { 
                        movements.Add(testDirection);
                    }
                    else
                    {
                        if (pieces[testDirection.x, testDirection.y].IsWhite != IsWhite)
                        { 
                            movements.Add(testDirection); 
                        }
                        break;
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