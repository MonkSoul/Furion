// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可删除仓储分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库实体定位器</typeparam>
    public partial class EFCoreRepository<TEntity, TDbContextLocator>
        where TEntity : class, IEntityBase, new()
        where TDbContextLocator : class, IDbContextLocator, new()
    {
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> Delete(TEntity entity)
        {
            return Entities.Remove(entity);
        }

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Delete(params TEntity[] entities)
        {
            Entities.RemoveRange(entities);
        }

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity)
        {
            return Task.FromResult(Delete(entity));
        }

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns>Task</returns>
        public virtual Task DeleteAsync(params TEntity[] entities)
        {
            Delete(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns>Task</returns>
        public virtual Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            Delete(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 删除一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> DeleteNow(TEntity entity)
        {
            var entityEntry = Delete(entity);
            SaveNow();
            return entityEntry;
        }

        /// <summary>
        /// 删除一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> DeleteNow(TEntity entity, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = Delete(entity);
            SaveNow(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void DeleteNow(params TEntity[] entities)
        {
            Delete(entities);
            SaveNow();
        }

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        public virtual void DeleteNow(TEntity[] entities, bool acceptAllChangesOnSuccess)
        {
            Delete(entities);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void DeleteNow(IEnumerable<TEntity> entities)
        {
            Delete(entities);
            SaveNow();
        }

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        public virtual void DeleteNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess)
        {
            Delete(entities);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 删除一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await DeleteAsync(entity);
            await SaveNowAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 删除一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual async Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await DeleteAsync(entity);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteNowAsync(params TEntity[] entities)
        {
            await DeleteAsync(entities);
            await SaveNowAsync();
        }

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteNowAsync(TEntity[] entities, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(entities);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(entities);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(entities);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(entities);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 根据主键删除一条记录
        /// </summary>
        /// <param name="key">主键</param>
        public virtual void Delete(object key)
        {
            var deleteEntity = CreateDeleteEntity(key);
            if (deleteEntity != null) return;

            var entity = FindOrDefault(key);
            if (entity != null) Delete(entity);
        }

        /// <summary>
        /// 根据主键删除一条记录
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteAsync(object key, CancellationToken cancellationToken = default)
        {
            var deleteEntity = CreateDeleteEntity(key);
            if (deleteEntity != null) return;

            var entity = await FindOrDefaultAsync(key, cancellationToken);
            if (entity != null) await DeleteAsync(entity);
        }

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        public virtual void DeleteNow(object key)
        {
            Delete(key);
            SaveNow();
        }

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        public virtual void DeleteNow(object key, bool acceptAllChangesOnSuccess)
        {
            Delete(key);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns></returns>
        public virtual async Task DeleteNowAsync(object key, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(key, cancellationToken);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns></returns>
        public virtual async Task DeleteNowAsync(object key, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(key, cancellationToken);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 根据主键删除一条记录
        /// </summary>
        /// <param name="key">主键</param>
        public virtual void DeleteSafely(object key)
        {
            var entity = Find(key);
            Delete(entity);
        }

        /// <summary>
        /// 根据主键删除一条记录
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteSafelyAsync(object key, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(key, cancellationToken);
            await DeleteAsync(entity);
        }

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        public virtual void DeleteSafelyNow(object key)
        {
            DeleteSafely(key);
            SaveNow();
        }

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        public virtual void DeleteSafelyNow(object key, bool acceptAllChangesOnSuccess)
        {
            DeleteSafely(key);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns></returns>
        public virtual async Task DeleteSafelyNowAsync(object key, CancellationToken cancellationToken = default)
        {
            await DeleteSafelyAsync(key, cancellationToken);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns></returns>
        public virtual async Task DeleteSafelyNowAsync(object key, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await DeleteSafelyAsync(key, cancellationToken);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 创建需要删除的实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        private TEntity CreateDeleteEntity(object key)
        {
            // 读取主键
            var keyProperty = EntityType.FindPrimaryKey().Properties.AsEnumerable().FirstOrDefault()?.PropertyInfo;
            if (keyProperty == null) return default;

            // 创建实体对象并设置主键值
            var entity = Activator.CreateInstance<TEntity>();
            keyProperty.SetValue(entity, key);

            // 设置实体状态为已删除
            ChangeEntityState(entity, EntityState.Deleted);

            return entity;
        }
    }
}