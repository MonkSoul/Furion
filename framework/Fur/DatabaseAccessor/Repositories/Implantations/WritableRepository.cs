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

using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可写仓储分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public partial class EFCoreRepository<TEntity, TDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 接受所有更改
        /// </summary>
        public virtual void AcceptAllChanges()
        {
            ChangeTracker.AcceptAllChanges();
        }

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns></returns>
        public int SavePoolNow()
        {
            return _dbContextPool.SavePoolNow();
        }

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public int SavePoolNow(bool acceptAllChangesOnSuccess)
        {
            return _dbContextPool.SavePoolNow(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> SavePoolNowAsync(CancellationToken cancellationToken = default)
        {
            return _dbContextPool.SavePoolNowAsync(cancellationToken);
        }

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> SavePoolNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return _dbContextPool.SavePoolNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <returns></returns>
        public virtual int SaveNow()
        {
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public virtual int SaveNow(bool acceptAllChangesOnSuccess)
        {
            return DbContext.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 提交更改操作（异步）
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> SaveNowAsync(CancellationToken cancellationToken = default)
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 提交更改操作（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> SaveNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return DbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}