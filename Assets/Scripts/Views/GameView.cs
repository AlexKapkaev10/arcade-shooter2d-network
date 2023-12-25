using System;
using Scripts.CustomNetwork;
using Scripts.Dates;
using Scripts.Interfaces;
using Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using Random = UnityEngine.Random;

namespace Scripts.Views
{
    public class GameView : View, IGameView
    {
        public event Action<PlayerData> OnSetPlayer;

        [SerializeField] private TMP_Text _globalCoins;
        [SerializeField] private TMP_Text _localCoins;
        [SerializeField] private TMP_InputField _inputFieldName;
        [SerializeField] private GameObject _spawnGroup;
        [SerializeField] private Button _buttonExit;

        private ISpawner _spawner;
        private IBankService _bankService;
        private INetworkRunner _runner;

        private GameViewSettings _settings;

        [Inject]
        private void Construct(IObjectResolver resolver, GameViewSettings settings)
        {
            _runner = resolver.Resolve<IContainerService>().NetworkRunner;
            _spawner = resolver.Resolve<ISpawner>();
            _settings = settings;
        }

        public void Initialize(IBankService bankService)
        {
            _bankService = bankService;
            _bankService.OnCoinCollect += DisplayLocalCoins;
        }

        public void SpawnClick()
        {
            if (string.IsNullOrWhiteSpace(_inputFieldName.text))
                return;
            
            OnSetPlayer?.Invoke(new PlayerData
            {
                Name = _inputFieldName.text,
                PhoneNumber = Random.Range(1234, 100000)
            });
            Destroy(_spawnGroup);
        }

        private void OnEnable()
        {
            _spawner.OnGlobalCoinsChange += DisplayGlobalCoins;
            _buttonExit.onClick.AddListener(_runner.ExitToLobby);
            
            if (_bankService != null)
                _bankService.OnCoinCollect += DisplayLocalCoins;
        }

        private void OnDisable()
        {
            _spawner.OnGlobalCoinsChange -= DisplayGlobalCoins;
            _buttonExit.onClick.RemoveListener(_runner.ExitToLobby);
            
            if (_bankService != null)
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