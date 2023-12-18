using System;

namespace Scripts.Interfaces
{
    public interface IBankService
    {
        public event Action<int>  OnCoinCollect;
        public int Coins { get; set; }
    }
}