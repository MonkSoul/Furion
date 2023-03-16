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
}