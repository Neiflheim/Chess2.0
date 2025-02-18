using System.Collections.Generic;
using Game;
using Handlers;
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
        
        public List<Node> NodeChildren;
        
        public Node(Piece[,] pieces, bool isWhiteTurn, bool isWhitePredictions)
        {
            Pieces = (Piece[,]) pieces.Clone();
            IsWhiteTurn = isWhiteTurn;
            IsWhitePredictions = isWhitePredictions;
        }
        
        public bool IsTerminal()
        {
            NodeChildren = Children();
            return NodeChildren.Count == 0;
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
                        int valueDependingOnPosition;
                        if (Pieces[i,j].IsWhite)
                        {
                            int [,] matrix = ValueDependOnPositionData.GetMatrix(Pieces[i, j].name);
                            valueDependingOnPosition = matrix[i,j];
                            
                            whiteHeuristicValue += Pieces[i, j].BaseValue + valueDependingOnPosition;

                            // if (Pieces[i,j].name == "WhiteKing" && Rules.IsCheckMate(Pieces, Pieces[i,j], new Vector2Int(i,j)))
                            {
                                isWhiteKingCheckMate = true;
                            }
                        }
                        else
                        {
                            int [,] matrix = ValueDependOnPositionData.GetMatrix(Pieces[i, j].name);
                            valueDependingOnPosition = matrix[i,j];
                            
                            blackHeuristicValue += Pieces[i, j].BaseValue + valueDependingOnPosition;
                            
                            // if (Pieces[i,j].name == "BlackKing" && Rules.IsCheckMate(Pieces, Pieces[i,j], new Vector2Int(i,j)))
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
                        // List<Vector2Int> availableMovements = Pieces[i,j].AvailableMovements(Pieces, new Vector2Int(i, j), true);
                        //
                        // if (availableMovements.Count == 0) continue;
                        //
                        // foreach (Vector2Int movement in availableMovements)
                        // {
                        //     Node node = new Node(Pieces, !IsWhiteTurn, IsWhitePredictions);
                        //     node.MovePiece(Pieces[i,j], new Vector2Int(i, j), movement);
                        //     Rules.PawnPromotion(node.Pieces, BoardsHandler.Instance._whiteQueen, BoardsHandler.Instance._blackQueen);
                        //     children.Add(node);
                        // }
                    }
                }
            }
            
            return children;
        }
        
        private void MovePiece(Piece piece, Vector2Int from, Vector2Int to)
        {
            // Applique le déplacement de la piece sur Pieces
            Pieces[from.x, from.y] = null;
            Pieces[to.x, to.y] = piece;
        }
    }
}