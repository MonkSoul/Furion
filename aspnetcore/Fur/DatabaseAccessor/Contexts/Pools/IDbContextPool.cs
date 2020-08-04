using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Contexts
{
    /// <summary>
    /// 数据库上下文池
    /// </summary>
    /// <remarks>
    /// <para>用来管理请求中所有 <see cref="DbContext"/> 实例</para>
    /// <para>非依赖注入方式创建的 <see cref="DbContext"/> 需手动调用 <see cref="SaveDbContext(DbContext)"/> 或 <see cref="SaveDbContextAsync(DbContext)"/> 保存到数据库上下文池中</para>
    /// <para>数据库上下文池必须注册为 <c>Scope</c> 范围实例，保证单次请求唯一，参见：依赖注入章节：<see cref="https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1"/></para>
    /// </remarks>
    public interface IDbContextPool
    {
        /// <summary>
        /// 保存数据库上下文
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        void SaveDbContext(DbContext dbContext);

        /// <summary>
        /// 保存数据库上下文（异步）
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <returns><see cref="Task"/></returns>
        Task SaveDbContextAsync(DbContext dbContext);

        /// <summary>
        /// 获取数据库上下文池中所有数据库上下文
        /// </summary>
        /// <returns>数据库上下文池中所有数据库上下文集合</returns>
        IEnumerable<DbContext> GetDbContexts();

        /// <summary>
        /// 提交数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns>已更改的数据库上下文个数</returns>
        int SavePoolChanges();

        /// <summary>
        /// 提交数据库上下文池中所有已更改的数据库上下文（异步）
        /// </summary>
        /// <returns>已更改的数据库上下文个数</returns>
        Task<int> SavePoolChangesAsync();
    }
}