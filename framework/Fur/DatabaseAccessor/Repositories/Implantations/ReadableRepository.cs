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

using Fur.LinqBuilder;
using Microsoft.EntityFrameworkCore;
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
    /// 可写仓储分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public partial class EFCoreRepository<TEntity, TDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 根据键查询一条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity Find(object key)
        {
            var entity = FindOrDefault(key) ?? throw DbHelpers.DataNotFoundException();
            return entity;
        }

        /// <summary>
        /// 根据多个键查询一条记录
        /// </summary>
        /// <param name="keyValues">多个键</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity Find(params object[] keyValues)
        {
            var entity = FindOrDefault(keyValues) ?? throw DbHelpers.DataNotFoundException();
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
            return entity ?? throw DbHelpers.DataNotFoundException();
        }

        /// <summary>
        /// 根据多个键查询一条记录
        /// </summary>
        /// <param name="keyValues">多个键</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            var entity = await FindOrDefaultAsync(keyValues);
            return entity ?? throw DbHelpers.DataNotFoundException();
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
            return entity ?? throw DbHelpers.DataNotFoundException();
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
        /// 根据表达式查询多条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(predicate, noTracking, ignoreQueryFilters);
        }

        /// <summary>
        /// 根据表达式查询多条记录
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(predicate, noTracking, ignoreQueryFilters);
        }

        /// <summary>
        /// 根据条件执行表达式查询多条记录
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IQueryable<TEntity> Where(bool condition, Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).Where(condition, predicate);
        }

        /// <summary>
        /// 根据条件执行表达式查询多条记录
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IQueryable<TEntity> Where(bool condition, Expression<Func<TEntity, int, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, int, bool>>), noTracking, ignoreQueryFilters).Where(condition, predicate);
        }

        /// <summary>
        /// 根据条件执行表达式查询多条记录
        /// </summary>
        /// <param name="predicates">表达式集合</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IQueryable<TEntity> Where(params Expression<Func<TEntity, bool>>[] predicates)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>)).Where(predicates);
        }

        /// <summary>
        /// 根据条件执行表达式查询多条记录
        /// </summary>
        /// <param name="predicates">表达式集合</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>>[] predicates, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).Where(predicates);
        }

        /// <summary>
        /// 根据条件执行表达式查询多条记录
        /// </summary>
        /// <param name="predicates">表达式集合</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IQueryable<TEntity> Where(params Expression<Func<TEntity, int, bool>>[] predicates)
        {
            return AsQueryable(default(Expression<Func<TEntity, int, bool>>)).Where(predicates);
        }

        /// <summary>
        /// 根据条件执行表达式查询多条记录
        /// </summary>
        /// <param name="predicates">表达式集合</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>>[] predicates, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, int, bool>>), noTracking, ignoreQueryFilters).Where(predicates);
        }

        /// <summary>
        /// 根据条件执行表达式查询多条记录
        /// </summary>
        /// <param name="conditionPredicates">条件表达式集合</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IQueryable<TEntity> Where(params (bool condition, Expression<Func<TEntity, bool>> expression)[] conditionPredicates)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>)).Where(conditionPredicates);
        }

        /// <summary>
        /// 根据条件执行表达式查询多条记录
        /// </summary>
        /// <param name="conditionPredicates">条件表达式集合</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IQueryable<TEntity> Where((bool condition, Expression<Func<TEntity, bool>> expression)[] conditionPredicates, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).Where(conditionPredicates);
        }

        /// <summary>
        /// 根据条件执行表达式查询多条记录
        /// </summary>
        /// <param name="conditionPredicates">条件表达式集合</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IQueryable<TEntity> Where(params (bool condition, Expression<Func<TEntity, int, bool>> expression)[] conditionPredicates)
        {
            return AsQueryable(default(Expression<Func<TEntity, int, bool>>)).Where(conditionPredicates);
        }

        /// <summary>
        /// 根据条件执行表达式查询多条记录
        /// </summary>
        /// <param name="conditionPredicates">条件表达式集合</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IQueryable<TEntity> Where((bool condition, Expression<Func<TEntity, int, bool>> expression)[] conditionPredicates, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, int, bool>>), noTracking, ignoreQueryFilters).Where(conditionPredicates);
        }

        /// <summary>
        /// 加载关联数据
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IIncludableQueryable<TEntity, TProperty> Include<TProperty>(Expression<Func<TEntity, TProperty>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).Include(predicate);
        }

        /// <summary>
        /// 根据条件加载关联数据
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>数据库中的多个实体</returns>
        public virtual IIncludableQueryable<TEntity, TProperty> Include<TProperty>(bool condition, Expression<Func<TEntity, TProperty>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).Include(condition, predicate);
        }

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>bool</returns>
        public virtual bool Any(bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).Any();
        }

        /// <summary>
        /// 根据表达式判断记录是否存在
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>bool</returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).Any(predicate);
        }

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>bool</returns>
        public virtual Task<bool> AnyAsync(bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).AnyAsync(cancellationToken);
        }

        /// <summary>
        /// 根据表达式判断记录是否存在
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>bool</returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 根据表达式判断记录是否全部满足条件
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>bool</returns>
        public virtual bool All(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).All(predicate);
        }

        /// <summary>
        /// 根据表达式判断记录是否全部满足条件
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>bool</returns>
        public virtual Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).AllAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>int</returns>
        public virtual int Count(bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).Count();
        }

        /// <summary>
        /// 根据表达式查询记录条数
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>int</returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).Count(predicate);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>int</returns>
        public virtual Task<int> CountAsync(bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).CountAsync(cancellationToken);
        }

        /// <summary>
        /// 根据表达式查询记录条数
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>int</returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).CountAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 查看最小记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>实体</returns>
        public virtual TEntity Min(bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).Min();
        }

        /// <summary>
        /// 根据表达式查看最小值
        /// </summary>
        /// <typeparam name="TResult">最小值类型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>TResult</returns>
        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).Min(predicate);
        }

        /// <summary>
        /// 查看最小记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> MinAsync(bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).MinAsync(cancellationToken);
        }

        /// <summary>
        /// 根据表达式查看最小值
        /// </summary>
        /// <typeparam name="TResult">最小值类型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>TResult</returns>
        public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).MinAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 查看最大记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>实体</returns>
        public virtual TEntity Max(bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).Max();
        }

        /// <summary>
        /// 根据表达式查看最小值
        /// </summary>
        /// <typeparam name="TResult">最小值类型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>TResult</returns>
        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).Max(predicate);
        }

        /// <summary>
        /// 查看最大记录
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> MaxAsync(bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).MaxAsync(cancellationToken);
        }

        /// <summary>
        /// 根据表达式查看最小值
        /// </summary>
        /// <typeparam name="TResult">最小值类型</typeparam>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>TResult</returns>
        public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(default(Expression<Func<TEntity, bool>>), noTracking, ignoreQueryFilters).MaxAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>IQueryable<TEntity></returns>
        public virtual IQueryable<TEntity> AsQueryable(bool noTracking = true)
        {
            return !noTracking ? Entities : DetachedEntities;
        }

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>IQueryable<TEntity></returns>
        public virtual IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var entities = AsQueryable(noTracking);
            if (ignoreQueryFilters) entities = entities.IgnoreQueryFilters();
            if (predicate != null) entities = entities.Where(predicate);

            return entities;
        }

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>IQueryable<TEntity></returns>
        public virtual IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, int, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var entities = AsQueryable(noTracking);
            if (ignoreQueryFilters) entities = entities.IgnoreQueryFilters();
            if (predicate != null) entities = entities.Where(predicate);

            return entities;
        }

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>List<TEntity></returns>
        public virtual List<TEntity> AsEnumerable(bool noTracking = true)
        {
            return AsQueryable(noTracking).ToList();
        }

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>List<TEntity></returns>
        public virtual List<TEntity> AsEnumerable(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(predicate, noTracking, ignoreQueryFilters).ToList();
        }

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <returns>List<TEntity></returns>
        public virtual List<TEntity> AsEnumerable(Expression<Func<TEntity, int, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return AsQueryable(predicate, noTracking, ignoreQueryFilters).ToList();
        }

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>List<TEntity></returns>
        public virtual Task<List<TEntity>> AsAsyncEnumerable(bool noTracking = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>List<TEntity></returns>
        public virtual Task<List<TEntity>> AsAsyncEnumerable(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(predicate, noTracking, ignoreQueryFilters).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>List<TEntity></returns>
        public virtual Task<List<TEntity>> AsAsyncEnumerable(Expression<Func<TEntity, int, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        {
            return AsQueryable(predicate, noTracking, ignoreQueryFilters).ToListAsync(cancellationToken);
        }
    }
}