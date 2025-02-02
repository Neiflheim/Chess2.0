using Game;
using Pieces;
using UnityEngine;
using Utils;

namespace Handlers
{
    public class BoardsHandler : MonoBehaviourSingleton<BoardsHandler>
    {
        [Header("Pieces Data")]
        [SerializeField] private Piece blackPawn;
        [SerializeField] private Piece whitePawn;
        [SerializeField] private Piece blackRook;
        [SerializeField] private Piece whiteRook;
        [SerializeField] private Piece blackKnight;
        [SerializeField] private Piece whiteKnight;
        [SerializeField] private Piece blackBishop;
        [SerializeField] private Piece whiteBishop;
        [SerializeField] private Piece blackKing;
        [SerializeField] private Piece whiteKing;
        [SerializeField] private Piece blackQueen;
        [SerializeField] private Piece whiteQueen;
        
        [Header("References")]
        [SerializeField] private GameObject piecePrefab;
        [SerializeField] private GameObject transparentPrefab;
        [SerializeField] private Transform gridParent;

        [Header("Matrix")]
        public Piece[,] Pieces;
        public GameObject[,] PiecesDisplay;

        private void Start()
        {
            Time.timeScale = 1;

            // BASEBOARD
            // Pieces = new Piece[,]
            // {
            //     { blackRook, blackKnight, blackBishop, blackQueen, blackKing, blackBishop, blackKnight, blackRook },
            //     { blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn },
            //     { whiteRook, whiteKnight, whiteBishop, whiteQueen, whiteKing, whiteBishop, whiteKnight, whiteRook }
            // };
            
            // TEST MINMAX/MINMAXALPHABETA
            Pieces = new Piece[,]
            {
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, blackKing, null, null, null, null },
                { null, null, whiteKnight, null, null, null, null, null },
                { null, whiteBishop, null, null, null, null, null, null },
                { whitePawn, whitePawn, null, null, null, null, null, null },
                { null, null, null, whiteKing, null, null, null, null },
                { null, null, null, null, null, null, null, null },
            };
            
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
            
            DisplayMatrix();
        }

        public void DisplayMatrix()
        {
            PiecesDisplay = new GameObject[Pieces.GetLength(0), Pieces.GetLength(1)];
            
            Rules.PawnPromotion(Pieces, whiteQueen, blackQueen);
            
            for (int i = 0; i < Pieces.GetLength(0); i++)
            {
                for (int j = 0; j < Pieces.GetLength(1); j++)
                {
                    GameObject newPiece;
                    
                    if (Pieces[i, j] != null)
                    {
                        // Instancier un prefab Image pour chaque élément
                        newPiece = Instantiate(piecePrefab, gridParent);
                        newPiece.GetComponent<PieceHandler>().Setup(Pieces[i, j], new Vector2Int(i, j));
                        
                        if (Pieces[i, j] == blackKing)
                        {
                            if (Rules.IsCheck(Pieces, Pieces[i, j], new Vector2Int(i, j)))
                            {
                                GameManager.Instance.IsBlackKingCheck = true;
                            }
                        }
                        if (Pieces[i, j] == whiteKing)
                        {
                            if (Rules.IsCheck(Pieces, Pieces[i, j], new Vector2Int(i, j)))
                            {
                                GameManager.Instance.IsWhiteKingCheck = true;
                            }
                        }
                    }
                    else
                    {
                        newPiece = Instantiate(transparentPrefab, gridParent);
                        newPiece.GetComponent<PieceHandler>().SetupTransparent(new Vector2Int(i, j));
                    }
                    
                    PiecesDisplay[i, j] = newPiece;
                }
            }
            
            Rules.IsGameOver(Pieces, whiteKing, blackKing);
        }

        public void ResetMatrix()
        {
            foreach (Transform child in gridParent.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}