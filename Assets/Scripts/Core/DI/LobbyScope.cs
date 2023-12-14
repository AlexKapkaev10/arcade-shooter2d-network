using Scripts.Controllers;
using Scripts.Interfaces;
using VContainer;
using VContainer.Unity;

namespace Scripts.Core
{
    public class LobbyScope : BaseScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterComponentInHierarchy<LobbyController>().As<ILobbyController>();
        }
    }
}