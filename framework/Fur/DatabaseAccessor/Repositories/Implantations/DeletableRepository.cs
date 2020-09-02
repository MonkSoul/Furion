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
        /// 移除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> Delete(TEntity entity)
        {
            return Entities.Remove(entity);
        }

        /// <summary>
        /// 移除多个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void Delete(params TEntity[] entities)
        {
            Entities.RemoveRange(entities);
        }

        /// <summary>
        /// 移除多个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity)
        {
            return Task.FromResult(Delete(entity));
        }

        /// <summary>
        /// 移除多个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync(params TEntity[] entities)
        {
            Delete(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 移除多个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            Delete(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 移除实体并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> DeleteNow(TEntity entity)
        {
            var entityEntry = Delete(entity);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 移除实体并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> DeleteNow(TEntity entity, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = Delete(entity);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 移除实体并立即提交
        /// </summary>
        /// <param name="entities"></param>
        public virtual void DeleteNow(params TEntity[] entities)
        {
            Delete(entities);
            SaveNow();
        }

        /// <summary>
        /// 移除实体并立即提交
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        public virtual void DeleteNow(TEntity[] entities, bool acceptAllChangesOnSuccess)
        {
            Delete(entities);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 移除实体并立即提交
        /// </summary>
        /// <param name="entities"></param>
        public virtual void DeleteNow(IEnumerable<TEntity> entities)
        {
            Delete(entities);
            SaveNow();
        }

        /// <summary>
        /// 移除实体并立即提交
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        public virtual void DeleteNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess)
        {
            Delete(entities);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 移除实体并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await DeleteAsync(entity);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 移除实体并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await DeleteAsync(entity);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 移除多个实体并立即提交（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task DeleteNowAsync(params TEntity[] entities)
        {
            await DeleteAsync(entities);
            await SaveNowAsync();
        }

        /// <summary>
        /// 移除多个实体并立即提交（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task DeleteNowAsync(TEntity[] entities, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(entities);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// 移除多个实体并立即提交（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task DeleteNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(entities);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 移除多个实体并立即提交（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task DeleteNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(entities);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// 移除多个实体并立即提交（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task DeleteNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(entities);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}