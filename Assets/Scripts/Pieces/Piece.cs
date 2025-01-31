using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    public abstract class Piece : ScriptableObject
    {
        public Sprite Sprite;
        public bool IsWhite;
        public int BaseValue;

        public abstract List<Vector2Int> AvailableMovements(Piece[,] pieces, Vector2Int position);
    }
}