using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可插入的仓储分部类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EFCoreRepository<TEntity> : IRepository<TEntity>, IUpdateableRepository<TEntity>
         where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> Update(TEntity entity)
        {
            return Entities.Update(entity);
        }

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void UpdateRange(params TEntity[] entities)
        {
            Entities.UpdateRange(entities);
        }

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            Entities.UpdateRange(entities);
        }

        /// <summary>
        /// 更新实体（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Entities.Update(entity));
        }

        /// <summary>
        /// 更新多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task UpdateRangeAsync(params TEntity[] entities)
        {
            Entities.UpdateRange(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 更新多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            Entities.UpdateRange(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 更新实体并立即保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateSaveChanges(TEntity entity)
        {
            var entityEntry = Update(entity);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 更新实体并立即保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateSaveChanges(TEntity entity, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = Update(entity);
            SaveChanges(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="entities"></param>
        public virtual void UpdateRangeSaveChanges(params TEntity[] entities)
        {
            UpdateRange(entities);
            SaveChanges();
        }

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        public virtual void UpdateRangeSaveChanges(bool acceptAllChangesOnSuccess, params TEntity[] entities)
        {
            UpdateRange(entities);
            SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="entities"></param>
        public virtual void UpdateRangeSaveChanges(IEnumerable<TEntity> entities)
        {
            UpdateRange(entities);
            SaveChanges();
        }

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        public virtual void UpdateRangeSaveChanges(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess)
        {
            UpdateRange(entities);
            SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 更新实体并立即保存（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateSaveChangesAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateAsync(entity);
            await SaveChangesAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新实体并立即保存（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateSaveChangesAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateAsync(entity);
            await SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task UpdateRangeSaveChangesAsync(params TEntity[] entities)
        {
            await UpdateRangeAsync(entities);
            await SaveChangesAsync();
        }

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task UpdateRangeSaveChangesAsync(bool acceptAllChangesOnSuccess, params TEntity[] entities)
        {
            await UpdateRangeAsync(entities);
            await SaveChangesAsync(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task UpdateRangeSaveChangesAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await UpdateRangeAsync(entities);
            await SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task UpdateRangeSaveChangesAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await UpdateRangeAsync(entities);
            await SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}