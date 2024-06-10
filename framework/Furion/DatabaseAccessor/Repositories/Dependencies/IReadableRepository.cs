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

using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 可读仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IReadableRepository<TEntity> : IReadableRepository<TEntity, MasterDbContextLocator>
    where TEntity : class, IPrivateEntity, new()
{
}

/// <summary>
/// 可读仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
public interface IReadableRepository<TEntity, TDbContextLocator> : IPrivateReadableRepository<TEntity>
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator : class, IDbContextLocator
{
}

/// <summary>
/// 可读仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IPrivateReadableRepository<TEntity> : IPrivateRootRepository
    where TEntity : class, IPrivateEntity, new()
{
    /// <summary>
    /// 根据键查询一条记录
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>数据库中的实体</returns>
    TEntity Find(object key);

    /// <summary>
    /// 根据多个键查询一条记录
    /// </summary>
    /// <param name="keyValues">多个键</param>
    /// <returns>数据库中的实体</returns>
    TEntity Find(params object[] keyValues);

    /// <summary>
    /// 根据键查询一条记录
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库实体</returns>
    Task<TEntity> FindAsync(object key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据多个键查询一条记录
    /// </summary>
    /// <param name="keyValues">多个键</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> FindAsync(params object[] keyValues);

    /// <summary>
    /// 根据多个键查询一条记录
    /// </summary>
    /// <param name="keyValues">多个键</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据键查询一条记录
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>数据库中的实体</returns>
    TEntity FindOrDefault(object key);

    /// <summary>
    /// 根据多个键查询一条记录
    /// </summary>
    /// <param name="keyValues">多个键</param>
    /// <returns>数据库中的实体</returns>
    TEntity FindOrDefault(params object[] keyValues);

    /// <summary>
    /// 根据键查询一条记录
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> FindOrDefaultAsync(object key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据多个键查询一条记录
    /// </summary>
    /// <param name="keyValues">多个键</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> FindOrDefaultAsync(params object[] keyValues);

    /// <summary>
    /// 根据多个键查询一条记录
    /// </summary>
    /// <param name="keyValues">多个键</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> FindOrDefaultAsync(object[] keyValues, CancellationToken cancellationToken = default);

    /// <summary>
    /// 查询一条记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>数据库中的实体</returns>
    TEntity Single(bool? tracking = null);

    /// <summary>
    /// 根据表达式查询一条记录
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>数据库中的实体</returns>
    TEntity Single(Expression<Func<TEntity, bool>> predicate, bool? tracking = null);

    /// <summary>
    /// 查询一条记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>数据库中的实体</returns>
    TEntity SingleOrDefault(bool? tracking = null);

    /// <summary>
    /// 根据表达式查询一条记录
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>数据库中的实体</returns>
    TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool? tracking = null);

    /// <summary>
    /// 查询一条记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> SingleAsync(bool? tracking = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据表达式查询一条记录
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 查询一条记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> SingleOrDefaultAsync(bool? tracking = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据表达式查询一条记录
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 查询一条记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>数据库中的实体</returns>
    TEntity First(bool? tracking = null);

    /// <summary>
    /// 根据表达式查询一条记录
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>数据库中的实体</returns>
    TEntity First(Expression<Func<TEntity, bool>> predicate, bool? tracking = null);

    /// <summary>
    /// 查询一条记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>数据库中的实体</returns>
    TEntity FirstOrDefault(bool? tracking = null);

    /// <summary>
    /// 根据表达式查询一条记录
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>数据库中的实体</returns>
    TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool? tracking = null);

    /// <summary>
    /// 查询一条记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> FirstAsync(bool? tracking = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据表达式查询一条记录
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 查询一条记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> FirstOrDefaultAsync(bool? tracking = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据表达式查询一条记录
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 查询一条记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>数据库中的实体</returns>
    TEntity Last(bool? tracking = null);

    /// <summary>
    /// 根据表达式查询一条记录
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>数据库中的实体</returns>
    TEntity Last(Expression<Func<TEntity, bool>> predicate, bool? tracking = null);

    /// <summary>
    /// 查询一条记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>数据库中的实体</returns>
    TEntity LastOrDefault(bool? tracking = null);

    /// <summary>
    /// 根据表达式查询一条记录
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>数据库中的实体</returns>
    TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, bool? tracking = null);

    /// <summary>
    /// 查询一条记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> LastAsync(bool? tracking = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据表达式查询一条记录
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 查询一条记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> LastOrDefaultAsync(bool? tracking = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据表达式查询一条记录
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>数据库中的实体</returns>
    Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据表达式查询多条记录
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>数据库中的多个实体</returns>
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 根据表达式查询多条记录
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>数据库中的多个实体</returns>
    IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 根据条件执行表达式查询多条记录
    /// </summary>
    /// <param name="condition">条件</param>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>数据库中的多个实体</returns>
    IQueryable<TEntity> Where(bool condition, Expression<Func<TEntity, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 根据条件执行表达式查询多条记录
    /// </summary>
    /// <param name="condition">条件</param>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>数据库中的多个实体</returns>
    IQueryable<TEntity> Where(bool condition, Expression<Func<TEntity, int, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 根据条件执行表达式查询多条记录
    /// </summary>
    /// <param name="predicates">表达式集合</param>
    /// <returns>数据库中的多个实体</returns>
    IQueryable<TEntity> Where(params Expression<Func<TEntity, bool>>[] predicates);

    /// <summary>
    /// 根据条件执行表达式查询多条记录
    /// </summary>
    /// <param name="predicates">表达式集合</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>数据库中的多个实体</returns>
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>>[] predicates, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 根据条件执行表达式查询多条记录
    /// </summary>
    /// <param name="predicates">表达式集合</param>
    /// <returns>数据库中的多个实体</returns>
    IQueryable<TEntity> Where(params Expression<Func<TEntity, int, bool>>[] predicates);

    /// <summary>
    /// 根据条件执行表达式查询多条记录
    /// </summary>
    /// <param name="predicates">表达式集合</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>数据库中的多个实体</returns>
    IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>>[] predicates, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 根据条件执行表达式查询多条记录
    /// </summary>
    /// <param name="conditionPredicates">条件表达式集合</param>
    /// <returns>数据库中的多个实体</returns>
    IQueryable<TEntity> Where(params (bool condition, Expression<Func<TEntity, bool>> expression)[] conditionPredicates);

    /// <summary>
    /// 根据条件执行表达式查询多条记录
    /// </summary>
    /// <param name="conditionPredicates">条件表达式集合</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>数据库中的多个实体</returns>
    IQueryable<TEntity> Where((bool condition, Expression<Func<TEntity, bool>> expression)[] conditionPredicates, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 根据条件执行表达式查询多条记录
    /// </summary>
    /// <param name="conditionPredicates">条件表达式集合</param>
    /// <returns>数据库中的多个实体</returns>
    IQueryable<TEntity> Where(params (bool condition, Expression<Func<TEntity, int, bool>> expression)[] conditionPredicates);

    /// <summary>
    /// 根据条件执行表达式查询多条记录
    /// </summary>
    /// <param name="conditionPredicates">条件表达式集合</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>数据库中的多个实体</returns>
    IQueryable<TEntity> Where((bool condition, Expression<Func<TEntity, int, bool>> expression)[] conditionPredicates, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 加载关联数据
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>数据库中的多个实体</returns>
    IIncludableQueryable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 根据条件加载关联数据
    /// </summary>
    /// <param name="condition">条件</param>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>数据库中的多个实体</returns>
    IQueryable<TEntity> Include<TProperty>(bool condition, Expression<Func<TEntity, TProperty>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 判断记录是否存在
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>bool</returns>
    bool Any(bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 根据表达式判断记录是否存在
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>bool</returns>
    bool Any(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 判断记录是否存在
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>bool</returns>
    Task<bool> AnyAsync(bool? tracking = null, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据表达式判断记录是否存在
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>bool</returns>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据表达式判断记录是否全部满足条件
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>bool</returns>
    bool All(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 根据表达式判断记录是否全部满足条件
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>bool</returns>
    Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 查看记录条数
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>int</returns>
    int Count(bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 根据表达式查询记录条数
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>int</returns>
    int Count(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 查看记录条数
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>int</returns>
    Task<int> CountAsync(bool? tracking = null, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据表达式查询记录条数
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>int</returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 查看最小记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>实体</returns>
    TEntity Min(bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 根据表达式查看最小值
    /// </summary>
    /// <typeparam name="TResult">最小值类型</typeparam>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>TResult</returns>
    TResult Min<TResult>(Expression<Func<TEntity, TResult>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 查看最小记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>实体</returns>
    Task<TEntity> MinAsync(bool? tracking = null, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据表达式查看最小值
    /// </summary>
    /// <typeparam name="TResult">最小值类型</typeparam>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>TResult</returns>
    Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> predicate, bool? tracking = null, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 查看最大记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>实体</returns>
    TEntity Max(bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 根据表达式查看最大值
    /// </summary>
    /// <typeparam name="TResult">最大值类型</typeparam>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>TResult</returns>
    TResult Max<TResult>(Expression<Func<TEntity, TResult>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 查看最大记录
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>实体</returns>
    Task<TEntity> MaxAsync(bool? tracking = null, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据表达式查看最大值
    /// </summary>
    /// <typeparam name="TResult">最大值类型</typeparam>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>TResult</returns>
    Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> predicate, bool? tracking = null, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 构建查询分析器
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>IQueryable{TEntity}</returns>
    IQueryable<TEntity> AsQueryable(bool? tracking = null);

    /// <summary>
    /// 构建查询分析器
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>IQueryable{TEntity}</returns>
    IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 构建查询分析器
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>IQueryable{TEntity}</returns>
    IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, int, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>IEnumerable{TEntity}</returns>
    IEnumerable<TEntity> AsEnumerable(bool? tracking = null);

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>List{TEntity}</returns>
    IEnumerable<TEntity> AsEnumerable(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>IEnumerable{TEntity}</returns>
    IEnumerable<TEntity> AsEnumerable(Expression<Func<TEntity, int, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <param name="tracking">是否跟踪实体</param>
    /// <returns>List{TEntity}</returns>
    IAsyncEnumerable<TEntity> AsAsyncEnumerable(bool? tracking = null);

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>IAsyncEnumerable{TEntity}</returns>
    IAsyncEnumerable<TEntity> AsAsyncEnumerable(Expression<Func<TEntity, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <param name="predicate">表达式</param>
    /// <param name="tracking">是否跟踪实体</param>
    /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
    /// <returns>IAsyncEnumerable{TEntity}</returns>
    IAsyncEnumerable<TEntity> AsAsyncEnumerable(Expression<Func<TEntity, int, bool>> predicate, bool? tracking = null, bool ignoreQueryFilters = false);

    /// <summary>
    /// 执行 Sql 返回 IQueryable
    /// </summary>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>IQueryable{TEntity}</returns>
    IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters);

    /// <summary>
    /// 执行 Sql 返回 IQueryable
    /// </summary>
    /// <remarks>
    /// 支持字符串内插语法
    /// </remarks>
    /// <param name="sql">sql 语句</param>
    /// <returns>IQueryable{TEntity}</returns>
    IQueryable<TEntity> FromSqlInterpolated(FormattableString sql);
}