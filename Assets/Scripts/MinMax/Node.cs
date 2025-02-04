using System.Collections.Generic;
using Game;
using Pieces;
using UnityEngine;
using Utils;

namespace MinMax
{
    public class Node
    {
        public Piece[,] Pieces;
        public bool IsWhiteTurn;
        public bool IsWhitePredictions;
        
        public Node(Piece[,] pieces, bool isWhiteTurn, bool isWhitePredictions)
        {
            Pieces = (Piece[,]) pieces.Clone();
            IsWhiteTurn = isWhiteTurn;
            IsWhitePredictions = isWhitePredictions;
        }
        
        public bool IsTerminal()
        {
            bool isTerminal = Children().Count == 0;
            
            return isTerminal;
        }
        
        public int HeuristicValue()
        {
            int boardHeuristicValue = 0;
            int whiteHeuristicValue = 0;
            int blackHeuristicValue = 0;
            int checkMateValue = 0;

            bool isWhiteKingCheckMate = false;
            bool isBlackKingCheckMate = false;
            
            for (int i = 0; i < Pieces.GetLength(0); i++)
            {
                for (int j = 0; j < Pieces.GetLength(1); j++)
                {
                    if (Pieces[i,j])
                    {
                        if (Pieces[i,j].IsWhite)
                        {
                            int valueDependingOnPosition = 0;
                            int [,] matrix = ValueDependOnPositionData.GetMatrix(Pieces[i, j].name);
                            valueDependingOnPosition = matrix[i,j];
                            
                            whiteHeuristicValue += Pieces[i, j].BaseValue + valueDependingOnPosition;

                            if (Pieces[i,j].name == "WhiteKing" && Rules.IsCheckMate(Pieces, Pieces[i,j], new Vector2Int(i,j)))
                            {
                                isWhiteKingCheckMate = true;
                            }
                        }
                        else
                        {
                            int valueDependingOnPosition = 0;
                            int [,] matrix = ValueDependOnPositionData.GetMatrix(Pieces[i, j].name);
                            valueDependingOnPosition = matrix[i,j];
                            
                            blackHeuristicValue += Pieces[i, j].BaseValue + valueDependingOnPosition;
                            
                            if (Pieces[i,j].name == "BlackKing" && Rules.IsCheckMate(Pieces, Pieces[i,j], new Vector2Int(i,j)))
                            {
                                isBlackKingCheckMate = true;
                            }
                        }
                    }
                }
            }
            
            if (IsWhitePredictions)
            {
                if (isWhiteKingCheckMate)
                {
                    checkMateValue = -100000;
                }

                if (isBlackKingCheckMate)
                {
                    checkMateValue = 100000;
                }
                
                boardHeuristicValue = whiteHeuristicValue + checkMateValue - blackHeuristicValue;
            }
            else
            {
                if (isWhiteKingCheckMate)
                {
                    checkMateValue = 100000;
                }

                if (isBlackKingCheckMate)
                {
                    checkMateValue = -100000;
                }
                
                boardHeuristicValue = blackHeuristicValue + checkMateValue - whiteHeuristicValue;
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
                        List<Vector2Int> availableMovements = piece.AvailableMovements(Pieces, position, true);
                        
                        if (availableMovements.Count == 0) continue;
                    
                        foreach (Vector2Int movement in availableMovements)
                        {
                            Piece[,] pieces = Pieces;
                            Node node = new Node(pieces, !IsWhiteTurn, IsWhitePredictions);
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