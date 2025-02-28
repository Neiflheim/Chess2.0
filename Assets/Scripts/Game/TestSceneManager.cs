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
    public class TestSceneManager : MonoBehaviourSingleton<TestSceneManager>
    {
        [Header("End Game")]
        public GameObject EndGamePanel;
        public TextMeshProUGUI GameOverText;
        
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
                if (_node == null)
                {
                    _node = new Node(BoardsHandler.Instance.BoardData, BoardsHandler.Instance.IsWhiteTurn, BoardsHandler.Instance.IsWhiteTurn);
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
                    BoardsHandler.Instance.BoardData = _node.Board;
                
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
                
                _node = new Node(BoardsHandler.Instance.BoardData, BoardsHandler.Instance.IsWhiteTurn, BoardsHandler.Instance.IsWhiteTurn);
                nodes = _node.Children();
                
                int maxHeuristic = int.MinValue;
                Node bestChildNode = null;
                
                foreach (Node child in nodes)
                {
                    // BoardsHandler.Instance.Pieces = child.Pieces;
                    int currentHeuristic = _aiHandler.MinMax(child, _depthMinMax - 1, false);
                    if (currentHeuristic > maxHeuristic)
                    {
                        maxHeuristic = currentHeuristic;
                        bestChildNode = child;
                    }
                }
                
                if (bestChildNode != null)
                {
                    BoardsHandler.Instance.BoardData = bestChildNode.Board;
                }
                
                BoardsHandler.Instance.ResetMatrix();
                BoardsHandler.Instance.DisplayMatrix(true);
                BoardsHandler.Instance.IsWhiteTurn = !BoardsHandler.Instance.IsWhiteTurn;
                
                stopwatch.Stop();
                Debug.Log("Execution Time : " + stopwatch.ElapsedMilliseconds + " ms / For : " + nodes.Count +" children");
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                // TESTER MINMAXALPHABETA
                
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                
                _node = new Node(BoardsHandler.Instance.BoardData, BoardsHandler.Instance.IsWhiteTurn, BoardsHandler.Instance.IsWhiteTurn);
                nodes = _node.Children();
                Node bestChildNode = null;
                
                int maxHeuristic = int.MinValue;
                int alpha = int.MinValue;
                int beta = int.MaxValue;
                
                foreach (Node child in nodes)
                {
                    BoardsHandler.Instance.BoardData = child.Board;
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
                
                if (bestChildNode != null)
                {
                    BoardsHandler.Instance.BoardData = bestChildNode.Board;
                }
                
                BoardsHandler.Instance.ResetMatrix();
                BoardsHandler.Instance.DisplayMatrix(true);
                BoardsHandler.Instance.IsWhiteTurn = !BoardsHandler.Instance.IsWhiteTurn;
                
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
            // TESTER MINMAXALPHABETA
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
                
            _node = new Node(BoardsHandler.Instance.BoardData, BoardsHandler.Instance.IsWhiteTurn, BoardsHandler.Instance.IsWhiteTurn);
            nodes = _node.Children();
            Node bestChildNode = null;
            
            int maxHeuristic = int.MinValue;
            int alpha = int.MinValue;
            int beta = int.MaxValue;
                
            foreach (Node child in nodes)
            {
                BoardsHandler.Instance.BoardData = child.Board;
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
                
            if (bestChildNode != null)
            {
                BoardsHandler.Instance.BoardData = bestChildNode.Board;
            }
                
            BoardsHandler.Instance.ResetMatrix();
            BoardsHandler.Instance.DisplayMatrix(true);
            BoardsHandler.Instance.IsWhiteTurn = !BoardsHandler.Instance.IsWhiteTurn;
                
            stopwatch.Stop();
            Debug.Log("Execution Time : " + stopwatch.ElapsedMilliseconds + " ms / For : " + nodes.Count +" children");
            
            Invoke(nameof(MinMaxAlphaBetaPlay), _delayMinMax);
        }

        private void ChangePieces(Node node)
        {
            BoardsHandler.Instance.BoardData = node.Board;
            Debug.Log("Child value : " + node.HeuristicValue());
                
            BoardsHandler.Instance.ResetMatrix();
            BoardsHandler.Instance.DisplayMatrix(false);
        }
    }
}