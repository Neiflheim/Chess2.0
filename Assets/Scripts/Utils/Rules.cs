using System.Collections.Generic;
using Pieces;
using UnityEngine;

namespace Utils
{
    public static class Rules
    {
        public static bool IsCheck(Piece[,] pieces, Piece currentPiece, Vector2Int position)
        {
            bool isCheck = false;
            
            for (int i = 0; i < pieces.GetLength(0); i++)
            {
                for (int j = 0; j < pieces.GetLength(1); j++)
                {
                    if (pieces[i,j] && pieces[i,j].IsWhite != currentPiece.IsWhite)
                    {
                        Piece piece = pieces[i,j];
                        Vector2Int piecePosition = new Vector2Int(i, j);
                        List<Vector2Int> availableMovements = piece.AvailableMovements(pieces, piecePosition);
                        
                        if (availableMovements.Count == 0) continue;
                    
                        foreach (Vector2Int movement in availableMovements)
                        {
                            if (movement == position)
                            {
                                isCheck = true;
                            }
                        }
                    }
                }
            }
            
            return isCheck;
        }

        public static bool IsCheckMate(Piece[,] pieces, Piece currentPiece, Vector2Int position)
        {
            bool isCheckMate = false;
            List<Vector2Int> movementsToRemove = new List<Vector2Int>();

            if (IsCheck(pieces, currentPiece, position))
            {
                List<Vector2Int> currentPieceAvailableMovements = currentPiece.AvailableMovements(pieces, position);
                foreach (Vector2Int currentPieceMovement in currentPieceAvailableMovements)
                {
                    Piece[,] testPieces = (Piece[,]) pieces.Clone();
                    testPieces[currentPieceMovement.x, currentPieceMovement.y] = currentPiece;
                    testPieces[position.x, position.y] = null;

                    if (IsCheck(testPieces, currentPiece, currentPieceMovement))
                    {
                        movementsToRemove.Add(currentPieceMovement);
                    }
                    
                    // for (int i = 0; i < testPieces.GetLength(0); i++)
                    // {
                    //     for (int j = 0; j < testPieces.GetLength(1); j++)
                    //     {
                    //         if (testPieces[i,j] && testPieces[i,j].IsWhite != currentPiece.IsWhite)
                    //         {
                    //             Piece piece = testPieces[i,j];
                    //             Vector2Int piecePosition = new Vector2Int(i, j);
                    //             List<Vector2Int> pieceAvailableMovements = piece.AvailableMovements(testPieces, piecePosition);
                    //     
                    //             if (pieceAvailableMovements.Count == 0) continue;
                    //
                    //             foreach (Vector2Int pieceMovement in pieceAvailableMovements)
                    //             {
                    //                 if ()
                    //                 {
                    //                     movementsToRemove.Add(pieceMovement);
                    //                 }
                    //             }
                    //         }
                    //     }
                    // }
                }

                foreach (Vector2Int movement in movementsToRemove)
                {
                    if (currentPieceAvailableMovements.Contains(movement))
                    {
                        currentPieceAvailableMovements.Remove(movement);
                    }
                }

                if (currentPieceAvailableMovements.Count == 0)
                {
                    isCheckMate = true;
                }
            }
            
            return isCheckMate;
        }
    }
}