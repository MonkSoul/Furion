using Fur.DatabaseAccessor.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 零碎操作 分部接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntityBase, new()
    {
        #region 判断实体是否设置了主键 + bool IsKeySet(TEntity entity)

        /// <summary>
        /// 判断实体是否设置了主键
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>是或否</returns>
        bool IsKeySet(TEntity entity);

        #endregion

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

        #region 附加实体到上下文中 + EntityEntry<TEntity> Attach(TEntity entity)

        /// <summary>
        /// 附加实体到上下文中
        /// <para>此时实体状态为 <c>Unchanged</c> 状态</para>
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> Attach(TEntity entity);

        #endregion

        #region 附加实体到上下文中 + void AttachRange(IEnumerable<TEntity> entites)

        /// <summary>
        /// 附加实体到上下文中
        /// <para>此时实体状态为 <c>Unchanged</c> 状态</para>
        /// </summary>
        /// <param name="entities">多个实体</param>
        void AttachRange(IEnumerable<TEntity> entites);

        #endregion

        #region 获取所有的数据库上下文 + IEnumerable<DbContext> GetDbContexts()

        /// <summary>
        /// 获取所有的数据库上下文
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<DbContext> GetDbContexts();

        #endregion

        #region 提交所有已更改的数据库上下文 +  int SavePoolChanges()

        /// <summary>
        /// 提交所有已更改的数据库上下文
        /// </summary>
        /// <returns>受影响行数</returns>
        int SavePoolChanges();

        #endregion

        #region 异步提交所有已更改的数据库上下文 + Task<int> SavePoolChangesAsync()

        /// <summary>
        /// 异步提交所有已更改的数据库上下文
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<int> SavePoolChangesAsync();

        #endregion
    }
}