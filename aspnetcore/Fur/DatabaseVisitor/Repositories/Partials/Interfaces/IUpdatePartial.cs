using Fur.DatabaseVisitor.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 更新全部列操作 分部接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 更新全部列操作 + EntityEntry<TEntity> Update(TEntity entity)
        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> Update(TEntity entity);
        #endregion

        #region 更新全部列操作 + void Update(params TEntity[] entities)
        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Update(params TEntity[] entities);
        #endregion

        #region 更新全部列操作 + void Update(IEnumerable<TEntity> entities)
        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Update(IEnumerable<TEntity> entities);
        #endregion

        #region 更新全部列操作 + Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity)
        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity);
        #endregion

        #region 更新全部列操作 + Task UpdateAsync(params TEntity[] entities)
        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateAsync(params TEntity[] entities);
        #endregion

        #region 更新全部列操作 + Task UpdateAsync(IEnumerable<TEntity> entities)
        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateAsync(IEnumerable<TEntity> entities);
        #endregion


        #region 更新全部列操作并立即保存 + EntityEntry<TEntity> UpdateSaveChanges(TEntity entity)
        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> UpdateSaveChanges(TEntity entity);
        #endregion

        #region 更新全部列操作并立即保存 + void UpdateSaveChanges(params TEntity[] entities)
        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entities"></param>
        void UpdateSaveChanges(params TEntity[] entities);
        #endregion

        #region 更新全部列操作并立即保存 + void UpdateSaveChanges(IEnumerable<TEntity> entities)
        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        void UpdateSaveChanges(IEnumerable<TEntity> entities);
        #endregion

        #region 更新全部列操作并立即保存 + Task<EntityEntry<TEntity>> UpdateSaveChangesAsync(TEntity entity)
        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateSaveChangesAsync(TEntity entity);
        #endregion

        #region 更新全部列操作并立即保存 + Task UpdateSaveChangesAsync(params TEntity[] entities)
        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task UpdateSaveChangesAsync(params TEntity[] entities);
        #endregion

        #region 更新全部列操作并立即保存 + Task UpdateSaveChangesAsync(IEnumerable<TEntity> entities)
        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entities"></param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateSaveChangesAsync(IEnumerable<TEntity> entities);
        #endregion


        #region 更新指定列 + EntityEntry<TEntity> UpdateIncludeProperties(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> UpdateIncludeProperties(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
        #endregion

        #region 更新指定列 + Task<EntityEntry<TEntity>> UpdateIncludePropertiesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateIncludePropertiesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
        #endregion

        #region 更新指定列 + void UpdateIncludeProperties(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        void UpdateIncludeProperties(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);
        #endregion

        #region 更新指定列 + Task UpdateIncludePropertiesAsync(IAsyncEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateIncludePropertiesAsync(IAsyncEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);
        #endregion


        #region 更新指定列并立即保存 + EntityEntry<TEntity> UpdateIncludePropertiesSaveChanges(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> UpdateIncludePropertiesSaveChanges(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
        #endregion

        #region 更新指定列并立即保存 + Task<EntityEntry<TEntity>> UpdateIncludePropertiesSaveChangesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateIncludePropertiesSaveChangesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);
        #endregion

        #region 更新指定列并立即保存 + void UpdateIncludePropertiesSaveChanges(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        void UpdateIncludePropertiesSaveChanges(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);
        #endregion

        #region 更新指定列并立即保存 + Task UpdateIncludePropertiesSaveChangesAsync(IAsyncEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateIncludePropertiesSaveChangesAsync(IAsyncEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);
        #endregion


        #region 更新指定列 + EntityEntry<TEntity> UpdateIncludeProperties(TEntity entity, params string[] propertyNames)
        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> UpdateIncludeProperties(TEntity entity, params string[] propertyNames);
        #endregion

        #region 更新指定列 + Task<EntityEntry<TEntity>> UpdateIncludePropertiesAsync(TEntity entity, params string[] propertyNames)
        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateIncludePropertiesAsync(TEntity entity, params string[] propertyNames);
        #endregion

        #region 更新指定列 + void UpdateIncludeProperties(IEnumerable<TEntity> entities, params string[] propertyNames)
        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        void UpdateIncludeProperties(IEnumerable<TEntity> entities, params string[] propertyNames);
        #endregion

        #region 更新指定列 + Task UpdateIncludePropertiesAsync(IAsyncEnumerable<TEntity> entities, params string[] propertyNames)
        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateIncludePropertiesAsync(IAsyncEnumerable<TEntity> entities, params string[] propertyNames);
        #endregion


        #region 更新指定列并立即保存 + EntityEntry<TEntity> UpdateIncludePropertiesSaveChanges(TEntity entity, params string[] propertyNames)
        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> UpdateIncludePropertiesSaveChanges(TEntity entity, params string[] propertyNames);
        #endregion

        #region 更新指定列并立即保存 + Task<EntityEntry<TEntity>> UpdateIncludePropertiesSaveChangesAsync(TEntity entity, params string[] propertyNames)
        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateIncludePropertiesSaveChangesAsync(TEntity entity, params string[] propertyNames);
        #endregion

        #region 更新指定列并立即保存 + void UpdateIncludePropertiesSaveChanges(IEnumerable<TEntity> entities, params string[] propertyNames)
        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        void UpdateIncludePropertiesSaveChanges(IEnumerable<TEntity> entities, params string[] propertyNames);
        #endregion

        #region 更新指定列并立即保存 + Task UpdateIncludePropertiesSaveChangesAsync(IAsyncEnumerable<TEntity> entities, params string[] propertyNames)
        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateIncludePropertiesSaveChangesAsync(IAsyncEnumerable<TEntity> entities, params string[] propertyNames);
        #endregion




        // 排除特定列
        EntityEntry<TEntity> UpdateExcludeProperties(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        Task<EntityEntry<TEntity>> UpdateExcludePropertiesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        EntityEntry<TEntity> UpdateExcludePropertiesSaveChanges(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        Task<EntityEntry<TEntity>> UpdateExcludePropertiesSaveChangesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        void UpdateExcludeProperties(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        Task UpdateExcludePropertiesAsync(IAsyncEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        void UpdateExcludePropertiesSaveChanges(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        Task UpdateExcludePropertiesSaveChangesAsync(IAsyncEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);



        EntityEntry<TEntity> UpdateExcludeProperties(TEntity entity, params string[] propertyNames);

        Task<EntityEntry<TEntity>> UpdateExcludePropertiesAsync(TEntity entity, params string[] propertyNames);

        EntityEntry<TEntity> UpdateExcludePropertiesSaveChanges(TEntity entity, params string[] propertyNames);

        Task<EntityEntry<TEntity>> UpdateExcludePropertiesSaveChangesAsync(TEntity entity, params string[] propertyNames);

        void UpdateExcludeProperties(IEnumerable<TEntity> entities, params string[] propertyNames);

        Task UpdateExcludePropertiesAsync(IAsyncEnumerable<TEntity> entities, params string[] propertyNames);

        void UpdateExcludePropertiesSaveChanges(IEnumerable<TEntity> entities, params string[] propertyNames);

        Task UpdateExcludePropertiesSaveChangesAsync(IAsyncEnumerable<TEntity> entities, params string[] propertyNames);
    }
}
