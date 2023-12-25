using Cysharp.Threading.Tasks;
using Mirror;
using Scripts.Interfaces;
using VContainer; 
using VContainer.Unity;

namespace Scripts.Services
{
    public class LocalService : NetworkBehaviour, ILocalService
    {
        private IObjectResolver _resolver;
        
        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
        }
        
        public override void OnStartClient()
        {
            base.OnStartClient();
            WaitLocalClientAsync().Forget();
        }

        private async UniTask WaitLocalClientAsync()
        {
            await UniTask.WaitUntil(() => NetworkClient.localPlayer != null);
            _resolver.InjectGameObject(NetworkClient.localPlayer.gameObject);
        }
    }
}
