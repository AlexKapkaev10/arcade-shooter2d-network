namespace Scripts.Interfaces
{
    public interface IGameView
    {
        public void Initialize(IBankService bankService);
        public void DisplayGlobalCoins(int count);
        public void DisplayLocalCoins(int count);
    }
}