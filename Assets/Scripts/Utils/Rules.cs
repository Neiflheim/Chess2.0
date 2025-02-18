using System.Collections.Generic;
using Game;
using Pieces;
using UnityEngine;

namespace Utils
{
    public static class Rules
    {
        // IsCheck
        public struct MoveData
        {
            public List<Vector2Int> WhiteMoves;
            public List<Vector2Int> BlackMoves;

            public MoveData(List<Vector2Int> whiteMoves, List<Vector2Int> blackMoves)
            {
                WhiteMoves = whiteMoves;
                BlackMoves = blackMoves;
            }
        }
        private static Dictionary<ulong, MoveData> _moveCache = new Dictionary<ulong, MoveData>();
        
        
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
            
            // USING ZOBRISTHASHING => SLOWER
            
            // bool isCheck = false;
            // ulong zobristHash = ZobristHashing.ComputeBoardHash(pieces);
            //
            // // Vérification du cache
            // if (_moveCache.TryGetValue(zobristHash, out MoveData moveData))
            // {
            //     Debug.Log("Use Zobrist Hash Cache");
            //     return currentPiece.IsWhite ? moveData.BlackMoves.Contains(position) : moveData.WhiteMoves.Contains(position);
            // }
            //
            // List<Vector2Int> whiteMoves = new List<Vector2Int>();
            // List<Vector2Int> blackMoves = new List<Vector2Int>();
            //
            // // Collecte des mouvements des pièces
            // for (int i = 0; i < pieces.GetLength(0); i++)
            // {
            //     for (int j = 0; j < pieces.GetLength(1); j++)
            //     {
            //         if (pieces[i, j] == null) continue;
            //
            //         List<Vector2Int> moves = pieces[i, j].AvailableMovements(pieces, new Vector2Int(i, j), false);
            //
            //         if (pieces[i, j].IsWhite)
            //         {
            //             whiteMoves.AddRange(moves);
            //         }
            //         else
            //         {
            //             blackMoves.AddRange(moves);
            //         }
            //
            //         // Vérification de la mise en échec sans interrompre l'accumulation des mouvements
            //         if (pieces[i, j].IsWhite != currentPiece.IsWhite && moves.Contains(position))
            //         {
            //             isCheck = true;
            //         }
            //     }
            // }
            //
            // // Stocker dans le cache pour accélérer les prochaines vérifications
            // _moveCache[zobristHash] = new MoveData(whiteMoves, blackMoves);
            //
            // return isCheck;
        }

        public static bool IsCheckMate(Piece[,] pieces, Piece currentPiece, Vector2Int position)
        {
            List<Vector2Int> movementsInCheck = new List<Vector2Int>();

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
                        movementsInCheck.Add(currentPieceMovement);
                    }
                }

                if (currentPieceAvailableMovements.Count == movementsInCheck.Count)
                {
                    return true;
                }
            }
            
            return false;
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