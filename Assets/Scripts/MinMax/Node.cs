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
            Pieces = (Piece[,]) pieces.Clone();
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
                            Piece[,] pieces = Pieces;
                            Node node = new Node(pieces, !IsWhiteTurn, IsWhitePlaying);
                            node.MovePiece(piece, position, movement);
                            children.Add(node);
                        }
                    }
                }
            }
            
            return children;
        }
        
        public void MovePiece(Piece piece, Vector2Int from, Vector2Int to)
        {
            // Applique le déplacement de la piece sur Pieces
            Pieces[from.x, from.y] = null;
            Pieces[to.x, to.y] = piece;
        }
    }
}