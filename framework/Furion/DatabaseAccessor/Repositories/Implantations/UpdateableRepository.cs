using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 可更新仓储分部类
    /// </summary>
    public partial class PrivateRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> Update(TEntity entity, bool? ignoreNullValues = null)
        {
            var entityEntry = Entities.Update(entity);

            // 忽略空值
            IgnoreNullValues(ref entity, ignoreNullValues);

            return entityEntry;
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
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity, bool? ignoreNullValues = null)
        {
            return Task.FromResult(Update(entity, ignoreNullValues));
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
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateNow(TEntity entity, bool? ignoreNullValues = null)
        {
            var entityEntry = Update(entity, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateNow(TEntity entity, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = Update(entity, ignoreNullValues);
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
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateNowAsync(TEntity entity, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateAsync(entity, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateAsync(entity, ignoreNullValues);
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
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            var entityEntry = ChangeEntityState(entity, EntityState.Detached);
            foreach (var propertyName in propertyNames)
            {
                EntityPropertyEntry(entity, propertyName).IsModified = true;
            }

            // 忽略空值
            IgnoreNullValues(ref entity, ignoreNullValues);

            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            // 判断是非参数只有一个，且是一个匿名类型
            if (propertyPredicates?.Length == 1 && propertyPredicates[0].Body is NewExpression newExpression)
            {
                var propertyNames = newExpression.Members.Select(u => u.Name);
                return UpdateInclude(entity, propertyNames, ignoreNullValues);
            }
            else
            {
                var entityEntry = ChangeEntityState(entity, EntityState.Detached);
                foreach (var propertyPredicate in propertyPredicates)
                {
                    EntityPropertyEntry(entity, propertyPredicate).IsModified = true;
                }

                // 忽略空值
                IgnoreNullValues(ref entity, ignoreNullValues);

                return entityEntry;
            }
        }

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            return UpdateInclude(entity, propertyNames.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            return UpdateInclude(entity, propertyPredicates.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateInclude(entity, propertyNames, ignoreNullValues));
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateInclude(entity, propertyPredicates, ignoreNullValues));
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateInclude(entity, propertyNames, ignoreNullValues));
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateInclude(entity, propertyPredicates, ignoreNullValues));
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyNames, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyNames, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyNames, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyNames, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateInclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            var entityEntry = ChangeEntityState(entity, EntityState.Modified);
            foreach (var propertyName in propertyNames)
            {
                EntityPropertyEntry(entity, propertyName).IsModified = false;
            }

            // 忽略空值
            IgnoreNullValues(ref entity, ignoreNullValues);

            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            // 判断是非参数只有一个，且是一个匿名类型
            if (propertyPredicates?.Length == 1 && propertyPredicates[0].Body is NewExpression newExpression)
            {
                var propertyNames = newExpression.Members.Select(u => u.Name);
                return UpdateExclude(entity, propertyNames, ignoreNullValues);
            }
            else
            {
                var entityEntry = ChangeEntityState(entity, EntityState.Modified);
                foreach (var propertyPredicate in propertyPredicates)
                {
                    EntityPropertyEntry(entity, propertyPredicate).IsModified = false;
                }

                // 忽略空值
                IgnoreNullValues(ref entity, ignoreNullValues);

                return entityEntry;
            }
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            return UpdateExclude(entity, propertyNames.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            return UpdateExclude(entity, propertyPredicates.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateExclude(entity, propertyNames, ignoreNullValues));
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateExclude(entity, propertyPredicates, ignoreNullValues));
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateExclude(entity, propertyNames, ignoreNullValues));
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            return Task.FromResult(UpdateExclude(entity, propertyPredicates, ignoreNullValues));
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyNames, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyNames, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyNames, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyNames, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExclude(entity, propertyPredicates, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExists(TEntity entity, bool? ignoreNullValues = null)
        {
            // 检查实体是否有效
            CheckEntityEffective(entity);

            return Update(entity, ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExistsAsync(TEntity entity, bool? ignoreNullValues = null)
        {
            // 检查实体是否有效
            await CheckEntityEffectiveAsync(entity);

            return await UpdateAsync(entity, ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExistsNow(TEntity entity, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExists(entity, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExistsNow(TEntity entity, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExists(entity, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExistsNowAsync(TEntity entity, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExistsAsync(entity, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExistsNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExistsAsync(entity, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeExists(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            // 检查实体是否有效
            CheckEntityEffective(entity);

            return UpdateInclude(entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeExists(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            // 检查实体是否有效
            CheckEntityEffective(entity);

            return UpdateInclude(entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeExists(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            return UpdateIncludeExists(entity, propertyNames.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录中特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeExists(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            return UpdateIncludeExists(entity, propertyPredicates.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeExistsAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            // 检查实体是否有效
            await CheckEntityEffectiveAsync(entity);

            return await UpdateIncludeAsync(entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeExistsAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            // 检查实体是否有效
            await CheckEntityEffectiveAsync(entity);

            return await UpdateIncludeAsync(entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeExistsAsync(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            return UpdateIncludeExistsAsync(entity, propertyNames.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeExistsAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            return UpdateIncludeExistsAsync(entity, propertyPredicates.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeExistsNow(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateIncludeExists(entity, propertyNames, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeExistsNow(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateIncludeExists(entity, propertyNames, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeExistsNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateIncludeExists(entity, propertyPredicates, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeExistsNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateIncludeExists(entity, propertyPredicates, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeExistsNow(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateIncludeExists(entity, propertyNames, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeExistsNow(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateIncludeExists(entity, propertyNames, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeExistsNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateIncludeExists(entity, propertyPredicates, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateIncludeExistsNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateIncludeExists(entity, propertyPredicates, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeExistsNowAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            var entityEntry = await UpdateIncludeExistsAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeExistsNowAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeExistsAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeExistsNowAsync(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeExistsAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeExistsNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = await UpdateIncludeExistsAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeExistsNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeExistsAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeExistsNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeExistsAsync(entity, propertyPredicates, ignoreNullValues);
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
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeExistsNowAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeExistsAsync(entity, propertyNames);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeExistsNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeExistsAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeExistsNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeExistsAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录中的特定属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeExistsNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeExistsAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeExists(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            // 检查实体是否有效
            CheckEntityEffective(entity);

            return UpdateExclude(entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeExists(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            // 检查实体是否有效
            CheckEntityEffective(entity);

            return UpdateExclude(entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeExists(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            return UpdateExcludeExists(entity, propertyNames.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录并排除属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeExists(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            return UpdateExcludeExists(entity, propertyPredicates.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeExistsAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            // 检查实体是否有效
            await CheckEntityEffectiveAsync(entity);

            return await UpdateExcludeAsync(entity, propertyNames, ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeExistsAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            // 检查实体是否有效
            await CheckEntityEffectiveAsync(entity);

            return await UpdateExcludeAsync(entity, propertyPredicates, ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeExistsAsync(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            return UpdateExcludeExistsAsync(entity, propertyNames.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录中的特定属性
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeExistsAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            return UpdateExcludeExistsAsync(entity, propertyPredicates.ToArray(), ignoreNullValues);
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeExistsNow(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExcludeExists(entity, propertyNames, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeExistsNow(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExcludeExists(entity, propertyNames, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeExistsNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExcludeExists(entity, propertyPredicates, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeExistsNow(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExcludeExists(entity, propertyPredicates, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeExistsNow(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExcludeExists(entity, propertyNames, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeExistsNow(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExcludeExists(entity, propertyNames, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeExistsNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExcludeExists(entity, propertyPredicates, ignoreNullValues);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> UpdateExcludeExistsNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        {
            var entityEntry = UpdateExcludeExists(entity, propertyPredicates, ignoreNullValues);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeExistsNowAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        {
            var entityEntry = await UpdateExcludeExistsAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeExistsNowAsync(TEntity entity, string[] propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeExistsAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeExistsNowAsync(TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeExistsAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeExistsNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        {
            var entityEntry = await UpdateExcludeExistsAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeExistsNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeExistsAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeExistsNowAsync(TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeExistsAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeExistsNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeExistsAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性名</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeExistsNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeExistsAsync(entity, propertyNames, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeExistsNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeExistsAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新一条记录并排除属性并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicates">属性表达式</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeExistsNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeExistsAsync(entity, propertyPredicates, ignoreNullValues);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 检查实体是否有效
        /// </summary>
        /// <param name="entity">实体</param>
        private void CheckEntityEffective(TEntity entity)
        {
            var (_, Value) = GetEntityKeyValue(entity);
            var trackEntity = FindOrDefault(Value) ?? throw DbHelpers.DataNotFoundException();
            // 取消跟踪实体
            Detach(trackEntity);
        }

        /// <summary>
        /// 检查实体是否有效
        /// </summary>
        /// <param name="entity">实体</param>
        private async Task CheckEntityEffectiveAsync(TEntity entity)
        {
            var (_, Value) = GetEntityKeyValue(entity);
            var trackEntity = await FindOrDefaultAsync(Value);
            if (trackEntity == null) throw DbHelpers.DataNotFoundException();
            // 取消跟踪实体
            Detach(trackEntity);
        }

        /// <summary>
        /// 获取实体键和值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private (string Key, object Value) GetEntityKeyValue(TEntity entity)
        {
            // 读取主键
            var keyProperty = EntityType.FindPrimaryKey().Properties.AsEnumerable().FirstOrDefault()?.PropertyInfo;
            if (keyProperty == null) return default;

            // 获取主键的值
            var keyName = keyProperty.Name;
            var keyValue = keyProperty.GetValue(entity);

            // 主键不能为空，且不能为 0，也不能为 Guid 空值
            if (keyValue == null || (keyProperty.PropertyType.IsValueType && (keyValue.Equals(0) || keyValue.Equals(Guid.Empty)))) throw new InvalidOperationException("The primary key value is not set.");

            return (keyName, keyValue);
        }
    }
}