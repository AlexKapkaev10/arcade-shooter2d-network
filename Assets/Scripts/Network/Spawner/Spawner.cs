using System;
using Mirror;
using Scripts.ScriptableObjects;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Scripts.CustomNetwork
{
    public class Spawner : NetworkBehaviour, ISpawner
    {
        public event Action<int> OnGlobalCoinsChange;

        private SpawnerSettings _settings;

        [SyncVar] private int _allCoins;

        [Inject]
        private void Construct(SpawnerSettings settings)
        {
            _settings = settings;
        }

        private void Start()
        {
            SpawnCoin();
        }

        public void SpawnCoin()
        {
            if (!isServer)
                return;

            _allCoins = _settings.CoinsSpawnCount;

            for (int i = 0; i < _allCoins; i++)
            {
                Vector2 spawnPosition = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
                
                var coin = Instantiate(_settings.CoinPrefab, spawnPosition, Quaternion.identity);
                coin.OnCollect += CoinCollect;
                NetworkServer.Spawn(coin.gameObject);
            }
            
            OnGlobalCoinsChange?.Invoke(_allCoins);
        }
        
        private void CoinCollect()
        {
            RpcCoinCollect();
        }

        [ClientRpc]
        private void RpcCoinCollect()
        {
            _allCoins--;
            OnGlobalCoinsChange?.Invoke(_allCoins);
        }
    }
}