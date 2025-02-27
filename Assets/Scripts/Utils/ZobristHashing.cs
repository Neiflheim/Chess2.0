using System;
using Pieces;
using UnityEngine;

namespace Utils
{
    public static class ZobristHashing
    {
        private static ulong[,] _zobristTable;
        private static System.Random _random = new System.Random();

        private const int BoardSize = 8;  // Échiquier 8x8
        private const int NumPieceTypes = 12;  // 6 types * 2 couleurs

        static ZobristHashing()
        {
            InitializeZobristTable();
        }

        public static void InitializeZobristTable()
        {
            _zobristTable = new ulong[NumPieceTypes, BoardSize * BoardSize];

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

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    int pieceIndex = pieces[x, y];
                    if (pieceIndex == 0) continue;

                    // int pieceIndex = GetPieceIndex(piece); // Doit être entre 0 et 11
                    int positionIndex = x * 8 + y; // Doit être entre 0 et 63

                    // Vérification des indices
                    if (pieceIndex < 0 || pieceIndex >= 12 || positionIndex < 0 || positionIndex >= 64)
                    {
                        Debug.LogError($"Index hors limites - pieceIndex: {pieceIndex}, positionIndex: {positionIndex}");
                        continue;
                    }

                    hash ^= _zobristTable[pieceIndex, positionIndex]; // XOR avec la valeur unique de la pièce
                }
            }

            return hash;
        }

        private static int GetPieceIndex(Piece piece)
        {
            // Assigne un index unique basé sur le type de pièce et sa couleur
            int typeIndex = (int)piece.Id; // Enum PieceType (Roi, Reine, etc.)
            return piece.IsWhite ? typeIndex : typeIndex + 6; // 0-5 pour blanc, 6-11 pour noir
        }
    }
}