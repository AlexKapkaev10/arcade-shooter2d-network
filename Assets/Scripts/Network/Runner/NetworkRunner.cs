using Mirror;
using Scripts.ScriptableObjects;
using UnityEngine;

namespace Scripts.CustomNetwork
{
    public class NetworkRunner : NetworkManager, INetworkRunner
    {
        [SerializeField] private NetworkRunnerSettings _settings;
        private NetworkManager _networkManager;

        public override void Awake()
        {
            base.Awake();
            Initialize();
        }

        public void Initialize()
        {
            if (!_networkManager)
            {
                _networkManager = singleton;
                _networkManager.networkAddress = _settings.GetNetworkAddress();
            }
        }

        public void StartGameHost()
        {
            if (!_networkManager)
                return;

            _networkManager.StartHost();
        }

        public void StartGameClient()
        {
            if (!_networkManager)
                return;
            
            _networkManager.StartClient();
        }

        public void StartGameServer()
        {
            if (!_networkManager)
                return;
            
            _networkManager.StartServer();
        }

        public void ExitToLobby()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                _networkManager.StopHost();
            }
            else if (NetworkClient.isConnected)
            {
                _networkManager.StopClient();
            }
            else if (NetworkServer.active)
            {
                _networkManager.StopServer();
            }
        }
    }
}