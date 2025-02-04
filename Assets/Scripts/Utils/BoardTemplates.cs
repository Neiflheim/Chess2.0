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
            // Pieces = new Piece[,]
            // {
            //     { null, null, whiteKing, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, whiteBishop, null, null, null, null },
            //     { null, null, whiteRook, blackKing, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null }
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
            // Pieces = new Piece[,]
            // {
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, whiteBishop, null, null, null, null, null, null },
            //     { whiteKnight, whiteKnight, null, null, null, null, null, null },
            //     { blackPawn, blackKing, null, whiteKing, null, null, null, null },
            //     { null, null, null, null, null, null, null, null }
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