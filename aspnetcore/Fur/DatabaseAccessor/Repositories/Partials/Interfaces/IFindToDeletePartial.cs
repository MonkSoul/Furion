using Fur.DatabaseAccessor.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
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

        #endregion 查找并真删除操作 + EntityEntry<TEntity> FindToDelete(object id)

        #region 查找并真删除操作 + Task<EntityEntry<TEntity>> FindToDeleteAsync(object id)

        /// <summary>
        /// 查找并真删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToDeleteAsync(object id);

        #endregion 查找并真删除操作 + Task<EntityEntry<TEntity>> FindToDeleteAsync(object id)

        #region 查找并真删除操作（抛异常） + EntityEntry<TEntity> FindToDelete(object id, Exception notFoundException)

        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">未找到异常</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToDelete(object id, Exception notFoundException);

        #endregion 查找并真删除操作（抛异常） + EntityEntry<TEntity> FindToDelete(object id, Exception notFoundException)

        #region 查找并真删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, Exception notFoundException)

        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">未找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, Exception notFoundException);

        #endregion 查找并真删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, Exception notFoundException)

        #region 查找并真删除操作（抛异常） + EntityEntry<TEntity> FindToDelete(object id, int oopsCode)

        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToDelete(object id, int oopsCode);

        #endregion 查找并真删除操作（抛异常） + EntityEntry<TEntity> FindToDelete(object id, int oopsCode)

        #region 查找并真删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, int oopsCode)

        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, int oopsCode);

        #endregion 查找并真删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, int oopsCode)

        #region 查找并真删除操作并立即保存 + EntityEntry<TEntity> FindToDeleteSaveChanges(object id)

        /// <summary>
        /// 查找并真删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToDeleteSaveChanges(object id);

        #endregion 查找并真删除操作并立即保存 + EntityEntry<TEntity> FindToDeleteSaveChanges(object id)

        #region 查找并真删除操作并立即保存 + Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id)

        /// <summary>
        /// 查找并真删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id);

        #endregion 查找并真删除操作并立即保存 + Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id)

        #region 查找并真删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToDeleteSaveChanges(object id, int oopsCode)

        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToDeleteSaveChanges(object id, int oopsCode);

        #endregion 查找并真删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToDeleteSaveChanges(object id, int oopsCode)

        #region 查找并真删除操作并立即保存（抛异常）+ Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, int oopsCode)

        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, int oopsCode);

        #endregion 查找并真删除操作并立即保存（抛异常）+ Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, int oopsCode)

        #region 查找并真删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToDeleteSaveChanges(object id, Exception notFoundException)

        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">未找到异常</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToDeleteSaveChanges(object id, Exception notFoundException);

        #endregion 查找并真删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToDeleteSaveChanges(object id, Exception notFoundException)

        #region 查找并真删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, Exception notFoundException)

        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">未找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, Exception notFoundException);

        #endregion 查找并真删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, Exception notFoundException)

        #region 查找并软删除操作 + EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)

        /// <summary>
        /// 查找并软删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标记属性</param>
        /// <param name="flagValue">标记值</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue);

        #endregion 查找并软删除操作 + EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)

        #region 查找并软删除操作 + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)

        /// <summary>
        /// 查找并软删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标记属性</param>
        /// <param name="flagValue">标记值</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue);

        #endregion 查找并软删除操作 + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)

        #region 查找并软删除操作（抛异常） + EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException);

        #endregion 查找并软删除操作（抛异常） + EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)

        #region 查找并软删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException);

        #endregion 查找并软删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)

        #region 查找并软删除操作（抛异常） + EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode);

        #endregion 查找并软删除操作（抛异常） + EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)

        #region 查找并软删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode);

        #endregion 查找并软删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)

        #region 查找并软删除操作 + EntityEntry<TEntity> FindToFakeDelete(object id)

        /// <summary>
        /// 软删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FindToFakeDelete(object id);

        #endregion 查找并软删除操作 + EntityEntry<TEntity> FindToFakeDelete(object id)

        #region 查找并软删除操作 + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id)

        /// <summary>
        /// 软删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id);

        #endregion 查找并软删除操作 + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id)

        #region 查找并软删除操作（抛异常） + EntityEntry<TEntity> FindToFakeDelete(object id, Exception notFoundException)

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToFakeDelete(object id, Exception notFoundException);

        #endregion 查找并软删除操作（抛异常） + EntityEntry<TEntity> FindToFakeDelete(object id, Exception notFoundException)

        #region 查找并软删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Exception notFoundException)

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Exception notFoundException);

        #endregion 查找并软删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Exception notFoundException)

        #region 查找并软删除操作（抛异常） + EntityEntry<TEntity> FindToFakeDelete(object id, int oopsCode)

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToFakeDelete(object id, int oopsCode);

        #endregion 查找并软删除操作（抛异常） + EntityEntry<TEntity> FindToFakeDelete(object id, int oopsCode)

        #region 查找并软删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, int oopsCode)

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, int oopsCode);

        #endregion 查找并软删除操作（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, int oopsCode)

        #region 查找并软删除操作并立即保存 + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)

        /// <summary>
        /// 查找并软删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue);

        #endregion 查找并软删除操作并立即保存 + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)

        #region 查找并软删除操作并立即保存 + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)

        /// <summary>
        /// 查找并软删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue);

        #endregion 查找并软删除操作并立即保存 + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)

        #region 查找并软删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns></returns>
        EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException);

        #endregion 查找并软删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)

        #region 查找并软删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException);

        #endregion 查找并软删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)

        #region 查找并软删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns></returns>
        EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode);

        #endregion 查找并软删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)

        #region 查找并软删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode);

        #endregion 查找并软删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)

        #region 查找并软删除操作并立即保存 + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id)

        /// <summary>
        /// 查找并软删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id);

        #endregion 查找并软删除操作并立即保存 + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id)

        #region 查找并软删除操作并立即保存 + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id)

        /// <summary>
        /// 查找并软删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id);

        #endregion 查找并软删除操作并立即保存 + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id)

        #region 查找并软删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Exception notFoundException)

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Exception notFoundException);

        #endregion 查找并软删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Exception notFoundException)

        #region 查找并软删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Exception notFoundException)

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Exception notFoundException);

        #endregion 查找并软删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Exception notFoundException)

        #region 查找并软删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, int oopsCode)

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, int oopsCode);

        #endregion 查找并软删除操作并立即保存（抛异常） + EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, int oopsCode)

        #region 查找并软删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, int oopsCode)

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, int oopsCode);

        #endregion 查找并软删除操作并立即保存（抛异常） + Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, int oopsCode)
    }
}