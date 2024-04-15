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