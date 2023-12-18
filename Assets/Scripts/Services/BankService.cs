using System;
using Scripts.Interfaces;

namespace Scripts.Services
{
    public class BankService : IBankService
    {
        public event Action<int> OnCoinCollect;
        private int _coins;

        public int Coins
        {
            get => _coins;
            set
            {
                _coins = value;
                OnCoinCollect?.Invoke(_coins);
            }
        }
    }
}