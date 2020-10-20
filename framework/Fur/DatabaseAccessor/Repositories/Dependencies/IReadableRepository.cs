// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
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
    public interface IReadableRepository<TEntity, TDbContextLocator> : IRepositoryDependency
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
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
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        TEntity Single(bool noTracking = false);

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        TEntity SingleOrDefault(bool noTracking = false);

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<TEntity> SingleAsync(bool noTracking = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        TEntity First(bool noTracking = false);

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        TEntity First(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        TEntity FirstOrDefault(bool noTracking = false);

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<TEntity> FirstAsync(bool noTracking = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        TEntity Last(bool noTracking = false);

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        TEntity Last(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        TEntity LastOrDefault(bool noTracking = false);

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = false);

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<TEntity> LastAsync(bool noTracking = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式查询多条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据表达式查询多条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据条件执行表达式查询多条记录
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        IQueryable<TEntity> Where(bool condition, Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据条件执行表达式查询多条记录
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        IQueryable<TEntity> Where(bool condition, Expression<Func<TEntity, int, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

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
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>>[] predicates, bool noTracking = true, bool ignoreQueryFilters = false);

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
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>>[] predicates, bool noTracking = true, bool ignoreQueryFilters = false);

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
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        IQueryable<TEntity> Where((bool condition, Expression<Func<TEntity, bool>> expression)[] conditionPredicates, bool noTracking = true, bool ignoreQueryFilters = false);

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
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        IQueryable<TEntity> Where((bool condition, Expression<Func<TEntity, int, bool>> expression)[] conditionPredicates, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 加载关联数据
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        IIncludableQueryable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据条件加载关联数据
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        IIncludableQueryable<TEntity, TProperty> Include<TProperty>(bool condition, Expression<Func<TEntity, TProperty>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>bool</returns>
        bool Any(bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据表达式判断记录是否存在
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>bool</returns>
        bool Any(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>bool</returns>
        Task<bool> AnyAsync(bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式判断记录是否存在
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>bool</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式判断记录是否全部满足条件
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>bool</returns>
        bool All(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据表达式判断记录是否全部满足条件
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>bool</returns>
        Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>int</returns>
        int Count(bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据表达式查询记录条数
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>int</returns>
        int Count(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>int</returns>
        Task<int> CountAsync(bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式查询记录条数
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>int</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查看最小记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>实体</returns>
        TEntity Min(bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据表达式查看最小值
        /// </summary>
        /// <typeparam name="TResult">最小值类型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>TResult</returns>
        TResult Min<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 查看最小记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>实体</returns>
        Task<TEntity> MinAsync(bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式查看最小值
        /// </summary>
        /// <typeparam name="TResult">最小值类型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>TResult</returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查看最大记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>实体</returns>
        TEntity Max(bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 根据表达式查看最小值
        /// </summary>
        /// <typeparam name="TResult">最小值类型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>TResult</returns>
        TResult Max<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 查看最大记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>实体</returns>
        Task<TEntity> MaxAsync(bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式查看最小值
        /// </summary>
        /// <typeparam name="TResult">最小值类型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>TResult</returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>IQueryable<TEntity></returns>
        IQueryable<TEntity> AsQueryable(bool noTracking = true);

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>IQueryable<TEntity></returns>
        IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>IQueryable<TEntity></returns>
        IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, int, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>List<TEntity></returns>
        List<TEntity> AsEnumerable(bool noTracking = true);

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>List<TEntity></returns>
        List<TEntity> AsEnumerable(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>List<TEntity></returns>
        List<TEntity> AsEnumerable(Expression<Func<TEntity, int, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false);

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>List<TEntity></returns>
        Task<List<TEntity>> AsAsyncEnumerable(bool noTracking = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>List<TEntity></returns>
        Task<List<TEntity>> AsAsyncEnumerable(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>List<TEntity></returns>
        Task<List<TEntity>> AsAsyncEnumerable(Expression<Func<TEntity, int, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);
    }
}