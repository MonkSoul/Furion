using Fur.DatabaseAccessor.Entities;
using Fur.DatabaseAccessor.Extensions;
using Fur.DatabaseAccessor.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 更新全部列操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> Update(TEntity entity)
        {
            return LoadDbEntityUpdateInterceptor(() => Entities.Update(entity), entity).First();
        }

        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Update(params TEntity[] entities)
        {
            LoadDbEntityUpdateInterceptor(() => Entities.UpdateRange(entities), entities);
        }

        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            LoadDbEntityUpdateInterceptor(() => Entities.UpdateRange(entities), entities.ToArray());
        }

        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity)
        {
            var entityEntry = LoadDbEntityUpdateInterceptor(() =>
            {
                Entities.Update(entity);
            }, entity).First();
            return Task.FromResult(entityEntry);
        }

        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        public virtual Task UpdateAsync(params TEntity[] entities)
        {
            LoadDbEntityUpdateInterceptor(() =>
            {
                Entities.UpdateRange(entities);
            }, entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 更新全部列操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        public virtual Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            LoadDbEntityUpdateInterceptor(() =>
            {
                Entities.UpdateRange(entities);
            }, entities.ToArray());
            return Task.CompletedTask;
        }

        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> UpdateSaveChanges(TEntity entity)
        {
            var trackEntity = Update(entity);
            SaveChanges();
            return trackEntity;
        }

        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entities"></param>
        public virtual void UpdateSaveChanges(params TEntity[] entities)
        {
            Update(entities);
            SaveChanges();
        }

        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        public virtual void UpdateSaveChanges(IEnumerable<TEntity> entities)
        {
            Update(entities);
            SaveChanges();
        }

        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateSaveChangesAsync(TEntity entity)
        {
            var trackEntities = await UpdateAsync(entity);
            await SaveChangesAsync();
            return trackEntities;
        }

        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task UpdateSaveChangesAsync(params TEntity[] entities)
        {
            await UpdateAsync(entities);
            await SaveChangesAsync();
        }

        /// <summary>
        /// 更新全部列操作并立即保存
        /// </summary>
        /// <param name="entities"></param>
        /// <returns><see cref="Task"/></returns>
        public virtual async Task UpdateSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            await UpdateAsync(entities);
            await SaveChangesAsync();
        }

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">表达式</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> UpdateIncludeProperties(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = Attach(entity);

            foreach (var expression in propertyExpressions)
            {
                EntityEntryProperty(entityEntry, expression).IsModified = true;
            }

            LoadDbEntityUpdateInterceptor(null, entity);
            return entityEntry;
        }

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludePropertiesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = Attach(entity);

            foreach (var expression in propertyExpressions)
            {
                EntityEntryProperty(entityEntry, expression).IsModified = true;
            }

            LoadDbEntityUpdateInterceptor(null, entity);
            return Task.FromResult(entityEntry);
        }

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">表达式</param>
        public virtual void UpdateIncludeProperties(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            foreach (var entity in entities)
            {
                UpdateIncludeProperties(entity, propertyExpressions);
            }
        }

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">表达式</param>
        /// <returns><see cref="Task"/></returns>
        public virtual Task UpdateIncludePropertiesAsync(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            foreach (var entity in entities)
            {
                UpdateIncludeProperties(entity, propertyExpressions);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">表达式</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> UpdateIncludePropertiesSaveChanges(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = UpdateIncludeProperties(entity, propertyExpressions);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludePropertiesSaveChangesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = await UpdateIncludePropertiesAsync(entity, propertyExpressions);
            await SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">表达式</param>
        public virtual void UpdateIncludePropertiesSaveChanges(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            UpdateIncludeProperties(entities, propertyExpressions);
            SaveChanges();
        }

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">表达式</param>
        /// <returns><see cref="Task"/></returns>
        public virtual async Task UpdateIncludePropertiesSaveChangesAsync(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            await UpdateIncludePropertiesAsync(entities, propertyExpressions);
            await SaveChangesAsync();
        }

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> UpdateIncludeProperties(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = Attach(entity);

            foreach (var propertyName in propertyNames)
            {
                EntityEntryProperty(entityEntry, propertyName).IsModified = true;
            }

            LoadDbEntityUpdateInterceptor(null, entity);
            return entityEntry;
        }

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludePropertiesAsync(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = Attach(entity);
            foreach (var propertyName in propertyNames)
            {
                EntityEntryProperty(entityEntry, propertyName).IsModified = true;
            }

            LoadDbEntityUpdateInterceptor(null, entity);
            return Task.FromResult(entityEntry);
        }

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        public virtual void UpdateIncludeProperties(IEnumerable<TEntity> entities, params string[] propertyNames)
        {
            foreach (var entity in entities)
            {
                UpdateIncludeProperties(entity, propertyNames);
            }
        }

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task"/></returns>
        public virtual Task UpdateIncludePropertiesAsync(IEnumerable<TEntity> entities, params string[] propertyNames)
        {
            foreach (var entity in entities)
            {
                UpdateIncludeProperties(entity, propertyNames);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> UpdateIncludePropertiesSaveChanges(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = UpdateIncludeProperties(entity, propertyNames);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludePropertiesSaveChangesAsync(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = await UpdateIncludePropertiesAsync(entity, propertyNames);
            await SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        public virtual void UpdateIncludePropertiesSaveChanges(IEnumerable<TEntity> entities, params string[] propertyNames)
        {
            UpdateIncludeProperties(entities, propertyNames);
            SaveChanges();
        }

        /// <summary>
        /// 更新指定列并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task"/></returns>
        public virtual async Task UpdateIncludePropertiesSaveChangesAsync(IEnumerable<TEntity> entities, params string[] propertyNames)
        {
            await UpdateIncludePropertiesAsync(entities, propertyNames);
            await SaveChangesAsync();
        }

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> UpdateExcludeProperties(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = Attach(entity);

            entityEntry.State = EntityState.Modified;
            foreach (var expression in propertyExpressions)
            {
                EntityEntryProperty(entityEntry, expression).IsModified = false;
            }

            LoadDbEntityUpdateInterceptor(null, entity);
            return entityEntry;
        }

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludePropertiesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = Attach(entity);

            entityEntry.State = EntityState.Modified;
            foreach (var expression in propertyExpressions)
            {
                EntityEntryProperty(entityEntry, expression).IsModified = false;
            }

            LoadDbEntityUpdateInterceptor(null, entity);
            return Task.FromResult(entityEntry);
        }

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        public virtual void UpdateExcludeProperties(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            foreach (var entity in entities)
            {
                UpdateExcludeProperties(entity, propertyExpressions);
            }
        }

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task"/></returns>
        public virtual Task UpdateExcludePropertiesAsync(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            foreach (var entity in entities)
            {
                UpdateExcludeProperties(entity, propertyExpressions);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> UpdateExcludePropertiesSaveChanges(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = UpdateExcludeProperties(entity, propertyExpressions);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludePropertiesSaveChangesAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = await UpdateExcludePropertiesAsync(entity, propertyExpressions);
            await SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        public virtual void UpdateExcludePropertiesSaveChanges(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            UpdateExcludeProperties(entities, propertyExpressions);
            SaveChanges();
        }

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyExpressions">属性</param>
        /// <returns><see cref="Task"/></returns>
        public virtual async Task UpdateExcludePropertiesSaveChangesAsync(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            await UpdateExcludePropertiesAsync(entities, propertyExpressions);
            await SaveChangesAsync();
        }

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> UpdateExcludeProperties(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = Attach(entity);

            entityEntry.State = EntityState.Modified;
            foreach (var propertyName in propertyNames)
            {
                EntityEntryProperty(entityEntry, propertyName).IsModified = false;
            }

            LoadDbEntityUpdateInterceptor(null, entity);
            return entityEntry;
        }

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludePropertiesAsync(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = Attach(entity);

            entityEntry.State = EntityState.Modified;
            foreach (var propertyName in propertyNames)
            {
                EntityEntryProperty(entityEntry, propertyName).IsModified = false;
            }

            LoadDbEntityUpdateInterceptor(null, entity);
            return Task.FromResult(entityEntry);
        }

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        public virtual void UpdateExcludeProperties(IEnumerable<TEntity> entities, params string[] propertyNames)
        {
            foreach (var entity in entities)
            {
                UpdateExcludeProperties(entity, propertyNames);
            }
        }

        /// <summary>
        /// 排除特定列更新
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task"/></returns>
        public virtual Task UpdateExcludePropertiesAsync(IEnumerable<TEntity> entities, params string[] propertyNames)
        {
            foreach (var entity in entities)
            {
                UpdateExcludeProperties(entity, propertyNames);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        public virtual EntityEntry<TEntity> UpdateExcludePropertiesSaveChanges(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = UpdateExcludeProperties(entity, propertyNames);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludePropertiesSaveChangesAsync(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = await UpdateExcludePropertiesAsync(entity, propertyNames);
            await SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        public virtual void UpdateExcludePropertiesSaveChanges(IEnumerable<TEntity> entities, params string[] propertyNames)
        {
            UpdateExcludeProperties(entities, propertyNames);
            SaveChanges();
        }

        /// <summary>
        /// 排除特定列更新并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="propertyNames">属性</param>
        /// <returns><see cref="Task"/></returns>
        public virtual async Task UpdateExcludePropertiesSaveChangesAsync(IEnumerable<TEntity> entities, params string[] propertyNames)
        {
            await UpdateExcludePropertiesAsync(entities, propertyNames);
            await SaveChangesAsync();
        }

        /// <summary>
        /// 加载实体拦截器
        /// </summary>
        /// <param name="updateHandle">更新程序</param>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        private EntityEntry<TEntity>[] LoadDbEntityUpdateInterceptor(Action handle, params TEntity[] entities)
        {
            var entityEntries = new List<EntityEntry<TEntity>>();
            foreach (var entity in entities)
            {
                var entityEntry = EntityEntry(entity);
                entityEntries.Add(entityEntry);

                _maintenanceInterceptor?.Updating(entityEntry);

                handle?.Invoke();

                _maintenanceInterceptor?.Updated(entityEntry);

                // 更新多租户信息
                if (App.SupportedMultipleTenant && App.MultipleTenantOptions == FurMultipleTenantOptions.OnTable)
                {
                    var tenantIdProperty = entityEntry.GetProperty(nameof(DbEntityBase.TenantId));
                    if (tenantIdProperty == null) throw new ArgumentNullException($"Not found the {nameof(DbEntityBase.TenantId)} Column.");
                    if (tenantIdProperty.IsModified)
                    {
                        tenantIdProperty.IsModified = false;
                    }
                }
            }
            return entityEntries.ToArray();
        }
    }
}