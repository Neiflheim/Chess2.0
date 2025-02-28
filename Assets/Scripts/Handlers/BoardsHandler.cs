using System.Collections.Generic;
using Pieces;
using UnityEngine;
using Utils;

namespace Handlers
{
    public class BoardsHandler : MonoBehaviourSingleton<BoardsHandler>
    {
        [Header("Board")]
        [SerializeField] private Boards _board;
        
        [Header("Pieces Data")]
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
        public int[,] LastBoardData;
        public int[,] BoardData;
        public GameObject[,] PiecesDisplay;
        
        [Header("Selected Piece")]
        public PieceHandler LastClickGameObject;
        
        [Header("Data")]
        public bool IsWhiteTurn = true;
        public bool IsBlackKingCheckMate;
        public bool IsWhiteKingCheckMate;

        public int BoardLength;
        public Dictionary< int, Piece> PiecesDictionary = new Dictionary< int, Piece>();
        
        private void Awake()
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

            BoardData = BoardTemplates.GetBoard(_board);
            BoardLength = BoardData.GetLength(0);
            LastBoardData = (int[,]) BoardData.Clone();
            
            DisplayMatrix(true);
        }

        public void DisplayMatrix(bool changeTurn)
        {
            PiecesDisplay = new GameObject[BoardLength, BoardLength];
            
            Rules.PawnPromotion(BoardData);
            Rules.Castling(BoardData, LastBoardData);
            
            for (int i = 0; i < BoardLength; i++)
            {
                for (int j = 0; j < BoardLength; j++)
                {
                    GameObject newPiece;
                    
                    if (BoardData[i, j] != 0)
                    {
                        // Instancier un prefab Image pour chaque élément
                        newPiece = Instantiate(_piecePrefab, _gridParent);
                        newPiece.GetComponent<PieceHandler>().Setup(PiecesDictionary[BoardData[i, j]], new Vector2Int(i, j));
                        
                        if (BoardData[i, j] == 12 && Rules.IsCheckMate(BoardData, BoardData[i, j], new Vector2Int(i, j)))
                        {
                            IsBlackKingCheckMate = true;
                        }
                        if (BoardData[i, j] == 6 && Rules.IsCheckMate(BoardData, BoardData[i, j], new Vector2Int(i, j)))
                        {
                            IsWhiteKingCheckMate = true;
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
                Rules.ThreefoldRepetition(BoardData);
                Rules.CheckNotMoved(BoardData);
            
                if (IsBlackKingCheckMate)
                {
                    Rules.IsGameOver(true);
                }
                else if (IsWhiteKingCheckMate)
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
    
    public enum Boards
    {
        BaseBoard,
        BasicChoice,
        CheckMateInTwoMoves1,
        CheckMateInTwoMoves2,
        TestCastling,
        WeirdChoice
    }
}