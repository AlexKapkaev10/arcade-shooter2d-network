using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(SpawnerSettings), menuName = "SO/CustomNetwork/SpawnerSettings")]
    public class SpawnerSettings : ScriptableObject
    {
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private byte _coinsSpawnCount = 5;

        public GameObject CoinPrefab => _coinPrefab;
        public byte CoinsSpawnCount => _coinsSpawnCount;
    }
}