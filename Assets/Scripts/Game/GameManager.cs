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

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                SceneManager.LoadScene(0);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Node node = new Node(BoardsHandler.Instance.Pieces, IsWhiteTurn);
                Debug.Log(node.HeuristicValue());
                Debug.Log(node.Children().Count);
            }
        }
    }
}