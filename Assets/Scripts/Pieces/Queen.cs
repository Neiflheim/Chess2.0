using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Queen", menuName = "Piece/Queen")]
    public class Queen : Piece
    {
        public override List<Vector2Int> AvailableMovements(Vector2Int position)
        {
            throw new System.NotImplementedException();
        }
    }
}