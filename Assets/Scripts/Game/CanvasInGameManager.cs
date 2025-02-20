using Handlers;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game
{
    public class CanvasInGameManager : MonoBehaviour
    {
        // Timer
        private bool _startTimers;
        private float _blackElapsedTime;
        private float _whiteElapsedTime;
        
        [Header("Timer")]
        [SerializeField] private Text _blackTimerText;
        [SerializeField] private Text _whiteTimerText;
        
        // Turn
        [Header("Turn Text")]
        [SerializeField] private Text _playerTurnText;

        private void Awake()
        {
            _blackTimerText.text = GameSettings.GameTimer + ":00:00";
            _whiteTimerText.text = GameSettings.GameTimer + ":00:00";
            _blackElapsedTime = GameSettings.GameTimer * 60;
            _whiteElapsedTime = GameSettings.GameTimer * 60;
        }

        private void Update()
        {
            // Timer
            if (Input.GetButtonDown("Fire1"))
            {
                _startTimers = true;
            }
            
            if (_startTimers)
            {
                if (BoardsHandler.Instance.IsWhiteTurn)
                {
                    _whiteElapsedTime -= Time.deltaTime;
                    UpdateWhiteTimerText(_whiteElapsedTime);
                }
                else
                {
                    _blackElapsedTime -= Time.deltaTime;
                    UpdateBlackTimerText(_blackElapsedTime);
                }
            }
            
            // Turn
            PlayerTurnText();
        }

        void UpdateBlackTimerText(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time % 60F);
            int milliseconds = Mathf.FloorToInt((time * 100F) % 100F);

            _blackTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);

            if (time <= 0f)
            {
                Rules.IsGameOver(true);
            }
        }
        
        void UpdateWhiteTimerText(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time % 60F);
            int milliseconds = Mathf.FloorToInt((time * 100F) % 100F);

            _whiteTimerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
            
            if (time <= 0f)
            {
                Rules.IsGameOver(false);
            }
        }
        
        // Turn
        public void PlayerTurnText()
        {
            if (BoardsHandler.Instance.IsWhiteTurn)
            {
                _playerTurnText.text = " White Player Turn ";
            }
            else
            {
                _playerTurnText.text = " Black Player Turn ";
            }
        }
    }
}
