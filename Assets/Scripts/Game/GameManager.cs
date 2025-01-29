using System.Collections.Generic;
using Handlers;
using MinMax;
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

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                SceneManager.LoadScene(0);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if (_node == null)
                {
                    _node = new Node(BoardsHandler.Instance.Pieces, IsWhiteTurn, IsWhiteTurn);
                    nodes = _node.Children();
                    Debug.Log("Actual heuristic value : " + _node.HeuristicValue());
                    Debug.Log("Child number : " + _node.Children().Count);
                }
                
                
                _index += 1;
                // Debug.Log(_index);
                ChangePieces(nodes[_index]);

                // int value = _aiHandler.MinMax(_node, 1, true);
                // BoardsHandler.Instance.Pieces = _aiHandler.bestChild.Pieces;
                // Debug.Log("MinMax : " + value);
                //
                // BoardsHandler.Instance.ResetMatrix();
                // BoardsHandler.Instance.DisplayMatrix();
                // Instance.IsWhiteTurn = !Instance.IsWhiteTurn;
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