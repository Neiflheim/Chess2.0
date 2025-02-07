using System.Collections.Generic;
using Game;
using MinMax;
using Pieces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
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
        public Piece BlackQueen;
        public Piece WhiteQueen;
        
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

            Pieces = new Piece[,]
            {
                { blackRook, blackKnight, blackBishop, BlackQueen, blackKing, blackBishop, blackKnight, blackRook },
                { blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn },
                { whiteRook, whiteKnight, whiteBishop, WhiteQueen, whiteKing, whiteBishop, whiteKnight, whiteRook }
            };
            
            // Pieces = new Piece[,]
            // {
            //     { null, null, null, null, null, null, null, blackRook },
            //     { null, null, null, blackKing, blackBishop, blackPawn, null, blackPawn },
            //     { null, WhiteQueen, null, null, null, whiteRook, null, null },
            //     { null, null, null, blackPawn, null, null, null, BlackQueen },
            //     { blackPawn, null, null, whitePawn, null, null, null, whiteBishop },
            //     { null, whitePawn, null, null, null, null, null, whiteKing },
            //     { whitePawn, null, null, null, null, null, null, whitePawn },
            //     { null, null, null, null, null, null, null, null }
            // };
            
            ZobristHashing.InitializeZobristTable();
            DisplayMatrix(true);
        }

        public void DisplayMatrix(bool changeTurn)
        {
            PiecesDisplay = new GameObject[Pieces.GetLength(0), Pieces.GetLength(1)];

            if (changeTurn)
            {
                Rules.PawnPromotion(Pieces, WhiteQueen, BlackQueen);
            }
            
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

            if (changeTurn)
            {
                Node currentBoard = new Node(Pieces, GameManager.Instance.IsWhiteTurn,GameManager.Instance.IsWhiteTurn);
                List<Node> children = currentBoard.Children();
                int count = 0;
                
                foreach (Node child in children)
                {
                    for (int i = 0; i < child.Pieces.GetLength(0); i++)
                    {
                        for (int j = 0; j < child.Pieces.GetLength(1); j++)
                        {
                            if (child.Pieces[i,j])
                            {
                                if (child.Pieces[i,j].IsWhite)
                                {
                                    if (child.Pieces[i,j].name == "WhiteKing" && Rules.IsCheckMate(child.Pieces, child.Pieces[i,j], new Vector2Int(i,j)))
                                    {
                                        count++;
                                    }
                                }
                                else
                                {
                                    if (child.Pieces[i,j].name == "BlackKing" && Rules.IsCheckMate(child.Pieces, child.Pieces[i,j], new Vector2Int(i,j)))
                                    {
                                        count++;
                                    }
                                }
                            }
                        }
                    }
                }

                if (count == children.Count)
                {
                    Rules.IsGameOver(Pieces, whiteKing, blackKing);
                }
                
                Rules.ThreefoldRepetition(Pieces);
            }
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