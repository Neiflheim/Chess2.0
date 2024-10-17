using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Knight", menuName = "Piece/Knight")]
    public class Knight : Piece
    {
        public override List<Vector2Int> AvailableMovements(Vector2Int position)
        {
            throw new System.NotImplementedException();
        }
    }
}