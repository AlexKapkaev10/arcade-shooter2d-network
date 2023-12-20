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
        [SerializeField] private Button _buttonServer;

        private IContainerService _containerService;
        private INetworkRunner _runner;
        
        [Inject]
        private void Construct(IContainerService containerService)
        {
            _containerService = containerService;
        }
        
        private void Start()
        {
            _containerService.InitializeNetworkRunner();
            _runner = _containerService.NetworkRunner;
            _buttonHost.onClick.AddListener(_runner.StartGameHost);
            _buttonClient.onClick.AddListener(_runner.StartGameClient);
            _buttonServer.onClick.AddListener(_runner.StartGameServer);
        }
    }
}