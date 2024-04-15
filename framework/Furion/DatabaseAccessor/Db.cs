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

using Microsoft.EntityFrameworkCore;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库公开类
/// </summary>
[SuppressSniffer]
public static class Db
{
    /// <summary>
    /// 迁移类库名称
    /// </summary>
    internal static string MigrationAssemblyName = "Furion.Database.Migrations";

    /// <summary>
    /// 是否启用自定义租户类型
    /// </summary>
    internal static bool CustomizeMultiTenants;

    /// <summary>
    /// 基于表的多租户外键名
    /// </summary>
    internal static string OnTableTenantId = nameof(Entity.TenantId);

    /// <summary>
    /// 获取非泛型仓储
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IRepository GetRepository(IServiceProvider serviceProvider = default)
    {
        return App.GetService<IRepository>(serviceProvider);
    }

    /// <summary>
    /// 获取实体仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>IRepository{TEntity}</returns>
    public static IRepository<TEntity> GetRepository<TEntity>(IServiceProvider serviceProvider = default)
        where TEntity : class, IPrivateEntity, new()
    {
        return App.GetService<IRepository<TEntity>>(serviceProvider);
    }

    /// <summary>
    /// 获取实体仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>IRepository{TEntity, TDbContextLocator}</returns>
    public static IRepository<TEntity, TDbContextLocator> GetRepository<TEntity, TDbContextLocator>(IServiceProvider serviceProvider = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return App.GetService<IRepository<TEntity, TDbContextLocator>>(serviceProvider);
    }

    /// <summary>
    /// 根据定位器类型获取仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="dbContextLocator"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IPrivateRepository<TEntity> GetRepository<TEntity>(Type dbContextLocator, IServiceProvider serviceProvider = default)
         where TEntity : class, IPrivateEntity, new()
    {
        return App.GetService(typeof(IRepository<,>).MakeGenericType(typeof(TEntity), dbContextLocator), serviceProvider) as IPrivateRepository<TEntity>;
    }

    /// <summary>
    /// 获取特定数据库上下文仓储
    /// </summary>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IDbRepository<TDbContextLocator> GetDbRepository<TDbContextLocator>(IServiceProvider serviceProvider = default)
        where TDbContextLocator : class, IDbContextLocator
    {
        return App.GetService<IDbRepository<TDbContextLocator>>(serviceProvider);
    }

    /// <summary>
    /// 获取Sql仓储
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns>ISqlRepository</returns>
    public static ISqlRepository GetSqlRepository(IServiceProvider serviceProvider = default)
    {
        return App.GetService<ISqlRepository>(serviceProvider);
    }

    /// <summary>
    /// 获取Sql仓储
    /// </summary>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>ISqlRepository{TDbContextLocator}</returns>
    public static ISqlRepository<TDbContextLocator> GetSqlRepository<TDbContextLocator>(IServiceProvider serviceProvider = default)
        where TDbContextLocator : class, IDbContextLocator
    {
        return App.GetService<ISqlRepository<TDbContextLocator>>(serviceProvider);
    }

    /// <summary>
    /// 获取随机主从库仓储
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns>ISqlRepository</returns>
    public static IMSRepository GetMSRepository(IServiceProvider serviceProvider = default)
    {
        return App.GetService<IMSRepository>(serviceProvider);
    }

    /// <summary>
    /// 获取随机主从库仓储
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">主库数据库上下文定位器</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>IMSRepository{TDbContextLocator}</returns>
    public static IMSRepository<TMasterDbContextLocator> GetMSRepository<TMasterDbContextLocator>(IServiceProvider serviceProvider = default)
        where TMasterDbContextLocator : class, IDbContextLocator
    {
        return App.GetService<IMSRepository<TMasterDbContextLocator>>(serviceProvider);
    }

    /// <summary>
    /// 获取 Sql 代理
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns>ISqlRepository</returns>
    public static TSqlDispatchProxy GetSqlProxy<TSqlDispatchProxy>(IServiceProvider serviceProvider = default)
        where TSqlDispatchProxy : class, ISqlDispatchProxy
    {
        return App.GetService<TSqlDispatchProxy>(serviceProvider);
    }

    /// <summary>
    /// 获取默认数据库上下文
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static DbContext GetDbContext(IServiceProvider serviceProvider = default)
    {
        return GetDbContext(typeof(MasterDbContextLocator), serviceProvider);
    }

    /// <summary>
    /// 获取特定数据库上下文
    /// </summary>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static DbContext GetDbContext<TDbContextLocator>(IServiceProvider serviceProvider = default)
        where TDbContextLocator : class, IDbContextLocator
    {
        return GetDbContext(typeof(TDbContextLocator), serviceProvider);
    }

    /// <summary>
    /// 获取特定数据库上下文
    /// </summary>
    /// <param name="dbContextLocator">数据库上下文定位器</param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static DbContext GetDbContext(Type dbContextLocator, IServiceProvider serviceProvider = default)
    {
        // 判断数据库上下文定位器是否绑定
        Penetrates.CheckDbContextLocator(dbContextLocator, out _);

        var dbContextResolve = App.GetService<Func<Type, IScoped, DbContext>>(serviceProvider);
        return dbContextResolve(dbContextLocator, default);
    }

    /// <summary>
    /// 获取新的默认数据库上下文（手动 using 释放）
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="contextOptions"></param>
    /// <returns></returns>
    public static DbContext GetNewDbContext(IServiceProvider serviceProvider = default, DbContextOptions contextOptions = null)
    {
        return GetNewDbContext(typeof(MasterDbContextLocator), serviceProvider, contextOptions);
    }

    /// <summary>
    /// 获取新的特定数据库上下文（手动 using 释放）
    /// </summary>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="serviceProvider"></param>
    /// <param name="contextOptions"></param>
    /// <returns></returns>
    public static DbContext GetNewDbContext<TDbContextLocator>(IServiceProvider serviceProvider = default, DbContextOptions contextOptions = null)
        where TDbContextLocator : class, IDbContextLocator
    {
        return GetNewDbContext(typeof(TDbContextLocator), serviceProvider, contextOptions);
    }

    /// <summary>
    /// 获取新的特定数据库上下文（手动 using 释放）
    /// </summary>
    /// <param name="dbContextLocator">数据库上下文定位器</param>
    /// <param name="serviceProvider"></param>
    /// <param name="contextOptions"></param>
    /// <returns></returns>
    public static DbContext GetNewDbContext(Type dbContextLocator, IServiceProvider serviceProvider = default, DbContextOptions contextOptions = null)
    {
        // 判断数据库上下文定位器是否绑定
        Penetrates.CheckDbContextLocator(dbContextLocator, out var dbContextType);

        // 解析 DbContextOptions<DbContext> 构造函数参数
        contextOptions ??= App.GetService(typeof(DbContextOptions<>).MakeGenericType(dbContextType), serviceProvider) as DbContextOptions;

        // 创建新实例
        var dbContext = (DbContext)Activator.CreateInstance(dbContextType, new object[] { contextOptions });

        // 实现动态数据库上下文功能，刷新 OnModelCreating
        var dbContextAttribute = DbProvider.GetAppDbContextAttribute(dbContextType);
        if (dbContextAttribute?.Mode == DbContextMode.Dynamic)
        {
            DynamicModelCacheKeyFactory.RebuildModels();
        }

        return dbContext;
    }

    /// <summary>
    /// 获取新的默认数据库上下文（手动 using 释放）
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="contextOptions"></param>
    /// <returns></returns>
    public static DbContext CreateDbContext(IServiceProvider serviceProvider = default, DbContextOptions contextOptions = null)
    {
        return GetNewDbContext(typeof(MasterDbContextLocator), serviceProvider, contextOptions);
    }

    /// <summary>
    /// 获取新的特定数据库上下文（手动 using 释放）
    /// </summary>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="serviceProvider"></param>
    /// <param name="contextOptions"></param>
    /// <returns></returns>
    public static DbContext CreateDbContext<TDbContextLocator>(IServiceProvider serviceProvider = default, DbContextOptions contextOptions = null)
        where TDbContextLocator : class, IDbContextLocator
    {
        return GetNewDbContext(typeof(TDbContextLocator), serviceProvider, contextOptions);
    }

    /// <summary>
    /// 获取新的特定数据库上下文（手动 using 释放）
    /// </summary>
    /// <param name="dbContextLocator">数据库上下文定位器</param>
    /// <param name="serviceProvider"></param>
    /// <param name="contextOptions"></param>
    /// <returns></returns>
    public static DbContext CreateDbContext(Type dbContextLocator, IServiceProvider serviceProvider = default, DbContextOptions contextOptions = null)
    {
        return GetNewDbContext(dbContextLocator, serviceProvider, contextOptions);
    }
}