using Fur.DatabaseVisitor.Entities;
using Fur.DependencyInjection.Lifetimes;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 删除操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 真删除操作 + public virtual EntityEntry<TEntity> Delete(TEntity entity)
        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        public virtual EntityEntry<TEntity> Delete(TEntity entity)
        {
            Attach(entity);
            return Entity.Remove(entity);
        }
        #endregion

        #region 真删除操作 + public virtual void Delete(params TEntity[] entities)
        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Delete(params TEntity[] entities)
        {
            AttachRange(entities);
            Entity.RemoveRange(entities);
        }
        #endregion

        #region 真删除操作 + public virtual void Delete(IEnumerable<TEntity> entities)
        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            AttachRange(entities);
            Entity.RemoveRange(entities);
        }
        #endregion

        #region 真删除操作 + public virtual Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity)
        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity)
        {
            Attach(entity);
            return Task.FromResult(Entity.Remove(entity));
        }
        #endregion

        #region 真删除操作 + public virtual Task DeleteAsync(params TEntity[] entities)
        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        public virtual Task DeleteAsync(params TEntity[] entities)
        {
            AttachRange(entities);
            Entity.RemoveRange(entities);
            return Task.CompletedTask;
        }
        #endregion

        #region 真删除操作 + public virtual Task DeleteAsync(IEnumerable<TEntity> entities)
        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        public virtual Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            AttachRange(entities);
            Entity.RemoveRange(entities);
            return Task.CompletedTask;
        }
        #endregion


        #region 真删除操作并立即保存 + public virtual EntityEntry<TEntity> DeleteSaveChanges(TEntity entity)
        /// <summary>
        /// 真删除操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        public virtual EntityEntry<TEntity> DeleteSaveChanges(TEntity entity)
        {
            var trackEntity = Delete(entity);
            SaveChanges();
            return trackEntity;
        }
        #endregion

        #region 真删除操作并立即保存 + public virtual void DeleteSaveChanges(params TEntity[] entities)
        /// <summary>
        /// 真删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void DeleteSaveChanges(params TEntity[] entities)
        {
            Delete(entities);
            SaveChanges();
        }
        #endregion

        #region 真删除操作并立即保存 + public virtual void DeleteSaveChanges(IEnumerable<TEntity> entities)
        /// <summary>
        /// 真删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void DeleteSaveChanges(IEnumerable<TEntity> entities)
        {
            Delete(entities);
            SaveChanges();
        }
        #endregion

        #region 真删除操作并立即保存 + public virtual async Task<EntityEntry<TEntity>> DeleteSaveChangesAsync(TEntity entity)
        /// <summary>
        /// 真删除操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> DeleteSaveChangesAsync(TEntity entity)
        {
            var trackEntity = await DeleteAsync(entity);
            await SaveChangesAsync();
            return trackEntity;
        }
        #endregion

        #region 真删除操作并立即保存 + public virtual async Task DeleteSaveChangesAsync(params TEntity[] entities)
        /// <summary>
        /// 真删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task DeleteSaveChangesAsync(params TEntity[] entities)
        {
            await DeleteAsync(entities);
            await SaveChangesAsync();
        }
        #endregion

        #region 真删除操作并立即保存 + public virtual async Task DeleteSaveChangesAsync(IEnumerable<TEntity> entities)
        /// <summary>
        /// 真删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task DeleteSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            await DeleteAsync(entities);
            await SaveChangesAsync();
        }
        #endregion


        #region 假删除操作 + public virtual EntityEntry<TEntity> FakeDelete(TEntity entity, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        /// <summary>
        /// 假删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        public virtual EntityEntry<TEntity> FakeDelete(TEntity entity, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        {
            var entityEntry = Attach(entity);
            EntityEntryProperty(entityEntry, flagProperty).CurrentValue = flagValue;
            return UpdateIncludeProperties(entity, flagProperty);
        }
        #endregion

        #region 假删除操作 + public virtual void FakeDelete(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        /// <summary>
        /// 假删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        public virtual void FakeDelete(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        {
            foreach (var entity in entities)
            {
                FakeDelete(entity, flagProperty, flagValue);
            }
        }
        #endregion

        #region 假删除操作 + public virtual Task<EntityEntry<TEntity>> FakeDeleteAsync(TEntity entity, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        /// <summary>
        /// 假删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<EntityEntry<TEntity>> FakeDeleteAsync(TEntity entity, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        {
            var entityEntry = Attach(entity);
            EntityEntryProperty(entityEntry, flagProperty).CurrentValue = flagValue;

            return UpdateIncludePropertiesAsync(entity, flagProperty);
        }
        #endregion

        #region 假删除操作 + public virtual Task FakeDeleteAsync(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        /// <summary>
        /// 假删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><<see cref="Task"/>/returns>
        public virtual Task FakeDeleteAsync(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        {
            foreach (var entity in entities)
            {
                FakeDelete(entity, flagProperty, flagValue);
            }
            return Task.CompletedTask;
        }
        #endregion


        #region 假删除操作并立即保存 + public virtual EntityEntry<TEntity> FakeDeleteSaveChanges(TEntity entity, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        /// <summary>
        /// 假删除操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        public virtual EntityEntry<TEntity> FakeDeleteSaveChanges(TEntity entity, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        {
            var entityEntry = FakeDelete(entity, flagProperty, flagValue);
            SaveChanges();
            return entityEntry;
        }
        #endregion

        #region 假删除操作并立即保存 + public virtual async Task<EntityEntry<TEntity>> FakeDeleteSaveChangesAsync(TEntity entity, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        /// <summary>
        /// 假删除操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FakeDeleteSaveChangesAsync(TEntity entity, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        {
            var entityEntry = await FakeDeleteAsync(entity, flagProperty, flagValue);
            await SaveChangesAsync();
            return entityEntry;
        }
        #endregion

        #region 假删除操作并立即保存 + public virtual void FakeDeleteSaveChanges(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        /// <summary>
        /// 假删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        public virtual void FakeDeleteSaveChanges(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        {
            foreach (var entity in entities)
            {
                FakeDelete(entity, flagProperty, flagValue);
            }
            SaveChanges();
        }
        #endregion

        #region 假删除操作并立即保存 + public virtual async Task FakeDeleteSaveChangesAsync(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        /// <summary>
        /// 假删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="flagProperty">标识属性</param>
        /// <param name="flagValue">标识值</param>
        /// <returns><see cref="Task"/></returns>
        public virtual async Task FakeDeleteSaveChangesAsync(IEnumerable<TEntity> entities, Expression<Func<TEntity, object>> flagProperty, object flagValue)
        {
            foreach (var entity in entities)
            {
                FakeDelete(entity, flagProperty, flagValue);
            }
            await SaveChangesAsync();
        }
        #endregion


        #region 假删除操作 + public virtual EntityEntry<TEntity> FakeDelete(TEntity entity)
        /// <summary>
        /// 假删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        public virtual EntityEntry<TEntity> FakeDelete(TEntity entity)
        {
            var (flagPropertyName, flagValue) = _maintenanceProvider.GetFakeDeletePropertyInfo();

            var entityEntry = Attach(entity);
            EntityEntryProperty(entityEntry, flagPropertyName).CurrentValue = flagValue;
            return UpdateIncludeProperties(entity, flagPropertyName);
        }
        #endregion

        #region 假删除操作 + public virtual void FakeDelete(IEnumerable<TEntity> entities)
        /// <summary>
        /// 假删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void FakeDelete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                FakeDelete(entity);
            }
        }
        #endregion

        #region 假删除操作 + public virtual Task<EntityEntry<TEntity>> FakeDeleteAsync(TEntity entity)
        /// <summary>
        /// 假删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<EntityEntry<TEntity>> FakeDeleteAsync(TEntity entity)
        {
            var (flagPropertyName, flagValue) = _maintenanceProvider.GetFakeDeletePropertyInfo();

            var entityEntry = Attach(entity);
            EntityEntryProperty(entityEntry, flagPropertyName).CurrentValue = flagValue;

            return UpdateIncludePropertiesAsync(entity, flagPropertyName);
        }
        #endregion

        #region 假删除操作 + public virtual Task FakeDeleteAsync(IEnumerable<TEntity> entities)
        /// <summary>
        /// 假删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        public virtual Task FakeDeleteAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                FakeDelete(entity);
            }

            return Task.CompletedTask;
        }
        #endregion


        #region 假删除操作并立即保存 + public virtual EntityEntry<TEntity> FakeDeleteSaveChanges(TEntity entity)
        /// <summary>
        /// 假删除操作并立即保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> FakeDeleteSaveChanges(TEntity entity)
        {
            var entityEntry = FakeDelete(entity);
            SaveChanges();
            return entityEntry;
        }
        #endregion

        #region 假删除操作并立即保存 + public virtual async Task<EntityEntry<TEntity>> FakeDeleteSaveChangesAsync(TEntity entity)
        /// <summary>
        /// 假删除操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FakeDeleteSaveChangesAsync(TEntity entity)
        {
            var entityEntry = await FakeDeleteAsync(entity);
            await SaveChangesAsync();
            return entityEntry;
        }
        #endregion

        #region 假删除操作并立即保存 + public virtual void FakeDeleteSaveChanges(IEnumerable<TEntity> entities)
        /// <summary>
        /// 假删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void FakeDeleteSaveChanges(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                FakeDelete(entity);
            }
            SaveChanges();
        }
        #endregion

        #region 假删除操作并立即保存 + public virtual Task FakeDeleteSaveChangesAsync(IEnumerable<TEntity> entities)
        /// <summary>
        /// 假删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        public virtual async Task FakeDeleteSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                FakeDelete(entity);
            }
            await SaveChangesAsync();
        }
        #endregion
    }
}
