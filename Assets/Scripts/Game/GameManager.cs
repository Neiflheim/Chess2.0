using System.Collections.Generic;
using System.Diagnostics;
using Handlers;
using MinMax;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Debug = UnityEngine.Debug;

namespace Game
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [Header("Selected Piece")]
        public PieceHandler LastClickGameObject;

        [Header("End Game")]
        public GameObject EndGamePanel;
        public TextMeshProUGUI GameOverText;
        
        [Header("Data")]
        public bool IsWhiteTurn = true;
        public bool IsBlackKingCheckMate;
        public bool IsBlackKingCheck;
        public bool IsWhiteKingCheckMate;
        public bool IsWhiteKingCheck;
        
        [Header("Selected Piece")]
        [SerializeField] private float _delayMinMax;
        [SerializeField] private int _depthMinMax;
        [SerializeField] private int _depthAlphaBeta;
        
        // Internal Component
        private AIHandler _aiHandler;
        
        // For MinMax
        private Node _node = null;
        private int _index = -1;
        private List<Node> nodes = new List<Node>();

        private void Awake()
        {
            _aiHandler = GetComponent<AIHandler>();
        }

        private void Start()
        {
            ValueDependOnPositionData.InitializeDictionary();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                SceneManager.LoadScene(0);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                // Pour tester et voir les enfants
                Debug.Log("See every child.");
                if (_node == null)
                {
                    _node = new Node(BoardsHandler.Instance.Pieces, IsWhiteTurn, IsWhiteTurn);
                    nodes = _node.Children();
                    Debug.Log("Actual heuristic value : " + _node.HeuristicValue());
                    Debug.Log("Child number : " + nodes.Count);
                }
                
                _index += 1;
                if (_index <= _node.Children().Count)
                {
                    ChangePieces(nodes[_index]);
                }
                else
                {
                    BoardsHandler.Instance.Pieces = _node.Pieces;
                
                    BoardsHandler.Instance.ResetMatrix();
                    BoardsHandler.Instance.DisplayMatrix(false);
                    _index = -1;
                }
                
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                
                // TESTER MINMAX
                Debug.Log("Use Minmax.");
                
                _node = new Node(BoardsHandler.Instance.Pieces, IsWhiteTurn, IsWhiteTurn);
                nodes = _node.Children();
                
                int maxHeuristic = int.MinValue;
                Node bestChildNode = null;
                
                foreach (Node child in nodes)
                {
                    BoardsHandler.Instance.Pieces = child.Pieces;
                    int currentHeuristic = _aiHandler.MinMax(child, _depthMinMax - 1, false);
                    if (currentHeuristic > maxHeuristic)
                    {
                        maxHeuristic = currentHeuristic;
                        bestChildNode = child;
                    }
                }
                
                if (bestChildNode != null)
                {
                    BoardsHandler.Instance.Pieces = bestChildNode.Pieces;
                }
                
                BoardsHandler.Instance.ResetMatrix();
                BoardsHandler.Instance.DisplayMatrix(true);
                IsWhiteTurn = !IsWhiteTurn;
                
                stopwatch.Stop();
                Debug.Log("Execution Time : " + stopwatch.ElapsedMilliseconds + " ms / For : " + nodes.Count +" children");
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                
                // TESTER MINMAXALPHABETA
                
                _node = new Node(BoardsHandler.Instance.Pieces, IsWhiteTurn, IsWhiteTurn);
                nodes = _node.Children();
                Node bestChildNode = null;
                
                string pieceHashing = TranspositionTableHandler.PiecesComputeSHA256(_node.Pieces);
                if (TranspositionTableHandler.TranspositionsTables.ContainsKey(pieceHashing))
                {
                    Debug.Log("Use Transposition table.");
                    bestChildNode = TranspositionTableHandler.TranspositionsTables[pieceHashing];
                }
                else
                {
                    Debug.Log("Use MinMaxAlphaBeta.");
                    int maxHeuristic = int.MinValue;
                    int alpha = int.MinValue;
                    int beta = int.MaxValue;
                
                    foreach (Node child in nodes)
                    {
                        BoardsHandler.Instance.Pieces = child.Pieces;
                        int currentHeuristic = _aiHandler.MinMaxAlphaBeta(child, _depthAlphaBeta - 1, false, alpha, beta);
                
                        if (currentHeuristic > maxHeuristic)
                        {
                            maxHeuristic = currentHeuristic;
                            bestChildNode = child;
                        }
                    
                        if (currentHeuristic >= beta)
                        {
                            break;
                        }
                        alpha = Mathf.Max(alpha, currentHeuristic);
                    }
                    
                    // TT
                    TranspositionTableHandler.TranspositionsTables.Add(pieceHashing, bestChildNode);
                }
                
                if (bestChildNode != null)
                {
                    BoardsHandler.Instance.Pieces = bestChildNode.Pieces;
                }
                
                BoardsHandler.Instance.ResetMatrix();
                BoardsHandler.Instance.DisplayMatrix(true);
                IsWhiteTurn = !IsWhiteTurn;
                
                stopwatch.Stop();
                Debug.Log("Execution Time : " + stopwatch.ElapsedMilliseconds + " ms / For : " + nodes.Count +" children");
            }

            if (Input.GetButtonDown("Jump"))
            {
                Invoke(nameof(MinMaxAlphaBetaPlay), _delayMinMax);
            }
        }

        private void MinMaxAlphaBetaPlay()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            _node = new Node(BoardsHandler.Instance.Pieces, IsWhiteTurn, IsWhiteTurn);
            nodes = _node.Children();
            Node bestChildNode = null;
                
            string pieceHashing = TranspositionTableHandler.PiecesComputeSHA256(_node.Pieces);
            if (TranspositionTableHandler.TranspositionsTables.ContainsKey(pieceHashing))
            {
                Debug.Log("Use Transposition table.");
                bestChildNode = TranspositionTableHandler.TranspositionsTables[pieceHashing];
            }
            else
            {
                Debug.Log("Use MinMaxAlphaBeta.");
                int maxHeuristic = int.MinValue;
                int alpha = int.MinValue;
                int beta = int.MaxValue;
                
                foreach (Node child in nodes)
                {
                    BoardsHandler.Instance.Pieces = child.Pieces;
                    int currentHeuristic = _aiHandler.MinMaxAlphaBeta(child, _depthAlphaBeta - 1, false, alpha, beta);
                
                    if (currentHeuristic > maxHeuristic)
                    {
                        maxHeuristic = currentHeuristic;
                        bestChildNode = child;
                    }
                    
                    if (currentHeuristic >= beta)
                    {
                        break;
                    }
                    alpha = Mathf.Max(alpha, currentHeuristic);
                }
                    
                // TT
                TranspositionTableHandler.TranspositionsTables.Add(pieceHashing, bestChildNode);
            }
                
            if (bestChildNode != null)
            {
                BoardsHandler.Instance.Pieces = bestChildNode.Pieces;
            }
                
            BoardsHandler.Instance.ResetMatrix();
            BoardsHandler.Instance.DisplayMatrix(true);
            IsWhiteTurn = !IsWhiteTurn;
                
            stopwatch.Stop();
            Debug.Log("Execution Time : " + stopwatch.ElapsedMilliseconds + " ms / For : " + nodes.Count +" children");
            
            Invoke(nameof(MinMaxAlphaBetaPlay), _delayMinMax);
        }

        private void ChangePieces(Node node)
        {
            BoardsHandler.Instance.Pieces = node.Pieces;
            Debug.Log("Child value : " + node.HeuristicValue());
                
            BoardsHandler.Instance.ResetMatrix();
            BoardsHandler.Instance.DisplayMatrix(false);
        }
    }
}