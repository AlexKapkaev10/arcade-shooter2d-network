using System;
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
            _runner = resolver.Resolve<INetworkRunner>();
        }

        private void Start()
        {
            //_spawner = _resolver.Resolve<ISpawner>();
            _runner.Initialize();
        }

        public void ExitLobby()
        {
            _runner.ExitToLobby();
        }
    }
}