// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可插入的仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IInsertableRepository<TEntity>
        where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Insert(TEntity entity);

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        void Insert(params TEntity[] entities);

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// 新增实体（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增多个实体（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(params TEntity[] entities);

        /// <summary>
        /// 新增多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增并提交更改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> InsertNow(TEntity entity);

        /// <summary>
        /// 新增并提交更改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        EntityEntry<TEntity> InsertNow(TEntity entity, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        void InsertNow(params TEntity[] entities);

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        void InsertNow(bool acceptAllChangesOnSuccess, params TEntity[] entities);

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        void InsertNow(IEnumerable<TEntity> entities);

        /// <summary>
        /// 新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        void InsertNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        ///  新增并提交更改（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> InsertNowAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增并提交更改（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> InsertNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        // <summary>
        /// 新增多个实体（异步）
        /// </summary>
        /// <param name="entities"></param>
        Task InsertNowAsync(params TEntity[] entities);

        /// <summary>
        /// 新增多个实体并提交更改（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task InsertNowAsync(bool acceptAllChangesOnSuccess, params TEntity[] entities);

        /// <summary>
        /// 新增多个实体并提交更改（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task InsertNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增多个实体并提交更改（异步）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task InsertNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}