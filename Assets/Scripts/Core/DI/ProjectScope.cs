using Scripts.CustomNetwork;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scripts.Core
{
    public class ProjectScope : BaseScope
    {
        [SerializeField] private NetworkRunner _networkRunner;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterComponentInNewPrefab(_networkRunner, Lifetime.Transient).As<INetworkRunner>();
        }
    }
}