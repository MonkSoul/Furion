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

using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可写仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库实体定位器</typeparam>
    public partial interface IWritableRepository<TEntity, TDbContextLocator>
        : IInsertableRepository<TEntity, TDbContextLocator>
        , IUpdateableRepository<TEntity, TDbContextLocator>
        , IDeletableRepository<TEntity, TDbContextLocator>
        , IOperableRepository<TEntity, TDbContextLocator>
        , ISqlExecutableRepository<TEntity, TDbContextLocator>
        where TEntity : class, IEntityBase, new()
        where TDbContextLocator : class, IDbContextLocator, new()
    {
        /// <summary>
        /// 接受所有更改
        /// </summary>
        void AcceptAllChanges();

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns></returns>
        int SavePoolNow();

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        int SavePoolNow(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SavePoolNowAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SavePoolNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <returns></returns>
        int SaveNow();

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        int SaveNow(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 提交更改操作（异步）
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveNowAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 提交更改操作（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}