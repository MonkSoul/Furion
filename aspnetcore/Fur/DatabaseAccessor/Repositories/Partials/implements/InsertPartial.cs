using Fur.ApplicationBase;
using Fur.DatabaseAccessor.Extensions;
using Fur.DatabaseAccessor.Models.Entities;
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
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity> where TEntity : class, IDbEntityBase, new()
    {
        #region 新增操作 + public virtual EntityEntry<TEntity> Insert(TEntity entity)
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> Insert(TEntity entity)
        {
            return LoadDbEntityInsertInterceptor(() => Entities.Add(entity), entity).First();
        }
        #endregion

        #region 新增操作 + public virtual void Insert(params TEntity[] entities)
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Insert(params TEntity[] entities)
        {
            LoadDbEntityInsertInterceptor(() => Entities.AddRange(entities), entities);
        }
        #endregion

        #region 新增操作 + public virtual void Insert(IEnumerable<TEntity> entities)
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            LoadDbEntityInsertInterceptor(() => Entities.AddRange(entities), entities.ToArray());
        }
        #endregion

        #region 新增操作 + public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="ValueTask{TResult}"/></returns>
        public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        {
            var entityEntry = LoadDbEntityInsertInterceptor(async () => await Entities.AddAsync(entity), entity).First();

            return ValueTask.FromResult(entityEntry);
        }
        #endregion

        #region 新增操作 + public virtual Task InsertAsync(params TEntity[] entities)
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task InsertAsync(params TEntity[] entities)
        {
            LoadDbEntityInsertInterceptor(async () => await Entities.AddRangeAsync(entities), entities);
            return Task.CompletedTask;
        }

        #endregion

        #region 新增操作 + public virtual Task InsertAsync(IEnumerable<TEntity> entities)
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task InsertAsync(IEnumerable<TEntity> entities)
        {
            LoadDbEntityInsertInterceptor(async () => await Entities.AddRangeAsync(entities), entities.ToArray());
            return Task.CompletedTask;
        }
        #endregion

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

        #endregion

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

        #endregion

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

        #endregion

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

        #endregion

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

        #endregion

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

        #endregion

        #region 加载实体拦截器 + private EntityEntry<TEntity>[] LoadDbEntityInterceptor(Action handle, params TEntity[] entities)
        /// <summary>
        /// 加载实体拦截器
        /// </summary>
        /// <param name="entities">多个实体</param>
        private EntityEntry<TEntity>[] LoadDbEntityInsertInterceptor(Action handle, params TEntity[] entities)
        {
            var entityEntries = new List<EntityEntry<TEntity>>();
            foreach (var entity in entities)
            {
                var entityEntry = EntityEntry(entity);
                entityEntries.Add(entityEntry);

                _maintenanceInterceptor?.Inserting(entityEntry);

                handle?.Invoke();

                _maintenanceInterceptor?.Inserted(entityEntry);

                if (AppGlobal.SupportedMultipleTenant)
                {
                    //var tenantIdProperty = entityEntry.GetProperty(nameof(DbEntityBase.TenantId));
                    //if (tenantIdProperty == null) throw new ArgumentNullException($"Not found the {nameof(DbEntityBase.TenantId)} Column.");

                    //tenantIdProperty.CurrentValue = TenantId.Value;
                }
            }
            return entityEntries.ToArray();
        }
        #endregion
    }
}