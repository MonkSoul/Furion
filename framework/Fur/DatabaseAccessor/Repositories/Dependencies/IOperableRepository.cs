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

using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可操作性仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IOperableRepository<TEntity> : IOperableRepository<TEntity, MasterDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
    {
    }

    /// <summary>
    /// 可操作性仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public partial interface IOperableRepository<TEntity, TDbContextLocator> : IRepositoryDependency
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 新增或更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdate(TEntity entity);

        /// <summary>
        /// 新增或更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条记录并立即执行
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateNow(TEntity entity);

        /// <summary>
        /// 新增或更新一条记录并立即执行
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateNow(TEntity entity, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 新增或更新一条记录并立即执行
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateNowAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条记录并立即执行
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateInclude(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateInclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates);

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateInclude(TEntity entity, IEnumerable<string> propertyNames);

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateInclude(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates);

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeAsync(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeAsync(TEntity entity, string[] propertyNames, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates);

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, IEnumerable<string> propertyNames);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, string[] propertyNames, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateExclude(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateExclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates);

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateExclude(TEntity entity, IEnumerable<string> propertyNames);

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateExclude(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates);

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeAsync(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeAsync(TEntity entity, string[] propertyNames, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates);

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, IEnumerable<string> propertyNames);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, string[] propertyNames, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行 Sql 返回 IQueryable
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>IQueryable<TEntity></returns>
        IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters);

        /// <summary>
        /// 执行 Sql 返回 IQueryable
        /// </summary>
        /// <remarks>
        /// 支持字符串内插语法
        /// </remarks>
        /// <param name="sql">sql 语句</param>
        /// <returns>IQueryable<TEntity></returns>
        IQueryable<TEntity> FromSqlInterpolated(FormattableString sql);
    }
}