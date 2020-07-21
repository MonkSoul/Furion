using Autofac;
using Fur.UnifyResult.Extensions.Injection;
using Fur.UnifyResult.Providers;

namespace Fur.Application
{
    public class FurApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterUnifyProvider<UnifyResultProvider>();
        }
    }
}