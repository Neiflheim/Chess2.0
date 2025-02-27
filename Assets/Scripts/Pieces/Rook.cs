using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Rook", menuName = "Piece/Rook")]
    public class Rook : Piece
    {
        public override List<Vector2Int> AvailableMovements(int[,] board, Vector2Int position, bool verifyKingIsCheck)
        {
            List<Vector2Int> movements = new List<Vector2Int>();

            int i;
            
            for (i = position.x + 1; i <= 7; i++) 
            { 
                if (board[i, position.y] == 0)
                { 
                    movements.Add(new Vector2Int(i, position.y));
                }
                else
                {
                    if (BoardsHandler.Instance.AreDifferentColors(board[position.x, position.y], board[i, position.y], false))
                    { 
                        movements.Add(new Vector2Int(i, position.y)); 
                    }
                    break;
                }
            } 
            
            for (i = position.x - 1; i >= 0; i--) 
            { 
                if (board[i, position.y] == 0) 
                { 
                    movements.Add(new Vector2Int(i, position.y));
                }
                else
                {
                    if (BoardsHandler.Instance.AreDifferentColors(board[position.x, position.y], board[i, position.y], false)) 
                    { 
                        movements.Add(new Vector2Int(i, position.y)); 
                    }
                    break;
                }
            }
            
            for (i = position.y + 1; i <= 7; i++) 
            { 
                if (board[position.x, i] == 0)
                { 
                    movements.Add(new Vector2Int(position.x, i));
                }
                else
                {
                    if (BoardsHandler.Instance.AreDifferentColors(board[position.x, position.y], board[position.x, i], false)) 
                    { 
                        movements.Add(new Vector2Int(position.x, i));
                    }
                    break;
                }
            } 
            
            for (i = position.y - 1; i >= 0; i--) 
            { 
                if (board[position.x, i] == 0) 
                { 
                    movements.Add(new Vector2Int(position.x, i));
                }
                else
                {
                    if (BoardsHandler.Instance.AreDifferentColors(board[position.x, position.y], board[position.x, i], false)) 
                    { 
                        movements.Add(new Vector2Int(position.x, i));
                    }
                    break;
                }
            }
            
            if (verifyKingIsCheck)
            {
                movements.RemoveAll(movement => !CanPlayThisMovement(board, position, movement));
            }
            
            return movements;
        }
    }
}