using Scripts.Controllers;
using Scripts.CustomNetwork;
using Scripts.Interfaces;
using Scripts.ScriptableObjects;
using Scripts.Services;
using Scripts.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scripts.Core
{
    public class GameScope : BaseScope
    {
        [SerializeField] private SpawnerSettings _spawnerSettings;
        [SerializeField] private GameViewSettings _gameViewSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<BankService>(Lifetime.Transient).As<IBankService>();
            builder.RegisterComponentInHierarchy<GameController>().As<IGameController>();
            builder.RegisterComponentInHierarchy<Spawner>()
                .As<ISpawner>()
                .WithParameter(_spawnerSettings);
            
            builder.RegisterComponentInHierarchy<GameView>().As<IGameView>().WithParameter(_gameViewSettings);
            builder.RegisterComponentInHierarchy<LocalService>().As<ILocalService>();
        }
    }
}