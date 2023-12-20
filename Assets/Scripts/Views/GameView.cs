using Scripts.CustomNetwork;
using Scripts.Game;
using Scripts.Interfaces;
using TMPro;
using UnityEngine;
using VContainer;

namespace Scripts.Views
{
    public class GameView : View, IGameView
    {
        [SerializeField] private TMP_Text _globalCoins;
        [SerializeField] private TMP_Text _localCoins;

        private INetworkRunner _runner;
        private ISpawner _spawner;
        private IBankService _bankService;

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            _spawner = resolver.Resolve<ISpawner>();
            _runner = resolver.Resolve<IContainerService>().NetworkRunner;
        }

        public void Initialize(IBankService bankService)
        {
            _bankService = bankService;
            _bankService.OnCoinCollect += DisplayLocalCoins;
        }

        private void OnEnable()
        {
            _spawner.OnGlobalCoinsChange += DisplayGlobalCoins;
            
            if (_bankService != null)
                _bankService.OnCoinCollect += DisplayLocalCoins;
        }

        private void OnDisable()
        {
            _spawner.OnGlobalCoinsChange -= DisplayGlobalCoins;
            _bankService.OnCoinCollect -= DisplayLocalCoins;
        }

        public void DisplayGlobalCoins(int count)
        {
            _globalCoins.SetText($"Coins left: {count}");
        }

        public void DisplayLocalCoins(int count)
        {
            _localCoins.SetText($"My Coins: {count}");
        }
    }
}