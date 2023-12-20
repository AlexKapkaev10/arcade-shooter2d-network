using Scripts.CustomNetwork;
using Scripts.Interfaces;
using VContainer;

namespace Scripts.Services
{
    public class ContainerService : IContainerService
    {
        private readonly IObjectResolver _resolver;
        private INetworkRunner _networkRunner;
        public INetworkRunner NetworkRunner => _networkRunner;
        
        [Inject]
        public ContainerService(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public void InitializeNetworkRunner()
        {
            _networkRunner = _resolver.Resolve<INetworkRunner>();
        }
    }
}