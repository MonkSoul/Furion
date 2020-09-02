// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可更新的仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IUpdateableRepository<TEntity>
        where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Update(TEntity entity);

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities"></param>
        void Update(params TEntity[] entities);

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities"></param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新实体（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateAsync(TEntity entity);

        /// <summary>
        /// 更新多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateAsync(params TEntity[] entities);

        /// <summary>
        /// 更新多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新实体并立即保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateNow(TEntity entity);

        /// <summary>
        /// 更新实体并立即保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateNow(TEntity entity, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="entities"></param>
        void UpdateNow(params TEntity[] entities);

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        void UpdateNow(bool acceptAllChangesOnSuccess, params TEntity[] entities);

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="entities"></param>
        void UpdateNow(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新多个实体并立即保存
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        void UpdateNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 更新实体并立即保存（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateNowAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新实体并立即保存（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateNowAsync(params TEntity[] entities);

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task UpdateNowAsync(bool acceptAllChangesOnSuccess, params TEntity[] entities);

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新多个实体并立即保存（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateInclude(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateInclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateInclude(TEntity entity, IEnumerable<string> propertyNames);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateInclude(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, IEnumerable<string> propertyNames);

        /// <summary>
        /// 更新特定属性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, bool acceptAllChangesOnSuccess, params string[] propertyNames);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, bool acceptAllChangesOnSuccess, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<string> propertyNames);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, bool acceptAllChangesOnSuccess, IEnumerable<string> propertyNames);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions);

        /// <summary>
        /// 更新特定属性并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateIncludeNow(TEntity entity, bool acceptAllChangesOnSuccess, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, params string[] propertyNames);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新特定属性并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateExclude(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateExclude(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateExclude(TEntity entity, IEnumerable<string> propertyNames);

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateExclude(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions);

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, IEnumerable<string> propertyNames);

        /// <summary>
        /// 排除特定属性更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateExcludeAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions);

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, bool acceptAllChangesOnSuccess, params string[] propertyNames);

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, bool acceptAllChangesOnSuccess, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<string> propertyNames);

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, bool acceptAllChangesOnSuccess, IEnumerable<string> propertyNames);

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions);

        /// <summary>
        /// 排除特定属性更新并立即提交
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        EntityEntry<TEntity> UpdateExcludeNow(TEntity entity, bool acceptAllChangesOnSuccess, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions);

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, params string[] propertyNames);

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, params string[] propertyNames);

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="propertyExpressions"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default);

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyNames"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions, CancellationToken cancellationToken = default);

        /// <summary>
        /// 排除特定属性更新并立即提交（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyExpressions"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}