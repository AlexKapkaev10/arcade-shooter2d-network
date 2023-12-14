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
        private SpawnerSettings _settings;

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

            for (int i = 0; i < _settings.CoinsSpawnCount; i++)
            {
                Vector2 spawnPosition = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
                GameObject coin = Instantiate(_settings.CoinPrefab, spawnPosition, Quaternion.identity);
                NetworkServer.Spawn(coin);
            }
        }
    }
}