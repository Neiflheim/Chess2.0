using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Rook", menuName = "Piece/Rook")]
    public class Rook : Piece
    {
        public override List<Vector2Int> AvailableMovements(Piece[,] pieces, Vector2Int position, bool firstCall)
        {
            List<Vector2Int> movements = new List<Vector2Int>();
            List<Vector2Int> movementsToRemove = new List<Vector2Int>();

            int i;
            int j;
            
            for (i = position.x + 1; i <= 7; i++) 
            { 
                if (pieces[i, position.y] == null)
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    continue;
                } 
                if (pieces[i, position.y].IsWhite != IsWhite)
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    break;
                }
                else 
                { 
                    break;
                }
            } 
            for (i = position.x - 1; i >= 0; i--) 
            { 
                if (pieces[i, position.y] == null) 
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    continue;
                } 
                if (pieces[i, position.y].IsWhite != IsWhite) 
                { 
                    movements.Add(new Vector2Int(i, position.y)); 
                    break;
                }
                else 
                { 
                    break;
                }
            }
            
            for (j = position.y + 1; j <= 7; j++) 
            { 
                if (pieces[position.x, j] == null)
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    continue;
                } 
                if (pieces[position.x, j].IsWhite != IsWhite) 
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    break;
                }
                else 
                { 
                    break;
                }
            } 
            for (j = position.y - 1; j >= 0; j--) 
            { 
                if (pieces[position.x, j] == null) 
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    continue;
                } 
                if (pieces[position.x, j].IsWhite != IsWhite) 
                { 
                    movements.Add(new Vector2Int(position.x, j)); 
                    break;
                }
                else 
                { 
                    break;
                }
            }
            
            if (firstCall)
            {
                foreach (Vector2Int movement in movements)
                {
                    if (!CanPlayThisMovement(pieces, this, position, movement))
                    {
                        movementsToRemove.Add(movement);
                    }
                }
            
                foreach (Vector2Int movement in movementsToRemove)
                {
                    if (movements.Contains(movement))
                    {
                        movements.Remove(movement);
                    }
                }
            }
            
            return movements;
        }
    }
}