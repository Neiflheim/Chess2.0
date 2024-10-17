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

        public Piece[,] Pieces;
        public GameObject[,] PiecesDisplay;

        private void Start()
        {
            Pieces = new Piece[,]
            {
                { blackRook, blackKnight, blackBishop, blackQueen, blackKing, blackBishop, blackKnight, blackRook },
                { blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn, blackPawn },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null },
                { whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn, whitePawn },
                { whiteRook, whiteKnight, whiteBishop, whiteQueen, whiteKing, whiteBishop, whiteKnight, whiteRook },
            };
            
            DisplayMatrix();
            WhoseTurnIsIt();
        }

        public void DisplayMatrix()
        {
            PiecesDisplay = new GameObject[Pieces.GetLength(0), Pieces.GetLength(1)];
            
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
                    }
                    else
                    {
                        newPiece = Instantiate(transparentPrefab, gridParent);
                        newPiece.GetComponent<PieceHandler>().SetupTransparent(new Vector2Int(i, j));
                    }
                    
                    PiecesDisplay[i, j] = newPiece;
                }
            }
        }

        public void ResetMatrix()
        {
            foreach (Transform child in gridParent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void WhoseTurnIsIt()
        {
            Debug.Log(" Whose Turn is it witch?");
            
            foreach (GameObject piece in PiecesDisplay)
            {
                Debug.Log(" coucou ");
                if (piece != CompareTag("Player"))
                {
                    if (GameManager.Instance.isWhiteTurn)
                    {
                        if (piece.GetComponent<PieceHandler>().Piece.isWhite == false)
                        {
                            piece.GetComponent<PieceHandler>().enabled = false;
                        }
                        else
                        {
                            piece.GetComponent<PieceHandler>().enabled = true;
                        }
                    }
                    else
                    {
                        if (piece.GetComponent<PieceHandler>().Piece.isWhite)
                        {
                            piece.GetComponent<PieceHandler>().enabled = false;
                        }
                        else
                        {
                            piece.GetComponent<PieceHandler>().enabled = true;
                        }
                    }
                }
            }
            
            Debug.Log(" hola ");
            GameManager.Instance.isWhiteTurn = !GameManager.Instance.isWhiteTurn;
        }
    }
}