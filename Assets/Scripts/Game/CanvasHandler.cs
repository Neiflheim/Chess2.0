using UnityEngine;

namespace Game
{
    public class CanvasHandler : MonoBehaviour
    {
        public void ChangeSceneByIndex(int sceneIndex)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void TimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
        }
    }
}
