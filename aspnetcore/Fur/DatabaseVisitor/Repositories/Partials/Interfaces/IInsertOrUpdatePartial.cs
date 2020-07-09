using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Options;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 新增或更新操作分部类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        EntityEntry<TEntity> InsertOrUpdate(TEntity entity);

        Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity);

        EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity);

        Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity);

        EntityEntry<TEntity> InsertOrUpdate(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions);

        Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions);

        EntityEntry<TEntity> InsertOrUpdateSaveChanges(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions);

        Task<EntityEntry<TEntity>> InsertOrUpdateSaveChangesAsync(TEntity entity, DbTablePropertyUpdateOptions dbTablePropertyUpdateOptions, params Expression<Func<TEntity, object>>[] propertyExpressions);
    }
}
