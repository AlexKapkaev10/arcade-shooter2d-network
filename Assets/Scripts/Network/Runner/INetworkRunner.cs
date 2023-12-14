namespace Scripts.CustomNetwork
{
    public interface INetworkRunner
    {
        public void Initialize();
        public void StartGameHost();
        public void StartGameClient();
        public void ExitToLobby();
    }
}