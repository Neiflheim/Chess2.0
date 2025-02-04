using System;
using System.Security.Cryptography;
using System.Text;
using Pieces;

namespace Utils
{
    public static class Helpers
    {
        public static string PiecesComputeSHA256(Piece[,] pieces)
        {
            StringBuilder piecesString = new StringBuilder();

            for (int i = 0; i < pieces.GetLength(0); i++)
            {
                for (int j = 0; j < pieces.GetLength(1); j++)
                {
                    Piece piece = pieces[i, j];
                    piecesString.Append(piece == null ? "0" : piece.GetHashCode().ToString());
                    piecesString.Append(",");
                }
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(piecesString.ToString()));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}