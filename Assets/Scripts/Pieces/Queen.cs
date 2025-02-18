using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Pieces
{
    [CreateAssetMenu(fileName = "Queen", menuName = "Piece/Queen")]
    public class Queen : Piece
    {
        private List<Vector2Int> _directions = new List<Vector2Int>
        {
            new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1)
        };
        
        public override List<Vector2Int> AvailableMovements(int[,] board, Vector2Int position, bool firstCall)
        {
            List<Vector2Int> movements = new List<Vector2Int>();
            
            // Rook movements
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
            
            // Bishop movements
            foreach (Vector2Int direction in _directions)
            {
                Vector2Int testDirection = position;

                while (true)
                {
                    testDirection += direction;

                    if ((uint)testDirection.x > 7 || (uint)testDirection.y > 7) break;

                    if (board[testDirection.x, testDirection.y] == 0)
                    { 
                        movements.Add(testDirection);
                    }
                    else
                    {
                        if (BoardsHandler.Instance.AreDifferentColors(board[position.x, position.y], board[testDirection.x, testDirection.y], false))
                        { 
                            movements.Add(testDirection);
                        }
                        break;
                    }
                }
            }
            
            if (firstCall)
            {
                movements.RemoveAll(movement => !CanPlayThisMovement(board, this, position, movement));
            }
            
            return movements;
        }
    }
}