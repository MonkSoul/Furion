// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Furion.DatabaseAccessor
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
    public partial interface IInsertableRepository<TEntity, TDbContextLocator> : IPrivateInsertableRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
    }

    /// <summary>
    /// 可插入仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IPrivateInsertableRepository<TEntity> : IPrivateRootRepository
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>代理的实体</returns>
        EntityEntry<TEntity> Insert(TEntity entity, bool? ignoreNullValues = null);

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
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理的实体</returns>
        Task<EntityEntry<TEntity>> InsertAsync(TEntity entity, bool? ignoreNullValues = null, CancellationToken cancellationToken = default);

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
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中返回的实体</returns>
        EntityEntry<TEntity> InsertNow(TEntity entity, bool? ignoreNullValues = null);

        /// <summary>
        /// 新增一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中返回的实体</returns>
        EntityEntry<TEntity> InsertNow(TEntity entity, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null);

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
        /// <param name="ignoreNullValues"></param>
        /// <returns>数据库中返回的实体</returns>
        Task<EntityEntry<TEntity>> InsertNowAsync(TEntity entity, bool? ignoreNullValues = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="ignoreNullValues"></param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中返回的实体</returns>
        Task<EntityEntry<TEntity>> InsertNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default);

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