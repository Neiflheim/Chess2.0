using Game;
using Handlers;
using Pieces;
using UnityEngine;

namespace Heuristic
{
    public class HeuristicHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private BoardsHandler _boardsHandler;

        private int _boardHeuristicValue;
        private int _whiteHeuristicValue;
        private int _blackHeuristicValue;

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log(BoardHeuristicValue());
            }
        }

        public int BoardHeuristicValue()
        {
            _boardHeuristicValue = 0;
            _whiteHeuristicValue = 0;
            _blackHeuristicValue = 0;
            
            foreach (Piece piece in _boardsHandler.Pieces)
            {
                if (piece && piece.IsWhite)
                {
                    _whiteHeuristicValue += piece.Value;
                }
            }
            foreach (Piece piece in _boardsHandler.Pieces)
            {
                if (piece && !piece.IsWhite)
                {
                    _blackHeuristicValue += piece.Value;
                }
            }
            
            if (GameManager.Instance.isWhiteTurn)
            {
                _boardHeuristicValue = _whiteHeuristicValue - _blackHeuristicValue;
            }
            else
            {
                _boardHeuristicValue = _blackHeuristicValue - _whiteHeuristicValue;
            }
            
            return _boardHeuristicValue;
        }
    }
}
