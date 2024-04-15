// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 数据库访问器服务拓展类
/// </summary>
[SuppressSniffer]
public static class DatabaseAccessorServiceCollectionExtensions
{
    /// <summary>
    /// 添加数据库上下文
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configure">配置</param>
    /// <param name="migrationAssemblyName">迁移类库名称</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddDatabaseAccessor(this IServiceCollection services, Action<IServiceCollection> configure = null, string migrationAssemblyName = default)
    {
        // 设置迁移类库名称
        if (!string.IsNullOrWhiteSpace(migrationAssemblyName)) Db.MigrationAssemblyName = migrationAssemblyName;

        // 配置数据库上下文
        configure?.Invoke(services);

        // 注册数据库上下文池
        services.TryAddScoped<IDbContextPool, DbContextPool>();

        // 注册 Sql 仓储
        services.TryAddScoped(typeof(ISqlRepository<>), typeof(SqlRepository<>));

        // 注册 Sql 非泛型仓储
        services.TryAddScoped<ISqlRepository, SqlRepository>();

        // 注册多数据库上下文仓储
        services.TryAddScoped(typeof(IRepository<,>), typeof(EFCoreRepository<,>));

        // 注册泛型仓储
        services.TryAddScoped(typeof(IRepository<>), typeof(EFCoreRepository<>));

        // 注册主从库仓储
        services.TryAddScoped(typeof(IMSRepository), typeof(MSRepository));
        services.TryAddScoped(typeof(IMSRepository<>), typeof(MSRepository<>));
        services.TryAddScoped(typeof(IMSRepository<,>), typeof(MSRepository<,>));
        services.TryAddScoped(typeof(IMSRepository<,,>), typeof(MSRepository<,,>));
        services.TryAddScoped(typeof(IMSRepository<,,,>), typeof(MSRepository<,,,>));
        services.TryAddScoped(typeof(IMSRepository<,,,,>), typeof(MSRepository<,,,,>));
        services.TryAddScoped(typeof(IMSRepository<,,,,,>), typeof(MSRepository<,,,,,>));
        services.TryAddScoped(typeof(IMSRepository<,,,,,,>), typeof(MSRepository<,,,,,,>));
        services.TryAddScoped(typeof(IMSRepository<,,,,,,,>), typeof(MSRepository<,,,,,,,>));

        // 注册非泛型仓储
        services.TryAddScoped<IRepository, EFCoreRepository>();

        // 注册工厂仓储
        services.TryAddSingleton(typeof(IRepositoryFactory<>), typeof(RepositoryFactory<>));
        services.TryAddSingleton(typeof(IRepositoryFactory<,>), typeof(RepositoryFactory<,>));

        // 注册多数据库仓储
        services.TryAddScoped(typeof(IDbRepository<>), typeof(DbRepository<>));

        // 注册解析数据库上下文委托
        services.TryAddScoped(provider =>
        {
            DbContext dbContextResolve(Type locator, IScoped transient)
            {
                return ResolveDbContext(provider, locator);
            }
            return (Func<Type, IScoped, DbContext>)dbContextResolve;
        });

        // 注册 Sql 代理接口
        services.AddDispatchProxyForInterface<SqlDispatchProxy, ISqlDispatchProxy>(typeof(IScoped));

        // 注册全局工作单元过滤器
        services.AddUnitOfWork<EFCoreUnitOfWork>();

        // 注册自动 SaveChanges
        services.AddMvcFilter<AutoSaveChangesFilter>();

        // 注册自动 SaveChanges（Razor Pages）
        services.AddMvcFilter<AutoSaveChangesPageFilter>();

        return services;
    }

    /// <summary>
    /// 启动自定义租户类型
    /// </summary>
    /// <param name="services"></param>
    /// <param name="onTableTenantId">基于表的多租户Id名称</param>
    /// <returns></returns>
    public static IServiceCollection CustomizeMultiTenants(this IServiceCollection services, string onTableTenantId = default)
    {
        Db.CustomizeMultiTenants = true;
        if (!string.IsNullOrWhiteSpace(onTableTenantId)) Db.OnTableTenantId = onTableTenantId;

        return services;
    }

    /// <summary>
    /// 注册默认数据库上下文
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    /// <param name="services">服务提供器</param>
    public static IServiceCollection RegisterDbContext<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
    {
        return services.RegisterDbContext<TDbContext, MasterDbContextLocator>();
    }

    /// <summary>
    /// 注册数据库上下文
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="services">服务提供器</param>
    public static IServiceCollection RegisterDbContext<TDbContext, TDbContextLocator>(this IServiceCollection services)
        where TDbContext : DbContext
        where TDbContextLocator : class, IDbContextLocator
    {
        // 存储数据库上下文和定位器关系
        Penetrates.DbContextDescriptors.AddOrUpdate(typeof(TDbContextLocator), typeof(TDbContext), (key, value) => typeof(TDbContext));

        // 注册数据库上下文
        services.TryAddScoped<TDbContext>();

        return services;
    }

    /// <summary>
    /// 通过定位器解析上下文
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="dbContextLocator"></param>
    /// <returns></returns>
    private static DbContext ResolveDbContext(IServiceProvider provider, Type dbContextLocator)
    {
        // 判断数据库上下文定位器是否绑定
        Penetrates.CheckDbContextLocator(dbContextLocator, out var dbContextType);

        // 动态解析数据库上下文
        var dbContext = provider.GetService(dbContextType) as DbContext;

        // 实现动态数据库上下文功能，刷新 OnModelCreating
        var dbContextAttribute = DbProvider.GetAppDbContextAttribute(dbContextType);
        if (dbContextAttribute?.Mode == DbContextMode.Dynamic)
        {
            DynamicModelCacheKeyFactory.RebuildModels();
        }

        // 添加数据库上下文到池中
        var dbContextPool = provider.GetService<IDbContextPool>();
        dbContextPool?.AddToPool(dbContext);

        return dbContext;
    }
}