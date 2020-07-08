using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Contexts
{
    /// <summary>
    /// 数据库上下文线程池接口
    /// <para>管理所有 DbContext 上下文，提供统一提交</para>
    /// </summary>
    public interface IDbContextPool
    {
        #region 保存 DbContext 上下文 + void SaveDbContext(DbContext dbContext)
        /// <summary>
        /// 保存 DbContext 上下文
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        void SaveDbContext(DbContext dbContext);
        #endregion

        #region 异步保存 DbContext 上下文 + void SaveDbContextAsync(DbContext dbContext)
        /// <summary>
        /// 异步保存 DbContext 上下文
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        /// <returns>任务</returns>
        Task SaveDbContextAsync(DbContext dbContext);
        #endregion

        #region 获取所有的数据库上下文 + IEnumerable<DbContext> GetDbContexts()
        /// <summary>
        /// 获取所有的数据库上下文
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<DbContext> GetDbContexts();
        #endregion

        #region 提交所有已更改的数据库上下文 +  int SavePoolChanges()
        /// <summary>
        /// 提交所有已更改的数据库上下文
        /// </summary>
        /// <returns>受影响行数</returns>
        int SavePoolChanges();
        #endregion

        #region 异步提交所有已更改的数据库上下文 + Task<int> SavePoolChangesAsync()
        /// <summary>
        /// 异步提交所有已更改的数据库上下文
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<int> SavePoolChangesAsync();
        #endregion
    }
}
