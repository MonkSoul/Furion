using Fur.DatabaseAccessor.Models.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 删除操作 分部接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IRepository<TEntity> where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> Delete(TEntity entity);

        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Delete(params TEntity[] entities);

        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity);

        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task DeleteAsync(params TEntity[] entities);

        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns></returns>
        Task DeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 真删除操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> DeleteSaveChanges(TEntity entity);

        /// <summary>
        /// 真删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        void DeleteSaveChanges(params TEntity[] entities);

        /// <summary>
        /// 真删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        void DeleteSaveChanges(IEnumerable<TEntity> entities);

        /// <summary>
        /// 真删除操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> DeleteSaveChangesAsync(TEntity entity);

        /// <summary>
        /// 真删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        Task DeleteSaveChangesAsync(params TEntity[] entities);

        /// <summary>
        /// 真删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        Task DeleteSaveChangesAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 软删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="flagProperty">标记属性</param>
        /// <param name="flagValue">标记值</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FakeDelete(TEntity entity, Expression<Func<TEntity, object>> flagProperty, object flagValue);

        /// <summary>
        /// 软删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="flagProperty">标记属性</param>
        /// <param name="flagValue">标记值</param>
        void FakeDelete(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> flagProperty, object flagValue);

        /// <summary>
        /// 软删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FakeDeleteAsync(TEntity entity, Expression<Func<TEntity, object>> flagProperty, object flagValue);

        /// <summary>
        /// 软删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task FakeDeleteAsync(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> flagProperty, object flagValue);

        /// <summary>
        /// 软删除操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="flagProperty">标记属性</param>
        /// <param name="flagValue">标记值</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FakeDeleteSaveChanges(TEntity entity, Expression<Func<TEntity, object>> flagProperty, object flagValue);

        /// <summary>
        /// 软删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="flagProperty">标记属性</param>
        /// <param name="flagValue">标记值</param>
        void FakeDeleteSaveChanges(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> flagProperty, object flagValue);

        /// <summary>
        /// 软删除操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="flagProperty">标记属性</param>
        /// <param name="flagValue">标记值</param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> FakeDeleteSaveChangesAsync(TEntity entity, Expression<Func<TEntity, object>> flagProperty, object flagValue);

        /// <summary>
        /// 软删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="flagProperty">标记属性</param>
        /// <param name="flagValue">标记值</param>
        /// <returns><see cref="Task"/></returns>
        Task FakeDeleteSaveChangesAsync(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> flagProperty, object flagValue);

        /// <summary>
        /// 软删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FakeDelete(TEntity entity);

        /// <summary>
        /// 软删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        void FakeDelete(IEnumerable<TEntity> entities);

        /// <summary>
        /// 软删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<EntityEntry<TEntity>> FakeDeleteAsync(TEntity entity);

        /// <summary>
        /// 软删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task FakeDeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 软删除操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        EntityEntry<TEntity> FakeDeleteSaveChanges(TEntity entity);

        /// <summary>
        /// 软删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        void FakeDeleteSaveChanges(IEnumerable<TEntity> entities);

        /// <summary>
        /// 软删除操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> FakeDeleteSaveChangesAsync(TEntity entity);

        /// <summary>
        /// 软删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        Task FakeDeleteSaveChangesAsync(IEnumerable<TEntity> entities);
    }
}