using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Rook", menuName = "Piece/Rook")]
    public class Rook : Piece
    {
        public override List<Vector2Int> AvailableMovements(Vector2Int position)
        {
            throw new System.NotImplementedException();
        }
    }
}