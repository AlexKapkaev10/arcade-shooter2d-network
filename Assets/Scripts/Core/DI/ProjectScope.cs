using Scripts.CustomNetwork;
using Scripts.Interfaces;
using Scripts.Services;
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
            builder
                .RegisterComponentInNewPrefab(_networkRunner, Lifetime.Transient)
                .As<INetworkRunner>();
            builder.Register<ContainerService>(Lifetime.Singleton).As<IContainerService>();
        }
    }
}