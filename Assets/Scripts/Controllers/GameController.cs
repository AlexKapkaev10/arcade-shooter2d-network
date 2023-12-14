using Mirror;
using Scripts.CustomNetwork;
using Scripts.Interfaces;
using UnityEngine;
using VContainer;

namespace Scripts.Controllers
{
    public class GameController : MonoBehaviour, IGameController
    {
        private IObjectResolver _resolver;
        private INetworkRunner _runner;
        private ISpawner _spawner;
        
        [Inject]
        private void Configure(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        private void Start()
        {
            _runner = NetworkManager.singleton.GetComponent<INetworkRunner>();
            _runner.Initialize();
        }

        public void ExitLobby()
        {
            _runner.ExitToLobby();
        }
    }
}