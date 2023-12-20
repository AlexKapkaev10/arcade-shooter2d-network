namespace Scripts.CustomNetwork
{
    public interface INetworkRunner
    {
        public void Initialize();
        public void SpawnClient();
        public void StartGameHost();
        public void StartGameClient();
        public void StartGameServer();
        public void ExitToLobby();
    }
}