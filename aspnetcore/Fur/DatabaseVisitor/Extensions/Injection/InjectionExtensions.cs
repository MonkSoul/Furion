using Autofac;
using Fur.DatabaseVisitor.Contexts;
using Fur.DatabaseVisitor.Identifiers;
using Fur.DatabaseVisitor.Providers;
using Fur.DatabaseVisitor.Repositories;
using Fur.DatabaseVisitor.Repositories.Multiples;
using Microsoft.EntityFrameworkCore;

namespace Fur.DatabaseVisitor.Extensions.Injection
{
    /// <summary>
    /// 数据库访问注册拓展类
    /// </summary>
    public static class InjectionExtensions
    {
        #region 注册默认数据库操作上下文 + public static ContainerBuilder RegisterDefaultDbContext<TDbContext>(this ContainerBuilder builder) where TDbContext : DbContext
        /// <summary>
        /// 注册默认数据库操作上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库操作上下文</typeparam>
        /// <param name="builder">容器构建器</param>
        /// <returns><see cref="ContainerBuilder"/></returns>
        public static ContainerBuilder RegisterDefaultDbContext<TDbContext>(this ContainerBuilder builder)
           where TDbContext : DbContext
        {
            builder.RegisterType<TDbContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterMultipleDbContext<TDbContext, FurDbContextIdentifier>();

            return builder;
        }
        #endregion

        #region 注册多个数据库操作上下文 + public static ContainerBuilder RegisterMultipleDbContext<TDbContext, TDbContextIdentifier>(this ContainerBuilder builder) where TDbContext : DbContext where TDbContextIdentifier : IDbContextIdentifier
        /// <summary>
        /// 注册多个数据库操作上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库操作上下文</typeparam>
        /// <typeparam name="TDbContextIdentifier">数据库上下文标识类</typeparam>
        /// <param name="builder">容器构建器</param>
        /// <returns><see cref="ContainerBuilder"/></returns>
        public static ContainerBuilder RegisterMultipleDbContext<TDbContext, TDbContextIdentifier>(this ContainerBuilder builder)
            where TDbContext : DbContext
            where TDbContextIdentifier : IDbContextIdentifier
        {
            builder.RegisterType<TDbContext>()
                .Named<DbContext>(typeof(TDbContextIdentifier).Name)
                .InstancePerLifetimeScope();

            return builder;
        }
        #endregion

        #region 注册仓储 + public static ContainerBuilder RegisterRepositories(this ContainerBuilder builder, bool includeMultiple = true)
        /// <summary>
        /// 注册仓储
        /// </summary>
        /// <param name="builder">容器构建器</param>
        /// <param name="includeMultiple">包含多上下文仓储</param>
        /// <returns><see cref="ContainerBuilder"/></returns>
        public static ContainerBuilder RegisterRepositories(this ContainerBuilder builder, bool includeMultiple = true)
        {
            builder.RegisterGeneric(typeof(EFCoreRepositoryOfT<>))
                .As(typeof(IRepositoryOfT<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<EFCoreRepository>()
                .As<IRepository>()
                .InstancePerLifetimeScope();

            if (includeMultiple)
            {
                builder.RegisterGeneric(typeof(MultipleDbContextEFCoreRepositoryOfT<,>))
                    .As(typeof(IMultipleDbContextRepositoryOfT<,>))
                    .InstancePerLifetimeScope();

                builder.RegisterType<MultipleDbContextEFCoreRepository>()
                    .As<IMultipleDbContextRepository>()
                    .InstancePerLifetimeScope();
            }

            return builder;
        }
        #endregion

        #region 注册租户提供器 + public static ContainerBuilder RegisterTenant<TTenantProvider>(this ContainerBuilder builder) where TTenantProvider : ITenantProvider
        /// <summary>
        /// 注册租户提供器
        /// </summary>
        /// <typeparam name="TTenantProvider">租户提供器</typeparam>
        /// <param name="builder">容器构建器</param>
        /// <returns><see cref="ContainerBuilder"/></returns>
        public static ContainerBuilder RegisterTenant<TTenantProvider>(this ContainerBuilder builder)
            where TTenantProvider : ITenantProvider
        {
            builder.RegisterMultipleDbContext<FurTenantDbContext, FurTenantDbContextIdentifier>();

            builder.RegisterType<TTenantProvider>()
                .As<ITenantProvider>()
                .InstancePerLifetimeScope();
            return builder;
        }
        #endregion
    }
}
