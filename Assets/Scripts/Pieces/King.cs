using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "King", menuName = "Piece/King")]
    public class King : Piece
    {
        public override List<Vector2Int> AvailableMovements(Vector2Int position)
        {
            throw new System.NotImplementedException();
        }
    }
}