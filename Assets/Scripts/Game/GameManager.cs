using System.Collections.Generic;
using Handlers;
using MinMax;
using Pieces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Game
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [Header("Selected Piece")]
        public PieceHandler LastClickGameObject;

        [Header("End Game")]
        public GameObject EndGamePanel;
        public Text EndGameText;

        [Header("Sound")]
        // public GameObject AudioManager;
        
        [Header("Data")]
        public bool IsWhiteTurn = true;
        public bool IsBlackKing;
        public bool IsWhiteKing;
        
        [Header("Selected Piece")]
        [SerializeField] private float _delayMinMax;
        [SerializeField] private int _depth;
        
        private AIHandler _aiHandler;
        
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

            if (Input.GetButtonDown("Fire3"))
            {
                // Pour tester et voir les enfants
                if (_node == null)
                {
                    _node = new Node(BoardsHandler.Instance.Pieces, IsWhiteTurn, IsWhiteTurn);
                    nodes = _node.Children();
                    Debug.Log("Actual heuristic value : " + _node.HeuristicValue());
                    Debug.Log("Child number : " + _node.Children().Count);
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
                    BoardsHandler.Instance.DisplayMatrix();
                    _index = -1;
                }
                
            }

            if (Input.GetButtonDown("Fire2"))
            {
                // Pour tester le MinMax
                _node = new Node(BoardsHandler.Instance.Pieces, IsWhiteTurn, IsWhiteTurn);
                nodes = _node.Children();
                int maxHeuristic = int.MinValue;
                Node bestChildNode = null;
                
                foreach (Node child in nodes)
                {
                    BoardsHandler.Instance.Pieces = child.Pieces;
                    int childheuristic = child.HeuristicValue();
                    int currentHeuristic = _aiHandler.MinMax(child, _depth -1, false);
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
                BoardsHandler.Instance.DisplayMatrix();
                Instance.IsWhiteTurn = !Instance.IsWhiteTurn;
            }

            if (Input.GetButtonDown("Jump"))
            {
                Invoke(nameof(Play), _delayMinMax);
            }
        }

        private void Play()
        {
            _node = new Node(BoardsHandler.Instance.Pieces, IsWhiteTurn, IsWhiteTurn);
            nodes = _node.Children();
            int maxHeuristic = int.MinValue;
            Node bestChildNode = null;
                
            foreach (Node child in nodes)
            {
                BoardsHandler.Instance.Pieces = child.Pieces;
                int childheuristic = child.HeuristicValue();
                int currentHeuristic = _aiHandler.MinMax(child, _depth -1, false);
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
            BoardsHandler.Instance.DisplayMatrix();
            Instance.IsWhiteTurn = !Instance.IsWhiteTurn;
            
            Invoke(nameof(Play), _delayMinMax);
        }

        private void ChangePieces(Node node)
        {
            BoardsHandler.Instance.Pieces = node.Pieces;
            Debug.Log("Child value : " + node.HeuristicValue());
                
            BoardsHandler.Instance.ResetMatrix();
            BoardsHandler.Instance.DisplayMatrix();
        }
    }
}