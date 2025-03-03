using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Bishop", menuName = "Piece/Bishop")]
    public class Bishop : Piece
    {
        private List<Vector2Int> _directions = new List<Vector2Int>
        {
            new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1)
        };
        
        public override List<Vector2Int> AvailableMovements(int[,] board, Vector2Int position, bool verifyKingIsCheck)
        {
            List<Vector2Int> movements = new List<Vector2Int>();

            foreach (Vector2Int direction in _directions)
            {
                Vector2Int testDirection = position;

                while (true)
                {
                    testDirection += direction;

                    if ((uint)testDirection.x > 7 || (uint)testDirection.y > 7) break;

                    if (board[testDirection.x, testDirection.y] == 0)
                    { 
                        movements.Add(testDirection);
                    }
                    else
                    {
                        if (Rules.AreDifferentColors(board[position.x, position.y], board[testDirection.x, testDirection.y], false))
                        { 
                            movements.Add(testDirection);
                        }
                        break;
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