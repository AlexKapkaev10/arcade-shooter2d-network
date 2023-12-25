using System;
using Scripts.Dates;

namespace Scripts.Interfaces
{
    public interface IGameView
    {
        public event Action<PlayerData> OnSetPlayer;
        public void Initialize(IBankService bankService);
        public void DisplayGlobalCoins(int count);
        public void DisplayLocalCoins(int count);
    }
}