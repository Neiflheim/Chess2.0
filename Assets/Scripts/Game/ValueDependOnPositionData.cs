using System.Collections.Generic;

namespace Game
{
    public static class ValueDependOnPositionData
    {
        static Dictionary<string, int[,]> valuesDepandOnPosition = new Dictionary<string, int[,]>();

        public static void InitializeDictionary()
        {
            valuesDepandOnPosition.Add("WhitePawn", WhitePawnMatrix);
            valuesDepandOnPosition.Add("BlackPawn", BlackPawnMatrix);
            valuesDepandOnPosition.Add("WhiteKnight", WhiteKnightMatrix);
            valuesDepandOnPosition.Add("BlackKnight", BlackKnightMatrix);
            valuesDepandOnPosition.Add("WhiteBishop", WhiteBishopMatrix);
            valuesDepandOnPosition.Add("BlackBishop", BlackBishopMatrix);
            valuesDepandOnPosition.Add("WhiteRook", WhiteRookMatrix);
            valuesDepandOnPosition.Add("BlackRook", BlackRookMatrix);
            valuesDepandOnPosition.Add("WhiteQueen", WhiteQueenMatrix);
            valuesDepandOnPosition.Add("BlackQueen", BlackQueenMatrix);
            valuesDepandOnPosition.Add("WhiteKing", WhiteKingMatrix);
            valuesDepandOnPosition.Add("BlackKing", BlackKingMatrix);
        }

        public static int[,] GetMatrix(string name)
        {
            return valuesDepandOnPosition.ContainsKey(name) ? valuesDepandOnPosition[name] : null;
        }
        
        public static int[,] WhitePawnMatrix = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 50, 50, 50, 50, 50, 50, 50, 50 },
            { 10, 10, 20, 30, 30, 20, 10, 10 },
            { 5, 5, 10, 25, 25, 10, 5, 5 },
            { 0, 0, 0, 20, 20, 0, 0, 0 },
            { 5, -5, -10, 0, 0, -10, -5, 5 },
            { 5, 10, 10, -20, -20, 10, 10, 5 },
            { 0, 0, 0, 0, 0, 0, 0, 0 }
        };
        
        public static int[,] BlackPawnMatrix = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 5, 10, 10, -20, -20, 10, 10, 5 },
            { 5, -5, -10, 0, 0, -10, -5, 5 },
            { 0, 0, 0, 20, 20, 0, 0, 0 },
            { 5, 5, 10, 25, 25, 10, 5, 5 },
            { 10, 10, 20, 30, 30, 20, 10, 10 },
            { 50, 50, 50, 50, 50, 50, 50, 50 },
            { 0, 0, 0, 0, 0, 0, 0, 0 }
        };
        
        public static int[,] WhiteKnightMatrix = new int[,]
        {
            { -50, -40, -30, -30, -30, -30, -40, -50 },
            { -50, -40, -30, -30, -30, -30, -40, -50 },
            { -40, -20,  0,  5,  5,  0, -20, -40 },
            { -30,  0, 10, 15, 15, 10,  0, -30 },
            { -30,  5, 15, 20, 20, 15,  5, -30 },
            { -30,  0, 10, 15, 15, 10,  0, -30 },
            { -40, -20,  0,  5,  5,  0, -20, -40 },
            { -50, -40, -30, -30, -30, -30, -40, -50 }
        };
        
        public static int[,] BlackKnightMatrix = new int[,]
        {
            { -50, -40, -30, -30, -30, -30, -40, -50 },
            { -40, -20,  0,  5,  5,  0, -20, -40 },
            { -30,  0, 10, 15, 15, 10,  0, -30 },
            { -30,  5, 15, 20, 20, 15,  5, -30 },
            { -30,  0, 10, 15, 15, 10,  0, -30 },
            { -40, -20,  0,  5,  5,  0, -20, -40 },
            { -50, -40, -30, -30, -30, -30, -40, -50 },
            { -50, -40, -30, -30, -30, -30, -40, -50 }
        };
        
        public static int[,] WhiteBishopMatrix = new int[,]
        {
            { -20, -10, -10, -10, -10, -10, -10, -20 },
            { -20, -10, -10, -10, -10, -10, -10, -20 },
            { -10,  0,  5, 10, 10,  5,  0, -10 },
            { -10,  5, 10, 15, 15, 10,  5, -10 },
            { -10, 10, 15, 20, 20, 15, 10, -10 },
            { -10,  5, 10, 15, 15, 10,  5, -10 },
            { -10,  0,  5, 10, 10,  5,  0, -10 },
            { -20, -10, -10, -10, -10, -10, -10, -20 }
        };
        
        public static int[,] BlackBishopMatrix = new int[,]
        {
            { -20, -10, -10, -10, -10, -10, -10, -20 },
            { -10,  0,  5, 10, 10,  5,  0, -10 },
            { -10,  5, 10, 15, 15, 10,  5, -10 },
            { -10, 10, 15, 20, 20, 15, 10, -10 },
            { -10,  5, 10, 15, 15, 10,  5, -10 },
            { -10,  0,  5, 10, 10,  5,  0, -10 },
            { -20, -10, -10, -10, -10, -10, -10, -20 },
            { -20, -10, -10, -10, -10, -10, -10, -20 }
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
            { -20, -10, -10, -10, -10, -10, -10, -20 },
            { -10,  0,  5,  5,  5,  5,  0, -10 },
            { -10,  5, 10, 10, 10, 10,  5, -10 },
            { -10,  5, 10, 15, 15, 10,  5, -10 },
            { -10,  5, 10, 15, 15, 10,  5, -10 },
            { -10,  5, 10, 10, 10, 10,  5, -10 },
            { -10,  0,  5,  5,  5,  5,  0, -10 },
            { -20, -10, -10, -10, -10, -10, -10, -20 }
        };
        
        public static int[,] BlackQueenMatrix = new int[,]
        {
            { -20, -10, -10, -10, -10, -10, -10, -20 },
            { -10,  0,  5,  5,  5,  5,  0, -10 },
            { -10,  5, 10, 10, 10, 10,  5, -10 },
            { -10,  5, 10, 15, 15, 10,  5, -10 },
            { -10,  5, 10, 15, 15, 10,  5, -10 },
            { -10,  5, 10, 10, 10, 10,  5, -10 },
            { -10,  0,  5,  5,  5,  5,  0, -10 },
            { -20, -10, -10, -10, -10, -10, -10, -20 }
        };
        
        public static int[,] WhiteKingMatrix = new int[,]
        {
            {  20,  30,  10,  0,  0,  10,  30,  20 },
            {  20,  20,  0,  0,  0,  0,  20,  20 },
            { -10, -20, -20, -30, -30, -20, -20, -10 },
            { -20, -30, -30, -40, -40, -30, -30, -20 },
            { -30, -40, -40, -50, -50, -40, -40, -30 },
            { -30, -40, -40, -50, -50, -40, -40, -30 },
            { -30, -40, -40, -50, -50, -40, -40, -30 },
            { -30, -40, -40, -50, -50, -40, -40, -30 }
        };
        
        public static int[,] BlackKingMatrix = new int[,]
        {
            { -30, -40, -40, -50, -50, -40, -40, -30 },
            { -30, -40, -40, -50, -50, -40, -40, -30 },
            { -30, -40, -40, -50, -50, -40, -40, -30 },
            { -30, -40, -40, -50, -50, -40, -40, -30 },
            { -20, -30, -30, -40, -40, -30, -30, -20 },
            { -10, -20, -20, -30, -30, -20, -20, -10 },
            {  20,  20,  0,  0,  0,  0,  20,  20 },
            {  20,  30,  10,  0,  0,  10,  30,  20 }
        };
    }
}
