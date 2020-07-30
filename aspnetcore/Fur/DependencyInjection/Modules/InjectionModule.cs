using Autofac;
using Fur.ApplicationBase;
using Fur.ApplicationBase.Wrappers;
using Fur.DependencyInjection.Lifetimes;
using Fur.DependencyInjection.Lifetimes.AsSelf;
using System.Collections.Generic;
using System.Linq;

namespace Fur.DependencyInjection.Modules
{
    internal class InjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var applicationTypes = AppGlobal.Application.PublicClassTypeWrappers.Where(u => u.CanBeNew);

            RegisterBaseTypes(builder, applicationTypes);
            RegisterGenericTypes(builder, applicationTypes);
        }

        /// <summary>
        /// 注册基础类型（非泛型）
        /// </summary>
        /// <param name="builder">容器构建器</param>
        /// <param name="applicationTypes">应用类型集合</param>
        private void RegisterBaseTypes(ContainerBuilder builder, IEnumerable<TypeWrapper> applicationTypes)
        {
            var baseTypes = applicationTypes.Where(t => !t.IsGenericType).Select(u => u.Type);

            builder.RegisterTypes(baseTypes.Where(t => typeof(ITransientLifetime).IsAssignableFrom(t)).ToArray())
                .AsImplementedInterfaces()
                .InstancePerDependency();
            builder.RegisterTypes(baseTypes.Where(t => typeof(ITransientAsSelfLifetime).IsAssignableFrom(t)).ToArray())
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterTypes(baseTypes.Where(t => typeof(IScopedLifetime).IsAssignableFrom(t)).ToArray())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterTypes(baseTypes.Where(t => typeof(IScopedAsSelfLifetime).IsAssignableFrom(t)).ToArray())
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterTypes(baseTypes.Where(t => typeof(ISingletonLifetime).IsAssignableFrom(t)).ToArray())
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterTypes(baseTypes.Where(t => typeof(ISingletonAsSelfLifetime).IsAssignableFrom(t)).ToArray())
               .AsSelf()
               .SingleInstance();
        }

        /// <summary>
        /// 注册泛型类型
        /// </summary>
        /// <param name="builder">容器构建器</param>
        /// <param name="applicationTypes">应用类型集合</param>
        private void RegisterGenericTypes(ContainerBuilder builder, IEnumerable<TypeWrapper> applicationTypes)
        {
            var genericTypes = applicationTypes.Where(t => t.IsGenericType);
            foreach (var type in genericTypes)
            {
                var genericType = type.GenericArgumentTypes.FirstOrDefault();

                if (typeof(ITransientLifetime<>).MakeGenericType(genericType).IsAssignableFrom(type.Type))
                {
                    builder.RegisterGeneric(type.Type).AsImplementedInterfaces().InstancePerDependency();
                }
                else if (typeof(ITransientAsSelfLifetime<>).MakeGenericType(genericType).IsAssignableFrom(type.Type))
                {
                    builder.RegisterGeneric(type.Type).AsSelf().InstancePerDependency();
                }
                else if (typeof(IScopedLifetime<>).MakeGenericType(genericType).IsAssignableFrom(type.Type))
                {
                    builder.RegisterGeneric(type.Type).AsImplementedInterfaces().InstancePerLifetimeScope();
                }
                else if (typeof(IScopedAsSelfLifetime<>).MakeGenericType(genericType).IsAssignableFrom(type.Type))
                {
                    builder.RegisterGeneric(type.Type).AsSelf().InstancePerLifetimeScope();
                }
                else if (typeof(IScopedLifetime<>).MakeGenericType(genericType).IsAssignableFrom(type.Type))
                {
                    builder.RegisterGeneric(type.Type).AsImplementedInterfaces().SingleInstance();
                }
                else if (typeof(IScopedAsSelfLifetime<>).MakeGenericType(genericType).IsAssignableFrom(type.Type))
                {
                    builder.RegisterGeneric(type.Type).AsSelf().SingleInstance();
                }
                else { }
            }
        }
    }
}