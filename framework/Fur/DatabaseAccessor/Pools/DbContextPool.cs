// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
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
            => dbContexts;

        /// <summary>
        /// 保存数据库上下文
        /// </summary>
        /// <param name="dbContext"></param>
        public void SaveDbContext(DbContext dbContext)
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
        public Task SaveDbContextAsync(DbContext dbContext)
        {
            SaveDbContext(dbContext);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns></returns>
        public int SavePoolChanges()
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
        public int SavePoolChanges(bool acceptAllChangesOnSuccess)
        {
            // 查找所有已改变的数据库上下文并保存更改
            return dbContexts
                .Where(u => u.ChangeTracker.HasChanges())
                .Select(u => u.SaveChanges(acceptAllChangesOnSuccess)).Count();
        }

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns></returns>
        public async Task<int> SavePoolChangesAsync()
        {
            // 查找所有已改变的数据库上下文并保存更改
            var tasks = dbContexts
                .Where(u => u.ChangeTracker.HasChanges())
                .Select(u => u.SaveChangesAsync());

            // 等等所有异步完成
            await Task.WhenAll(tasks);

            return tasks.Count();
        }

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> SavePoolChangesAsync(CancellationToken cancellationToken = default)
        {
            // 查找所有已改变的数据库上下文并保存更改
            var tasks = dbContexts
                .Where(u => u.ChangeTracker.HasChanges())
                .Select(u => u.SaveChangesAsync(cancellationToken));

            // 等等所有异步完成
            await Task.WhenAll(tasks);

            return tasks.Count();
        }

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> SavePoolChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            // 查找所有已改变的数据库上下文并保存更改
            var tasks = dbContexts
                .Where(u => u.ChangeTracker.HasChanges())
                .Select(u => u.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));

            // 等等所有异步完成
            await Task.WhenAll(tasks);

            return tasks.Count();
        }
    }
}