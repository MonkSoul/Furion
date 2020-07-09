using Fur.DatabaseVisitor.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 零碎操作分部类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {

        EntityEntry<TEntity> EntityEntry(TEntity entity);

        void Attach(TEntity entity);

        void AttachRange(TEntity[] entities);
        bool IsKeySet(TEntity entity);

        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess);

        bool Exists(Expression<Func<TEntity, bool>> expression = null);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression = null);

        int Count(Expression<Func<TEntity, bool>> expression = null);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null);

        TEntity Max();

        TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression);

        Task<TEntity> MaxAsync();

        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression);

        TEntity Min();

        TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression);

        Task<TEntity> MinAsync();

        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression);
    }
}
