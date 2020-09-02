// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

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
    public partial class EFCoreRepository<TEntity>
         where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> Insert(TEntity entity)
        {
            return Entities.Add(entity);
        }

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Insert(params TEntity[] entities)
        {
            Entities.AddRange(entities);
        }

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        /// <summary>
        /// 新增实体（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await Entities.AddAsync(entity, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 新增多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task InsertAsync(params TEntity[] entities)
        {
            return Entities.AddRangeAsync(entities);
        }

        /// <summary>
        /// 新增多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return Entities.AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// 新增并提交更改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> InsertNow(TEntity entity)
        {
            var entityEntry = Insert(entity);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 新增并提交更改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> InsertNow(TEntity entity, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = Insert(entity);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void InsertNow(params TEntity[] entities)
        {
            Insert(entities);
            SaveNow();
        }

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        public virtual void InsertNow(TEntity[] entities, bool acceptAllChangesOnSuccess)
        {
            Insert(entities);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void InsertNow(IEnumerable<TEntity> entities)
        {
            Insert(entities);
            SaveNow();
        }

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        public virtual void InsertNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess)
        {
            Insert(entities);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增并提交更改（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> InsertNowAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await InsertAsync(entity, cancellationToken);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 新增并提交更改（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> InsertNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await InsertAsync(entity, cancellationToken);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 新增多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task InsertNowAsync(params TEntity[] entities)
        {
            await InsertAsync(entities);
            await SaveNowAsync();
        }

        /// <summary>
        /// 新增多个实体并提交更改（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task InsertNowAsync(TEntity[] entities, CancellationToken cancellationToken = default)
        {
            await InsertAsync(entities);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// 新增多个实体并提交更改（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task InsertNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await InsertAsync(entities);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 新增多个实体并提交更改（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task InsertNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await InsertAsync(entities, cancellationToken);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// 新增多个实体并提交更改（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task InsertNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await InsertAsync(entities, cancellationToken);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}