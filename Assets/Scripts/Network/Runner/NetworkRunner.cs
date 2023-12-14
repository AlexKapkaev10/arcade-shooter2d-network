using Mirror;
using UnityEngine;

namespace Scripts.CustomNetwork
{
    public class NetworkRunner : MonoBehaviour, INetworkRunner
    {
        private NetworkManager _networkManager;

        public void Initialize()
        {
            if (!_networkManager)
                _networkManager = NetworkManager.singleton;
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