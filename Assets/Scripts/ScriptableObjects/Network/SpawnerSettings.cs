using Scripts.Game;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(SpawnerSettings), menuName = "SO/CustomNetwork/SpawnerSettings")]
    public class SpawnerSettings : ScriptableObject
    {
        [SerializeField] private Coin _coinPrefab;
        [SerializeField] private byte _coinsSpawnCount = 5;

        public Coin CoinPrefab => _coinPrefab;
        public byte CoinsSpawnCount => _coinsSpawnCount;
    }
}