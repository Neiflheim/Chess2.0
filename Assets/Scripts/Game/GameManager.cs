using Handlers;
using Utils;

namespace Game
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        public PieceHandler lastClickGameObject;
        public bool isWhiteTurn = true;
    }
}