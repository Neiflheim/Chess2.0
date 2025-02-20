using System;
using System.Collections;
using System.Collections.Generic;
using Handlers;
using MinMax;
using TMPro;
using UnityEngine;
using Utils;
using Debug = UnityEngine.Debug;

namespace Game
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [Header("End Game")]
        public GameObject EndGamePanel;
        public TextMeshProUGUI GameOverText;
        
        [Header("Selected Piece")]
        [SerializeField] private float _delayMinMax;
        [SerializeField] private int _depthFirstAi;
        [SerializeField] private int _depthSecondAi;
        
        // Internal Component
        private AIHandler _aiHandler;
        
        // For MinMax
        private Node _node = null;
        private int _index = -1;
        private List<Node> nodes = new List<Node>();
        
        private void Awake()
        {
            _aiHandler = GetComponent<AIHandler>();

            _depthFirstAi = GameSettings.FirstAIDifficulty;
            _depthSecondAi = GameSettings.SecondAIDifficulty;
        }
        
        private void Start()
        {
            ValueDependOnPositionData.InitializeDictionary();
            
            if (GameSettings.GameMode == 3)
            {
                Debug.Log("Play in Game Mode 3");
                StartCoroutine(MinMaxAlphaBetaPlay(_depthFirstAi));
            }
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                throw new NotImplementedException();
            }
        }

        // Methode premier appel
        public void MinMaxAlphaBeta(int depth)
        {
            Debug.Log("MinMaxAlphaBeta : " + depth);
            
            _node = new Node(BoardsHandler.Instance.BoardData, BoardsHandler.Instance.IsWhiteTurn, BoardsHandler.Instance.IsWhiteTurn);
            nodes = _node.Children();
            Node bestChildNode = null;
                
            string pieceHashing = TranspositionTableHandler.PiecesComputeSHA256(_node.Board);
            if (TranspositionTableHandler.TranspositionsTables.ContainsKey(pieceHashing))
            {
                bestChildNode = TranspositionTableHandler.TranspositionsTables[pieceHashing];
            }
            else
            {
                int maxHeuristic = int.MinValue;
                int alpha = int.MinValue;
                int beta = int.MaxValue;
                
                foreach (Node child in nodes)
                {
                    BoardsHandler.Instance.BoardData = child.Board;
                    int currentHeuristic = _aiHandler.MinMaxAlphaBeta(child, depth - 1, false, alpha, beta);
                
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
                BoardsHandler.Instance.BoardData = bestChildNode.Board;
            }
                
            BoardsHandler.Instance.ResetMatrix();
            BoardsHandler.Instance.DisplayMatrix(true);
            BoardsHandler.Instance.IsWhiteTurn = !BoardsHandler.Instance.IsWhiteTurn;
        }
        
        // GameMode 2
        public void StartCoroutineAiTurn()
        {
            StartCoroutine(AiTurn());
        }
        
        public IEnumerator AiTurn()
        {
            Debug.Log("coucou");
            yield return new WaitForSeconds(_delayMinMax);
            Debug.Log("recoucou");
            MinMaxAlphaBeta(_depthFirstAi);
        }
        
        // GameMode 3
        private IEnumerator MinMaxAlphaBetaPlay(int depth)
        {
            MinMaxAlphaBeta(depth);
            
            yield return new WaitForSeconds(_delayMinMax);

            if (BoardsHandler.Instance.IsWhiteTurn)
            {
                StartCoroutine(MinMaxAlphaBetaPlay(_depthFirstAi));
            }
            else
            {
                StartCoroutine(MinMaxAlphaBetaPlay(_depthSecondAi));
            }
        }
    }
}
