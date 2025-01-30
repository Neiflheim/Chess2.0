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
            return HeuristicValue() >= 15000 || HeuristicValue() <= -15000;
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
                            int [,] matrix = ValueDependOnPositionData.GetMatrix(Pieces[i, j].name);
                            valueDependingOnPosition = matrix[i,j];
                            
                            whiteHeuristicValue += Pieces[i, j].BaseValue + valueDependingOnPosition;
                            
                            // whiteHeuristicValue += Pieces[i, j].BaseValue;
                        }
                        else
                        {
                            int valueDependingOnPosition = 0;
                            int [,] matrix = ValueDependOnPositionData.GetMatrix(Pieces[i, j].name);
                            valueDependingOnPosition = matrix[i,j];
                            
                            blackHeuristicValue += Pieces[i, j].BaseValue + valueDependingOnPosition;
                            
                            // blackHeuristicValue += Pieces[i, j].BaseValue;
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
                        // Debug.Log(piece.name + " : " + piece.AvailableMovements(position).Count);
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