using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Pieces
{
    [CreateAssetMenu(fileName = "King", menuName = "Piece/King")]
    public class King : Piece
    {
        private bool _canShortCastling;
        private bool _canLongCastling;
        
        private List<Vector2Int> _directions = new List<Vector2Int>
        {
            new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0),
            new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1)
        };
        
        public override List<Vector2Int> AvailableMovements(int[,] board, Vector2Int position, bool verifyKingIsCheck)
        {
            List<Vector2Int> movements = new List<Vector2Int>();

            foreach (Vector2Int direction in _directions)
            {
                Vector2Int testDirection = position + direction;

                if ((uint)testDirection.x > 7 || (uint)testDirection.y > 7) continue;
                
                if (Rules.AreDifferentColors(board[position.x, position.y], board[testDirection.x, testDirection.y], true))
                {
                    movements.Add(testDirection);
                }
            }
            
            // Castling
            _canShortCastling = true;
            _canLongCastling = true;
            
            if (IsWhite)
            {
                if (Rules.WhiteKingAndWhiteNearestRookHaveNotMoved)
                {
                    for (int i = position.y + 1; i <= position.y + 2; i++)
                    {
                        if (i > 7 || i < 0) continue;
                        
                        if (board[position.x, i] != 0)
                        {
                            _canShortCastling = false;
                            break;
                        }
                    }
                    
                    if (_canShortCastling)
                    {
                        Vector2Int testDirection = new Vector2Int(position.x, position.y + 2);

                        if (position.y + 2 <= 7 || position.y + 2 >= 0)
                        {
                            movements.Add(new Vector2Int(position.x, position.y + 2));
                        }
                    }
                }
            
                if (Rules.WhiteKingAndWhiteFarthestRookHaveNotMoved)
                {
                    for (int i = position.y - 1; i >= position.y - 3; i--)
                    {
                        if (i > 7 || i < 0) continue;
                        
                        if (board[position.x, i] != 0)
                        {
                            _canLongCastling = false;
                            break;
                        }
                    }
                    
                    if (_canLongCastling)
                    {
                        if (position.y + 2 <= 7 || position.y + 2 >= 0)
                        {
                            movements.Add(new Vector2Int(position.x, position.y - 2));
                        }
                    }
                }
            }
            else
            {
                if (Rules.BlackKingAndBlackNearestRookHaveNotMoved)
                {
                    for (int i = position.y + 1; i <= position.y + 2; i++)
                    {
                        if (i > 7 || i < 0) continue;
                        
                        if (board[position.x, i] != 0)
                        {
                            _canShortCastling = false;
                            break;
                        }
                    }
                    
                    if (_canShortCastling)
                    {
                        if (position.y + 2 <= 7 || position.y + 2 >= 0)
                        {
                            movements.Add(new Vector2Int(position.x, position.y + 2));
                        }
                    }
                }
            
                if (Rules.BlackKingAndBlackFarthestRookHaveNotMoved)
                {
                    for (int i = position.y - 1; i >= position.y - 3; i--)
                    {
                        if (i > 7 || i < 0) continue;
                        
                        if (board[position.x, i] != 0)
                        {
                            _canLongCastling = false;
                            break;
                        }
                    }
                    
                    if (_canLongCastling)
                    {
                        if (position.y + 2 <= 7 || position.y + 2 >= 0)
                        {
                            movements.Add(new Vector2Int(position.x, position.y - 2));
                        }
                    }
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