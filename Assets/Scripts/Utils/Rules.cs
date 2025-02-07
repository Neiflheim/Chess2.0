using System.Collections.Generic;
using Game;
using Pieces;
using UnityEngine;

namespace Utils
{
    public static class Rules
    {
        // IsCheck
        private static Dictionary<ulong, List<Vector2Int>> _cachedEnemyMovements = new Dictionary<ulong, List<Vector2Int>>();
        
        // ThreefoldRepetition
        private static List<string> _oldPieces = new List<string>();
        
        public static void PawnPromotion(Piece[,] pieces, Piece whiteQueen, Piece blackQueen)
        {
            for (int i = 0; i < pieces.GetLength(0); i++)
            {
                if (pieces[0, i])
                {
                    if (pieces[0, i].name == "WhitePawn")
                    {
                        pieces[0, i] = whiteQueen;
                    }
                }

                if (pieces[7, i])
                {
                    if (pieces[7, i].name == "BlackPawn")
                    {
                        pieces[7, i] = blackQueen;
                    }
                }
                
            }
        }
        
        public static bool IsCheck(Piece[,] pieces, Piece currentPiece, Vector2Int position)
        {
            List<Vector2Int> availableMovements = new List<Vector2Int>();
            
            for (int i = 0; i < pieces.GetLength(0); i++)
            {
                for (int j = 0; j < pieces.GetLength(1); j++)
                {
                    if (pieces[i,j] && pieces[i,j].IsWhite != currentPiece.IsWhite)
                    {
                        availableMovements = pieces[i,j].AvailableMovements(pieces, new Vector2Int(i, j), false);
                        
                        if (availableMovements.Count == 0) continue;
                    
                        foreach (Vector2Int movement in availableMovements)
                        {
                            if (movement == position)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            
            return false;
            
            // USING ZOBRISTHASHING => FASTER BUT DON'T PLAY THE RIGHT MOV
            
            // List<Vector2Int> availableMovements;
            //
            // ulong zobristHash = ZobristHashing.ComputeBoardHash(pieces); // Remplace SHA-256
            // if (_cachedEnemyMovements.TryGetValue(zobristHash, out availableMovements)) // Vérifie si l'état est en cache
            // {
            //     return availableMovements.Contains(position); // Vérifie directement
            // }
            //
            // availableMovements = new List<Vector2Int>();
            //
            // for (int i = 0; i < pieces.GetLength(0); i++)
            // {
            //     for (int j = 0; j < pieces.GetLength(1); j++)
            //     {
            //         Piece piece = pieces[i, j];
            //         if (piece != null && piece.IsWhite != currentPiece.IsWhite)
            //         {
            //             List<Vector2Int> moves = piece.AvailableMovements(pieces, new Vector2Int(i, j), false);
            //     
            //             foreach (Vector2Int move in moves)
            //             {
            //                 availableMovements.Add(move);
            //         
            //                 if (move == position)
            //                 {
            //                     _cachedEnemyMovements[zobristHash] = availableMovements; // Met à jour le cache
            //                     return true; // Arrête immédiatement, pas besoin de continuer
            //                 }
            //             }
            //         }
            //     }
            // }
            //
            // _cachedEnemyMovements[zobristHash] = availableMovements; // Stocke les mouvements si non trouvé
            // return false;
        }

        public static bool IsCheckMate(Piece[,] pieces, Piece currentPiece, Vector2Int position)
        {
            bool isCheckMate = false;
            List<Vector2Int> movementsToRemove = new List<Vector2Int>();

            if (IsCheck(pieces, currentPiece, position))
            {
                List<Vector2Int> currentPieceAvailableMovements = currentPiece.AvailableMovements(pieces, position, false);
                foreach (Vector2Int currentPieceMovement in currentPieceAvailableMovements)
                {
                    Piece[,] testPieces = (Piece[,]) pieces.Clone();
                    testPieces[currentPieceMovement.x, currentPieceMovement.y] = currentPiece;
                    testPieces[position.x, position.y] = null;

                    if (IsCheck(testPieces, currentPiece, currentPieceMovement))
                    {
                        movementsToRemove.Add(currentPieceMovement);
                    }
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

        public static void ThreefoldRepetition(Piece[,] pieces)
        {
            int count = 0;

            string piecesHasher = TranspositionTableHandler.PiecesComputeSHA256(pieces);
            
            foreach (string piece in _oldPieces)
            {
                if (piece == piecesHasher)
                {
                    count++;
                }
            }
            
            if (count >= 2)
            {
                Time.timeScale = 0;
                GameManager.Instance.EndGamePanel.SetActive(true);
                GameManager.Instance.GameOverText.text = " DRAW ";
                return;
            }
            
            _oldPieces.Add(piecesHasher);

            if (_oldPieces.Count >= 10)
            {
                _oldPieces.RemoveAt(0);
            }
        }

        public static void IsGameOver(bool isWhitePlayer)
        {
            if (isWhitePlayer)
            {
                Time.timeScale = 0;
                GameManager.Instance.EndGamePanel.SetActive(true);
                GameManager.Instance.GameOverText.text = " CHECKMATE : Victory White Player ! ";
            }
            else
            {
                Time.timeScale = 0;
                GameManager.Instance.EndGamePanel.SetActive(true);
                GameManager.Instance.GameOverText.text = " CHECKMATE : Victory Black Player ! ";
            }
        }
    }
}