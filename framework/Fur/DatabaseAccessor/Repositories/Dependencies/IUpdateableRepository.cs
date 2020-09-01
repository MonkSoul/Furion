using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
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
    }
}