using System;
using Pieces;
using UnityEngine;

namespace Utils
{
    public static class ZobristHashing
    {
        private static ulong[,] _zobristTable;
        private static System.Random _random = new System.Random();

        private const int BoardSize = 8;  // Taille de l'échiquier 8x8
        private const int NumPieceTypes = 12;  // 6 types * 2 couleurs, pièces indexées de 1 à 12

        static ZobristHashing()
        {
            InitializeZobristTable();
        }

        public static void InitializeZobristTable()
        {
            _zobristTable = new ulong[NumPieceTypes, BoardSize * BoardSize];

            // Remplir la table avec des valeurs aléatoires
            for (int pieceType = 0; pieceType < NumPieceTypes; pieceType++) 
            {
                for (int position = 0; position < BoardSize * BoardSize; position++)
                {
                    _zobristTable[pieceType, position] = GenerateRandomUlong();
                }
            }
        }

        private static ulong GenerateRandomUlong()
        {
            byte[] buffer = new byte[8];
            _random.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        public static ulong ComputeBoardHash(int[,] pieces)
        {
            ulong hash = 0;

            // Parcours de l'échiquier (8x8)
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    int pieceIndex = pieces[x, y]; // Indice de la pièce (0 = case vide, 1-12 = pièces)
                    
                    // Si la case est vide, on ne modifie pas le hachage
                    if (pieceIndex == 0) continue;

                    int positionIndex = x * BoardSize + y; // Indice de la position sur l'échiquier (0 à 63)

                    // Vérification que l'indice de la pièce est valide (entre 1 et 12)
                    if (pieceIndex < 1 || pieceIndex > 12)
                    {
                        Debug.LogError($"Index de pièce invalide : {pieceIndex} à la position ({x},{y})");
                        continue;
                    }

                    // Vérification que l'indice de position est valide (entre 0 et 63)
                    if (positionIndex < 0 || positionIndex >= BoardSize * BoardSize)
                    {
                        Debug.LogError($"Index de position invalide : {positionIndex} pour la position ({x},{y})");
                        continue;
                    }

                    // Calcul du hachage en utilisant la table de Zobrist
                    hash ^= _zobristTable[pieceIndex - 1, positionIndex]; // Accéder à l'indice correct en utilisant `pieceIndex - 1`
                }
            }

            return hash;
        }

        private static int GetPieceIndex(Piece piece)
        {
            // Retourne l'indice de la pièce en fonction de son type et de sa couleur
            int typeIndex = (int)piece.Id; // Enumération des types de pièces (Roi, Reine, etc.)
            return piece.IsWhite ? typeIndex + 1 : typeIndex + 7; // 1-6 pour blanc, 7-12 pour noir
        }
    }
}
