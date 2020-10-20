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

using Fur.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库上下文池
    /// </summary>
    [SkipScan]
    public sealed class DbContextPool : IDbContextPool
    {
        /// <summary>
        /// 线程安全的数据库上下文集合
        /// </summary>
        private readonly ConcurrentBag<DbContext> dbContexts;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DbContextPool()
        {
            dbContexts = new ConcurrentBag<DbContext>();
        }

        /// <summary>
        /// 获取所有数据库上下文
        /// </summary>
        /// <returns></returns>
        public ConcurrentBag<DbContext> GetDbContexts()
        {
            return dbContexts;
        }

        /// <summary>
        /// 保存数据库上下文
        /// </summary>
        /// <param name="dbContext"></param>
        public void AddToPool(DbContext dbContext)
        {
            // 排除已经存在的数据库上下文
            if (!dbContexts.Contains(dbContext))
            {
                dbContexts.Add(dbContext);
            }
        }

        /// <summary>
        /// 保存数据库上下文（异步）
        /// </summary>
        /// <param name="dbContext"></param>
        public Task AddToPoolAsync(DbContext dbContext)
        {
            AddToPool(dbContext);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns></returns>
        public int SavePoolNow()
        {
            // 查找所有已改变的数据库上下文并保存更改
            return dbContexts
                .Where(u => u.ChangeTracker.HasChanges())
                .Select(u => u.SaveChanges()).Count();
        }

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public int SavePoolNow(bool acceptAllChangesOnSuccess)
        {
            // 查找所有已改变的数据库上下文并保存更改
            return dbContexts
                .Where(u => u.ChangeTracker.HasChanges())
                .Select(u => u.SaveChanges(acceptAllChangesOnSuccess)).Count();
        }

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> SavePoolNowAsync(CancellationToken cancellationToken = default)
        {
            // 查找所有已改变的数据库上下文并保存更改
            var tasks = dbContexts
                .Where(u => u.ChangeTracker.HasChanges())
                .Select(u => u.SaveChangesAsync(cancellationToken));

            // 等待所有异步完成
            var results = await Task.WhenAll(tasks);
            return results.Length;
        }

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> SavePoolNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            // 查找所有已改变的数据库上下文并保存更改
            var tasks = dbContexts
                .Where(u => u.ChangeTracker.HasChanges())
                .Select(u => u.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));

            // 等待所有异步完成
            var results = await Task.WhenAll(tasks);
            return results.Length;
        }
    }
}