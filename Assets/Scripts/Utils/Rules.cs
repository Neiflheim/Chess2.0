using System.Collections.Generic;
using Game;
using Handlers;
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
        
        // Rules
        public static void PawnPromotion(int[,] board)
        {
            int boardLength = board.GetLength(0);
            for (int i = 0; i < boardLength; i++)
            {
                if (board[0, i] == 1)
                {
                    board[0, i] = 5;
                }

                if (board[7, i] == 7)
                {
                    board[7, i] = 11;
                }
                
            }
        }
        
        public static bool IsCheck(int[,] board, Vector2Int piecePosition)
        {
            int boardLength = board.GetLength(0);
            var boardHandler = BoardsHandler.Instance;
            
            // DON'T USE CACHE
            List<Vector2Int> availableMovements;
            
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    if (AreDifferentColors(board[piecePosition.x, piecePosition.y], board[i, j], false))
                    {
                        availableMovements = boardHandler.PiecesDictionary[board[i,j]].AvailableMovements(board, new Vector2Int(i, j), false);
                        
                        if (availableMovements.Count == 0) continue;
                    
                        foreach (Vector2Int movement in availableMovements)
                        {
                            if (movement == piecePosition)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            
            return false;
            
            
            // USE CACHE AND ZOBRISTHASHER
            // ulong hashingBoard = ZobristHashing.ComputeBoardHash(board);
            //
            // if (_moveCache.TryGetValue(hashingBoard, out MoveData moveData))
            // {
            //     return PieceIsWhite(board[piecePosition.x, piecePosition.y]) ? moveData.BlackMoves.Contains(piecePosition) : moveData.WhiteMoves.Contains(piecePosition);
            // }
            //
            // bool isCheck = false;
            //
            // var whiteMoves = new List<Vector2Int>();
            // var blackMoves = new List<Vector2Int>();
            //
            // bool pieceIsWhite = PieceIsWhite(board[piecePosition.x, piecePosition.y]);
            //
            // // Collecte des mouvements des pièces
            // for (int i = 0; i < boardLength; i++)
            // {
            //     for (int j = 0; j < boardLength; j++)
            //     {
            //         int pieceIndex = board[i, j];
            //         if (pieceIndex == 0) continue;
            //
            //         bool isWhite = PieceIsWhite(pieceIndex);
            //         List<Vector2Int> moves = boardHandler.PiecesDictionary[pieceIndex].AvailableMovements(board, new Vector2Int(i, j), false);
            //
            //         if (isWhite)
            //         {
            //             whiteMoves.AddRange(moves);
            //         }
            //         else
            //         {
            //             blackMoves.AddRange(moves);
            //         }
            //
            //         // Vérification de la mise en échec sans interrompre l'accumulation des mouvements
            //         if (isWhite != pieceIsWhite && moves.Contains(piecePosition))
            //         {
            //             isCheck = true;
            //         }
            //     }
            // }
            //
            // // Stocker dans le cache pour accélérer les prochaines vérifications
            // _moveCache[hashingBoard] = new MoveData(whiteMoves, blackMoves);
            //
            // return isCheck;
        }

        public static bool IsCheckMate(int[,] board, int pieceIndex, Vector2Int position)
        {
            var boardHandler = BoardsHandler.Instance;
            List<Vector2Int> movementsInCheck = new List<Vector2Int>();

            if (IsCheck(board, position))
            {
                var currentPieceAvailableMovements = boardHandler.PiecesDictionary[pieceIndex].AvailableMovements(board, position, false);
                foreach (Vector2Int currentPieceMovement in currentPieceAvailableMovements)
                {
                    int[,] testBoard = (int[,]) board.Clone();
                    testBoard[currentPieceMovement.x, currentPieceMovement.y] = pieceIndex;
                    testBoard[position.x, position.y] = 0;
            
                    if (IsCheck(testBoard, currentPieceMovement))
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

        public static void ThreefoldRepetition(int[,] board)
        {
            int count = 0;

            string piecesHasher = TranspositionTableHandler.PiecesComputeSHA256(board);
            
            foreach (string piece in _oldPieces)
            {
                if (piece == piecesHasher)
                {
                    count++;
                    Debug.Log(count);
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
        
        // Utils
        public static bool PieceIsWhite(int pieceIndex)
        {
            return pieceIndex <= 6 && pieceIndex != 0;
        }
        
        public static bool AreDifferentColors(int pieceOne, int pieceTwo, bool emptyVerification)
        {
            if (emptyVerification)
            {
                if (pieceTwo == 0)
                {
                    return true;
                }
            }
            
            return (pieceOne <= 6 && pieceTwo > 6) || (pieceOne > 6 && pieceTwo <= 6 && pieceTwo >= 1);
        }
    }
}