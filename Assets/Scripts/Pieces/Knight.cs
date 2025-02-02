using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Knight", menuName = "Piece/Knight")]
    public class Knight : Piece
    {
        List<Vector2Int> _directions = new List<Vector2Int>
        {
            new Vector2Int(2, 1), new Vector2Int(2, -1), new Vector2Int(-2, 1), new Vector2Int(-2, -1),
            new Vector2Int(1, 2), new Vector2Int(-1, 2), new Vector2Int(1, -2), new Vector2Int(-1, -2)
        };
        
        
        public override List<Vector2Int> AvailableMovements(Piece[,] pieces, Vector2Int position, bool firstCall)
        {
            List<Vector2Int> movements = new List<Vector2Int>();
            List<Vector2Int> movementsToRemove = new List<Vector2Int>();
            

            foreach (Vector2Int direction in _directions)
            {
                Vector2Int testDirection = position + direction;

                if (testDirection.x < 0 || testDirection.x > 7 || testDirection.y < 0 || testDirection.y > 7)
                {
                    continue;
                }
                if (pieces[testDirection.x, testDirection.y] == null)
                { 
                    movements.Add(new Vector2Int(testDirection.x, testDirection.y)); 
                    continue;
                } 
                if (pieces[testDirection.x, testDirection.y].IsWhite != IsWhite)
                { 
                    movements.Add(new Vector2Int(testDirection.x, testDirection.y));
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