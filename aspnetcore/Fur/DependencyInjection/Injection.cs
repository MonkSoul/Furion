using Autofac;
using Fur.ApplicationSystem;
using System.Linq;

namespace Fur.DependencyInjection
{
    public sealed class Injection
    {
        public static void Initialize(ContainerBuilder builder)
            => builder.RegisterAssemblyModules(ApplicationPrepare.ApplicationAssemblies.Select(a => a.Assembly).ToArray());
    }
}
