using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Options;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 新增或更新操作 分部接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntityBase, new()
    {
        #region 新增或更新操作 + EntityEntry<TEntity> InsertOrUpdate(TEntity entity)

        /// <summary>
        /// 新增或更新操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> InsertOrUpdate(TEntity entity);

        #endregion 新增或更新操作 + EntityEntry<TEntity> InsertOrUpdate(TEntity entity)

        #region 新增或更新操作 + Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity)

        /// <summary>
        /// 新增或更新操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity);

        #endregion 新增或更新操作 + Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity)

        #region 新增或更新操作 + EntityEntry<TEntity> InsertOrUpdate(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 新增或更新操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyExpressions">更新/排除的属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> InsertOrUpdate(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions);

        #endregion 新增或更新操作 + EntityEntry<TEntity> InsertOrUpdate(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions);

        #region 新增或更新操作 + Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)

        /// <summary>
        /// 新增或更新操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyExpressions">更新/排除的属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions);

        #endregion 新增或更新操作 + Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)

        #region 新增或更新操作 + EntityEntry<TEntity> InsertOrUpdate(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyNames)

        /// <summary>
        /// 新增或更新操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyNames">更新/排除的属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> InsertOrUpdate(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyNames);

        #endregion 新增或更新操作 + EntityEntry<TEntity> InsertOrUpdate(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyNames)

        #region 新增或更新操作 + Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyNames)

        /// <summary>
        /// 新增或更新操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyNames">更新/排除的属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyNames);

        #endregion 新增或更新操作 + Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyNames)

        #region 新增或更新操作并立即保存 + EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity)

        /// <summary>
        /// 新增或更新操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity);

        #endregion 新增或更新操作并立即保存 + EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity)

        #region 新增或更新操作并立即保存 + Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity)

        /// <summary>
        /// 新增或更新操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity);

        #endregion 新增或更新操作并立即保存 + Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity)

        #region 新增或更新操作并立即保存 + EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)

        /// <summary>
        /// 新增或更新操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyExpressions">更新/排除的属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions);

        #endregion 新增或更新操作并立即保存 + EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)

        #region 新增或更新操作并立即保存 + Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)

        /// <summary>
        /// 新增或更新操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyExpressions">更新/排除的属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions);

        #endregion 新增或更新操作并立即保存 + Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions)

        #region 新增或更新操作并立即保存 + EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyExpressions)

        /// <summary>
        /// 新增或更新操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyExpressions">更新/排除的属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyExpressions);

        #endregion 新增或更新操作并立即保存 + EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyExpressions)

        #region 新增或更新操作并立即保存 + Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyExpressions)

        /// <summary>
        /// 新增或更新操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="dbTablePropertyUpdateOptions">更新选项 <see cref="DbTablePropertyUpdateOptions"/></param>
        /// <param name="propertyExpressions">更新/排除的属性</param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyExpressions);

        #endregion 新增或更新操作并立即保存 + Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params string[] propertyExpressions)
    }
}