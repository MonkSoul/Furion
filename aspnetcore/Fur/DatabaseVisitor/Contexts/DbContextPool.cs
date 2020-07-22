using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Contexts
{
    /// <summary>
    /// 数据库上下文线程池
    /// <para>管理所有 DbContext 上下文，提供统一提交</para>
    /// </summary>
    public class DbContextPool : IDbContextPool
    {
        /// <summary>
        /// 线程安全的数据库上下文集合
        /// </summary>
        private readonly ConcurrentBag<DbContext> dbContexts;

        #region 默认构造函数 + public DbContextPool()
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DbContextPool()
        {
            dbContexts ??= new ConcurrentBag<DbContext>();
        }
        #endregion

        #region 保存 DbContext 上下文 + public void SaveDbContext(DbContext dbContext)
        /// <summary>
        /// 保存 DbContext 上下文
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        public void SaveDbContext(DbContext dbContext)
        {
            if (!dbContexts.Contains(dbContext))
            {
                dbContexts.Add(dbContext);
            }
        }
        #endregion

        #region 异步保存 DbContext 上下文 + public Task SaveDbContextAsync(DbContext dbContext)
        /// <summary>
        /// 异步保存 DbContext 上下文
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <returns>任务</returns>
        public Task SaveDbContextAsync(DbContext dbContext)
        {
            if (!dbContexts.Contains(dbContext))
            {
                dbContexts.Add(dbContext);
            }
            return Task.CompletedTask;
        }
        #endregion

        #region 获取所有的数据库上下文 + public IEnumerable<DbContext> GetDbContexts()
        /// <summary>
        /// 获取所有的数据库上下文
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public IEnumerable<DbContext> GetDbContexts()
        {
            return dbContexts.ToList();
        }
        #endregion

        #region 提交所有已更改的数据库上下文 + public int SavePoolChanges()
        /// <summary>
        /// 提交所有已更改的数据库上下文
        /// </summary>
        /// <returns>受影响行数</returns>
        public int SavePoolChanges()
        {
            var hasChangeCount = 0;
            foreach (var dbContext in dbContexts)
            {
                if (dbContext.ChangeTracker.HasChanges())
                {
                    dbContext.SaveChanges();
                    hasChangeCount++;
                }
            }
            return hasChangeCount;
        }
        #endregion

        #region 异步提交所有已更改的数据库上下文 + public async Task<int> SavePoolChangesAsync()
        /// <summary>
        /// 异步提交所有已更改的数据库上下文
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        public async Task<int> SavePoolChangesAsync()
        {
            var hasChangeCount = 0;
            foreach (var dbContext in dbContexts)
            {
                if (dbContext.ChangeTracker.HasChanges())
                {
                    hasChangeCount++;
                    await dbContext.SaveChangesAsync();
                }
            }
            return hasChangeCount;
        }

        void IDbContextPool.SaveDbContext(DbContext dbContext)
        {
            throw new System.NotImplementedException();
        }

        Task IDbContextPool.SaveDbContextAsync(DbContext dbContext)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<DbContext> IDbContextPool.GetDbContexts()
        {
            throw new System.NotImplementedException();
        }

        int IDbContextPool.SavePoolChanges()
        {
            throw new System.NotImplementedException();
        }

        Task<int> IDbContextPool.SavePoolChangesAsync()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
