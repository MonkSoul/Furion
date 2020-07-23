using Autofac;
using Fur.DatabaseAccessor.Contexts;
using Fur.DatabaseAccessor.Contexts.Pool;
using Fur.DatabaseAccessor.Identifiers;
using Fur.DatabaseAccessor.Providers;
using Fur.DatabaseAccessor.Repositories;
using Fur.DatabaseAccessor.Repositories.MasterSlave;
using Fur.DatabaseAccessor.Repositories.Multiples;
using Fur.DatabaseAccessor.Tangent;
using Microsoft.EntityFrameworkCore;

namespace Fur.DatabaseAccessor.Extensions.Injection
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
            builder.RegisterType<DbContextPool>()
                .As<IDbContextPool>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TDbContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterMultipleRepository<TDbContext, FurDbContextIdentifier>();

            return builder;
        }

        #endregion 注册默认数据库操作上下文 + public static ContainerBuilder RegisterDefaultDbContext<TDbContext>(this ContainerBuilder builder) where TDbContext : DbContext

        #region 注册多个数据库操作上下文 + public static ContainerBuilder RegisterMultipleRepository<TDbContext, TDbContextIdentifier>(this ContainerBuilder builder) where TDbContext : DbContext where TDbContextIdentifier : IDbContextIdentifier

        /// <summary>
        /// 注册多个数据库操作上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库操作上下文</typeparam>
        /// <typeparam name="TDbContextIdentifier">数据库上下文标识类</typeparam>
        /// <param name="builder">容器构建器</param>
        /// <returns><see cref="ContainerBuilder"/></returns>
        public static ContainerBuilder RegisterMultipleRepository<TDbContext, TDbContextIdentifier>(this ContainerBuilder builder)
            where TDbContext : DbContext
            where TDbContextIdentifier : IDbContextIdentifier
        {
            builder.RegisterType<TDbContext>()
                .Named<DbContext>(typeof(TDbContextIdentifier).Name)
                .InstancePerLifetimeScope();

            return builder;
        }

        #endregion 注册多个数据库操作上下文 + public static ContainerBuilder RegisterMultipleRepository<TDbContext, TDbContextIdentifier>(this ContainerBuilder builder) where TDbContext : DbContext where TDbContextIdentifier : IDbContextIdentifier

        #region 注册仓储 + public static ContainerBuilder RegisterRepositories(this ContainerBuilder builder, bool includeMultiple = true, bool includeReadOrWrite = true)

        /// <summary>
        /// 注册仓储
        /// </summary>
        /// <param name="builder">容器构建器</param>
        /// <param name="includeMultiple">包含多上下文仓储</param>
        /// <returns><see cref="ContainerBuilder"/></returns>
        public static ContainerBuilder RegisterRepositories(this ContainerBuilder builder, bool includeMultiple = true, bool includeReadOrWrite = true)
        {
            builder.RegisterGeneric(typeof(EFCoreRepositoryOfT<>))
                .As(typeof(IRepositoryOfT<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<EFCoreRepository>()
                .As<IRepository>()
                .InstancePerLifetimeScope();

            if (includeMultiple)
            {
                builder.RegisterGeneric(typeof(MultipleEFCoreRepositoryOfT<,>))
                    .As(typeof(IMultipleRepositoryOfT<,>))
                    .InstancePerLifetimeScope();

                builder.RegisterType<MultipleEFCoreRepository>()
                    .As<IMultipleRepository>()
                    .InstancePerLifetimeScope();
            }

            if (includeReadOrWrite)
            {
                builder.RegisterGeneric(typeof(MasterSlaveEFCoreRepositoryOfT<,,>))
                    .As(typeof(IMasterSlaveRepositoryOfT<,,>))
                    .InstancePerLifetimeScope();

                builder.RegisterType<MasterSlaveEFCoreRepository>()
                   .As<IMasterSlaveRepository>()
                   .InstancePerLifetimeScope();
            }

            return builder;
        }

        #endregion 注册仓储 + public static ContainerBuilder RegisterRepositories(this ContainerBuilder builder, bool includeMultiple = true, bool includeReadOrWrite = true)

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
            builder.RegisterMultipleRepository<FurTenantDbContext, FurTenantDbContextIdentifier>();

            builder.RegisterType<TTenantProvider>()
                .As<ITenantProvider>()
                .InstancePerLifetimeScope();
            return builder;
        }

        #endregion 注册租户提供器 + public static ContainerBuilder RegisterTenant<TTenantProvider>(this ContainerBuilder builder) where TTenantProvider : ITenantProvider

        #region 注册切面上下文 + public static ContainerBuilder RegisterTangentDbContext(this ContainerBuilder builder)

        /// <summary>
        /// 注册切面上下文
        /// </summary>
        /// <param name="builder">容器构建器</param>
        /// <returns>容器构建器</returns>
        public static ContainerBuilder RegisterTangentDbContext(this ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(TangentDbContextOfT<>))
                .As(typeof(ITangentDbContextOfT<>))
                .InstancePerLifetimeScope();

            return builder;
        }

        #endregion 注册切面上下文 + public static ContainerBuilder RegisterTangentDbContext(this ContainerBuilder builder)
    }
}