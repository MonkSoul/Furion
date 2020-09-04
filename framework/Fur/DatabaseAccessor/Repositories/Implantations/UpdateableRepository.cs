// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Fur.FriendlyException;
using Microsoft.EntityFrameworkCore;
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
    /// 可更新仓储分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库实体定位器</typeparam>
    public partial class EFCoreRepository<TEntity, TDbContextLocator>
        where TEntity : class, IEntityBase, new()
        where TDbContextLocator : class, IDbContextLocator, new()
    {
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> Update(TEntity entity)
        {
            return Entities.Update(entity);
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Update(params TEntity[] entities)
        {
            Entities.UpdateRange(entities);
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            Entities.UpdateRange(entities);
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns>Task</returns>
        public virtual Task UpdateAsync(params TEntity[] entities)
        {
            Update(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns>Task</returns>
        public virtual Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            Update(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateNow(TEntity entity)
        {
            var entityEntry = Update(entity);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateNow(TEntity entity, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = Update(entity);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void UpdateNow(params TEntity[] entities)
        {
            Update(entities);
            SaveNow();
        }

        /// <summary>
        /// 更新多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        public virtual void UpdateNow(TEntity[] entities, bool acceptAllChangesOnSuccess)
        {
            Update(entities);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 更新多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void UpdateNow(IEnumerable<TEntity> entities)
        {
            Update(entities);
            SaveNow();
        }

        /// <summary>
        /// 更新多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        public virtual void UpdateNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess)
        {
            Update(entities);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateNowAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateAsync(entity);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateAsync(entity);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns>Task</returns>
        public virtual async Task UpdateNowAsync(params TEntity[] entities)
        {
            await UpdateAsync(entities);
            await SaveNowAsync();
        }

        /// <summary>
        /// 更新多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns></returns>
        public virtual async Task UpdateNowAsync(TEntity[] entities, CancellationToken cancellationToken = default)
        {
            await UpdateAsync(entities);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// 更新多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        public virtual async Task UpdateNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await UpdateAsync(entities);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 更新多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        public virtual async Task UpdateNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await UpdateAsync(entities);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// 更新多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        public virtual async Task UpdateNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await UpdateAsync(entities);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = ChangeEntityState(entity, EntityState.Unchanged);
            foreach (var propertyName in propertyNames)
            {
                EntityPropertyEntry(entity, propertyName).IsModified = true;
            }
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            var entityEntry = ChangeEntityState(entity, EntityState.Unchanged);
            foreach (var propertyPredicate in propertyPredicates)
            {
                EntityPropertyEntry(entity, propertyPredicate).IsModified = true;
            }
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity, IEnumerable<string> propertyNames)
        {
            return UpdateInclude(entity, propertyNames.ToArray());
        }

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates)
        {
            return UpdateInclude(entity, propertyPredicates.ToArray());
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, params string[] propertyNames)
        {
            return Task.FromResult(UpdateInclude(entity, propertyNames));
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            return Task.FromResult(UpdateInclude(entity, propertyPredicates));
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, IEnumerable<string> propertyNames)
        {
            return Task.FromResult(UpdateInclude(entity, propertyNames));
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates)
        {
            return Task.FromResult(UpdateInclude(entity, propertyPredicates));
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = UpdateInclude(entity, propertyNames);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = UpdateInclude(entity, propertyNames);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            var entityEntry = UpdateInclude(entity, propertyPredicates);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = UpdateInclude(entity, propertyPredicates);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<string> propertyNames)
        {
            var entityEntry = UpdateInclude(entity, propertyNames);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = UpdateInclude(entity, propertyNames);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates)
        {
            var entityEntry = UpdateInclude(entity, propertyPredicates);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = UpdateInclude(entity, propertyPredicates);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames);
            await SaveNowAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, string[] propertyNames, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyPredicates);
            await SaveNowAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyPredicates);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyPredicates);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyPredicates);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyPredicates);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = ChangeEntityState(entity, EntityState.Modified);
            foreach (var propertyName in propertyNames)
            {
                EntityPropertyEntry(entity, propertyName).IsModified = false;
            }
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            var entityEntry = ChangeEntityState(entity, EntityState.Modified);
            foreach (var propertyPredicate in propertyPredicates)
            {
                EntityPropertyEntry(entity, propertyPredicate).IsModified = false;
            }
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity, IEnumerable<string> propertyNames)
        {
            return UpdateExclude(entity, propertyNames.ToArray());
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates)
        {
            return UpdateExclude(entity, propertyPredicates.ToArray());
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, params string[] propertyNames)
        {
            return Task.FromResult(UpdateExclude(entity, propertyNames));
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            return Task.FromResult(UpdateExclude(entity, propertyPredicates));
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, IEnumerable<string> propertyNames)
        {
            return Task.FromResult(UpdateExclude(entity, propertyNames));
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates)
        {
            return Task.FromResult(UpdateExclude(entity, propertyPredicates));
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = UpdateExclude(entity, propertyNames);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = UpdateExclude(entity, propertyNames);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            var entityEntry = UpdateExclude(entity, propertyPredicates);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = UpdateExclude(entity, propertyPredicates);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<string> propertyNames)
        {
            var entityEntry = UpdateExclude(entity, propertyNames);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = UpdateExclude(entity, propertyNames);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates)
        {
            var entityEntry = UpdateExclude(entity, propertyPredicates);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = UpdateExclude(entity, propertyPredicates);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames);
            await SaveNowAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, string[] propertyNames, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPredicates)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyPredicates);
            await SaveNowAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyPredicates);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyPredicates);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyPredicates);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyPredicates);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateSafely(TEntity entity)
        {
            var (Key, Value) = IsEntityKeySet(entity);
            var _ = FindOrDefault(Value) ?? throw Oops.Oh(EFCoreErrorCodes.DataNotFound, Key, Value);

            return Update(entity);
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateSafelyAsync(TEntity entity)
        {
            var (Key, Value) = IsEntityKeySet(entity);
            var e = await FindOrDefaultAsync(Value);
            if (e == null) throw Oops.Oh(EFCoreErrorCodes.DataNotFound, Key, Value);

            return await UpdateAsync(entity);
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateSafelyNow(TEntity entity)
        {
            var entityEntry = UpdateSafely(entity);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateSafelyNow(TEntity entity, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = UpdateSafely(entity);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateSafelyNowAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateSafelyAsync(entity);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateSafelyNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateSafelyAsync(entity);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 检查实体是否设置了主键
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private (string Key, object Value) IsEntityKeySet(TEntity entity)
        {
            // 读取主键
            var keyProperty = EntityType.FindPrimaryKey().Properties.AsEnumerable().FirstOrDefault()?.PropertyInfo;
            if (keyProperty == null) return default;

            // 获取主键的值
            var keyName = keyProperty.Name;
            var keyValue = keyProperty.GetValue(entity);

            // 主键不能为空，且不能为0，也不能为GUID空值
            if (keyValue == null || (keyProperty.PropertyType.IsValueType && (keyValue.Equals(0) || keyValue.Equals(Guid.Empty)))) throw Oops.Oh(EFCoreErrorCodes.KeyNotSet, keyName, keyValue);

            return (keyName, keyValue);
        }
    }
}