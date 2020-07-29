using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Options;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 新增或更新操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDbEntityBase, new()
    {
        #region 新增或更新操作 + public virtual EntityEntry<TEntity> InsertOrUpdate(TEntity entity)

        /// <summary>
        /// 新增或更新操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> InsertOrUpdate(TEntity entity)
        {
            if (!IsKeySet(entity))
            {
                return Insert(entity);
            }
            else
            {
                return Update(entity);
            }
        }

        #endregion

        #region 新增或更新操作 + public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity)

        /// <summary>
        /// 新增或更新操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity)
        {
            if (!IsKeySet(entity))
            {
                var entityEntry = await InsertAsync(entity);
                return entityEntry;
            }
            else
            {
                return await UpdateAsync(entity);
            }
        }

        #endregion

        #region 新增或更新操作 + public virtual EntityEntry<TEntity> InsertOrUpdate(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)

        /// <summary>
        /// 新增或更新操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyExpressions">更新/排除的属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> InsertOrUpdate(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            if (!IsKeySet(entity))
            {
                return Insert(entity);
            }
            else
            {
                if (dbTablePropertyUpdateOptions == DbTablePropertyUpdateOptions.Include)
                {
                    return UpdateIncludeProperties(entity, propertyExpressions);
                }
                else if (dbTablePropertyUpdateOptions == DbTablePropertyUpdateOptions.Exclude)
                {
                    return UpdateExcludeProperties(entity, propertyExpressions);
                }
                else
                {
                    return Update(entity);
                }
            }
        }

        #endregion

        #region 新增或更新操作 + public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)

        /// <summary>
        /// 新增或更新操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyExpressions">更新/排除的属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            if (!IsKeySet(entity))
            {
                var entityEntry = await InsertAsync(entity);
                return entityEntry;
            }
            else
            {
                if (dbTablePropertyUpdateOptions == DbTablePropertyUpdateOptions.Include)
                {
                    return await UpdateIncludePropertiesAsync(entity, propertyExpressions);
                }
                else if (dbTablePropertyUpdateOptions == DbTablePropertyUpdateOptions.Exclude)
                {
                    return await UpdateExcludePropertiesAsync(entity, propertyExpressions);
                }
                else
                {
                    return await UpdateAsync(entity);
                }
            }
        }

        #endregion

        #region 新增或更新操作 + public virtual EntityEntry<TEntity> InsertOrUpdate(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyNames)

        /// <summary>
        /// 新增或更新操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyNames">更新/排除的属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> InsertOrUpdate(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyNames)
        {
            if (!IsKeySet(entity))
            {
                return Insert(entity);
            }
            else
            {
                if (dbTablePropertyUpdateOptions == DbTablePropertyUpdateOptions.Include)
                {
                    return UpdateIncludeProperties(entity, propertyNames);
                }
                else if (dbTablePropertyUpdateOptions == DbTablePropertyUpdateOptions.Exclude)
                {
                    return UpdateExcludeProperties(entity, propertyNames);
                }
                else
                {
                    return Update(entity);
                }
            }
        }

        #endregion

        #region 新增或更新操作 + public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyNames)

        /// <summary>
        /// 新增或更新操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyNames">更新/排除的属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyNames)
        {
            if (!IsKeySet(entity))
            {
                var entityEntry = await InsertAsync(entity);
                return entityEntry;
            }
            else
            {
                if (dbTablePropertyUpdateOptions == DbTablePropertyUpdateOptions.Include)
                {
                    return await UpdateIncludePropertiesAsync(entity, propertyNames);
                }
                else if (dbTablePropertyUpdateOptions == DbTablePropertyUpdateOptions.Exclude)
                {
                    return await UpdateExcludePropertiesAsync(entity, propertyNames);
                }
                else
                {
                    return await UpdateAsync(entity);
                }
            }
        }

        #endregion

        #region 新增或更新操作并立即保存 + public virtual EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity)

        /// <summary>
        /// 新增或更新操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity)
        {
            var entityEntry = InsertOrUpdate(entity);
            SaveChanges();
            return entityEntry;
        }

        #endregion

        #region 新增或更新操作并立即保存 + public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity)

        /// <summary>
        /// 新增或更新操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity)
        {
            var entityEntry = await InsertOrUpdateAsync(entity);
            await SaveChangesAsync();
            return entityEntry;
        }

        #endregion

        #region 新增或更新操作并立即保存 + public virtual EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)

        /// <summary>
        /// 新增或更新操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyExpressions">更新/排除的属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = InsertOrUpdate(entity, dbTablePropertyUpdateOptions, propertyExpressions);
            SaveChanges();
            return entityEntry;
        }

        #endregion

        #region 新增或更新操作并立即保存 + public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)

        /// <summary>
        /// 新增或更新操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyExpressions">更新/排除的属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = await InsertOrUpdateAsync(entity, dbTablePropertyUpdateOptions, propertyExpressions);
            await SaveChangesAsync();
            return entityEntry;
        }

        #endregion

        #region 新增或更新操作并立即保存 + public virtual EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyNames)

        /// <summary>
        /// 新增或更新操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyNames">更新/排除的属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyNames)
        {
            var entityEntry = InsertOrUpdate(entity, dbTablePropertyUpdateOptions, propertyNames);
            SaveChanges();
            return entityEntry;
        }

        #endregion

        #region 新增或更新操作并立即保存 + public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyNames)

        /// <summary>
        /// 新增或更新操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyNames">更新/排除的属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyNames)
        {
            var entityEntry = await InsertOrUpdateAsync(entity, dbTablePropertyUpdateOptions, propertyNames);
            await SaveChangesAsync();
            return entityEntry;
        }

        #endregion
    }
}