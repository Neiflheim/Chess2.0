using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    public abstract class Piece : ScriptableObject
    {
        public Sprite Sprite;
        public bool IsWhite;
        public int Value;

        public abstract List<Vector2Int> AvailableMovements(Vector2Int position);
    }
}