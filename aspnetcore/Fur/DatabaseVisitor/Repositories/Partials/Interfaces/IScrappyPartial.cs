using Fur.DatabaseVisitor.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 零碎操作 分部类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 获取实体变更信息 + EntityEntry<TEntity> EntityEntry(TEntity entity)
        /// <summary>
        /// 获取实体变更信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体变更包装对象</returns>
        EntityEntry<TEntity> EntityEntry(TEntity entity);
        #endregion

        #region 获取实体属性变更信息 + PropertyEntry EntityEntryProperty(TEntity entity, Expression<Func<TEntity, object>> propertyExpression)
        /// <summary>
        /// 获取实体属性变更信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpression">变更属性</param>
        /// <returns><see cref="PropertyEntry"/></returns>
        PropertyEntry EntityEntryProperty(TEntity entity, Expression<Func<TEntity, object>> propertyExpression);
        #endregion

        #region 获取实体属性变更信息 + PropertyEntry EntityEntryProperty(TEntity entity, string propertyName)
        /// <summary>
        /// 获取实体属性变更信息
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyName">变更属性</param>
        /// <returns><see cref="PropertyEntry"/></returns>
        PropertyEntry EntityEntryProperty(TEntity entity, string propertyName);
        #endregion

        #region 获取实体属性变更信息 + PropertyEntry EntityEntryProperty(EntityEntry<TEntity> entityEntry, Expression<Func<TEntity, object>> propertyExpression)
        /// <summary>
        /// 获取实体属性变更信息
        /// </summary>
        /// <param name="entityEntry">实体变更包装对象</param>
        /// <param name="propertyExpression">属性</param>
        /// <returns><see cref="PropertyEntry"/></returns>
        PropertyEntry EntityEntryProperty(EntityEntry<TEntity> entityEntry, Expression<Func<TEntity, object>> propertyExpression);
        #endregion

        #region 获取实体属性变更信息 + PropertyEntry EntityEntryProperty(EntityEntry<TEntity> entityEntry, string propertyName)
        /// <summary>
        /// 获取实体属性变更信息
        /// </summary>
        /// <param name="entityEntry">实体变更包装对象</param>
        /// <param name="propertyName">属性</param>
        /// <returns><see cref="PropertyEntry"/></returns>
        PropertyEntry EntityEntryProperty(EntityEntry<TEntity> entityEntry, string propertyName);
        #endregion


        #region 提交更改操作 + int SaveChanges()
        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <returns>int</returns>
        int SaveChanges();
        #endregion

        #region 提交更改操作 + Task<int> SaveChangesAsync()
        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<int> SaveChangesAsync();
        #endregion

        #region 提交更改操作 + int SaveChanges(bool acceptAllChangesOnSuccess)
        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">是否提交所有的更改</param>
        /// <returns>int</returns>
        int SaveChanges(bool acceptAllChangesOnSuccess);
        #endregion

        #region 提交更改操作 + Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess)
        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">是否提交所有的更改</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess);
        #endregion


        #region 附加实体到上下文中 + void Attach(TEntity entity)
        /// <summary>
        /// 附加实体到上下文中
        /// <para>此时实体状态为 <c>Unchanged</c> 状态</para>
        /// </summary>
        /// <param name="entity">实体</param>
        void Attach(TEntity entity);
        #endregion

        #region 附加实体到上下文中 + void AttachRange(TEntity[] entities)
        /// <summary>
        /// 附加实体到上下文中
        /// <para>此时实体状态为 <c>Unchanged</c> 状态</para>
        /// </summary>
        /// <param name="entities">多个实体</param>
        void AttachRange(TEntity[] entities);
        #endregion



        bool IsKeySet(TEntity entity);



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
