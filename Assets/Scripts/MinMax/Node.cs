using System.Collections.Generic;
using Game;
using Pieces;
using UnityEngine;

namespace MinMax
{
    public class Node
    {
        public Piece[,] Pieces;
        public bool IsWhiteTurn;
        public bool IsWhitePlaying;
        
        public Node(Piece[,] pieces, bool isWhiteTurn, bool isWhitePlaying)
        {
            Pieces = pieces;
            IsWhiteTurn = isWhiteTurn;
            IsWhitePlaying = isWhitePlaying;
        }
        
        public bool IsTerminal()
        {
            return HeuristicValue() >= 20000 || HeuristicValue() <= -20000;
        }
        
        public int HeuristicValue()
        {
            int boardHeuristicValue;
            int whiteHeuristicValue = 0;
            int blackHeuristicValue = 0;
            
            for (int i = 0; i < Pieces.GetLength(0); i++)
            {
                for (int j = 0; j < Pieces.GetLength(1); j++)
                {
                    if (Pieces[i,j])
                    {
                        if (Pieces[i,j].IsWhite)
                        {
                            int valueDependingOnPosition = 0;
                            if (Pieces[i,j].name == "WhitePawn")
                            {
                                valueDependingOnPosition = ValueDependingOnPositionData.WhitePawnMatrix[i, j];
                            }
                            if (Pieces[i,j].name == "WhiteBishop")
                            {
                                valueDependingOnPosition = ValueDependingOnPositionData.WhiteBishopMatrix[i, j];
                            }
                            if (Pieces[i,j].name == "WhiteKnight")
                            {
                                valueDependingOnPosition = ValueDependingOnPositionData.WhiteKnightMatrix[i, j];
                            }
                            if (Pieces[i,j].name == "WhiteRook")
                            {
                                valueDependingOnPosition = ValueDependingOnPositionData.WhiteRookMatrix[i, j];
                            }
                            if (Pieces[i,j].name == "WhiteQueen")
                            {
                                valueDependingOnPosition = ValueDependingOnPositionData.WhiteQueenMatrix[i, j];
                            }
                            if (Pieces[i,j].name == "WhiteKing")
                            {
                                valueDependingOnPosition = ValueDependingOnPositionData.WhiteKingMatrix[i, j];
                            }

                            whiteHeuristicValue += Pieces[i, j].BaseValue + valueDependingOnPosition;
                        }
                        else
                        {
                            int valueDependingOnPosition = 0;
                            if (Pieces[i,j].name == "BlackPawn")
                            {
                                valueDependingOnPosition = ValueDependingOnPositionData.BlackPawnMatrix[i, j];
                            }
                            if (Pieces[i,j].name == "BlackBishop")
                            {
                                valueDependingOnPosition = ValueDependingOnPositionData.BlackBishopMatrix[i, j];
                            }
                            if (Pieces[i,j].name == "BlackKnight")
                            {
                                valueDependingOnPosition = ValueDependingOnPositionData.BlackKnightMatrix[i, j];
                            }
                            if (Pieces[i,j].name == "BlackRook")
                            {
                                valueDependingOnPosition = ValueDependingOnPositionData.BlackRookMatrix[i, j];
                            }
                            if (Pieces[i,j].name == "BlackQueen")
                            {
                                valueDependingOnPosition = ValueDependingOnPositionData.BlackQueenMatrix[i, j];
                            }
                            if (Pieces[i,j].name == "BlackKing")
                            {
                                valueDependingOnPosition = ValueDependingOnPositionData.BlackKingMatrix[i, j];
                            }

                            blackHeuristicValue += Pieces[i, j].BaseValue + valueDependingOnPosition;
                        }
                    }
                }
            }
            
            // foreach (Piece piece in Pieces)
            // {
            //     if (piece)
            //     {
            //         if (piece.IsWhite)
            //         {
            //             whiteHeuristicValue += piece.BaseValue;
            //         }
            //         else
            //         {
            //             blackHeuristicValue += piece.BaseValue;
            //         }
            //     }
            // }
            
            if (IsWhitePlaying)
            {
                boardHeuristicValue = whiteHeuristicValue - blackHeuristicValue;
            }
            else
            {
                boardHeuristicValue = blackHeuristicValue - whiteHeuristicValue;
            }
            
            return boardHeuristicValue;
        }
        
        public List<Node> Children()
        {
            List<Node> children = new List<Node>();
            
            // Crée une Node pour chaque mouvements disponibles de chaque piece de la couleur dont c'est le tour
            for (int i = 0; i < Pieces.GetLength(0); i++)
            {
                for (int j = 0; j < Pieces.GetLength(1); j++)
                {
                    if (Pieces[i,j] && Pieces[i,j].IsWhite == IsWhiteTurn)
                    {
                        Piece piece = Pieces[i,j];
                        Vector2Int position = new Vector2Int(i, j);
                        List<Vector2Int> availableMovements = piece.AvailableMovements(position);
                        
                        if (availableMovements.Count == 0) continue;
                    
                        foreach (Vector2Int movement in availableMovements)
                        {
                            Piece[,] pieces = CreateCopy();
                            pieces = MovePiece(pieces, piece, position, movement);
                            Node node = new Node(pieces, !IsWhiteTurn, IsWhitePlaying); 
                            children.Add(node);
                        }
                    }
                }
            }
            
            return children;
        }
        
        public Piece[,] MovePiece(Piece[,] pieces, Piece piece, Vector2Int from, Vector2Int to)
        {
            // Déplacement de la piece sur le pieces
            Piece[,] newPieces = (Piece[,]) pieces.Clone();
            newPieces[from.x, from.y] = null;
            newPieces[to.x, to.y] = piece;
            return newPieces;
        }
        
        private Piece[,] CreateCopy()
        {
            if (Pieces == null) return null;

            int rows = Pieces.GetLength(0);
            int cols = Pieces.GetLength(1);
            Piece[,] newPieces = new Piece[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (Pieces[row, col] != null)
                    {
                        newPieces[row, col] = Pieces[row, col];
                    }
                }
            }
            
            return newPieces;
        }
    }
}