using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可更新的仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IUpdateableRepository<TEntity>
        where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Update(TEntity entity);

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRange(params TEntity[] entities);

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新实体（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity);

        /// <summary>
        /// 更新多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateRangeAsync(params TEntity[] entities);

        /// <summary>
        /// 更新多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新实体并立即保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateSaveChanges(TEntity entity);

        /// <summary>
        /// 更新实体并立即保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateSaveChanges(TEntity entity, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRangeSaveChanges(params TEntity[] entities);

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        void UpdateRangeSaveChanges(bool acceptAllChangesOnSuccess, params TEntity[] entities);

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRangeSaveChanges(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        void UpdateRangeSaveChanges(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 更新实体并立即保存（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateSaveChangesAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新实体并立即保存（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateSaveChangesAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateRangeSaveChangesAsync(params TEntity[] entities);

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateRangeSaveChangesAsync(bool acceptAllChangesOnSuccess, params TEntity[] entities);

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateRangeSaveChangesAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateRangeSaveChangesAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateInclude(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateInclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateInclude(TEntity entity, IEnumerable<string> propertyNames);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateInclude(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, IEnumerable<string> propertyNames);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeSaveChanges(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeSaveChanges(TEntity entity, bool acceptAllChangesOnSuccess, params string[] propertyNames);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeSaveChanges(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeSaveChanges(TEntity entity, bool acceptAllChangesOnSuccess, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeSaveChanges(TEntity entity, IEnumerable<string> propertyNames);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeSaveChanges(TEntity entity, bool acceptAllChangesOnSuccess, IEnumerable<string> propertyNames);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeSaveChanges(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeSaveChanges(TEntity entity, bool acceptAllChangesOnSuccess, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeSaveChangesAsync(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeSaveChangesAsync(TEntity entity, bool acceptAllChangesOnSuccess, params string[] propertyNames);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeSaveChangesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeSaveChangesAsync(TEntity entity, bool acceptAllChangesOnSuccess, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeSaveChangesAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeSaveChangesAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeSaveChangesAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeSaveChangesAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}