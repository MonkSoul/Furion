using Fur.DatabaseVisitor.Dependencies;
using Fur.DependencyInjection.Lifetimes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IEntity, new()
    {
        public EFCoreRepositoryOfT(DbContext dbContext)
        {
            DbContext = dbContext;
            Entity = DbContext.Set<TEntity>();
        }

        public virtual DbContext DbContext { get; }
        public virtual DbSet<TEntity> Entity { get; }
        public virtual DatabaseFacade Database => DbContext.Database;
        public virtual DbConnection DbConnection => DbContext.Database.GetDbConnection();

        public EntityEntry<TEntity> Delete(TEntity entity)
        {
            return Entity.Remove(entity);
        }

        public void Delete(params TEntity[] entities)
        {
            Entity.RemoveRange(entities);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            Entity.RemoveRange(entities);
        }

        public EntityEntry<TEntity> Delete(object id)
        {
            var entity = Entity.Find(id);
            return Delete(entity);
        }

        public Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity)
        {
            return Task.FromResult(Entity.Remove(entity));
        }

        public Task DeleteAsync(params TEntity[] entities)
        {
            Entity.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            Entity.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public async Task<EntityEntry<TEntity>> DeleteAsync(object id)
        {
            var entity = await Entity.FindAsync(id);
            return await DeleteAsync(entity);
        }

        public EntityEntry<TEntity> DeleteSaveChanges(TEntity entity)
        {
            var trackEntity = Delete(entity);
            DbContext.SaveChanges();
            return trackEntity;
        }

        public void DeleteSaveChanges(params TEntity[] entities)
        {
            Delete(entities);
            DbContext.SaveChanges();
        }

        public void DeleteSaveChanges(IEnumerable<TEntity> entities)
        {
            Delete(entities);
            DbContext.SaveChanges();
        }

        public EntityEntry<TEntity> DeleteSaveChanges(object id)
        {
            var trackEntity = Delete(id);
            DbContext.SaveChanges();
            return trackEntity;
        }

        public async Task<EntityEntry<TEntity>> DeleteSaveChangesAsync(TEntity entity)
        {
            var trackEntity = await DeleteAsync(entity);
            await DbContext.SaveChangesAsync();
            return trackEntity;
        }

        public async Task DeleteSaveChangesAsync(params TEntity[] entities)
        {
            await DeleteAsync(entities);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            await DeleteAsync(entities);
            await DbContext.SaveChangesAsync();
        }

        public async Task<EntityEntry<TEntity>> DeleteSaveChangesAsync(object id)
        {
            var trackEntity = await DeleteAsync(id);
            await DbContext.SaveChangesAsync();
            return trackEntity;
        }

        // 新增操作
        public virtual EntityEntry<TEntity> Insert(TEntity entity)
        {
            return Entity.Add(entity);
        }

        public virtual void Insert(params TEntity[] entities)
        {
            Entity.AddRange(entities);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            Entity.AddRange(entities);
        }

        public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity)
        {
            return Entity.AddAsync(entity);
        }

        public virtual Task InsertAsync(params TEntity[] entities)
        {
            return Entity.AddRangeAsync();
        }

        public virtual Task InsertAsync(IEnumerable<TEntity> entities)
        {
            return Entity.AddRangeAsync();
        }

        public virtual EntityEntry<TEntity> InsertSaveChanges(TEntity entity)
        {
            var trackEntity = Insert(entity);
            DbContext.SaveChanges();
            return trackEntity;
        }

        public virtual void InsertSaveChanges(params TEntity[] entities)
        {
            Insert(entities);
            DbContext.SaveChanges();
        }

        public virtual void InsertSaveChanges(IEnumerable<TEntity> entities)
        {
            Insert(entities);
            DbContext.SaveChanges();
        }

        public virtual async ValueTask<EntityEntry<TEntity>> InsertSaveChangesAsync(TEntity entity)
        {
            var trackEntity = await InsertAsync(entity);
            await DbContext.SaveChangesAsync();
            return trackEntity;
        }

        public virtual async Task InsertSaveChangesAsync(params TEntity[] entities)
        {
            await InsertAsync(entities);
            await DbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public virtual async Task InsertSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            await InsertAsync(entities);
            await DbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }

        // 更新操作
        public EntityEntry<TEntity> Update(TEntity entity)
        {
            return Entity.Update(entity);
        }

        public void Update(params TEntity[] entities)
        {
            Entity.UpdateRange(entities);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            Entity.UpdateRange(entities);
        }

        public Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity)
        {
            var trackEntity = Entity.Update(entity);
            return Task.FromResult(trackEntity);
        }

        public Task UpdateAsync(params TEntity[] entities)
        {
            Entity.UpdateRange(entities);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            Entity.UpdateRange(entities);
            return Task.CompletedTask;
        }

        public EntityEntry<TEntity> UpdateSaveChanges(TEntity entity)
        {
            var trackEntity = Update(entity);
            DbContext.SaveChanges();
            return trackEntity;
        }

        public void UpdateSaveChanges(params TEntity[] entities)
        {
            Update(entities);
            DbContext.SaveChanges();
        }

        public void UpdateSaveChanges(IEnumerable<TEntity> entities)
        {
            Update(entities);
            DbContext.SaveChanges();
        }

        public async Task<EntityEntry<TEntity>> UpdateSaveChangesAsync(TEntity entity)
        {
            var trackEntities = await UpdateAsync(entity);
            await DbContext.SaveChangesAsync();
            return trackEntities;
        }

        public async Task UpdateSaveChangesAsync(params TEntity[] entities)
        {
            await UpdateAsync(entities);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            await UpdateAsync(entities);
            await DbContext.SaveChangesAsync();
        }
    }
}
