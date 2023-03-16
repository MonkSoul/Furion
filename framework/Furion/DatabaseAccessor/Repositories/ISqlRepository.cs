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

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 操作仓储接口
/// </summary>
public interface ISqlRepository : ISqlRepository<MasterDbContextLocator>
    , ISqlExecutableRepository
    , ISqlReaderRepository
{
}

/// <summary>
/// Sql 操作仓储接口
/// </summary>
/// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
public interface ISqlRepository<TDbContextLocator> : IPrivateSqlRepository
    , ISqlExecutableRepository<TDbContextLocator>
    , ISqlReaderRepository<TDbContextLocator>
   where TDbContextLocator : class, IDbContextLocator
{
}

/// <summary>
/// 私有 Sql 仓储
/// </summary>
public interface IPrivateSqlRepository : IPrivateSqlExecutableRepository
    , IPrivateSqlReaderRepository
    , IPrivateRootRepository
{
    /// <summary>
    /// 数据库操作对象
    /// </summary>
    DatabaseFacade Database { get; }

    /// <summary>
    /// 数据库上下文
    /// </summary>
    DbContext Context { get; }

    /// <summary>
    /// 动态数据库上下文
    /// </summary>
    dynamic DynamicContext { get; }

    /// <summary>
    /// 切换仓储
    /// </summary>
    /// <typeparam name="TChangeDbContextLocator">数据库上下文定位器</typeparam>
    /// <returns>仓储</returns>
    ISqlRepository<TChangeDbContextLocator> Change<TChangeDbContextLocator>()
        where TChangeDbContextLocator : class, IDbContextLocator;

    /// <summary>
    /// 解析服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    TService GetService<TService>()
        where TService : class;

    /// <summary>
    /// 解析服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    TService GetRequiredService<TService>()
        where TService : class;

    /// <summary>
    /// 将仓储约束为特定仓储
    /// </summary>
    /// <typeparam name="TRestrainRepository">特定仓储</typeparam>
    /// <returns>TRestrainRepository</returns>
    TRestrainRepository Constraint<TRestrainRepository>()
        where TRestrainRepository : class, IPrivateRootRepository;

    /// <summary>
    /// 确保工作单元（事务）可用
    /// </summary>
    void EnsureTransaction();
}