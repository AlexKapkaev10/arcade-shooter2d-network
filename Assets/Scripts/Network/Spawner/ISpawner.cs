using System;

namespace Scripts.CustomNetwork
{
    public interface ISpawner
    {
        public event Action<int> OnGlobalCoinsChange;
        public void SpawnCoin();
    }
}