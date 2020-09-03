// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Fur.FriendlyException;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可写仓储分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库实体定位器</typeparam>
    public partial class EFCoreRepository<TEntity, TDbContextLocator>
        where TEntity : class, IEntityBase, new()
        where TDbContextLocator : class, IDbContextLocator, new()
    {
        /// <summary>
        /// 未查询到数据异常消息
        /// </summary>
        private const string NotFoundErrorMessage = "Sequence contains no elements";

        /// <summary>
        /// 根据键查询一条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity Find(object key)
        {
            var entity = FindOrDefault(key) ?? throw Oops.Oh(NotFoundErrorMessage, typeof(InvalidOperationException));
            return entity;
        }

        /// <summary>
        /// 根据多个键查询一条记录
        /// </summary>
        /// <param name="keyValues">多个键</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity Find(params object[] keyValues)
        {
            var entity = FindOrDefault(keyValues) ?? throw Oops.Oh(NotFoundErrorMessage, typeof(InvalidOperationException));
            return entity;
        }

        /// <summary>
        /// 根据键查询一条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库实体</returns>
        public virtual async Task<TEntity> FindAsync(object key, CancellationToken cancellationToken = default)
        {
            var entity = await FindOrDefaultAsync(key, cancellationToken);
            return entity ?? throw Oops.Oh(NotFoundErrorMessage, typeof(InvalidOperationException));
        }

        /// <summary>
        /// 根据多个键查询一条记录
        /// </summary>
        /// <param name="keyValues">多个键</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            var entity = await FindOrDefaultAsync(keyValues);
            return entity ?? throw Oops.Oh(NotFoundErrorMessage, typeof(InvalidOperationException));
        }

        /// <summary>
        /// 根据多个键查询一条记录
        /// </summary>
        /// <param name="keyValues">多个键</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var entity = await FindOrDefaultAsync(keyValues, cancellationToken);
            return entity ?? throw Oops.Oh(NotFoundErrorMessage, typeof(InvalidOperationException));
        }

        /// <summary>
        /// 根据键查询一条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity FindOrDefault(object key)
        {
            return Entities.Find(key);
        }

        /// <summary>
        /// 根据多个键查询一条记录
        /// </summary>
        /// <param name="keyValues">多个键</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity FindOrDefault(params object[] keyValues)
        {
            return Entities.Find(keyValues);
        }

        /// <summary>
        /// 根据键查询一条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<TEntity> FindOrDefaultAsync(object key, CancellationToken cancellationToken = default)
        {
            var entity = await Entities.FindAsync(new object[] { key }, cancellationToken);
            return entity;
        }

        /// <summary>
        /// 根据多个键查询一条记录
        /// </summary>
        /// <param name="keyValues">多个键</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<TEntity> FindOrDefaultAsync(params object[] keyValues)
        {
            var entity = await Entities.FindAsync(keyValues);
            return entity;
        }

        /// <summary>
        /// 根据多个键查询一条记录
        /// </summary>
        /// <param name="keyValues">多个键</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<TEntity> FindOrDefaultAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var entity = await Entities.FindAsync(keyValues, cancellationToken);
            return entity;
        }

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity Single(bool noTracking = false)
        {
            return AsQueryable(noTracking).Single();
        }

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            return AsQueryable(noTracking).Single(predicate);
        }

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity SingleOrDefault(bool noTracking = false)
        {
            return AsQueryable(noTracking).SingleOrDefault();
        }

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            return AsQueryable(noTracking).SingleOrDefault(predicate);
        }

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<TEntity> SingleAsync(bool noTracking = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking).SingleAsync(cancellationToken);
        }

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking).SingleAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity First(bool noTracking = false)
        {
            return AsQueryable(noTracking).First();
        }

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity First(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            return AsQueryable(noTracking).First(predicate);
        }

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity FirstOrDefault(bool noTracking = false)
        {
            return AsQueryable(noTracking).FirstOrDefault();
        }

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            return AsQueryable(noTracking).FirstOrDefault(predicate);
        }

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<TEntity> FirstAsync(bool noTracking = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking).FirstAsync(cancellationToken);
        }

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking).FirstAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity Last(bool noTracking = false)
        {
            return AsQueryable(noTracking).Last();
        }

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity Last(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            return AsQueryable(noTracking).Last(predicate);
        }

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity LastOrDefault(bool noTracking = false)
        {
            return AsQueryable(noTracking).LastOrDefault();
        }

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = false)
        {
            return AsQueryable(noTracking).LastOrDefault(predicate);
        }

        /// <summary>
        /// 查询一条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<TEntity> LastAsync(bool noTracking = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking).LastAsync(cancellationToken);
        }

        /// <summary>
        /// 根据表达式查询一条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking).LastAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 查询多条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IQueryable<TEntity> Filter(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery);
        }

        /// <summary>
        /// 根据表达式查询多条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return AsQueryable(predicate);
        }

        /// <summary>
        /// 查询多条记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual Task<List<TEntity>> FilterAsync(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 根据表达式查询多条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(predicate, noTracking, ignoreQueryFilters, asSplitQuery).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 分页查询多条记录
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual PagedList<TEntity> PagedFilter(int pageIndex = 1, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).ToPagedList(pageIndex, pageSize);
        }

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
        public virtual PagedList<TEntity> PagedFilter(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return AsQueryable(predicate, noTracking, ignoreQueryFilters, asSplitQuery).ToPagedList(pageSize, pageIndex);
        }

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
        public virtual Task<PagedList<TEntity>> PagedFilterAsync(int pageIndex = 1, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

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
        public virtual Task<PagedList<TEntity>> PagedFilterAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(predicate, noTracking, ignoreQueryFilters, asSplitQuery).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>bool</returns>
        public virtual bool Any(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).Any();
        }

        /// <summary>
        /// 根据表达式判断记录是否存在
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>bool</returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).Any(predicate);
        }

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>bool</returns>
        public virtual Task<bool> AnyAsync(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).AnyAsync(cancellationToken);
        }

        /// <summary>
        /// 根据表达式判断记录是否存在
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>bool</returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 根据表达式判断记录是否全部满足条件
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>bool</returns>
        public virtual bool All(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).All(predicate);
        }

        /// <summary>
        /// 根据表达式判断记录是否全部满足条件
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>bool</returns>
        public virtual Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).AllAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>int</returns>
        public virtual int Count(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).Count();
        }

        /// <summary>
        /// 根据表达式查询记录条数
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>int</returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).Count(predicate);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>int</returns>
        public virtual Task<int> CountAsync(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).CountAsync(cancellationToken);
        }

        /// <summary>
        /// 根据表达式查询记录条数
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>int</returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).CountAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 查看最小记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>实体</returns>
        public virtual TEntity Min(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).Min();
        }

        /// <summary>
        /// 根据表达式查看最小值
        /// </summary>
        /// <typeparam name="TResult">最小值类型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>TResult</returns>
        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).Min(predicate);
        }

        /// <summary>
        /// 查看最小记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> MinAsync(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).MinAsync(cancellationToken);
        }

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
        public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).MinAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 查看最大记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>实体</returns>
        public virtual TEntity Max(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).Max();
        }

        /// <summary>
        /// 根据表达式查看最小值
        /// </summary>
        /// <typeparam name="TResult">最小值类型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>TResult</returns>
        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).Max(predicate);
        }

        /// <summary>
        /// 查看最大记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> MaxAsync(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).MaxAsync(cancellationToken);
        }

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
        public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).MaxAsync(predicate, cancellationToken);
        }
    }
}