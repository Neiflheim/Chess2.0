using System.Collections.Generic;
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
        
        [Header("Selected Piece")]
        public PieceHandler LastClickGameObject;
        
        [Header("Data")]
        public bool IsWhiteTurn = true;
        public bool IsBlackKingCheckMate;
        public bool IsWhiteKingCheckMate;

        public int BoardLength;
        
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
            

            BoardData = new int[,]
            {
                { 10, 8, 9,11,12, 9, 8,10 },
                {  7, 7, 7, 7, 7, 7, 7, 7 },
                {  0, 0, 0, 0, 0, 0, 0, 0 },
                {  0, 0, 0, 0, 0, 0, 0, 0 },
                {  0, 0, 0, 0, 0, 0, 0, 0 },
                {  0, 0, 0, 0, 0, 0, 0, 0 },
                {  1, 1, 1, 1, 1, 1, 1, 1 },
                {  4, 2, 3, 5, 6, 3, 2, 4 }
            };
            
            BoardLength = BoardData.GetLength(0);
            
            DisplayMatrix(true);
        }

        public void DisplayMatrix(bool changeTurn)
        {
            PiecesDisplay = new GameObject[BoardLength, BoardLength];
            
            Rules.PawnPromotion(BoardData);
            
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
        
        public bool AreDifferentColors(int pieceOne, int pieceTwo, bool emptyVerification)
        {
            if (emptyVerification)
            {
                if (pieceTwo == 0)
                {
                    return true;
                }
            }
            
            return (pieceOne <= 6 && pieceTwo > 6) || (pieceOne > 6 && pieceTwo <= 6 && pieceTwo >= 1);
        }
        
        public bool PieceIsWhite(int pieceIndex)
        {
            return pieceIndex <= 6 && pieceIndex != 0;
        }
    }
}