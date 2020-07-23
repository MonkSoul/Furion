using Fur.DatabaseAccessor.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 新增 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 新增操作 + public virtual EntityEntry<TEntity> Insert(TEntity entity)

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> Insert(TEntity entity)
        {
            SetInsertMaintenanceFields(entity);
            return Entities.Add(entity);
        }

        #endregion 新增操作 + public virtual EntityEntry<TEntity> Insert(TEntity entity)

        #region 新增操作 + public virtual void Insert(params TEntity[] entities)

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Insert(params TEntity[] entities)
        {
            SetInsertMaintenanceFields(entities);
            Entities.AddRange(entities);
        }

        #endregion 新增操作 + public virtual void Insert(params TEntity[] entities)

        #region 新增操作 + public virtual void Insert(IEnumerable<TEntity> entities)

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            SetInsertMaintenanceFields(entities.ToArray());
            Entities.AddRange(entities);
        }

        #endregion 新增操作 + public virtual void Insert(IEnumerable<TEntity> entities)

        #region 新增操作 + public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity)

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="ValueTask{TResult}"/></returns>
        public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        {
            SetInsertMaintenanceFields(entity);
            return Entities.AddAsync(entity);
        }

        #endregion 新增操作 + public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity)

        #region 新增操作 + public virtual Task InsertAsync(params TEntity[] entities)

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task InsertAsync(params TEntity[] entities)
        {
            SetInsertMaintenanceFields(entities);
            return Entities.AddRangeAsync();
        }

        #endregion 新增操作 + public virtual Task InsertAsync(params TEntity[] entities)

        #region 新增操作 + public virtual Task InsertAsync(IEnumerable<TEntity> entities)

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task InsertAsync(IEnumerable<TEntity> entities)
        {
            SetInsertMaintenanceFields(entities.ToArray());
            return Entities.AddRangeAsync();
        }

        #endregion 新增操作 + public virtual Task InsertAsync(IEnumerable<TEntity> entities)

        #region 新增操作并立即保存 + public virtual EntityEntry<TEntity> InsertSaveChanges(TEntity entity)

        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> InsertSaveChanges(TEntity entity)
        {
            var trackEntity = Insert(entity);
            SaveChanges();
            return trackEntity;
        }

        #endregion 新增操作并立即保存 + public virtual EntityEntry<TEntity> InsertSaveChanges(TEntity entity)

        #region 新增操作并立即保存 + public virtual void InsertSaveChanges(params TEntity[] entities)

        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void InsertSaveChanges(params TEntity[] entities)
        {
            Insert(entities);
            SaveChanges();
        }

        #endregion 新增操作并立即保存 + public virtual void InsertSaveChanges(params TEntity[] entities)

        #region 新增操作并立即保存 + public virtual void InsertSaveChanges(IEnumerable<TEntity> entities)

        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void InsertSaveChanges(IEnumerable<TEntity> entities)
        {
            Insert(entities);
            SaveChanges();
        }

        #endregion 新增操作并立即保存 + public virtual void InsertSaveChanges(IEnumerable<TEntity> entities)

        #region 新增操作并立即保存 + public virtual async ValueTask<EntityEntry<TEntity>> InsertSaveChangesAsync(TEntity entity)

        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="ValueTask{TResult}"/></returns>
        public virtual async ValueTask<EntityEntry<TEntity>> InsertSaveChangesAsync(TEntity entity)
        {
            var trackEntity = await InsertAsync(entity);
            await SaveChangesAsync();
            return trackEntity;
        }

        #endregion 新增操作并立即保存 + public virtual async ValueTask<EntityEntry<TEntity>> InsertSaveChangesAsync(TEntity entity)

        #region 新增操作并立即保存 + public virtual async Task InsertSaveChangesAsync(params TEntity[] entities)

        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        public virtual async Task InsertSaveChangesAsync(params TEntity[] entities)
        {
            await InsertAsync(entities);
            await SaveChangesAsync();
            await Task.CompletedTask;
        }

        #endregion 新增操作并立即保存 + public virtual async Task InsertSaveChangesAsync(params TEntity[] entities)

        #region 新增操作并立即保存 + public virtual async Task InsertSaveChangesAsync(IEnumerable<TEntity> entities)

        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        public virtual async Task InsertSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            await InsertAsync(entities);
            await SaveChangesAsync();
            await Task.CompletedTask;
        }

        #endregion 新增操作并立即保存 + public virtual async Task InsertSaveChangesAsync(IEnumerable<TEntity> entities)

        #region 设置新增时维护字段 + private void SetInsertMaintenanceFields(params TEntity[] entities)

        /// <summary>
        /// 设置新增时维护字段
        /// </summary>
        /// <param name="entities">多个实体</param>
        private void SetInsertMaintenanceFields(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                var entityEntry = EntityEntry(entity);

                var (createdTimePropertyName, createdTimePropertyValue) = _maintenanceProvider?.GetCreatedTimeFieldInfo()
                    ?? (nameof(DbEntityBase.CreatedTime), DateTime.Now);

                var createdTimeProperty = EntityEntryProperty(entityEntry, createdTimePropertyName);
                if (createdTimeProperty != null)
                {
                    createdTimeProperty.CurrentValue = createdTimePropertyValue;
                }

                if (TenantId.HasValue)
                {
                    var tenantIdProperty = EntityEntryProperty(entityEntry, nameof(DbEntity.TenantId));
                    if (tenantIdProperty != null)
                    {
                        tenantIdProperty.CurrentValue = TenantId;
                    }
                }
            }
        }

        #endregion 设置新增时维护字段 + private void SetInsertMaintenanceFields(params TEntity[] entities)
    }
}