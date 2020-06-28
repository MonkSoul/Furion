using Autofac;
using Fur.ApplicationSystem;
using Fur.DependencyInjection.Lifetimes;
using Fur.DependencyInjection.Lifetimes.AsSelf;
using System.Linq;

namespace Fur.DependencyInjection.Modules
{
    internal class InjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var applicationTypes = ApplicationPrepare.ApplicationAssemblies.SelectMany(a => a.PublicClassTypes);

            var noGenericTypes = applicationTypes.Where(t => !t.IsGenericType);
            builder.RegisterTypes(noGenericTypes.Where(t => typeof(ITransientLifetime).IsAssignableFrom(t.Type)).Select(u => u.Type).ToArray())
                .AsImplementedInterfaces()
                .InstancePerDependency();
            builder.RegisterTypes(noGenericTypes.Where(t => typeof(ITransientAsSelfLifetime).IsAssignableFrom(t.Type)).Select(u => u.Type).ToArray())
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterTypes(noGenericTypes.Where(t => typeof(IScopedLifetime).IsAssignableFrom(t.Type)).Select(u => u.Type).ToArray())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterTypes(noGenericTypes.Where(t => typeof(IScopedAsSelfLifetime).IsAssignableFrom(t.Type)).Select(u => u.Type).ToArray())
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterTypes(noGenericTypes.Where(t => typeof(ISingletonLifetime).IsAssignableFrom(t.Type)).Select(u => u.Type).ToArray())
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterTypes(noGenericTypes.Where(t => typeof(ISingletonAsSelfLifetime).IsAssignableFrom(t.Type)).Select(u => u.Type).ToArray())
               .AsSelf()
               .SingleInstance();

            var genericTypes = applicationTypes.Where(t => t.IsGenericType);
            foreach (var type in genericTypes)
            {
                if (typeof(ITransientLifetimeOfT<>).MakeGenericType(type.GenericArguments.ToArray()).IsAssignableFrom(type.Type))
                {
                    builder.RegisterGeneric(type.Type).AsImplementedInterfaces().InstancePerDependency();
                }
                else if (typeof(ITransientAsSelfLifetimeOfT<>).MakeGenericType(type.GenericArguments.ToArray()).IsAssignableFrom(type.Type))
                {
                    builder.RegisterGeneric(type.Type).AsSelf().InstancePerDependency();
                }
                else if (typeof(IScopedLifetimeOfT<>).MakeGenericType(type.GenericArguments.ToArray()).IsAssignableFrom(type.Type))
                {
                    builder.RegisterGeneric(type.Type).AsImplementedInterfaces().InstancePerLifetimeScope();
                }
                else if (typeof(IScopedAsSelfLifetimeOfT<>).MakeGenericType(type.GenericArguments.ToArray()).IsAssignableFrom(type.Type))
                {
                    builder.RegisterGeneric(type.Type).AsSelf().InstancePerLifetimeScope();
                }
                else if (typeof(IScopedLifetimeOfT<>).MakeGenericType(type.GenericArguments.ToArray()).IsAssignableFrom(type.Type))
                {
                    builder.RegisterGeneric(type.Type).AsImplementedInterfaces().SingleInstance();
                }
                else if (typeof(IScopedAsSelfLifetimeOfT<>).MakeGenericType(type.GenericArguments.ToArray()).IsAssignableFrom(type.Type))
                {
                    builder.RegisterGeneric(type.Type).AsSelf().SingleInstance();
                }
                else { }
            }
        }
    }
}
