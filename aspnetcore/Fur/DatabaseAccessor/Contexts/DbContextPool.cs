using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Contexts
{
    /// <summary>
    /// 数据库上下文池
    /// <para>用来管理请求中所有创建的 <see cref="DbContext"/> 对象</para>
    /// <para>说明：非依赖注入方式创建的 <see cref="DbContext"/> 需手动调用 <see cref="SaveDbContext(DbContext)"/> 或 <see cref="SaveDbContextAsync(DbContext)"/> 保存到数据库上下文池中</para>
    /// <para>数据库上下文池必须注册为 <c>Scope</c> 范围实例，保证单次请求唯一，参见：依赖注入章节：<see cref="https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1"/></para>
    /// </summary>
    public class DbContextPool : IDbContextPool
    {
        /// <summary>
        /// 线程安全的数据库上下文集合
        /// </summary>
        private readonly ConcurrentBag<DbContext> dbContexts;

        #region 构造函数 + public DbContextPool()
        /// <summary>
        /// 构造函数
        /// <para>首次初始化时，会检查数据库上下文集合是否为空，如果为空，则自动创建</para>
        /// </summary>
        public DbContextPool()
        {
            dbContexts ??= new ConcurrentBag<DbContext>();
        }
        #endregion

        #region 保存数据库上下文 + public void SaveDbContext(DbContext dbContext)
        /// <summary>
        /// 保存数据库上下文
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

        #region 保存数据库上下文（异步） + public Task SaveDbContextAsync(DbContext dbContext)
        /// <summary>
        /// 保存数据库上下文（异步）
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <returns><see cref="Task"/></returns>
        public Task SaveDbContextAsync(DbContext dbContext)
        {
            if (!dbContexts.Contains(dbContext))
            {
                dbContexts.Add(dbContext);
            }
            return Task.CompletedTask;
        }
        #endregion

        #region 获取数据库上下文池中所有数据库上下文 + public IEnumerable<DbContext> GetDbContexts()
        /// <summary>
        /// 获取数据库上下文池中所有数据库上下文
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public IEnumerable<DbContext> GetDbContexts()
        {
            return dbContexts;
        }
        #endregion

        #region 提交数据库上下文池中所有已更改的数据库上下文 + public int SavePoolChanges()
        /// <summary>
        /// 提交数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns>已更改的数据库上下文个数</returns>
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

        #region 提交数据库上下文池中所有已更改的数据库上下文（异步） + public async Task<int> SavePoolChangesAsync()
        /// <summary>
        /// 提交数据库上下文池中所有已更改的数据库上下文（异步）
        /// </summary>
        /// <returns>已更改的数据库上下文个数</returns>
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
        #endregion
    }
}