using Scripts.CustomNetwork;

namespace Scripts.Interfaces
{
    public interface IContainerService
    {
        public INetworkRunner NetworkRunner { get; }
        public void InitializeNetworkRunner();
    }
}