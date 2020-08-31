using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 可插入的仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IInsertableRepository<TEntity>
        where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Add(TEntity entity);

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(params TEntity[] entities);

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 新增实体（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增多个实体（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddRangeAsync(params TEntity[] entities);

        /// <summary>
        /// 新增多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增并提交更改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> AddSaveChanges(TEntity entity);

        /// <summary>
        /// 新增并提交更改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        EntityEntry<TEntity> AddSaveChanges(TEntity entity, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        void AddRangeSaveChanges(params TEntity[] entities);

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        void AddRangeSaveChanges(bool acceptAllChangesOnSuccess, params TEntity[] entities);

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        void AddRangeSaveChanges(IEnumerable<TEntity> entities);

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        void AddRangeSaveChanges(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        ///  新增并提交更改（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> AddSaveChangesAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增并提交更改（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> AddSaveChangesAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        // <summary>
        /// 新增多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        Task AddRangeSaveChangesAsync(params TEntity[] entities);

        /// <summary>
        /// 新增多个实体并提交更改（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddRangeSaveChangesAsync(bool acceptAllChangesOnSuccess, params TEntity[] entities);

        /// <summary>
        /// 新增多个实体并提交更改（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddRangeSaveChangesAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增多个实体并提交更改（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddRangeSaveChangesAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}