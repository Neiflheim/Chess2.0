using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "King", menuName = "Piece/King")]
    public class King : Piece
    {
        private List<Vector2Int> _directions = new List<Vector2Int>
        {
            new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0),
            new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1)
        };
        
        public override List<Vector2Int> AvailableMovements(Piece[,] pieces, Vector2Int position, bool firstCall)
        {
            List<Vector2Int> movements = new List<Vector2Int>();

            foreach (Vector2Int direction in _directions)
            {
                Vector2Int testDirection = position + direction;

                if ((uint)testDirection.x > 7 || (uint)testDirection.y > 7) continue;
                
                Piece targetPiece = pieces[testDirection.x, testDirection.y];
                if (!targetPiece || targetPiece.IsWhite != IsWhite)
                {
                    movements.Add(testDirection);
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