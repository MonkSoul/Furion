using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 可插入的仓储分部类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EFCoreRepository<TEntity> : IRepository<TEntity>, IInsertableRepository<TEntity>
         where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> Add(TEntity entity)
        {
            return Entities.Add(entity);
        }

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void AddRange(params TEntity[] entities)
        {
            Entities.AddRange(entities);
        }

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        /// <summary>
        /// 新增实体（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await Entities.AddAsync(entity, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 新增多个实体（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task AddRangeAsync(params TEntity[] entities)
        {
            return Entities.AddRangeAsync(entities);
        }

        /// <summary>
        /// 新增多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return Entities.AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// 新增并提交更改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> AddSaveChanges(TEntity entity)
        {
            var entityEntry = Add(entity);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 新增并提交更改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> AddSaveChanges(TEntity entity, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = Add(entity);
            SaveChanges(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void AddRangeSaveChanges(params TEntity[] entities)
        {
            AddRange(entities);
            SaveChanges();
        }

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        public virtual void AddRangeSaveChanges(bool acceptAllChangesOnSuccess, params TEntity[] entities)
        {
            AddRange(entities);
            SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void AddRangeSaveChanges(IEnumerable<TEntity> entities)
        {
            AddRange(entities);
            SaveChanges();
        }

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        public virtual void AddRangeSaveChanges(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess)
        {
            AddRange(entities);
            SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        ///  新增并提交更改（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> AddSaveChangesAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await AddAsync(entity, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 新增并提交更改（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> AddSaveChangesAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await AddAsync(entity, cancellationToken);
            await SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        // <summary>
        /// 新增多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        public virtual async Task AddRangeSaveChangesAsync(params TEntity[] entities)
        {
            await AddRangeAsync(entities);
            await SaveChangesAsync();
        }

        /// <summary>
        /// 新增多个实体并提交更改（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task AddRangeSaveChangesAsync(bool acceptAllChangesOnSuccess, params TEntity[] entities)
        {
            await AddRangeAsync(entities);
            await SaveChangesAsync(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增多个实体并提交更改（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task AddRangeSaveChangesAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await AddRangeAsync(entities, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 新增多个实体并提交更改（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task AddRangeSaveChangesAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await AddRangeAsync(entities, cancellationToken);
            await SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}