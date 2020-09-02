// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可插入的仓储分部类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EFCoreRepository<TEntity>
         where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> Update(TEntity entity)
        {
            return Entities.Update(entity);
        }

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Update(params TEntity[] entities)
        {
            Entities.UpdateRange(entities);
        }

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            Entities.UpdateRange(entities);
        }

        /// <summary>
        /// 更新实体（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        /// <summary>
        /// 更新多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task UpdateAsync(params TEntity[] entities)
        {
            Update(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 更新多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            Update(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 更新实体并立即保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateNow(TEntity entity)
        {
            var entityEntry = Update(entity);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 更新实体并立即保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateNow(TEntity entity, bool acceptAllChangesOnSuccess)
        {
            var entityEntry = Update(entity);
            SaveChanges(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="entities"></param>
        public virtual void UpdateNow(params TEntity[] entities)
        {
            Update(entities);
            SaveChanges();
        }

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        public virtual void UpdateNow(bool acceptAllChangesOnSuccess, params TEntity[] entities)
        {
            Update(entities);
            SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="entities"></param>
        public virtual void UpdateNow(IEnumerable<TEntity> entities)
        {
            Update(entities);
            SaveChanges();
        }

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        public virtual void UpdateNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess)
        {
            Update(entities);
            SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 更新实体并立即保存（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateNowAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateAsync(entity);
            await SaveChangesAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新实体并立即保存（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateAsync(entity);
            await SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task UpdateNowAsync(params TEntity[] entities)
        {
            await UpdateAsync(entities);
            await SaveChangesAsync();
        }

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task UpdateNowAsync(bool acceptAllChangesOnSuccess, params TEntity[] entities)
        {
            await UpdateAsync(entities);
            await SaveChangesAsync(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task UpdateNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await UpdateAsync(entities);
            await SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task UpdateNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await UpdateAsync(entities);
            await SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = Entry(entity);
            entityEntry.State = EntityState.Unchanged;
            foreach (var propertyName in propertyNames)
            {
                EntityPropertyEntry(entity, propertyName).IsModified = true;
            }
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = Entry(entity);
            entityEntry.State = EntityState.Unchanged;
            foreach (var propertyExpression in propertyExpressions)
            {
                EntityPropertyEntry(entity, propertyExpression).IsModified = true;
            }
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity, IEnumerable<string> propertyNames)
        {
            return UpdateInclude(entity, propertyNames.ToArray());
        }

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateInclude(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions)
        {
            return UpdateInclude(entity, propertyExpressions.ToArray());
        }

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, params string[] propertyNames)
        {
            return Task.FromResult(UpdateInclude(entity, propertyNames));
        }

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            return Task.FromResult(UpdateInclude(entity, propertyExpressions));
        }

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, IEnumerable<string> propertyNames)
        {
            return Task.FromResult(UpdateInclude(entity, propertyNames));
        }

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions)
        {
            return Task.FromResult(UpdateInclude(entity, propertyExpressions));
        }

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = UpdateInclude(entity, propertyNames);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, bool acceptAllChangesOnSuccess, params string[] propertyNames)
        {
            var entityEntry = UpdateInclude(entity, propertyNames);
            SaveChanges(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = UpdateInclude(entity, propertyExpressions);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, bool acceptAllChangesOnSuccess, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = UpdateInclude(entity, propertyExpressions);
            SaveChanges(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<string> propertyNames)
        {
            var entityEntry = UpdateInclude(entity, propertyNames);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, bool acceptAllChangesOnSuccess, IEnumerable<string> propertyNames)
        {
            var entityEntry = UpdateInclude(entity, propertyNames);
            SaveChanges(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions)
        {
            var entityEntry = UpdateInclude(entity, propertyExpressions);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, bool acceptAllChangesOnSuccess, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions)
        {
            var entityEntry = UpdateInclude(entity, propertyExpressions);
            SaveChanges(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames);
            await SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, params string[] propertyNames)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames);
            await SaveChangesAsync(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyExpressions);
            await SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyExpressions);
            await SaveChangesAsync(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames);
            await SaveChangesAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyNames);
            await SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyExpressions);
            await SaveChangesAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateIncludeAsync(entity, propertyExpressions);
            await SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = Entry(entity);
            entityEntry.State = EntityState.Modified;
            foreach (var propertyName in propertyNames)
            {
                EntityPropertyEntry(entity, propertyName).IsModified = false;
            }
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = Entry(entity);
            entityEntry.State = EntityState.Modified;
            foreach (var propertyExpression in propertyExpressions)
            {
                EntityPropertyEntry(entity, propertyExpression).IsModified = false;
            }
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity, IEnumerable<string> propertyNames)
        {
            return UpdateExclude(entity, propertyNames.ToArray());
        }

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateExclude(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions)
        {
            return UpdateExclude(entity, propertyExpressions.ToArray());
        }

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, params string[] propertyNames)
        {
            return Task.FromResult(UpdateExclude(entity, propertyNames));
        }

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            return Task.FromResult(UpdateExclude(entity, propertyExpressions));
        }

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, IEnumerable<string> propertyNames)
        {
            return Task.FromResult(UpdateExclude(entity, propertyNames));
        }

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions)
        {
            return Task.FromResult(UpdateExclude(entity, propertyExpressions));
        }

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = UpdateExclude(entity, propertyNames);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, bool acceptAllChangesOnSuccess, params string[] propertyNames)
        {
            var entityEntry = UpdateExclude(entity, propertyNames);
            SaveChanges(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = UpdateExclude(entity, propertyExpressions);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, bool acceptAllChangesOnSuccess, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = UpdateExclude(entity, propertyExpressions);
            SaveChanges(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<string> propertyNames)
        {
            var entityEntry = UpdateExclude(entity, propertyNames);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, bool acceptAllChangesOnSuccess, IEnumerable<string> propertyNames)
        {
            var entityEntry = UpdateExclude(entity, propertyNames);
            SaveChanges(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions)
        {
            var entityEntry = UpdateExclude(entity, propertyExpressions);
            SaveChanges();
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, bool acceptAllChangesOnSuccess, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions)
        {
            var entityEntry = UpdateExclude(entity, propertyExpressions);
            SaveChanges(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, params string[] propertyNames)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames);
            await SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, params string[] propertyNames)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames);
            await SaveChangesAsync(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyExpressions);
            await SaveChangesAsync();
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyExpressions);
            await SaveChangesAsync(acceptAllChangesOnSuccess);
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames);
            await SaveChangesAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyNames);
            await SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyExpressions);
            await SaveChangesAsync(cancellationToken);
            return entityEntry;
        }

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntry = await UpdateExcludeAsync(entity, propertyExpressions);
            await SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return entityEntry;
        }
    }
}