using Fur.DatabaseAccessor.Models.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 更新全部列操作 分部接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepository<TEntity> where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> Update(TEntity entity);

        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Update(params TEntity[] entities);

        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity);

        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateAsync(params TEntity[] entities);

        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> UpdateSaveChanges(TEntity entity);

        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entities"></param>
        void UpdateSaveChanges(params TEntity[] entities);

        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        void UpdateSaveChanges(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateSaveChangesAsync(TEntity entity);

        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task UpdateSaveChangesAsync(params TEntity[] entities);

        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entities"></param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateSaveChangesAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> UpdateIncludeProperties(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateIncludePropertiesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        void UpdateIncludeProperties(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateIncludePropertiesAsync(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> UpdateIncludePropertiesSaveChanges(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateIncludePropertiesSaveChangesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        void UpdateIncludePropertiesSaveChanges(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateIncludePropertiesSaveChangesAsync(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> UpdateIncludeProperties(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateIncludePropertiesAsync(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        void UpdateIncludeProperties(IEnumerable<TEntity> entities, params string[] propertyNames);

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateIncludePropertiesAsync(IEnumerable<TEntity> entities, params string[] propertyNames);

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> UpdateIncludePropertiesSaveChanges(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateIncludePropertiesSaveChangesAsync(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        void UpdateIncludePropertiesSaveChanges(IEnumerable<TEntity> entities, params string[] propertyNames);

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateIncludePropertiesSaveChangesAsync(IEnumerable<TEntity> entities, params string[] propertyNames);

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> UpdateExcludeProperties(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateExcludePropertiesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        void UpdateExcludeProperties(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateExcludePropertiesAsync(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> UpdateExcludePropertiesSaveChanges(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateExcludePropertiesSaveChangesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        void UpdateExcludePropertiesSaveChanges(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateExcludePropertiesSaveChangesAsync(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> UpdateExcludeProperties(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateExcludePropertiesAsync(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        void UpdateExcludeProperties(IEnumerable<TEntity> entities, params string[] propertyNames);

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateExcludePropertiesAsync(IEnumerable<TEntity> entities, params string[] propertyNames);

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> UpdateExcludePropertiesSaveChanges(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> UpdateExcludePropertiesSaveChangesAsync(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        void UpdateExcludePropertiesSaveChanges(IEnumerable<TEntity> entities, params string[] propertyNames);

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task"/></returns>
        Task UpdateExcludePropertiesSaveChangesAsync(IEnumerable<TEntity> entities, params string[] propertyNames);
    }
}