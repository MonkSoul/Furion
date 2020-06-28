using Autofac;
using Fur.ApplicationSystem;
using Fur.DependencyInjection.Lifetimes;
using Fur.DependencyInjection.Lifetimes.AsSelf;
using Fur.Models.ApplicationSystem;
using System.Collections.Generic;
using System.Linq;

namespace Fur.DependencyInjection.Modules
{
    internal class InjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var applicationTypes = ApplicationPrepare.ApplicationAssemblies.SelectMany(a => a.PublicClassTypes);

            RegisterBaseTypes(builder, applicationTypes);
            RegisterGenericTypes(builder, applicationTypes);
        }

        #region 注册基础类型（非泛型）- private void RegisterBaseTypes(ContainerBuilder builder, IEnumerable<ApplicationTypeInfo> applicationTypes)
        /// <summary>
        /// 注册基础类型（非泛型）
        /// </summary>
        /// <param name="builder">容器构建器</param>
        /// <param name="applicationTypes">应用类型集合</param>
        private void RegisterBaseTypes(ContainerBuilder builder, IEnumerable<ApplicationTypeInfo> applicationTypes)
        {
            var baseTypes = applicationTypes.Where(t => !t.IsGenericType);

            builder.RegisterTypes(baseTypes.Where(t => typeof(ITransientLifetime).IsAssignableFrom(t.Type)).Select(u => u.Type).ToArray())
                .AsImplementedInterfaces()
                .InstancePerDependency();
            builder.RegisterTypes(baseTypes.Where(t => typeof(ITransientAsSelfLifetime).IsAssignableFrom(t.Type)).Select(u => u.Type).ToArray())
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterTypes(baseTypes.Where(t => typeof(IScopedLifetime).IsAssignableFrom(t.Type)).Select(u => u.Type).ToArray())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterTypes(baseTypes.Where(t => typeof(IScopedAsSelfLifetime).IsAssignableFrom(t.Type)).Select(u => u.Type).ToArray())
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterTypes(baseTypes.Where(t => typeof(ISingletonLifetime).IsAssignableFrom(t.Type)).Select(u => u.Type).ToArray())
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterTypes(baseTypes.Where(t => typeof(ISingletonAsSelfLifetime).IsAssignableFrom(t.Type)).Select(u => u.Type).ToArray())
               .AsSelf()
               .SingleInstance();
        }
        #endregion

        #region 注册泛型类型 - private void RegisterGenericTypes(ContainerBuilder builder, IEnumerable<ApplicationTypeInfo> applicationTypes)
        /// <summary>
        /// 注册泛型类型
        /// </summary>
        /// <param name="builder">容器构建器</param>
        /// <param name="applicationTypes">应用类型集合</param>
        private void RegisterGenericTypes(ContainerBuilder builder, IEnumerable<ApplicationTypeInfo> applicationTypes)
        {
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
        #endregion
    }
}
