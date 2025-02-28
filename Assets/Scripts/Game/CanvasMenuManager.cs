using TMPro;
using UnityEngine;

namespace Game
{
    public class CanvasMenuManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Dropdown _dropdownTimerPlayerVsPlayer;
        [SerializeField] private TMP_Dropdown _dropdownTimerPlayerVsAi;
        [SerializeField] private TMP_Dropdown _dropdownDifficultyPlayerVsAi;
        [SerializeField] private TMP_Dropdown _dropdownDifficultyOneAiVsAi;
        [SerializeField] private TMP_Dropdown _dropdownDifficultyTwoAiVsAi;

        private void Start()
        {
            // Add Listener
            _dropdownTimerPlayerVsPlayer.onValueChanged.AddListener(OnTimerValueChanged);
            _dropdownTimerPlayerVsAi.onValueChanged.AddListener(OnTimerValueChanged);
            _dropdownDifficultyPlayerVsAi.onValueChanged.AddListener(OnDifficultyOneValueChanged);
            _dropdownDifficultyOneAiVsAi.onValueChanged.AddListener(OnDifficultyOneValueChanged);
            _dropdownDifficultyTwoAiVsAi.onValueChanged.AddListener(OnDifficultyTwoValueChanged);
            
            // Base Settings Timer
            if (_dropdownTimerPlayerVsPlayer.value == 0)
            {
                GameSettings.GameTimer = 10f;
                Debug.Log($"Timer: {GameSettings.GameTimer}");
            }
            if (_dropdownTimerPlayerVsPlayer.value == 1)
            {
                GameSettings.GameTimer = 15f;
                Debug.Log($"Timer: {GameSettings.GameTimer}");
            }
            if (_dropdownTimerPlayerVsPlayer.value == 2)
            {
                GameSettings.GameTimer = 20f;
                Debug.Log($"Timer: {GameSettings.GameTimer}");
            }
            // Base Settings AI 1
            if (_dropdownDifficultyPlayerVsAi.value == 0)
            {
                GameSettings.FirstAIDifficulty = 2;
                Debug.Log("FirstAIDifficulty: " + GameSettings.FirstAIDifficulty);
            }
            if (_dropdownDifficultyPlayerVsAi.value == 1)
            {
                GameSettings.FirstAIDifficulty = 3;
                Debug.Log("FirstAIDifficulty: " + GameSettings.FirstAIDifficulty);
            }
            if (_dropdownDifficultyPlayerVsAi.value == 2)
            {
                GameSettings.FirstAIDifficulty = 4;
                Debug.Log("FirstAIDifficulty: " + GameSettings.FirstAIDifficulty);
            }
            // Base Settings Ai 2
            if (_dropdownDifficultyTwoAiVsAi.value == 0)
            {
                GameSettings.SecondAIDifficulty = 2;
                Debug.Log("SecondAIDifficulty: " + GameSettings.SecondAIDifficulty);
            }
            if (_dropdownDifficultyTwoAiVsAi.value == 1)
            {
                GameSettings.SecondAIDifficulty = 3;
                Debug.Log("SecondAIDifficulty: " + GameSettings.SecondAIDifficulty);
            }
            if (_dropdownDifficultyTwoAiVsAi.value == 2)
            {
                GameSettings.SecondAIDifficulty = 4;
                Debug.Log("SecondAIDifficulty: " + GameSettings.SecondAIDifficulty);
            }
        }

        private void OnTimerValueChanged(int index)
        {
            if (index == 0)
            {
                GameSettings.GameTimer = 10f;
                Debug.Log($"Timer: {GameSettings.GameTimer}");
            }
            if (index == 1)
            {
                GameSettings.GameTimer = 15f;
                Debug.Log($"Timer: {GameSettings.GameTimer}");
            }
            if (index == 2)
            {
                GameSettings.GameTimer = 20f;
                Debug.Log($"Timer: {GameSettings.GameTimer}");
            }
        }
        private void OnDifficultyOneValueChanged(int index)
        {
            if (index == 0)
            {
                GameSettings.FirstAIDifficulty = 2;
                Debug.Log("FirstAIDifficulty: " + GameSettings.FirstAIDifficulty);
            }
            if (index == 1)
            {
                GameSettings.FirstAIDifficulty = 3;
                Debug.Log("FirstAIDifficulty: " + GameSettings.FirstAIDifficulty);
            }
            if (index == 2)
            {
                GameSettings.FirstAIDifficulty = 4;
                Debug.Log("FirstAIDifficulty: " + GameSettings.FirstAIDifficulty);
            }
        }
        private void OnDifficultyTwoValueChanged(int index)
        {
            if (index == 0)
            {
                GameSettings.SecondAIDifficulty = 2;
                Debug.Log("SecondAIDifficulty: " + GameSettings.SecondAIDifficulty);
            }
            if (index == 1)
            {
                GameSettings.SecondAIDifficulty = 3;
                Debug.Log("SecondAIDifficulty: " + GameSettings.SecondAIDifficulty);
            }
            if (index == 2)
            {
                GameSettings.SecondAIDifficulty = 4;
                Debug.Log("SecondAIDifficulty: " + GameSettings.SecondAIDifficulty);
            }
        }

        public void SelectGameMode(int mode)
        {
            GameSettings.GameMode = mode;
            Debug.Log("Game Mode: " + GameSettings.GameMode);
        }
            
        // Changer de Scene
        public void ChangeSceneByIndex(int sceneIndex)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        }

        // Quit Game
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}