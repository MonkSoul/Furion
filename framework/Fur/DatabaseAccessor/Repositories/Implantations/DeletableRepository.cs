// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.FriendlyException;
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
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public partial class EFCoreRepository<TEntity, TDbContextLocator>
        where TEntity : class, IEntity, new()
        where TDbContextLocator : class, IDbContextLocator
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
            var deletedEntity = BuildDeletedEntity(key);
            if (deletedEntity != null) return;

            // 如果主键不存在，则采用 Find 查询
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
            var deletedEntity = BuildDeletedEntity(key);
            if (deletedEntity != null) return;

            // 如果主键不存在，则采用 FindAsync 查询
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
        public virtual void DeleteExists(object key)
        {
            var entity = FindOrDefault(key) ?? throw Oops.Oh(EFCoreErrorCodes.DataNotFound, key);
            Delete(entity);
        }

        /// <summary>
        /// 根据主键删除一条记录
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteExistsAsync(object key, CancellationToken cancellationToken = default)
        {
            var entity = await FindOrDefaultAsync(key, cancellationToken);
            if (entity == null) throw Oops.Oh(EFCoreErrorCodes.DataNotFound, key);

            await DeleteAsync(entity);
        }

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        public virtual void DeleteExistsNow(object key)
        {
            DeleteExists(key);
            SaveNow();
        }

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        public virtual void DeleteExistsNow(object key, bool acceptAllChangesOnSuccess)
        {
            DeleteExists(key);
            SaveNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns></returns>
        public virtual async Task DeleteExistsNowAsync(object key, CancellationToken cancellationToken = default)
        {
            await DeleteExistsAsync(key, cancellationToken);
            await SaveNowAsync(cancellationToken);
        }

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns></returns>
        public virtual async Task DeleteExistsNowAsync(object key, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await DeleteExistsAsync(key, cancellationToken);
            await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 构建被删除的实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        private TEntity BuildDeletedEntity(object key)
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