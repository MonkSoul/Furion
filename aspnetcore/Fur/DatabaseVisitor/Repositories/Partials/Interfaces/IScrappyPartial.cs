using Fur.DatabaseVisitor.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 零碎操作 分部接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
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


        #region 判断记录是否存在 + bool Any(Expression<Func<TEntity, bool>> expression = null)
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>存在与否</returns>
        bool Any(Expression<Func<TEntity, bool>> expression = null);
        #endregion

        #region 判断记录是否存在 + Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null);
        #endregion


        #region 获取记录条数 + int Count(Expression<Func<TEntity, bool>> expression = null)
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>int</returns>

        int Count(Expression<Func<TEntity, bool>> expression = null);
        #endregion

        #region 获取记录条数 + Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>int</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null);
        #endregion


        #region 获取实体队列中最大实体 + TEntity Max()
        /// <summary>
        /// 获取实体队列中最大实体
        /// </summary>
        /// <returns><see cref="TEntity"/></returns>
        TEntity Max();
        #endregion

        #region 获取实体队列中最大实体 + Task<TEntity> MaxAsync()
        /// <summary>
        /// 获取实体队列中最大实体
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TEntity> MaxAsync();
        #endregion

        #region 获取最大值 + TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression)
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>最大值</returns>
        TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression);
        #endregion

        #region 获取最大值 + Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression);
        #endregion


        #region 获取实体队列中最小实体 + TEntity Min()
        /// <summary>
        /// 获取实体队列中最小实体
        /// </summary>
        /// <returns>实体</returns>
        TEntity Min();
        #endregion

        #region 获取实体队列中最小实体 + Task<TEntity> MinAsync()
        /// <summary>
        /// 获取实体队列中最小实体
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TEntity> MinAsync();
        #endregion

        #region 获取最小值 + TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression)
        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>最大值</returns>
        TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression);
        #endregion

        #region 获取最小值 + Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression);
        #endregion
    }
}
