using System.Collections.Generic;

namespace Game
{
    public static class ValueDependOnPositionData
    {
        static Dictionary<int, int[,]> _valuesDepandOnPosition = new Dictionary<int, int[,]>();

        public static void InitializeDictionary()
        {
            if (_valuesDepandOnPosition.Count == 0)
            {
                _valuesDepandOnPosition.Add(1, WhitePawnMatrix);
                _valuesDepandOnPosition.Add(7, BlackPawnMatrix);
                _valuesDepandOnPosition.Add(2, WhiteKnightMatrix);
                _valuesDepandOnPosition.Add(8, BlackKnightMatrix);
                _valuesDepandOnPosition.Add(3, WhiteBishopMatrix);
                _valuesDepandOnPosition.Add(9, BlackBishopMatrix);
                _valuesDepandOnPosition.Add(4, WhiteRookMatrix);
                _valuesDepandOnPosition.Add(10, BlackRookMatrix);
                _valuesDepandOnPosition.Add(5, WhiteQueenMatrix);
                _valuesDepandOnPosition.Add(11, BlackQueenMatrix);
                _valuesDepandOnPosition.Add(6, WhiteKingMatrix);
                _valuesDepandOnPosition.Add(12, BlackKingMatrix);
            }
        }

        public static int[,] GetMatrix(int index)
        {
            return _valuesDepandOnPosition.ContainsKey(index) ? _valuesDepandOnPosition[index] : null;
        }
        
        public static int[,] WhitePawnMatrix = new int[,]
        {
            {  0,  0,  0,  0,  0,  0,  0,  0 },
            { 50, 50, 50, 50, 50, 50, 50, 50 },
            { 10, 10, 20, 30, 30, 20, 10, 10 },
            {  5,  5, 10, 25, 25, 10,  5,  5 },
            {  0,  0,  0, 20, 20,  0,  0,  0 },
            {  5, -5,-10,  0,  0,-10, -5,  5 },
            {  5, 10, 10,-20,-20, 10, 10,  5 },
            {  0,  0,  0,  0,  0,  0,  0,  0 }
        };
        
        public static int[,] BlackPawnMatrix = new int[,]
        {
            {  0,  0,  0,  0,  0,  0,  0,  0 },
            {  5, 10, 10,-20,-20, 10, 10,  5 },
            {  5, -5,-10,  0,  0,-10, -5,  5 },
            {  0,  0,  0, 20, 20,  0,  0,  0 },
            {  5,  5, 10, 25, 25, 10,  5,  5 },
            { 10, 10, 20, 30, 30, 20, 10, 10 },
            { 50, 50, 50, 50, 50, 50, 50, 50 },
            {  0,  0,  0,  0,  0,  0,  0,  0 }
        };
        
        public static int[,] WhiteKnightMatrix = new int[,]
        {
            { -50,-40,-30,-30,-30,-30,-40,-50 },
            { -50,-40,-30,-30,-30,-30,-40,-50 },
            { -40,-20,  0,  5,  5,  0,-20,-40 },
            { -30,  0, 10, 15, 15, 10,  0,-30 },
            { -30,  5, 15, 20, 20, 15,  5,-30 },
            { -30,  0, 10, 15, 15, 10,  0,-30 },
            { -40,-20,  0,  5,  5,  0,-20,-40 },
            { -50,-40,-30,-30,-30,-30,-40,-50 }
        };
        
        public static int[,] BlackKnightMatrix = new int[,]
        {
            { -50,-40,-30,-30,-30,-30,-40,-50 },
            { -40,-20,  0,  5,  5,  0,-20,-40 },
            { -30,  0, 10, 15, 15, 10,  0,-30 },
            { -30,  5, 15, 20, 20, 15,  5,-30 },
            { -30,  0, 10, 15, 15, 10,  0,-30 },
            { -40,-20,  0,  5,  5,  0,-20,-40 },
            { -50,-40,-30,-30,-30,-30,-40,-50 },
            { -50,-40,-30,-30,-30,-30,-40,-50 }
        };
        
        public static int[,] WhiteBishopMatrix = new int[,]
        {
            { -20,-10,-10,-10,-10,-10,-10,-20 },
            { -20,-10,-10,-10,-10,-10,-10,-20 },
            { -10,  0,  5, 10, 10,  5,  0,-10 },
            { -10,  5, 10, 15, 15, 10,  5,-10 },
            { -10, 10, 15, 20, 20, 15, 10,-10 },
            { -10,  5, 10, 15, 15, 10,  5,-10 },
            { -10,  0,  5, 10, 10,  5,  0,-10 },
            { -20,-10,-10,-10,-10,-10,-10,-20 }
        };
        
        public static int[,] BlackBishopMatrix = new int[,]
        {
            { -20,-10,-10,-10,-10,-10,-10,-20 },
            { -10,  0,  5, 10, 10,  5,  0,-10 },
            { -10,  5, 10, 15, 15, 10,  5,-10 },
            { -10, 10, 15, 20, 20, 15, 10,-10 },
            { -10,  5, 10, 15, 15, 10,  5,-10 },
            { -10,  0,  5, 10, 10,  5,  0,-10 },
            { -20,-10,-10,-10,-10,-10,-10,-20 },
            { -20,-10,-10,-10,-10,-10,-10,-20 }
        };
        
        public static int[,] WhiteRookMatrix = new int[,]
        {
            {  0,  0,  0,  0,  0,  0,  0,  0 },
            {  5, 10, 10, 10, 10, 10, 10,  5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            {  0,  0,  0,  5,  5,  0,  0,  0 }
        };
        
        public static int[,] BlackRookMatrix = new int[,]
        {
            {  0,  0,  0,  5,  5,  0,  0,  0 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            { -5,  0,  0,  0,  0,  0,  0, -5 },
            {  5, 10, 10, 10, 10, 10, 10,  5 },
            {  0,  0,  0,  0,  0,  0,  0,  0 }
        };
        
        public static int[,] WhiteQueenMatrix = new int[,]
        {
            { -20,-10,-10,-10,-10,-10,-10,-20 },
            { -10,  0,  5,  5,  5,  5,  0,-10 },
            { -10,  5, 10, 10, 10, 10,  5,-10 },
            { -10,  5, 10, 15, 15, 10,  5,-10 },
            { -10,  5, 10, 15, 15, 10,  5,-10 },
            { -10,  5, 10, 10, 10, 10,  5,-10 },
            { -10,  0,  5,  5,  5,  5,  0,-10 },
            { -20,-10,-10,-10,-10,-10,-10,-20 }
        };
        
        public static int[,] BlackQueenMatrix = new int[,]
        {
            { -20,-10,-10,-10,-10,-10,-10,-20 },
            { -10,  0,  5,  5,  5,  5,  0,-10 },
            { -10,  5, 10, 10, 10, 10,  5,-10 },
            { -10,  5, 10, 15, 15, 10,  5,-10 },
            { -10,  5, 10, 15, 15, 10,  5,-10 },
            { -10,  5, 10, 10, 10, 10,  5,-10 },
            { -10,  0,  5,  5,  5,  5,  0,-10 },
            { -20,-10,-10,-10,-10,-10,-10,-20 }
        };
        
        public static int[,] WhiteKingMatrix = new int[,]
        {
            { -50,-30,-30,-30,-30,-30,-30,-50 },
            { -30,-30,  0,  0,  0,  0,-30,-30 },
            { -30,-10, 20, 30, 30, 20,-10,-30 },
            { -30,-10, 30, 40, 40, 30,-10,-30 },
            { -30,-10, 30, 40, 40, 30,-10,-30 },
            { -30,-10, 20, 30, 30, 20,-10,-30 },
            { -30,-20,-10,  0,  0,-10,-20,-30 },
            { -50,-40,-30,-20,-20,-30,-40,-50 }
        };
        
        public static int[,] BlackKingMatrix = new int[,]
        {
            { -50,-40,-30,-20,-20,-30,-40,-50 },
            { -30,-20,-10,  0,  0,-10,-20,-30 },
            { -30,-10, 20, 30, 30, 20,-10,-30 },
            { -30,-10, 30, 40, 40, 30,-10,-30 },
            { -30,-10, 30, 40, 40, 30,-10,-30 },
            { -30,-10, 20, 30, 30, 20,-10,-30 },
            { -30,-30,  0,  0,  0,  0,-30,-30 },
            { -50,-30,-30,-30,-30,-30,-30,-50 }
        };
    }
}
