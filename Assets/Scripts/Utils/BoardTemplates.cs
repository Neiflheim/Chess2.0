namespace Utils
{
    public class BoardTemplates
    {
        // TEST MINMAX/MINMAXALPHABETA
            // Pieces = new Piece[,]
            // {
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, blackKing, null, null, null, null },
            //     { null, null, whiteKnight, null, null, null, null, null },
            //     { null, whiteBishop, null, null, null, null, null, null },
            //     { whitePawn, whitePawn, null, null, null, null, null, null },
            //     { null, null, null, whiteKing, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            // };
            
            // TEST BASIQUE CHOIX DEPTH 2
            // BoardData = new int[,]
            // {
            //     {  0, 0, 6, 0, 0, 0, 0, 0 },
            //     {  0, 0, 0, 0, 0, 0, 0, 0 },
            //     {  0, 0, 0, 3, 0, 0, 0, 0 },
            //     {  0, 0, 4,12, 0, 0, 0, 0 },
            //     {  0, 0, 0, 0, 0, 0, 0, 0 },
            //     {  0, 0, 0, 0, 0, 0, 0, 0 },
            //     {  0, 0, 0, 0, 0, 0, 0, 0 },
            //     {  0, 0, 0, 0, 0, 0, 0, 0 }
            // };
            
            // TEST ISCHECK/ISCHECKMATE
            // Pieces = new Piece[,]
            // {
            //     { null, null, whiteKing, null, null, whiteRook, blackRook, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, whiteRook, null, blackKing, null, null, null, whiteRook },
            //     { null, whiteRook, null, null, null, null, whiteBishop, null },
            //     { null, null, null, null, null, whiteBishop, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null }
            // };
            
            // TEST MAT EN DEUX COUPS
            // BoardData = new int[,]
            // {
            //     {  0, 0, 0, 0, 0, 0, 0, 0 },
            //     {  0, 0, 0, 0, 0, 0, 0, 0 },
            //     {  0, 0, 0, 0, 0, 0, 0, 0 },
            //     {  0, 0, 0, 0, 0, 0, 0, 0 },
            //     {  0, 3, 0, 0, 0, 0, 0, 0 },
            //     {  2, 2, 0, 0, 0, 0, 0, 0 },
            //     {  7,12, 0, 6, 0, 0, 0, 0 },
            //     {  0, 0, 0, 0, 0, 0, 0, 0 }
            // };
            
            // BoardData = new int[,]
            // {
            //     {  0, 0, 0, 0, 0, 0, 0,10 },
            //     {  0, 0, 0,12, 9, 7, 0, 7 },
            //     {  0, 5, 0, 0, 0, 4, 0, 0 },
            //     {  0, 0, 0, 7, 0, 0, 0,11 },
            //     {  7, 0, 0, 1, 0, 0, 0, 3 },
            //     {  0, 1, 0, 0, 0, 0, 0, 6 },
            //     {  1, 0, 0, 0, 0, 0, 0, 1 },
            //     {  0, 0, 0, 0, 0, 0, 0, 0 }
            // };
            
            // Pieces = new Piece[,]
            // {
            //     { null, null, null, null, null, null, null, blackRook },
            //     { null, null, null, blackKing, blackBishop, blackPawn, null, blackPawn },
            //     { null, whiteQueen, null, null, null, whiteRook, null, null },
            //     { null, null, null, blackPawn, null, null, null, blackQueen },
            //     { blackPawn, null, null, whitePawn, null, null, null, whiteBishop },
            //     { null, whitePawn, null, null, null, null, null, whiteKing },
            //     { whitePawn, null, null, null, null, null, null, whitePawn },
            //     { null, null, null, null, null, null, null, null }
            // };
    }
}