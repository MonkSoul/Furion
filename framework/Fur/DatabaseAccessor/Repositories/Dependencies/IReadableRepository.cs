// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

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
    public interface IReadableRepository<TEntity>
        : IReadableRepository<TEntity, DbContextLocator>
        , ISqlQueryableRepository<TEntity>
        where TEntity : class, IEntityBase, new()
    {
    }

    /// <summary>
    /// 可读仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库实体定位器</typeparam>
    public interface IReadableRepository<TEntity, TDbContextLocator>
    : ISqlQueryableRepository<TEntity, TDbContextLocator>
    where TEntity : class, IEntityBase, new()
    where TDbContextLocator : class, IDbContextLocator, new()
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
        /// 查询多条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>数据库中的多个实体</returns>
        IQueryable<TEntity> Filter(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);

        /// <summary>
        /// 根据表达式查询多条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>数据库中的多个实体</returns>
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);

        /// <summary>
        /// 查询多条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的多个实体</returns>
        Task<List<TEntity>> FilterAsync(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式查询多条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的多个实体</returns>
        Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页查询多条记录
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>数据库中的多个实体</returns>
        PagedList<TEntity> PagedFilter(int pageIndex = 1, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);

        /// <summary>
        /// 根据表达式分页查询多条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>数据库中的多个实体</returns>
        PagedList<TEntity> PagedFilter(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);

        /// <summary>
        /// 分页查询多条记录
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的多个实体</returns>
        Task<PagedList<TEntity>> PagedFilterAsync(int pageIndex = 1, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式分页查询多条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的多个实体</returns>
        Task<PagedList<TEntity>> PagedFilterAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>bool</returns>
        bool Any(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);

        /// <summary>
        /// 根据表达式判断记录是否存在
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>bool</returns>
        bool Any(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>bool</returns>
        Task<bool> AnyAsync(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式判断记录是否存在
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>bool</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式判断记录是否全部满足条件
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>bool</returns>
        bool All(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);

        /// <summary>
        /// 根据表达式判断记录是否全部满足条件
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>bool</returns>
        Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>int</returns>
        int Count(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);

        /// <summary>
        /// 根据表达式查询记录条数
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>int</returns>
        int Count(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>int</returns>
        Task<int> CountAsync(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式查询记录条数
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>int</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查看最小记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>实体</returns>
        TEntity Min(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);

        /// <summary>
        /// 根据表达式查看最小值
        /// </summary>
        /// <typeparam name="TResult">最小值类型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>TResult</returns>
        TResult Min<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);

        /// <summary>
        /// 查看最小记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>实体</returns>
        Task<TEntity> MinAsync(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式查看最小值
        /// </summary>
        /// <typeparam name="TResult">最小值类型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>TResult</returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查看最大记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>实体</returns>
        TEntity Max(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);

        /// <summary>
        /// 根据表达式查看最小值
        /// </summary>
        /// <typeparam name="TResult">最小值类型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>TResult</returns>
        TResult Max<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);

        /// <summary>
        /// 查看最大记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>实体</returns>
        Task<TEntity> MaxAsync(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式查看最小值
        /// </summary>
        /// <typeparam name="TResult">最小值类型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>TResult</returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default);

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
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>IQueryable<TEntity></returns>
        IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);
    }
}