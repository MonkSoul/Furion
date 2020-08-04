using Fur.DatabaseAccessor.Entities;
using Fur.DatabaseAccessor.Extensions;
using Fur.DatabaseAccessor.Providers;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 删除操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        public virtual EntityEntry<TEntity> Delete(TEntity entity)
        {
            Attach(entity);
            return Entities.Remove(entity);
        }

        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Delete(params TEntity[] entities)
        {
            AttachRange(entities);
            Entities.RemoveRange(entities);
        }

        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            AttachRange(entities);
            Entities.RemoveRange(entities);
        }

        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity)
        {
            Attach(entity);
            return Task.FromResult(Entities.Remove(entity));
        }

        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        public virtual Task DeleteAsync(params TEntity[] entities)
        {
            AttachRange(entities);
            Entities.RemoveRange(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 真删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        public virtual Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            AttachRange(entities);
            Entities.RemoveRange(entities);
            return Task.CompletedTask;
        }

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

        /// <summary>
        /// 真删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void DeleteSaveChanges(params TEntity[] entities)
        {
            Delete(entities);
            SaveChanges();
        }

        /// <summary>
        /// 真删除操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void DeleteSaveChanges(IEnumerable<TEntity> entities)
        {
            Delete(entities);
            SaveChanges();
        }

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

        /// <summary>
        /// 软删除操作
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

        /// <summary>
        /// 软删除操作
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

        /// <summary>
        /// 软删除操作
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

        /// <summary>
        /// 软删除操作
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

        /// <summary>
        /// 软删除操作并立即保存
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

        /// <summary>
        /// 软删除操作并立即保存
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

        /// <summary>
        /// 软删除操作并立即保存
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

        /// <summary>
        /// 软删除操作并立即保存
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

        /// <summary>
        /// 软删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry(TEntity)"/></returns>
        public virtual EntityEntry<TEntity> FakeDelete(TEntity entity)
        {
            PropertyEntry propertyEntry;
            (string propertyName, object flagValue) propertyData;

            if (_fakeDeleteProvider == null) propertyData = (nameof(DbEntity.IsDeleted), true);
            else
            {
                propertyData = (_fakeDeleteProvider.Property, _fakeDeleteProvider.FlagValue);
            }

            var entityEntry = Attach(entity);
            propertyEntry = entityEntry.GetProperty(propertyData.propertyName);

            if (propertyEntry == null) throw new ArgumentNullException($"{nameof(IFakeDeleteProvider)} is not implemented.");

            propertyEntry.CurrentValue = propertyData.flagValue;
            return UpdateIncludeProperties(entity, propertyData.propertyName);
        }

        /// <summary>
        /// 软删除操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void FakeDelete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                FakeDelete(entity);
            }
        }

        /// <summary>
        /// 软删除操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<EntityEntry<TEntity>> FakeDeleteAsync(TEntity entity)
        {
            PropertyEntry propertyEntry;
            (string propertyName, object flagValue) propertyData;

            if (_fakeDeleteProvider == null) propertyData = (nameof(DbEntity.IsDeleted), true);
            else
            {
                propertyData = (_fakeDeleteProvider.Property, _fakeDeleteProvider.FlagValue);
            }

            var entityEntry = Attach(entity);
            propertyEntry = entityEntry.GetProperty(propertyData.propertyName);

            if (propertyEntry == null) throw new ArgumentNullException($"{nameof(IFakeDeleteProvider)} is not implemented.");

            propertyEntry.CurrentValue = propertyData.flagValue;
            return UpdateIncludePropertiesAsync(entity, propertyData.propertyName);
        }

        /// <summary>
        /// 软删除操作
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

        /// <summary>
        /// 软删除操作并立即保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> FakeDeleteSaveChanges(TEntity entity)
        {
            var entityEntry = FakeDelete(entity);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 软删除操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> FakeDeleteSaveChangesAsync(TEntity entity)
        {
            var entityEntry = await FakeDeleteAsync(entity);
            await SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 软删除操作并立即保存
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

        /// <summary>
        /// 软删除操作并立即保存
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
    }
}