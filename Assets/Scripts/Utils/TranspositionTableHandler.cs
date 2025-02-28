using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    public static class TranspositionTableHandler
    {
        public static string PiecesComputeSHA256(int[,] board)
        {
            string boardString = string.Join(",", board.Cast<int>());
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(boardString));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}