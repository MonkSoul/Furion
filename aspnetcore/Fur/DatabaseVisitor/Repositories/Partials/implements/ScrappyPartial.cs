using Fur.DatabaseVisitor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 零碎操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 判断实体是否设置了主键 + public virtual bool IsKeySet(TEntity entity)

        /// <summary>
        /// 判断实体是否设置了主键
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>是或否</returns>
        public virtual bool IsKeySet(TEntity entity)
        {
            return EntityEntry(entity).IsKeySet;
        }

        #endregion 判断实体是否设置了主键 + public virtual bool IsKeySet(TEntity entity)

        #region 获取实体变更信息 + public virtual EntityEntry<TEntity> EntityEntry(TEntity entity)

        /// <summary>
        /// 获取实体变更信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体变更包装对象</returns>
        public virtual EntityEntry<TEntity> EntityEntry(TEntity entity) => DbContext.Entry(entity);

        #endregion 获取实体变更信息 + public virtual EntityEntry<TEntity> EntityEntry(TEntity entity)

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

        #endregion 获取实体属性变更信息 + public virtual PropertyEntry EntityEntryProperty(TEntity entity, Expression<Func<TEntity, object>> propertyExpression)

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

        #endregion 获取实体属性变更信息 + public virtual PropertyEntry EntityEntryProperty(TEntity entity, string propertyName)

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

        #endregion 获取实体属性变更信息 +  public virtual PropertyEntry EntityEntryProperty(EntityEntry<TEntity> entityEntry, Expression<Func<TEntity, object>> propertyExpression)

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

        #endregion 获取实体属性变更信息 + public virtual PropertyEntry EntityEntryProperty(EntityEntry<TEntity> entityEntry, string propertyName)

        #region 提交更改操作 + public virtual int SaveChanges()

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <returns>int</returns>
        public virtual int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        #endregion 提交更改操作 + public virtual int SaveChanges()

        #region 提交更改操作 + public virtual Task<int> SaveChangesAsync()

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        #endregion 提交更改操作 + public virtual Task<int> SaveChangesAsync()

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

        #endregion 提交更改操作 + public virtual int SaveChanges(bool acceptAllChangesOnSuccess)

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

        #endregion 提交更改操作 + public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess)

        #region 附加实体到上下文中 + public virtual EntityEntry<TEntity> Attach(TEntity entity)

        /// <summary>
        /// 附加实体到上下文中
        /// <para>此时实体状态为 <c>Unchanged</c> 状态</para>
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> Attach(TEntity entity)
        {
            var entityEntry = EntityEntry(entity);
            if (entityEntry.State == EntityState.Detached)
            {
                DbContext.Attach(entity);
            }
            return entityEntry;
        }

        #endregion 附加实体到上下文中 + public virtual EntityEntry<TEntity> Attach(TEntity entity)

        #region 附加实体到上下文中 + public virtual void AttachRange(IEnumerable<TEntity> entities)

        /// <summary>
        /// 附加实体到上下文中
        /// <para>此时实体状态为 <c>Unchanged</c> 状态</para>
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void AttachRange(IEnumerable<TEntity> entities)
        {
            var noTrackEntites = entities.Where(u => EntityEntry(u).State == EntityState.Deleted);
            DbContext.AttachRange(noTrackEntites);
        }

        #endregion 附加实体到上下文中 + public virtual void AttachRange(IEnumerable<TEntity> entities)

        #region 获取所有的数据库上下文 + public virtual IEnumerable<DbContext> GetDbContexts()

        /// <summary>
        /// 获取所有的数据库上下文
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<DbContext> GetDbContexts()
        {
            return _dbContextPool.GetDbContexts();
        }

        #endregion 获取所有的数据库上下文 + public virtual IEnumerable<DbContext> GetDbContexts()

        #region 提交所有已更改的数据库上下文 +  public virtual int SavePoolChanges()

        /// <summary>
        /// 提交所有已更改的数据库上下文
        /// </summary>
        /// <returns>受影响行数</returns>
        public virtual int SavePoolChanges()
        {
            return _dbContextPool.SavePoolChanges();
        }

        #endregion 提交所有已更改的数据库上下文 +  public virtual int SavePoolChanges()

        #region 异步提交所有已更改的数据库上下文 + public virtual Task<int> SavePoolChangesAsync()

        /// <summary>
        /// 异步提交所有已更改的数据库上下文
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<int> SavePoolChangesAsync()
        {
            return _dbContextPool.SavePoolChangesAsync();
        }

        #endregion 异步提交所有已更改的数据库上下文 + public virtual Task<int> SavePoolChangesAsync()
    }
}