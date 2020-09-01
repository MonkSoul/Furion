using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可删除的仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IDeletableRepository<TEntity>
        where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Delete(TEntity entity);

        /// <summary>
        /// 移除多个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Delete(params TEntity[] entities);

        /// <summary>
        /// 移除多个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity);

        /// <summary>
        /// 移除多个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(params TEntity[] entities);

        /// <summary>
        /// 移除多个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 移除实体并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> DeleteNow(TEntity entity);

        /// <summary>
        /// 移除实体并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        EntityEntry<TEntity> DeleteNow(TEntity entity, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 移除实体并立即提交
        /// </summary>
        /// <param name="entities"></param>
        void DeleteNow(params TEntity[] entities);

        /// <summary>
        /// 移除实体并立即提交
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        void DeleteNow(bool acceptAllChangesOnSuccess, params TEntity[] entities);

        /// <summary>
        /// 移除实体并立即提交
        /// </summary>
        /// <param name="entities"></param>
        void DeleteNow(IEnumerable<TEntity> entities);

        /// <summary>
        /// 移除实体并立即提交
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        void DeleteNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 移除实体并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 移除实体并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 移除多个实体并立即提交（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task DeleteNowAsync(params TEntity[] entities);

        /// <summary>
        /// 移除多个实体并立即提交（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task DeleteNowAsync(bool acceptAllChangesOnSuccess, params TEntity[] entities);

        /// <summary>
        /// 移除多个实体并立即提交（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 移除多个实体并立即提交（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}