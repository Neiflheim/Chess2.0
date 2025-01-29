using System;
using System.Collections.Generic;
using Handlers;
using MinMax;
using Unity.VisualScripting;
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

            if (Input.GetButtonDown("Fire1"))
            {
                // Pour tester et voir les enfants
                
                // if (_node == null)
                // {
                //     _node = new Node(BoardsHandler.Instance.Pieces, IsWhiteTurn, IsWhiteTurn);
                //     nodes = _node.Children();
                //     Debug.Log("Actual heuristic value : " + _node.HeuristicValue());
                //     Debug.Log("Child number : " + _node.Children().Count);
                // }
                // _index += 1;
                // ChangePieces(nodes[_index]);
                
                
                
                // Pour tester le MinMax
                
                _node = new Node(BoardsHandler.Instance.Pieces, IsWhiteTurn, IsWhiteTurn);
                nodes = _node.Children();
                
                int value = _aiHandler.MinMax(_node, 3, true, true);
                BoardsHandler.Instance.Pieces = _aiHandler.BestChild.Pieces;
                Debug.Log("MinMax : " + value);
                
                BoardsHandler.Instance.ResetMatrix();
                BoardsHandler.Instance.DisplayMatrix();
                Instance.IsWhiteTurn = !Instance.IsWhiteTurn;
            }
        }

        private void ChangePieces(Node node)
        {
            BoardsHandler.Instance.Pieces = node.Pieces;
            Debug.Log("Child value : " + node.HeuristicValue());
                
            BoardsHandler.Instance.ResetMatrix();
            BoardsHandler.Instance.DisplayMatrix();
            // Instance.IsWhiteTurn = !Instance.IsWhiteTurn;
        }
    }
}