using Pieces;

namespace Helpers
{
    public static class BoardTemplate
    {
        // public static Piece[,] Base => new Piece[,]
        //     {
        //         { blackRook, blackKnight, blackBishop, blackQueen, blackKing, blackBishop, blackKnight, blackRook },
        //         { blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn },
        //         { null, null, null, null, null, null, null, null },
        //         { null, null, null, null, null, null, null, null },
        //         { null, null, null, null, null, null, null, null },
        //         { null, null, null, null, null, null, null, null },
        //         { whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn },
        //         { whiteRook, whiteKnight, whiteBishop, whiteQueen, whiteKing, whiteBishop, whiteKnight, whiteRook },
        //     };
        //     
        //     // TEST MAT EN DEUX COUPS
        //     Pieces = new Piece[,]
        //     {
        //         { null, null, null, null, null, null, null, null },
        //         { null, null, null, null, null, null, null, null },
        //         { null, null, null, null, null, null, null, null },
        //         { null, null, null, null, null, null, null, null },
        //         { null, blackBishop, null, null, null, null, null, null },
        //         { blackKnight, blackKnight, null, null, null, null, null, null },
        //         { whitePawn, whiteKing, null, blackKing, null, null, null, null },
        //         { null, null, null, null, null, null, null, null },
        //     };
            
            // Pieces = new Piece[,]
            // {
            //     { null, null, null, null, null, null, null, blackRook },
            //     { null, null, null, blackKing, blackBishop, blackPawn, null, blackPawn },
            //     { null, whiteQueen, null, null, null, whiteRook, null, null },
            //     { null, null, null, blackPawn, null, null, null, blackQueen },
            //     { blackPawn, null, null, whitePawn, null, null, null, whiteBishop },
            //     { null, whitePawn, null, null, null, null, null, whiteKing },
            //     { whitePawn, null, null, null, null, null, null, whitePawn },
            //     { null, null, null, null, null, null, null, null },
            // };
    }
}