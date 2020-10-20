// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可插入仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IInsertableRepository<TEntity> : IInsertableRepository<TEntity, MasterDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
    {
    }

    /// <summary>
    /// 可插入仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public partial interface IInsertableRepository<TEntity, TDbContextLocator> : IRepositoryDependency
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理的实体</returns>
        EntityEntry<TEntity> Insert(TEntity entity);

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Insert(params TEntity[] entities);

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理的实体</returns>
        Task<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns>Task</returns>
        Task InsertAsync(params TEntity[] entities);

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns></returns>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>数据库中返回的实体</returns>
        EntityEntry<TEntity> InsertNow(TEntity entity);

        /// <summary>
        /// 新增一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中返回的实体</returns>
        EntityEntry<TEntity> InsertNow(TEntity entity, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        void InsertNow(params TEntity[] entities);

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        void InsertNow(TEntity[] entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        void InsertNow(IEnumerable<TEntity> entities);

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        void InsertNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 新增一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中返回的实体</returns>
        Task<EntityEntry<TEntity>> InsertNowAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中返回的实体</returns>
        Task<EntityEntry<TEntity>> InsertNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns>Task</returns>
        Task InsertNowAsync(params TEntity[] entities);

        /// <summary>
        /// 新增多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        Task InsertNowAsync(TEntity[] entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        Task InsertNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        Task InsertNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        Task InsertNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}