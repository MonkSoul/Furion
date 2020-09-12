// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.FriendlyException;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 实体拓展类
    /// </summary>
    public static class IEntityExtensions
    {
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理的实体</returns>
        public static EntityEntry<TEntity> Insert<TEntity>(this TEntity entity)
            where TEntity : class, IEntity, new()
        {
            return GetRepository<TEntity>().Insert(entity);
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理的实体</returns>
        public static Task<EntityEntry<TEntity>> InsertAsync<TEntity>(this TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity, new()
        {
            return GetRepository<TEntity>().InsertAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>数据库中的实体</returns>
        public static EntityEntry<TEntity> InsertNow<TEntity>(this TEntity entity)
            where TEntity : class, IEntity, new()
        {
            return GetRepository<TEntity>().InsertNow(entity);
        }

        /// <summary>
        /// 新增一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>数据库中的实体</returns>
        public static EntityEntry<TEntity> InsertNow<TEntity>(this TEntity entity, bool acceptAllChangesOnSuccess)
            where TEntity : class, IEntity, new()
        {
            return GetRepository<TEntity>().InsertNow(entity, acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>数据库中的实体</returns>
        public static Task<EntityEntry<TEntity>> InsertNowAsync<TEntity>(this TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity, new()
        {
            return GetRepository<TEntity>().InsertNowAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>数据库中的实体</returns>
        public static Task<EntityEntry<TEntity>> InsertNowAsync<TEntity>(this TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity, new()
        {
            return GetRepository<TEntity>().InsertNowAsync(entity, acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        public static EntityEntry<TEntity> Update<TEntity>(this TEntity entity)
            where TEntity : class, IEntity, new()
        {
            return GetRepository<TEntity>().Update(entity);
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        public static Task<EntityEntry<TEntity>> UpdateAsync<TEntity>(this TEntity entity)
             where TEntity : class, IEntity, new()
        {
            return GetRepository<TEntity>().UpdateAsync(entity);
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>数据库中的实体</returns>
        public static EntityEntry<TEntity> UpdateNow<TEntity>(this TEntity entity)
            where TEntity : class, IEntity, new()
        {
            return GetRepository<TEntity>().UpdateNow(entity);
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public static EntityEntry<TEntity> UpdateNow<TEntity>(this TEntity entity, bool acceptAllChangesOnSuccess)
             where TEntity : class, IEntity, new()
        {
            return GetRepository<TEntity>().UpdateNow(entity, acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public static Task<EntityEntry<TEntity>> UpdateNowAsync<TEntity>(this TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity, new()
        {
            return GetRepository<TEntity>().UpdateNowAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 更新一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public static Task<EntityEntry<TEntity>> UpdateNowAsync<TEntity>(this TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
            where TEntity : class, IEntity, new()
        {
            return GetRepository<TEntity>().UpdateNowAsync(entity, acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 获取实体仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>IRepository<TEntity></returns>
        private static IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IEntity, new()
        {
            return App.RequestServiceProvider.GetService<IRepository<TEntity>>()
                ?? throw Oops.Oh("Reading IRepository<TEntity> instances on non HTTP requests is not supported.", typeof(NotSupportedException));
        }
    }
}