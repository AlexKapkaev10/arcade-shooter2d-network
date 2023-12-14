using Scripts.Core;
using Scripts.CustomNetwork;
using Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Scripts.Controllers
{
    public class LobbyController : MonoBehaviour, ILobbyController
    {
        [SerializeField] private Button _buttonHost;
        [SerializeField] private Button _buttonClient;

        private INetworkRunner _networkRunner;
        
        [Inject]
        private void Construct(INetworkRunner networkRunner)
        {
            _networkRunner = networkRunner;
        }

        private void Start()
        {
            _networkRunner.Initialize();
            _buttonHost.onClick.AddListener(_networkRunner.StartGameHost);
            _buttonClient.onClick.AddListener(_networkRunner.StartGameClient);
        }
    }
}