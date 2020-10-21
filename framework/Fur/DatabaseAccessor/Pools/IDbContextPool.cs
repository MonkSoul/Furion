// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库上下文池
    /// </summary>
    public interface IDbContextPool
    {
        /// <summary>
        /// 获取所有数据库上下文
        /// </summary>
        /// <returns></returns>
        ConcurrentBag<DbContext> GetDbContexts();

        /// <summary>
        /// 保存数据库上下文
        /// </summary>
        /// <param name="dbContext"></param>
        void AddToPool(DbContext dbContext);

        /// <summary>
        /// 异步保存数据库上下文
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        Task AddToPoolAsync(DbContext dbContext);

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
        /// 保存数据库上下文池中所有已更改的数据库上下文（异步）
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SavePoolNowAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SavePoolNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}