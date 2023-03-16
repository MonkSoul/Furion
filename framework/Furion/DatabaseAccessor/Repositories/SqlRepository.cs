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
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 操作仓储实现
/// </summary>
[SuppressSniffer]
public partial class SqlRepository : SqlRepository<MasterDbContextLocator>, ISqlRepository
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    public SqlRepository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}

/// <summary>
/// Sql 操作仓储实现
/// </summary>
[SuppressSniffer]
public partial class SqlRepository<TDbContextLocator> : PrivateSqlRepository, ISqlRepository<TDbContextLocator>
    where TDbContextLocator : class, IDbContextLocator
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    public SqlRepository(IServiceProvider serviceProvider) : base(typeof(TDbContextLocator), serviceProvider)
    {
    }
}

/// <summary>
/// 私有 Sql 仓储
/// </summary>
public partial class PrivateSqlRepository : IPrivateSqlRepository
{
    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dbContextLocator"></param>
    /// <param name="serviceProvider">服务提供器</param>
    public PrivateSqlRepository(Type dbContextLocator, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        // 解析数据库上下文
        var dbContextResolve = serviceProvider.GetService<Func<Type, IScoped, DbContext>>();
        var dbContext = dbContextResolve(dbContextLocator, default);
        DynamicContext = Context = dbContext;

        // 初始化数据库相关数据
        Database = Context.Database;
    }

    /// <summary>
    /// 数据库上下文
    /// </summary>
    public virtual DbContext Context { get; }

    /// <summary>
    /// 动态数据库上下文
    /// </summary>
    public virtual dynamic DynamicContext { get; }

    /// <summary>
    /// 数据库操作对象
    /// </summary>
    public virtual DatabaseFacade Database { get; }

    /// <summary>
    /// 切换仓储
    /// </summary>
    /// <typeparam name="TChangeDbContextLocator">数据库上下文定位器</typeparam>
    /// <returns>仓储</returns>
    public virtual ISqlRepository<TChangeDbContextLocator> Change<TChangeDbContextLocator>()
         where TChangeDbContextLocator : class, IDbContextLocator
    {
        return _serviceProvider.GetService<ISqlRepository<TChangeDbContextLocator>>();
    }

    /// <summary>
    /// 解析服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public virtual TService GetService<TService>()
        where TService : class
    {
        return _serviceProvider.GetService<TService>();
    }

    /// <summary>
    /// 解析服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public virtual TService GetRequiredService<TService>()
        where TService : class
    {
        return _serviceProvider.GetRequiredService<TService>();
    }

    /// <summary>
    /// 将仓储约束为特定仓储
    /// </summary>
    /// <typeparam name="TRestrainRepository">特定仓储</typeparam>
    /// <returns>TRestrainRepository</returns>
    public virtual TRestrainRepository Constraint<TRestrainRepository>()
        where TRestrainRepository : class, IPrivateRootRepository
    {
        var type = typeof(TRestrainRepository);
        if (!type.IsInterface || typeof(IPrivateRootRepository) == type || type.Name.Equals(nameof(IRepository)) || (type.IsGenericType && type.GetGenericTypeDefinition().Name.Equals(nameof(IRepository))))
        {
            throw new InvalidCastException("Invalid type conversion.");
        }

        return this as TRestrainRepository;
    }

    /// <summary>
    /// 确保工作单元（事务）可用
    /// </summary>
    public virtual void EnsureTransaction()
    {
        var httpContext = App.HttpContext;

        // 如果请求上下文为空，则跳过
        if (httpContext == null) return;

        // 获取数据库上下文
        var dbContextPool = httpContext.RequestServices.GetService<IDbContextPool>();
        if (dbContextPool == null) return;

        // 追加上下文
        dbContextPool.AddToPool(Context);
        // 开启事务
        dbContextPool.BeginTransaction();
    }
}