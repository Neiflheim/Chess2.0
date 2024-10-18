using Handlers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Game
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        public PieceHandler lastClickGameObject;
        public bool isWhiteTurn = true;
        public bool endGame;

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                LoadSceneByIndex(0);
            }
        }
        
        public void LoadSceneByIndex(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}