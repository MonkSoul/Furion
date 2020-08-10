using Fur.DatabaseAccessor.Entities;
using Fur.FriendlyException;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 查找并删除操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDbEntityBase, new()
    {
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

        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToDelete(object id, int oopsCode)
        {
            var entity = Find(id) ?? throw Oops.To(oopsCode);
            return Delete(entity);
        }

        /// <summary>
        /// 查找并真删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToDeleteAsync(object id, int oopsCode)
        {
            var entity = (await FindAsync(id)) ?? throw Oops.To(oopsCode);
            var entityEntry = await DeleteAsync(entity);
            return entityEntry;
        }

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

        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToDeleteSaveChanges(object id, int oopsCode)
        {
            var entityEntry = FindToDelete(id, oopsCode);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 查找并真删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToDeleteSaveChangesAsync(object id, int oopsCode)
        {
            var entityEntry = await FindToDeleteAsync(id, oopsCode);
            await SaveChangesAsync();
            return entityEntry;
        }

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

        /// <summary>
        /// 查找并软删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        public virtual EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        {
            var entity = Find(id);
            return FakeDelete(entity, flagProperty, flagValue);
        }

        /// <summary>
        /// 查找并软删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        {
            var entity = await FindAsync(id);
            return await FakeDeleteAsync(entity, flagProperty, flagValue);
        }

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        public virtual EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)
        {
            var entity = Find(id) ?? throw notFoundException;
            return FakeDelete(entity, flagProperty, flagValue);
        }

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)
        {
            var entity = await FindAsync(id) ?? throw notFoundException;
            return await FakeDeleteAsync(entity, flagProperty, flagValue);
        }

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        public virtual EntityEntry<TEntity> FindToFakeDelete(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)
        {
            var entity = Find(id) ?? throw Oops.To(oopsCode);
            return FakeDelete(entity, flagProperty, flagValue);
        }

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)
        {
            var entity = await FindAsync(id) ?? throw Oops.To(oopsCode);
            return await FakeDeleteAsync(entity, flagProperty, flagValue);
        }

        /// <summary>
        /// 查找并软删除操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToFakeDelete(object id)
        {
            var entity = Find(id);
            return FakeDelete(entity);
        }

        /// <summary>
        /// 查找并软删除操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id)
        {
            var entity = await FindAsync(id);
            return await FakeDeleteAsync(entity);
        }

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToFakeDelete(object id, Exception notFoundException)
        {
            var entity = Find(id) ?? throw notFoundException;
            return FakeDelete(entity);
        }

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, Exception notFoundException)
        {
            var entity = await FindAsync(id) ?? throw notFoundException;
            return await FakeDeleteAsync(entity);
        }

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToFakeDelete(object id, int oopsCode)
        {
            var entity = Find(id) ?? throw Oops.To(oopsCode);
            return FakeDelete(entity);
        }

        /// <summary>
        /// 查找并软删除操作（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToFakeDeleteAsync(object id, int oopsCode)
        {
            var entity = await FindAsync(id) ?? throw Oops.To(oopsCode);
            return await FakeDeleteAsync(entity);
        }

        /// <summary>
        /// 查找并软删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        {
            var entityEntry = FindToFakeDelete(id, flagProperty, flagValue);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 查找并软删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        {
            var entityEntry = await FindToFakeDeleteAsync(id, flagProperty, flagValue);
            await SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)
        {
            var entityEntry = FindToFakeDelete(id, flagProperty, flagValue, notFoundException);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, Exception notFoundException)
        {
            var entityEntry = await FindToFakeDeleteAsync(id, flagProperty, flagValue, notFoundException);
            await SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)
        {
            var entityEntry = FindToFakeDelete(id, flagProperty, flagValue, oopsCode);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Expression<Func<TEntity, object>> flagProperty, object flagValue, int oopsCode)
        {
            var entityEntry = await FindToFakeDeleteAsync(id, flagProperty, flagValue, oopsCode);
            await SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 查找并软删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id)
        {
            var entityEntry = FindToFakeDelete(id);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 查找并软删除操作并立即保存
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id)
        {
            var entityEntry = await FindToFakeDeleteAsync(id);
            await SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, Exception notFoundException)
        {
            var entityEntry = FindToFakeDelete(id, notFoundException);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="notFoundException">没找到异常</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, Exception notFoundException)
        {
            var entityEntry = await FindToFakeDeleteAsync(id, notFoundException);
            await SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> FindToFakeDeleteSaveChanges(object id, int oopsCode)
        {
            var entityEntry = FindToFakeDelete(id, oopsCode);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 查找并软删除操作并立即保存（抛异常）
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="oopsCode">异常状态码</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FindToFakeDeleteSaveChangesAsync(object id, int oopsCode)
        {
            var entityEntry = await FindToFakeDeleteAsync(id, oopsCode);
            await SaveChangesAsync();
            return entityEntry;
        }
    }
}