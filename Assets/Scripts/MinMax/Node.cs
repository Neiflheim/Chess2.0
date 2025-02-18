using System.Collections.Generic;
using Game;
using Handlers;
using UnityEngine;
using Utils;

namespace MinMax
{
    public class Node
    {
        public int[,] Board;
        public bool IsWhiteTurn;
        public bool IsWhitePredictions;
        
        public List<Node> NodeChildren;
        
        public Node(int[,] board, bool isWhiteTurn, bool isWhitePredictions)
        {
            Board = (int[,]) board.Clone();
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
            
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i,j] != 0)
                    {
                        int valueDependingOnPosition;
                        if (Board[i,j] <= 6)
                        {
                            int [,] matrix = ValueDependOnPositionData.GetMatrix(Board[i, j]);
                            valueDependingOnPosition = matrix[i,j];
                            
                            whiteHeuristicValue += BoardsHandler.Instance.PiecesDictionary[Board[i, j]].BaseValue + valueDependingOnPosition;

                            if (Board[i,j] == 6 && Rules.IsCheckMate(Board, Board[i,j], new Vector2Int(i,j)))
                            {
                                isWhiteKingCheckMate = true;
                            }
                        }
                        else
                        {
                            int [,] matrix = ValueDependOnPositionData.GetMatrix(Board[i, j]);
                            valueDependingOnPosition = matrix[i,j];
                            
                            blackHeuristicValue += BoardsHandler.Instance.PiecesDictionary[Board[i, j]].BaseValue + valueDependingOnPosition;
                            
                            if (Board[i,j] == 12 && Rules.IsCheckMate(Board, Board[i,j], new Vector2Int(i,j)))
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
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i,j] != 0 && BoardsHandler.Instance.PieceIsWhite(Board[i,j]) == IsWhiteTurn)
                    {
                        List<Vector2Int> availableMovements = BoardsHandler.Instance.PiecesDictionary[Board[i,j]].AvailableMovements(Board, new Vector2Int(i, j), true);
                        
                        if (availableMovements.Count == 0) continue;
                        
                        foreach (Vector2Int movement in availableMovements)
                        {
                            Node node = new Node(Board, !IsWhiteTurn, IsWhitePredictions);
                            node.MovePiece(Board[i,j], new Vector2Int(i, j), movement);
                            Rules.PawnPromotion(node.Board);
                            children.Add(node);
                        }
                    }
                }
            }
            
            return children;
        }
        
        private void MovePiece(int pieceIndex, Vector2Int from, Vector2Int to)
        {
            // Applique le déplacement de la piece sur pieceIndex
            Board[from.x, from.y] = 0;
            Board[to.x, to.y] = pieceIndex;
        }
    }
}