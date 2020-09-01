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
        void SaveDbContext(DbContext dbContext);

        /// <summary>
        /// 异步保存数据库上下文
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        Task SaveDbContextAsync(DbContext dbContext);

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns></returns>
        int SavePoolChanges();

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        int SavePoolChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns></returns>
        Task<int> SavePoolChangesAsync();

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文（异步）
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SavePoolChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SavePoolChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}