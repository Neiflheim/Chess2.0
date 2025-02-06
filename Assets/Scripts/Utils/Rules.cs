using System.Collections.Generic;
using Game;
using Pieces;
using UnityEngine;

namespace Utils
{
    public static class Rules
    {
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
            bool isCheck = false;
            
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

        public static void IsGameOver(Piece[,] pieces, Piece whiteKing, Piece blackKing)
        {
            GameManager.Instance.IsBlackKingCheckMate = false;
            GameManager.Instance.IsWhiteKingCheckMate = false;
            
            for (int i = 0; i < pieces.GetLength(0); i++)
            {
                for (int j = 0; j < pieces.GetLength(1); j++)
                {
                    if (pieces[i, j])
                    {
                        if (pieces[i, j] == blackKing)
                        {
                            if (IsCheckMate(pieces, pieces[i, j], new Vector2Int(i, j)))
                            {
                                GameManager.Instance.IsBlackKingCheckMate = true;
                            }
                        }
                        if (pieces[i, j] == whiteKing)
                        {
                            if (IsCheckMate(pieces, pieces[i, j], new Vector2Int(i, j)))
                            {
                                GameManager.Instance.IsWhiteKingCheckMate = true;
                            }
                        }
                    }
                }
            }

            if (GameManager.Instance.IsBlackKingCheckMate)
            {
                Time.timeScale = 0;
                GameManager.Instance.EndGamePanel.SetActive(true);
                GameManager.Instance.GameOverText.text = " CHECKMATE : Victory White Player ! ";
            }
            if (GameManager.Instance.IsWhiteKingCheckMate)
            {
                Time.timeScale = 0;
                GameManager.Instance.EndGamePanel.SetActive(true);
                GameManager.Instance.GameOverText.text = " CHECKMATE : Victory Black Player ! ";
            }
        }
    }
}