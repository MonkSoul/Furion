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