using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Knight", menuName = "Piece/Knight")]
    public class Knight : Piece
    {
        private List<Vector2Int> _directions = new List<Vector2Int>
        {
            new Vector2Int(2, 1), new Vector2Int(2, -1), new Vector2Int(-2, 1), new Vector2Int(-2, -1),
            new Vector2Int(1, 2), new Vector2Int(-1, 2), new Vector2Int(1, -2), new Vector2Int(-1, -2)
        };
        
        public override List<Vector2Int> AvailableMovements(int[,] board, Vector2Int position, bool firstCall)
        {
            List<Vector2Int> movements = new List<Vector2Int>();
            
            foreach (Vector2Int direction in _directions)
            {
                Vector2Int testDirection = position + direction;

                if ((uint)testDirection.x > 7 || (uint)testDirection.y > 7) continue;
                
                if (BoardsHandler.Instance.AreDifferentColors(board[position.x, position.y], board[testDirection.x, testDirection.y], true))
                {
                    movements.Add(testDirection);
                }
            }
            
            if (firstCall)
            {
                movements.RemoveAll(movement => !CanPlayThisMovement(board, this, position, movement));
            }
            
            return movements;
        }
    }
}