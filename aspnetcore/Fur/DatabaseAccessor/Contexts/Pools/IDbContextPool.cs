using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Contexts.Pools
{
    /// <summary>
    /// 数据库上下文池
    /// <para>用来管理请求中所有创建的 <see cref="DbContext"/> 对象</para>
    /// <para>说明：非依赖注入方式创建的 <see cref="DbContext"/> 需手动调用 <see cref="SaveDbContext(DbContext)"/> 或 <see cref="SaveDbContextAsync(DbContext)"/> 保存到数据库上下文池中</para>
    /// <para>数据库上下文池必须注册为 <c>Scope</c> 范围实例，保证单次请求唯一，参见：依赖注入章节：<see cref="https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1"/></para>
    /// </summary>
    public interface IDbContextPool
    {
        #region 保存数据库上下文 + void SaveDbContext(DbContext dbContext)
        /// <summary>
        /// 保存数据库上下文
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        void SaveDbContext(DbContext dbContext);
        #endregion

        #region 保存数据库上下文（异步） + Task SaveDbContextAsync(DbContext dbContext)
        /// <summary>
        /// 保存数据库上下文（异步）
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <returns><see cref="Task"/></returns>
        Task SaveDbContextAsync(DbContext dbContext);
        #endregion

        #region 获取数据库上下文池中所有数据库上下文 + IEnumerable<DbContext> GetDbContexts()
        /// <summary>
        /// 获取数据库上下文池中所有数据库上下文
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<DbContext> GetDbContexts();
        #endregion

        #region 提交数据库上下文池中所有已更改的数据库上下文 + int SavePoolChanges()
        /// <summary>
        /// 提交数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns>已更改的数据库上下文个数</returns>
        int SavePoolChanges();
        #endregion

        #region 提交数据库上下文池中所有已更改的数据库上下文（异步） + Task<int> SavePoolChangesAsync()
        /// <summary>
        /// 提交数据库上下文池中所有已更改的数据库上下文（异步）
        /// </summary>
        /// <returns>已更改的数据库上下文个数</returns>
        Task<int> SavePoolChangesAsync();
        #endregion
    }
}