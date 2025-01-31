using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "King", menuName = "Piece/King")]
    public class King : Piece
    {
        public override List<Vector2Int> AvailableMovements(Piece[,] pieces, Vector2Int position)
        {
            List<Vector2Int> movements = new List<Vector2Int>();

            List<Vector2Int> directions = new List<Vector2Int>
            {
                new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0),
                new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1)
            };

            foreach (Vector2Int direction in directions)
            {
                Vector2Int testDirection = position + direction;

                // Vérification si le mouvement est dans la matrice
                if (testDirection.x < 0 || testDirection.x > 7 || testDirection.y < 0 || testDirection.y > 7)
                {
                    continue;
                }
                
                // Ajoute les mouvements
                if (pieces[testDirection.x, testDirection.y] == null)
                { 
                    movements.Add(new Vector2Int(testDirection.x, testDirection.y));
                    continue;
                } 
                if (pieces[testDirection.x, testDirection.y].IsWhite != IsWhite)
                { 
                    movements.Add(new Vector2Int(testDirection.x, testDirection.y));
                }
            }
            
            return movements;
        }
    }
}