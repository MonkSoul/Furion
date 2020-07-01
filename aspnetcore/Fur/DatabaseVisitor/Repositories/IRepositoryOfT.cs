using Fur.DatabaseVisitor.Dependencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IEntity, new()
    {
        public DbContext DbContext { get; }
        public DbSet<TEntity> Entity { get; }
        public DatabaseFacade Database { get; }
        public DbConnection DbConnection { get; }

        // 新增操作
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

        // 更新操作
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

        // 删除操作
        EntityEntry<TEntity> Delete(TEntity entity);
        void Delete(params TEntity[] entities);
        void Delete(IEnumerable<TEntity> entities);
        Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity);
        Task DeleteAsync(params TEntity[] entities);
        Task DeleteAsync(IEnumerable<TEntity> entities);
        EntityEntry<TEntity> DeleteSaveChanges(TEntity entity);
        void DeleteSaveChanges(params TEntity[] entities);
        void DeleteSaveChanges(IEnumerable<TEntity> entities);
        Task<EntityEntry<TEntity>> DeleteSaveChangesAsync(TEntity entity);
        Task DeleteSaveChangesAsync(params TEntity[] entities);
        Task DeleteSaveChangesAsync(IEnumerable<TEntity> entities);
        EntityEntry<TEntity> Delete(object id);
        Task<EntityEntry<TEntity>> DeleteAsync(object id);
        EntityEntry<TEntity> DeleteSaveChanges(object id);
        Task<EntityEntry<TEntity>> DeleteSaveChangesAsync(object id);
    }
}
