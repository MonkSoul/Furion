// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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