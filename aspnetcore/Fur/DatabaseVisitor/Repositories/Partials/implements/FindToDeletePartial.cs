using Fur.DatabaseVisitor.Entities;
using Fur.DependencyInjection.Lifetimes;
using Fur.FriendlyException;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 查找并删除操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 查找并真删除操作 + public virtual EntityEntry<TEntity> FindToDelete(object id)
        /// <summary>
        /// 查找并真删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToDelete(object id)
        {
            var entity = Find(id);
            return Delete(entity);
        }
        #endregion

        #region 查找并真删除操作 + public virtual async Task<EntityEntry<TEntity>> FindToDeleteAsync(object id)
        /// <summary>
        /// 查找并真删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToDeleteAsync(object id)
        {
            var entity = await FindAsync(id);
            var entityEntry = await DeleteAsync(entity);
            return entityEntry;
        }
        #endregion


        #region 查找并真删除操作（抛异常） + public virtual EntityEntry<TEntity> FindToDelete(object id, Exception notFoundException)
        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">未找到异常</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToDelete(object id, Exception notFoundException)
        {
            var entity = Find(id) ?? throw notFoundException;
            return Delete(entity);
        }
        #endregion

        #region 查找并真删除操作（抛异常） + public virtual async Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, Exception notFoundException)
        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">未找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, Exception notFoundException)
        {
            var entity = (await FindAsync(id)) ?? throw notFoundException;
            var entityEntry = await DeleteAsync(entity);
            return entityEntry;
        }
        #endregion


        #region 查找并真删除操作（抛异常） + public virtual EntityEntry<TEntity> FindToDelete(object id, int oopsCode)
        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">错误状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToDelete(object id, int oopsCode)
        {
            var entity = Find(id) ?? throw Oops.Set(oopsCode, true);
            return Delete(entity);
        }
        #endregion

        #region 查找并真删除操作（抛异常） + public virtual EntityEntry<TEntity> FindToDelete(object id, string oopsCode)
        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">错误状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToDelete(object id, string oopsCode)
        {
            var entity = Find(id) ?? throw Oops.Set(oopsCode, true);
            return Delete(entity);
        }
        #endregion

        #region 查找并真删除操作（抛异常） + public virtual async Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, int oopsCode)
        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">错误状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, int oopsCode)
        {
            var entity = (await FindAsync(id)) ?? throw Oops.Set(oopsCode, true);
            var entityEntry = await DeleteAsync(entity);
            return entityEntry;
        }
        #endregion

        #region 查找并真删除操作（抛异常） + public virtual async Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, string oopsCode)
        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">错误状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, string oopsCode)
        {
            var entity = (await FindAsync(id)) ?? throw Oops.Set(oopsCode, true);
            var entityEntry = await DeleteAsync(entity);
            return entityEntry;
        }
        #endregion


        #region 查找并真删除操作并立即保存 + public virtual EntityEntry<TEntity> FindToDeleteSaveChanges(object id)
        /// <summary>
        /// 查找并真删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToDeleteSaveChanges(object id)
        {
            var entityEntry = FindToDelete(id);
            SaveChanges();
            return entityEntry;
        }
        #endregion

        #region 查找并真删除操作并立即保存 + public virtual async Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id)
        /// <summary>
        /// 查找并真删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id)
        {
            var entityEntry = await FindToDeleteAsync(id);
            await SaveChangesAsync();
            return entityEntry;
        }
        #endregion

        #region 查找并真删除操作并立即保存（抛异常） + public virtual EntityEntry<TEntity> FindToDeleteSaveChanges(object id, int oopsCode)
        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">错误状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToDeleteSaveChanges(object id, int oopsCode)
        {
            var entityEntry = FindToDelete(id, oopsCode);
            SaveChanges();
            return entityEntry;
        }
        #endregion

        #region 查找并真删除操作并立即保存（抛异常） + public virtual EntityEntry<TEntity> FindToDeleteSaveChanges(object id, string oopsCode)
        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">错误状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToDeleteSaveChanges(object id, string oopsCode)
        {
            var entityEntry = FindToDelete(id, oopsCode);
            SaveChanges();
            return entityEntry;
        }
        #endregion

        #region 查找并真删除操作并立即保存（抛异常） + public virtual async Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, int oopsCode)
        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">错误状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, int oopsCode)
        {
            var entityEntry = await FindToDeleteAsync(id, oopsCode);
            await SaveChangesAsync();
            return entityEntry;
        }
        #endregion

        #region 查找并真删除操作并立即保存（抛异常） + public virtual async Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, string oopsCode)
        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">错误状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, string oopsCode)
        {
            var entityEntry = await FindToDeleteAsync(id, oopsCode);
            await SaveChangesAsync();
            return entityEntry;
        }
        #endregion


        #region 查找并真删除操作并立即保存（抛异常） + public virtual EntityEntry<TEntity> FindToDeleteSaveChanges(object id, Exception notFoundException)
        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">未找到异常</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToDeleteSaveChanges(object id, Exception notFoundException)
        {
            var entityEntry = FindToDelete(id, notFoundException);
            SaveChanges();
            return entityEntry;
        }
        #endregion

        #region 查找并真删除操作并立即保存（抛异常） + public virtual async Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, Exception notFoundException)
        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">未找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, Exception notFoundException)
        {
            var entityEntry = await FindToDeleteAsync(id, notFoundException);
            await SaveChangesAsync();
            return entityEntry;
        }
        #endregion
    }
}
