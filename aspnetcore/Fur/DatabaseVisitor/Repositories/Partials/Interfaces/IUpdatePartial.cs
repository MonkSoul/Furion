using Fur.DatabaseVisitor.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 更新操作分部类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        EntityEntry<TEntity> Update(TEntity entity);

        void Update(params TEntity[] entities);

        void Update(IEnumerable<TEntity> entities);

        Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity);

        Task UpdateAsync(params TEntity[] entities);

        Task UpdateAsync(IEnumerable<TEntity> entities);

        EntityEntry<TEntity> UpdateSaveChanges(TEntity entity);

        void UpdateSaveChanges(params TEntity[] entities);

        void UpdateSaveChanges(IEnumerable<TEntity> entities);

        Task<EntityEntry<TEntity>> UpdateSaveChangesAsync(TEntity entity);

        Task UpdateSaveChangesAsync(params TEntity[] entities);

        Task UpdateSaveChangesAsync(IEnumerable<TEntity> entities);

        // 更新特定列
        EntityEntry<TEntity> UpdateIncludeProperties(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        Task<EntityEntry<TEntity>> UpdateIncludePropertiesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        EntityEntry<TEntity> UpdateIncludePropertiesSaveChanges(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        Task<EntityEntry<TEntity>> UpdateIncludePropertiesSaveChangesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        void UpdateIncludeProperties(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        Task UpdateIncludePropertiesAsync(IAsyncEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        void UpdateIncludePropertiesSaveChanges(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        Task UpdateIncludePropertiesSaveChangesAsync(IAsyncEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        // 排除特定列
        EntityEntry<TEntity> UpdateExcludeProperties(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        Task<EntityEntry<TEntity>> UpdateExcludePropertiesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        EntityEntry<TEntity> UpdateExcludePropertiesSaveChanges(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        Task<EntityEntry<TEntity>> UpdateExcludePropertiesSaveChangesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        void UpdateExcludeProperties(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        Task UpdateExcludePropertiesAsync(IAsyncEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        void UpdateExcludePropertiesSaveChanges(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);

        Task UpdateExcludePropertiesSaveChangesAsync(IAsyncEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);
    }
}
