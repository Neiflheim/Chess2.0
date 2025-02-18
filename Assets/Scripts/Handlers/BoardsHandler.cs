using System.Collections.Generic;
using Game;
using Pieces;
using UnityEngine;
using Utils;

namespace Handlers
{
    public class BoardsHandler : MonoBehaviourSingleton<BoardsHandler>
    {
        [Header("Pieces Data")]
        public Dictionary< int, Piece> PiecesDictionary = new Dictionary< int, Piece>();
        [SerializeField] private Piece _blackPawn;
        [SerializeField] private Piece _whitePawn;
        [SerializeField] private Piece _blackRook;
        [SerializeField] private Piece _whiteRook;
        [SerializeField] private Piece _blackKnight;
        [SerializeField] private Piece _whiteKnight;
        [SerializeField] private Piece _blackBishop;
        [SerializeField] private Piece _whiteBishop;
        [SerializeField] private Piece _blackQueen;
        [SerializeField] private Piece _whiteQueen;
        [SerializeField] private Piece _blackKing;
        [SerializeField] private Piece _whiteKing;
        
        [Header("References")]
        [SerializeField] private GameObject _piecePrefab;
        [SerializeField] private GameObject _transparentPrefab;
        [SerializeField] private Transform _gridParent;

        [Header("Matrix")]
        public int[,] BoardData;
        public GameObject[,] PiecesDisplay;
        
        public Piece[,] Pieces;
        
        private void Start()
        {
            Time.timeScale = 1;
            
            PiecesDictionary.Add(1, _whitePawn);
            PiecesDictionary.Add(2, _whiteKnight);
            PiecesDictionary.Add(3, _whiteBishop);
            PiecesDictionary.Add(4, _whiteRook);
            PiecesDictionary.Add(5, _whiteQueen);
            PiecesDictionary.Add(6, _whiteKing);
            PiecesDictionary.Add(7, _blackPawn);
            PiecesDictionary.Add(8, _blackKnight);
            PiecesDictionary.Add(9, _blackBishop);
            PiecesDictionary.Add(10, _blackRook);
            PiecesDictionary.Add(11, _blackQueen);
            PiecesDictionary.Add(12, _blackKing);
            

            BoardData = new int[,]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 }
            };

            // Pieces = new Piece[,]
            // {
            //     { _blackRook, _blackKnight, _blackBishop, _blackQueen, _blackKing, _blackBishop, _blackKnight, _blackRook },
            //     { _blackPawn, _blackPawn, _blackPawn, _blackPawn, _blackPawn, _blackPawn, _blackPawn, _blackPawn },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { null, null, null, null, null, null, null, null },
            //     { _whitePawn, _whitePawn, _whitePawn, _whitePawn, _whitePawn, _whitePawn, _whitePawn, _whitePawn },
            //     { _whiteRook, _whiteKnight, _whiteBishop, _whiteQueen, _whiteKing, _whiteBishop, _whiteKnight, _whiteRook }
            // };
            
            ZobristHashing.InitializeZobristTable();
            DisplayMatrix(true);
        }

        public void DisplayMatrix(bool changeTurn)
        {
            PiecesDisplay = new GameObject[Pieces.GetLength(0), Pieces.GetLength(1)];
            
            Rules.PawnPromotion(Pieces, _whiteQueen, _blackQueen);
            
            for (int i = 0; i < Pieces.GetLength(0); i++)
            {
                for (int j = 0; j < Pieces.GetLength(1); j++)
                {
                    GameObject newPiece;
                    
                    if (Pieces[i, j] != null)
                    {
                        // Instancier un prefab Image pour chaque élément
                        newPiece = Instantiate(_piecePrefab, _gridParent);
                        newPiece.GetComponent<PieceHandler>().Setup(Pieces[i, j], new Vector2Int(i, j));
                        
                        if (Pieces[i, j] == _blackKing && Rules.IsCheckMate(Pieces, Pieces[i, j], new Vector2Int(i, j)))
                        {
                            GameManager.Instance.IsBlackKingCheckMate = true;
                        }
                        if (Pieces[i, j] == _whiteKing && Rules.IsCheckMate(Pieces, Pieces[i, j], new Vector2Int(i, j)))
                        {
                            GameManager.Instance.IsWhiteKingCheckMate = true;
                        }
                    }
                    else
                    {
                        newPiece = Instantiate(_transparentPrefab, _gridParent);
                        newPiece.GetComponent<PieceHandler>().SetupTransparent(new Vector2Int(i, j));
                    }
                    
                    PiecesDisplay[i, j] = newPiece;
                }
            }

            if (changeTurn)
            {
                Rules.ThreefoldRepetition(Pieces);

                if (GameManager.Instance.IsBlackKingCheckMate)
                {
                    Rules.IsGameOver(true);
                }
                else if (GameManager.Instance.IsWhiteKingCheckMate)
                {
                    Rules.IsGameOver(false);
                }
            }
        }

        public void ResetMatrix()
        {
            foreach (Transform child in _gridParent.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}