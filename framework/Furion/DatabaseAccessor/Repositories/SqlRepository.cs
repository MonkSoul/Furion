// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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
        var dbContext = dbContextResolve?.Invoke(dbContextLocator, default);
        DynamicContext = Context = dbContext;

        // 初始化数据库相关数据
        Database = Context?.Database;
    }

    /// <summary>
    /// 数据库上下文
    /// </summary>
    public virtual DbContext Context { get; internal set; }

    /// <summary>
    /// 动态数据库上下文
    /// </summary>
    public virtual dynamic DynamicContext { get; internal set; }

    /// <summary>
    /// 数据库操作对象
    /// </summary>
    public virtual DatabaseFacade Database { get; internal set; }

    /// <summary>
    /// 标记是否是未释放的上下文
    /// </summary>
    internal bool UndisposedContext { get; set; } = false;

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

    /// <summary>
    /// 释放上下文
    /// </summary>
    public void Dispose()
    {
        if (UndisposedContext)
        {
            Context?.Dispose();
        }
    }
}