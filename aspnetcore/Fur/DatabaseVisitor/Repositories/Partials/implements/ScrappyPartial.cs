using Fur.DatabaseVisitor.Entities;
using Fur.DependencyInjection.Lifetimes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 零碎操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 获取实体变更信息 + public virtual EntityEntry<TEntity> EntityEntry(TEntity entity)
        /// <summary>
        /// 获取实体变更信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体变更包装对象</returns>
        public virtual EntityEntry<TEntity> EntityEntry(TEntity entity) => DbContext.Entry(entity);
        #endregion

        #region 获取实体属性变更信息 + public virtual PropertyEntry EntityEntryProperty(TEntity entity, Expression<Func<TEntity, object>> propertyExpression)
        /// <summary>
        /// 获取实体属性变更信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpression">变更属性</param>
        /// <returns><see cref="PropertyEntry"/></returns>
        public virtual PropertyEntry EntityEntryProperty(TEntity entity, Expression<Func<TEntity, object>> propertyExpression)
        {
            return EntityEntry(entity).Property(propertyExpression);
        }
        #endregion

        #region 获取实体属性变更信息 + public virtual PropertyEntry EntityEntryProperty(TEntity entity, string propertyName)
        /// <summary>
        /// 获取实体属性变更信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyName">变更属性</param>
        /// <returns><see cref="PropertyEntry"/></returns>
        public virtual PropertyEntry EntityEntryProperty(TEntity entity, string propertyName)
        {
            return EntityEntry(entity).Property(propertyName);
        }
        #endregion

        #region 获取实体属性变更信息 +  public virtual PropertyEntry EntityEntryProperty(EntityEntry<TEntity> entityEntry, Expression<Func<TEntity, object>> propertyExpression)
        /// <summary>
        /// 获取实体属性变更信息
        /// </summary>
        /// <param name="entityEntry">实体变更包装对象</param>
        /// <param name="propertyExpression">属性</param>
        /// <returns><see cref="PropertyEntry"/></returns>
        public virtual PropertyEntry EntityEntryProperty(EntityEntry<TEntity> entityEntry, Expression<Func<TEntity, object>> propertyExpression)
        {
            return entityEntry.Property(propertyExpression);
        }
        #endregion

        #region 获取实体属性变更信息 + public virtual PropertyEntry EntityEntryProperty(EntityEntry<TEntity> entityEntry, string propertyName)
        /// <summary>
        /// 获取实体属性变更信息
        /// </summary>
        /// <param name="entityEntry">实体变更包装对象</param>
        /// <param name="propertyName">属性</param>
        /// <returns><see cref="PropertyEntry"/></returns>
        public virtual PropertyEntry EntityEntryProperty(EntityEntry<TEntity> entityEntry, string propertyName)
        {
            return entityEntry.Property(propertyName);
        }
        #endregion


        #region 提交更改操作 + public virtual int SaveChanges()
        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <returns>int</returns>
        public virtual int SaveChanges()
        {
            return DbContext.SaveChanges();
        }
        #endregion

        #region 提交更改操作 + public virtual Task<int> SaveChangesAsync()
        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }
        #endregion

        #region 提交更改操作 + public virtual int SaveChanges(bool acceptAllChangesOnSuccess)
        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">是否提交所有的更改</param>
        /// <returns>int</returns>
        public virtual int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return DbContext.SaveChanges(acceptAllChangesOnSuccess);
        }
        #endregion

        #region 提交更改操作 + public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess)
        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">是否提交所有的更改</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess)
        {
            return DbContext.SaveChangesAsync(acceptAllChangesOnSuccess);
        }
        #endregion


        #region 附加实体到上下文中 + public virtual void Attach(TEntity entity)
        /// <summary>
        /// 附加实体到上下文中
        /// <para>此时实体状态为 <c>Unchanged</c> 状态</para>
        /// </summary>
        /// <param name="entity">实体</param>
        public virtual void Attach(TEntity entity)
        {
            if (EntityEntry(entity).State == EntityState.Detached)
            {
                DbContext.Attach(entity);
            }
        }
        #endregion

        #region 附加实体到上下文中 + public virtual void AttachRange(TEntity[] entities)
        /// <summary>
        /// 附加实体到上下文中
        /// <para>此时实体状态为 <c>Unchanged</c> 状态</para>
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void AttachRange(TEntity[] entities)
        {
            var noTrackEntites = entities.Where(u => EntityEntry(u).State == EntityState.Deleted);
            DbContext.AttachRange(noTrackEntites);
        }
        #endregion




        public virtual bool Exists(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entity.Any() : Entity.Any(expression);
        }

        public virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entity.AnyAsync() : Entity.AnyAsync(expression);
        }

        public virtual int Count(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entity.Count() : Entity.Count(expression);
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entity.CountAsync() : Entity.CountAsync(expression);
        }

        public virtual TEntity Max()
        {
            return Entity.Max();
        }

        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entity.Max(expression);
        }

        public virtual Task<TEntity> MaxAsync()
        {
            return Entity.MaxAsync();
        }

        public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entity.MaxAsync(expression);
        }

        public virtual TEntity Min()
        {
            return Entity.Min();
        }

        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entity.Min(expression);
        }

        public virtual Task<TEntity> MinAsync()
        {
            return Entity.MinAsync();
        }

        public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entity.MinAsync(expression);
        }

        public virtual bool IsKeySet(TEntity entity)
        {
            return EntityEntry(entity).IsKeySet;
        }


    }
}
