using Scripts.Controllers;
using Scripts.CustomNetwork;
using Scripts.Interfaces;
using Scripts.ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scripts.Core
{
    public class GameScope : BaseScope
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private SpawnerSettings _spawnerSettings;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterComponentInHierarchy<GameController>().As<IGameController>();
            builder.RegisterComponentInHierarchy<Spawner>()
                .As<ISpawner>()
                .WithParameter(_spawnerSettings);
        }
    }
}