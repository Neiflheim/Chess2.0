using System.Collections.Generic;
using Game;
using Handlers;
using UnityEngine;

namespace Utils
{
    public static class Rules
    {
        // Castling
        public static bool WhiteKingAndWhiteNearestRookHaveNotMoved = true;
        public static bool WhiteKingAndWhiteFarthestRookHaveNotMoved = true;
        public static bool BlackKingAndBlackNearestRookHaveNotMoved = true;
        public static bool BlackKingAndBlackFarthestRookHaveNotMoved = true;
        
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

        public static void CheckNotMoved(int[,] board)
        {
            // Black
            if (board[0, 4] != 12 && BlackKingAndBlackNearestRookHaveNotMoved || board[0, 4] != 12 && BlackKingAndBlackFarthestRookHaveNotMoved)
            {
                BlackKingAndBlackNearestRookHaveNotMoved = false;
                BlackKingAndBlackFarthestRookHaveNotMoved = false;
            }
            else if (board[0, 7] != 10 && BlackKingAndBlackNearestRookHaveNotMoved)
            {
                BlackKingAndBlackNearestRookHaveNotMoved = false;
            }
            else if (board[0, 0] != 10 && BlackKingAndBlackFarthestRookHaveNotMoved)
            {
                BlackKingAndBlackFarthestRookHaveNotMoved = false;
            }
                
            // White
            if (board[7, 4] != 6 && WhiteKingAndWhiteNearestRookHaveNotMoved || board[7, 4] != 6 && WhiteKingAndWhiteFarthestRookHaveNotMoved)
            {
                WhiteKingAndWhiteNearestRookHaveNotMoved = false;
                WhiteKingAndWhiteFarthestRookHaveNotMoved = false;
            }
            else if (board[7, 7] != 4 && WhiteKingAndWhiteNearestRookHaveNotMoved)
            {
                WhiteKingAndWhiteNearestRookHaveNotMoved = false;
            }
            else if (board[7, 0] != 4 && WhiteKingAndWhiteFarthestRookHaveNotMoved)
            {
                WhiteKingAndWhiteFarthestRookHaveNotMoved = false;
            }
        }

        public static void Castling(int[,] board, int[,] lastBoard)
        {
            // Black
            if (lastBoard[0, 4] == 12 && board[0, 6] == 12)
            {
                board[0, 5] = 10;
                board[0, 7] = 0;
            }
            
            if (lastBoard[0, 4] == 12 && board[0, 2] == 12)
            {
                board[0, 3] = 10;
                board[0, 0] = 0;
            }
            
            // White
            if (lastBoard[7, 4] == 6 && board[7, 6] == 6)
            {
                board[7, 5] = 4;
                board[7, 7] = 0;
            }
            
            if (lastBoard[7, 4] == 6 && board[7, 2] == 6)
            {
                board[7, 3] = 4;
                board[7, 0] = 0;
            }
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