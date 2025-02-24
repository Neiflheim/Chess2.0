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
        private static Dictionary<string, MoveData> _moveCache = new Dictionary<string, MoveData>();
        
        
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
            // DON'T USE CACHE : depth : 4 => 400ms
            List<Vector2Int> availableMovements = new List<Vector2Int>();
            
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    if (BoardsHandler.Instance.AreDifferentColors(board[piecePosition.x, piecePosition.y], board[i, j], false))
                    {
                        availableMovements = BoardsHandler.Instance.PiecesDictionary[board[i,j]].AvailableMovements(board, new Vector2Int(i, j), false);
                        
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
            
            
            // USE CACHE WITHOUT HASHER : depth : 4 => 1300ms
            // bool isCheck = false;
            //
            // if (_moveCache.TryGetValue(board, out MoveData moveData))
            // {
            //     Debug.Log("Use Hash Cache");
            //     return BoardsHandler.Instance.PieceIsWhite(board[piecePosition.x, piecePosition.y]) ? moveData.BlackMoves.Contains(piecePosition) : moveData.WhiteMoves.Contains(piecePosition);
            // }
            //
            // List<Vector2Int> whiteMoves = new List<Vector2Int>();
            // List<Vector2Int> blackMoves = new List<Vector2Int>();
            //
            // // Collecte des mouvements des pièces
            // for (int i = 0; i < board.GetLength(0); i++)
            // {
            //     for (int j = 0; j < board.GetLength(1); j++)
            //     {
            //         if (board[i, j] == 0) continue;
            //
            //         List<Vector2Int> moves = BoardsHandler.Instance.PiecesDictionary[board[i, j]].AvailableMovements(board, new Vector2Int(i, j), false);
            //
            //         if (BoardsHandler.Instance.PieceIsWhite(board[i, j]))
            //         {
            //             whiteMoves.AddRange(moves);
            //         }
            //         else
            //         {
            //             blackMoves.AddRange(moves);
            //         }
            //
            //         // Vérification de la mise en échec sans interrompre l'accumulation des mouvements
            //         if (BoardsHandler.Instance.PieceIsWhite(board[i, j]) != BoardsHandler.Instance.PieceIsWhite(board[piecePosition.x, piecePosition.y]) && moves.Contains(piecePosition))
            //         {
            //             isCheck = true;
            //         }
            //     }
            // }
            //
            // // Stocker dans le cache pour accélérer les prochaines vérifications
            // _moveCache[board] = new MoveData(whiteMoves, blackMoves);
            //
            // return isCheck;
            
            
            // USE CACHE WITH HASHER : depth : 4 => 2700ms
            // bool isCheck = false;
            // string boardHasherVersion = TranspositionTableHandler.BoardsHasher(board);
            //
            // if (_moveCache.TryGetValue(boardHasherVersion, out MoveData moveData))
            // {
            //     Debug.Log("Use Hash Cache");
            //     return BoardsHandler.Instance.PieceIsWhite(board[piecePosition.x, piecePosition.y]) ? moveData.BlackMoves.Contains(piecePosition) : moveData.WhiteMoves.Contains(piecePosition);
            // }
            //
            // List<Vector2Int> whiteMoves = new List<Vector2Int>();
            // List<Vector2Int> blackMoves = new List<Vector2Int>();
            //
            // // Collecte des mouvements des pièces
            // for (int i = 0; i < board.GetLength(0); i++)
            // {
            //     for (int j = 0; j < board.GetLength(1); j++)
            //     {
            //         if (board[i, j] == 0) continue;
            //
            //         List<Vector2Int> moves = BoardsHandler.Instance.PiecesDictionary[board[i, j]].AvailableMovements(board, new Vector2Int(i, j), false);
            //
            //         if (BoardsHandler.Instance.PieceIsWhite(board[i, j]))
            //         {
            //             whiteMoves.AddRange(moves);
            //         }
            //         else
            //         {
            //             blackMoves.AddRange(moves);
            //         }
            //
            //         // Vérification de la mise en échec sans interrompre l'accumulation des mouvements
            //         if (BoardsHandler.Instance.PieceIsWhite(board[i, j]) != BoardsHandler.Instance.PieceIsWhite(board[piecePosition.x, piecePosition.y]) && moves.Contains(piecePosition))
            //         {
            //             isCheck = true;
            //         }
            //     }
            // }
            //
            // // Stocker dans le cache pour accélérer les prochaines vérifications
            // _moveCache[boardHasherVersion] = new MoveData(whiteMoves, blackMoves);
            //
            // return isCheck;
        }

        public static bool IsCheckMate(int[,] board, int pieceIndex, Vector2Int position)
        {
            List<Vector2Int> movementsInCheck = new List<Vector2Int>();

            if (IsCheck(board, position))
            {
                List<Vector2Int> currentPieceAvailableMovements = BoardsHandler.Instance.PiecesDictionary[pieceIndex].AvailableMovements(board, position, false);
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
    }
}