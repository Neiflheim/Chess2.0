using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Rook", menuName = "Piece/Rook")]
    public class Rook : Piece
    {
        public override List<Vector2Int> AvailableMovements(Piece[,] pieces, Vector2Int position, bool firstCall)
        {
            List<Vector2Int> movements = new List<Vector2Int>();

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
            
            if (firstCall)
            {
                movements.RemoveAll(movement => !CanPlayThisMovement(pieces, this, position, movement));
            }
            
            return movements;
        }
    }
}