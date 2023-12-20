using Game.Interfaces;
using Mirror;
using VContainer;

namespace Scripts.Game
{
    public class GamePhone : NetworkBehaviour, IGamePhone
    {
        private IObjectResolver _resolver;
        
        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
        }
    }
}