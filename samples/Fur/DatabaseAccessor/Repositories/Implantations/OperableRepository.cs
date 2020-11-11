using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可操作仓储分部类
    /// </summary>
    public partial class EFCoreRepository<TEntity, TDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 新增或更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdate(TEntity entity)
        {
            return IsKeySet(entity) ? Update(entity) : Insert(entity);
        }

        /// <summary>
        /// 新增或更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateAsync(entity) : InsertAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条记录并立即执行
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateNow(TEntity entity)
        {
            return IsKeySet(entity) ? UpdateNow(entity) : InsertNow(entity);
        }

        /// <summary>
        /// 新增或更新一条记录并立即执行
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateNow(TEntity entity, bool acceptAllChangesOnSuccess)
        {
            return IsKeySet(entity) ? UpdateNow(entity, acceptAllChangesOnSuccess) : InsertNow(entity, acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增或更新一条记录并立即执行
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateNowAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateNowAsync(entity, cancellationToken) : InsertNowAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条记录并立即执行
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateNowAsync(entity, acceptAllChangesOnSuccess, cancellationToken) : InsertNowAsync(entity, acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateInclude(TEntity entity, params string[] propertyNames)
        {
            return IsKeySet(entity) ? UpdateInclude(entity, propertyNames) : Insert(entity);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateInclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            return IsKeySet(entity) ? UpdateInclude(entity, propertyPredicates) : Insert(entity);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateInclude(TEntity entity, IEnumerable<string> propertyNames)
        {
            return IsKeySet(entity) ? UpdateInclude(entity, propertyNames) : Insert(entity);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateInclude(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates)
        {
            return IsKeySet(entity) ? UpdateInclude(entity, propertyPredicates) : Insert(entity);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeAsync(TEntity entity, params string[] propertyNames)
        {
            return IsKeySet(entity) ? UpdateIncludeAsync(entity, propertyNames) : InsertAsync(entity);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeAsync(TEntity entity, string[] propertyNames, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateIncludeAsync(entity, propertyNames) : InsertAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            return IsKeySet(entity) ? UpdateIncludeAsync(entity, propertyPredicates) : InsertAsync(entity);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateIncludeAsync(entity, propertyPredicates) : InsertAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateIncludeAsync(entity, propertyNames) : InsertAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateIncludeAsync(entity, propertyPredicates) : InsertAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, params string[] propertyNames)
        {
            return IsKeySet(entity) ? UpdateIncludeNow(entity, propertyNames) : InsertNow(entity);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess)
        {
            return IsKeySet(entity) ? UpdateIncludeNow(entity, propertyNames, acceptAllChangesOnSuccess) : InsertNow(entity, acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            return IsKeySet(entity) ? UpdateIncludeNow(entity, propertyPredicates) : InsertNow(entity);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess)
        {
            return IsKeySet(entity) ? UpdateIncludeNow(entity, propertyPredicates, acceptAllChangesOnSuccess) : InsertNow(entity, acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, IEnumerable<string> propertyNames)
        {
            return IsKeySet(entity) ? UpdateIncludeNow(entity, propertyNames) : InsertNow(entity);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess)
        {
            return IsKeySet(entity) ? UpdateIncludeNow(entity, propertyNames, acceptAllChangesOnSuccess) : InsertNow(entity, acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates)
        {
            return IsKeySet(entity) ? UpdateIncludeNow(entity, propertyPredicates) : InsertNow(entity);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateIncludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess)
        {
            return IsKeySet(entity) ? UpdateIncludeNow(entity, propertyPredicates, acceptAllChangesOnSuccess) : InsertNow(entity, acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, params string[] propertyNames)
        {
            return IsKeySet(entity) ? UpdateIncludeNowAsync(entity, propertyNames) : InsertNowAsync(entity);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, string[] propertyNames, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateIncludeNowAsync(entity, propertyNames, cancellationToken) : InsertNowAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateIncludeNowAsync(entity, propertyNames, acceptAllChangesOnSuccess, cancellationToken) : InsertNowAsync(entity, acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            return IsKeySet(entity) ? UpdateIncludeNowAsync(entity, propertyPredicates) : InsertNowAsync(entity);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateIncludeNowAsync(entity, propertyPredicates, cancellationToken) : InsertNowAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateIncludeNowAsync(entity, propertyPredicates, acceptAllChangesOnSuccess, cancellationToken) : InsertNowAsync(entity, acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateIncludeNowAsync(entity, propertyNames, cancellationToken) : InsertNowAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateIncludeNowAsync(entity, propertyNames, acceptAllChangesOnSuccess, cancellationToken) : InsertNowAsync(entity, acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateIncludeNowAsync(entity, propertyPredicates, cancellationToken) : InsertNowAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateIncludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateIncludeNowAsync(entity, propertyPredicates, acceptAllChangesOnSuccess, cancellationToken) : InsertNowAsync(entity, acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateExclude(TEntity entity, params string[] propertyNames)
        {
            return IsKeySet(entity) ? UpdateExclude(entity, propertyNames) : Insert(entity);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateExclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            return IsKeySet(entity) ? UpdateExclude(entity, propertyPredicates) : Insert(entity);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateExclude(TEntity entity, IEnumerable<string> propertyNames)
        {
            return IsKeySet(entity) ? UpdateExclude(entity, propertyNames) : Insert(entity);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateExclude(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates)
        {
            return IsKeySet(entity) ? UpdateExclude(entity, propertyPredicates) : Insert(entity);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeAsync(TEntity entity, params string[] propertyNames)
        {
            return IsKeySet(entity) ? UpdateExcludeAsync(entity, propertyNames) : InsertAsync(entity);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeAsync(TEntity entity, string[] propertyNames, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateExcludeAsync(entity, propertyNames) : InsertAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            return IsKeySet(entity) ? UpdateExcludeAsync(entity, propertyPredicates) : InsertAsync(entity);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateExcludeAsync(entity, propertyPredicates) : InsertAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateExcludeAsync(entity, propertyNames) : InsertAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateExcludeAsync(entity, propertyPredicates) : InsertAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, params string[] propertyNames)
        {
            return IsKeySet(entity) ? UpdateExcludeNow(entity, propertyNames) : InsertNow(entity);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess)
        {
            return IsKeySet(entity) ? UpdateExcludeNow(entity, propertyNames, acceptAllChangesOnSuccess) : InsertNow(entity, acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            return IsKeySet(entity) ? UpdateExcludeNow(entity, propertyPredicates) : InsertNow(entity);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess)
        {
            return IsKeySet(entity) ? UpdateExcludeNow(entity, propertyPredicates, acceptAllChangesOnSuccess) : InsertNow(entity, acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, IEnumerable<string> propertyNames)
        {
            return IsKeySet(entity) ? UpdateExcludeNow(entity, propertyNames) : InsertNow(entity);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess)
        {
            return IsKeySet(entity) ? UpdateExcludeNow(entity, propertyNames, acceptAllChangesOnSuccess) : InsertNow(entity, acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates)
        {
            return IsKeySet(entity) ? UpdateExcludeNow(entity, propertyPredicates) : InsertNow(entity);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateExcludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess)
        {
            return IsKeySet(entity) ? UpdateExcludeNow(entity, propertyPredicates, acceptAllChangesOnSuccess) : InsertNow(entity, acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, params string[] propertyNames)
        {
            return IsKeySet(entity) ? UpdateExcludeNowAsync(entity, propertyNames) : InsertNowAsync(entity);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, string[] propertyNames, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateExcludeNowAsync(entity, propertyNames, cancellationToken) : InsertNowAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateExcludeNowAsync(entity, propertyNames, acceptAllChangesOnSuccess, cancellationToken) : InsertNowAsync(entity, acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            return IsKeySet(entity) ? UpdateExcludeNowAsync(entity, propertyPredicates) : InsertNowAsync(entity);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateExcludeNowAsync(entity, propertyPredicates, cancellationToken) : InsertNowAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateExcludeNowAsync(entity, propertyPredicates, acceptAllChangesOnSuccess, cancellationToken) : InsertNowAsync(entity, acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateExcludeNowAsync(entity, propertyNames, cancellationToken) : InsertNowAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateExcludeNowAsync(entity, propertyNames, acceptAllChangesOnSuccess, cancellationToken) : InsertNowAsync(entity, acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateExcludeNowAsync(entity, propertyPredicates, cancellationToken) : InsertNowAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条排除特定属性记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateExcludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateExcludeNowAsync(entity, propertyPredicates, acceptAllChangesOnSuccess, cancellationToken) : InsertNowAsync(entity, acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}