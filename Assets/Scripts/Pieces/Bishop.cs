using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Bishop", menuName = "Piece/Bishop")]
    public class Bishop : Piece
    {
        public override List<Vector2Int> AvailableMovements(Vector2Int position)
        {
            throw new System.NotImplementedException();
        }
    }
}