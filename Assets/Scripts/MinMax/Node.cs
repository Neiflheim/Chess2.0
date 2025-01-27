using System.Collections.Generic;
using Pieces;
using UnityEngine;

namespace MinMax
{
    public class Node
    {
        public Piece[,] Pieces;
        public bool IsWhiteTurn;
        
        public Node(Piece[,] pieces, bool isWhiteTurn)
        {
            Pieces = pieces;
            IsWhiteTurn = isWhiteTurn;
        }
        
        public bool IsTerminal()
        {
            return HeuristicValue() >= 100 || HeuristicValue() <= -100;
        }
        
        public int HeuristicValue()
        {
            int boardHeuristicValue;
            int whiteHeuristicValue = 0;
            int blackHeuristicValue = 0;
            
            foreach (Piece piece in Pieces)
            {
                if (piece)
                {
                    if (piece.IsWhite)
                    {
                        whiteHeuristicValue += piece.Value;
                    }
                    else
                    {
                        blackHeuristicValue += piece.Value;
                    }
                }
            }
            
            if (IsWhiteTurn)
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
            children.Clear();
            
            // Crée une Node pour chaque mouvements disponibles de chaque piece de la couleur dont c'est le tour
            for (int i = 0; i < Pieces.GetLength(0); i++)
            {
                for (int j = 0; j < Pieces.GetLength(0); j++)
                {
                    if (Pieces[i,j] && Pieces[i,j].IsWhite == IsWhiteTurn)
                    {
                        Piece piece = Pieces[i,j];
                        Vector2Int position = new Vector2Int(i, j);
                        List<Vector2Int> availableMovements = piece.AvailableMovements(new Vector2Int(i, j));
                    
                        foreach (Vector2Int movement in availableMovements)
                        {
                            Piece[,] pieces = CreateCopy();
                            MovePiece(pieces, piece, position, movement);
                            Node node = new Node(pieces, !IsWhiteTurn); 
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
            pieces[from.x, from.y] = null;
            pieces[to.x, to.y] = piece;
            return pieces;
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