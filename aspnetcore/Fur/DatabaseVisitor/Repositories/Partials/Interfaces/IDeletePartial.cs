using Fur.DatabaseVisitor.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 删除操作分部类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
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

        // 假删除
        EntityEntry<TEntity> FakeDelete(TEntity entity, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue);

        void FakeDelete(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue);

        Task<EntityEntry<TEntity>> FakeDeleteAsync(TEntity entity, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue);

        Task FakeDeleteAsync(IAsyncEnumerable<TEntity> entities, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue);

        EntityEntry<TEntity> FakeDeleteSaveChanges(TEntity entity, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue);

        void FakeDeleteSaveChanges(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue);

        Task<EntityEntry<TEntity>> FakeDeleteSaveChangesAsync(TEntity entity, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue);

        Task FakeDeleteSaveChangesAsync(IAsyncEnumerable<TEntity> entities, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue);

        EntityEntry<TEntity> FakeDelete(object id, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue);

        Task<EntityEntry<TEntity>> FakeDeleteAsync(object id, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue);

        EntityEntry<TEntity> FakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue);

        Task<EntityEntry<TEntity>> FakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> fakeDeleteProperty, object flagValue);
    }
}
