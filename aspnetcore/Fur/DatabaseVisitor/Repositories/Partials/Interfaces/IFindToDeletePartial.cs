using Fur.DatabaseVisitor.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 查找并删除操作 分部接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 查找并真删除操作 + EntityEntry<TEntity> FindToDelete(object id)
        /// <summary>
        /// 查找并真删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToDelete(object id);
        #endregion

        #region 查找并真删除操作 + Task<EntityEntry<TEntity>> FindToDeleteAsync(object id)
        /// <summary>
        /// 查找并真删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToDeleteAsync(object id);
        #endregion


        #region 查找并真删除操作（抛异常） + EntityEntry<TEntity> FindToDelete(object id, Exception notFoundException)
        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">未找到异常</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToDelete(object id, Exception notFoundException);
        #endregion

        #region 查找并真删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, Exception notFoundException)
        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">未找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, Exception notFoundException);
        #endregion


        #region 查找并真删除操作（抛异常） + EntityEntry<TEntity> FindToDelete(object id, int oopsCode)
        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToDelete(object id, int oopsCode);
        #endregion

        #region 查找并真删除操作（抛异常） + EntityEntry<TEntity> FindToDelete(object id, string oopsCode)
        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToDelete(object id, string oopsCode);
        #endregion

        #region 查找并真删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, int oopsCode)
        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, int oopsCode);
        #endregion

        #region 查找并真删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, string oopsCode)
        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, string oopsCode);
        #endregion


        #region 查找并真删除操作并立即保存 + EntityEntry<TEntity> FindToDeleteSaveChanges(object id)
        /// <summary>
        /// 查找并真删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToDeleteSaveChanges(object id);
        #endregion

        #region 查找并真删除操作并立即保存 + Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id)
        /// <summary>
        /// 查找并真删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id);
        #endregion

        #region 查找并真删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToDeleteSaveChanges(object id, int oopsCode)
        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToDeleteSaveChanges(object id, int oopsCode);
        #endregion

        #region 查找并真删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToDeleteSaveChanges(object id, string oopsCode)
        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToDeleteSaveChanges(object id, string oopsCode);
        #endregion

        #region 查找并真删除操作并立即保存（抛异常）+ Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, int oopsCode)
        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, int oopsCode);
        #endregion

        #region 查找并真删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, string oopsCode)
        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, string oopsCode);
        #endregion


        #region 查找并真删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToDeleteSaveChanges(object id, Exception notFoundException)
        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">未找到异常</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToDeleteSaveChanges(object id, Exception notFoundException);
        #endregion

        #region 查找并真删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, Exception notFoundException)
        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">未找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, Exception notFoundException);
        #endregion


        #region 查找并假删除操作 + EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        /// <summary>
        /// 查找并假删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标记属性</param>
        /// <param name="flagValue">标记值</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue);
        #endregion

        #region 查找并假删除操作 + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        /// <summary>
        /// 查找并假删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标记属性</param>
        /// <param name="flagValue">标记值</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue);
        #endregion


        #region 查找并假删除操作（抛异常） + EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)
        /// <summary>
        /// 查找并假删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException);
        #endregion

        #region 查找并假删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)
        /// <summary>
        /// 查找并假删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException);
        #endregion

        #region 查找并假删除操作（抛异常） + EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)
        /// <summary>
        /// 查找并假删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode);
        #endregion

        #region 查找并假删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)
        /// <summary>
        /// 查找并假删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode);
        #endregion

        #region 查找并假删除操作（抛异常） + EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, string oopsCode)
        /// <summary>
        /// 查找并假删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, string oopsCode);
        #endregion

        #region 查找并假删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, string oopsCode)
        /// <summary>
        /// 查找并假删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, string oopsCode);
        #endregion


        #region 查找并假删除操作 + EntityEntry<TEntity> FindToFakeDelete(object id)
        /// <summary>
        /// 假删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FindToFakeDelete(object id);
        #endregion

        #region 查找并假删除操作 + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id)
        /// <summary>
        /// 假删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id);
        #endregion


        #region 查找并假删除操作（抛异常） + EntityEntry<TEntity> FindToFakeDelete(object id, Exception notFoundException)
        /// <summary>
        /// 查找并假删除操作（抛异常）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToFakeDelete(object id, Exception notFoundException);
        #endregion

        #region 查找并假删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Exception notFoundException)
        /// <summary>
        /// 查找并假删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Exception notFoundException);
        #endregion

        #region 查找并假删除操作（抛异常） + EntityEntry<TEntity> FindToFakeDelete(object id, int oopsCode)
        /// <summary>
        /// 查找并假删除操作（抛异常）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToFakeDelete(object id, int oopsCode);
        #endregion

        #region 查找并假删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, int oopsCode)
        /// <summary>
        /// 查找并假删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, int oopsCode);
        #endregion

        #region 查找并假删除操作（抛异常） + EntityEntry<TEntity> FindToFakeDelete(object id, string oopsCode)
        /// <summary>
        /// 查找并假删除操作（抛异常）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToFakeDelete(object id, string oopsCode);
        #endregion

        #region 查找并假删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, string oopsCode)
        /// <summary>
        /// 查找并假删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, string oopsCode);
        #endregion


        #region 查找并假删除操作并立即保存 + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        /// <summary>
        /// 查找并假删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue);
        #endregion

        #region 查找并假删除操作并立即保存 + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        /// <summary>
        /// 查找并假删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue);
        #endregion


        #region 查找并假删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)
        /// <summary>
        /// 查找并假删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns></returns>
        EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException);
        #endregion

        #region 查找并假删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)
        /// <summary>
        /// 查找并假删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException);
        #endregion

        #region 查找并假删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)
        /// <summary>
        /// 查找并假删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns></returns>
        EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode);
        #endregion

        #region 查找并假删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)
        /// <summary>
        /// 查找并假删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode);
        #endregion

        #region 查找并假删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, string oopsCode)
        /// <summary>
        /// 查找并假删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns></returns>
        EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, string oopsCode);
        #endregion

        #region 查找并假删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, string oopsCode)
        /// <summary>
        /// 查找并假删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, string oopsCode);
        #endregion


        #region 查找并假删除操作并立即保存 + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id)
        /// <summary>
        /// 查找并假删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id);
        #endregion

        #region 查找并假删除操作并立即保存 + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id)
        /// <summary>
        /// 查找并假删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id);
        #endregion

        #region 查找并假删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Exception notFoundException)
        /// <summary>
        /// 查找并假删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Exception notFoundException);
        #endregion

        #region 查找并假删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Exception notFoundException)
        /// <summary>
        /// 查找并假删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Exception notFoundException);
        #endregion


        #region 查找并假删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, int oopsCode)
        /// <summary>
        /// 查找并假删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, int oopsCode);
        #endregion

        #region 查找并假删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, int oopsCode)
        /// <summary>
        /// 查找并假删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, int oopsCode);
        #endregion

        #region 查找并假删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, string oopsCode)
        /// <summary>
        /// 查找并假删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, string oopsCode);
        #endregion

        #region 查找并假删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, string oopsCode)
        /// <summary>
        /// 查找并假删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, string oopsCode);
        #endregion
    }
}
