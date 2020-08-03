using Autofac;
using Fur.AppCore.Attributes;

namespace Fur.DependencyInjection.Extensions
{
    [NonWrapper]
    internal static class LifetimeScopeExtensions
    {
        internal static TService GetService<TService>(this ILifetimeScope lifetimeScope)
            where TService : class
        {
            if (lifetimeScope.IsRegistered<TService>()) return lifetimeScope.Resolve<TService>();
            else return default;
        }

        internal static TService GetNamedService<TService>(this ILifetimeScope lifetimeScope, string serviceName)
            where TService : class
        {
            if (lifetimeScope.IsRegistered<TService>()) return lifetimeScope.ResolveNamed<TService>(serviceName);
            else return default;
        }
    }
}