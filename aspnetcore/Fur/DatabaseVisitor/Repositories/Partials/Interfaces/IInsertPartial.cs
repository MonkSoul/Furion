using Fur.DatabaseVisitor.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 新增操作分部类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        EntityEntry<TEntity> Insert(TEntity entity);
        void Insert(params TEntity[] entities);
        void Insert(IEnumerable<TEntity> entities);
        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity);
        Task InsertAsync(params TEntity[] entities);
        Task InsertAsync(IEnumerable<TEntity> entities);
        EntityEntry<TEntity> InsertSaveChanges(TEntity entity);
        void InsertSaveChanges(params TEntity[] entities);
        void InsertSaveChanges(IEnumerable<TEntity> entities);
        ValueTask<EntityEntry<TEntity>> InsertSaveChangesAsync(TEntity entity);
        Task InsertSaveChangesAsync(params TEntity[] entities);
        Task InsertSaveChangesAsync(IEnumerable<TEntity> entities);
    }
}
