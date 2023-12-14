using VContainer;
using VContainer.Unity;

namespace Scripts.Core
{
    public class BaseScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
        }
    }
}