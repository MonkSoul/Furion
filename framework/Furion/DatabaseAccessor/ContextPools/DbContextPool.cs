// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 数据库上下文池
    /// </summary>
    [SuppressSniffer]
    public class DbContextPool : IDbContextPool
    {
        /// <summary>
        ///  MiniProfiler 分类名
        /// </summary>
        private const string MiniProfilerCategory = "Transaction";

        /// <summary>
        /// MiniProfiler 组件状态
        /// </summary>
        private readonly bool InjectMiniProfiler;

        /// <summary>
        /// 是否打印数据库连接信息
        /// </summary>
        private readonly bool IsPrintDbConnectionInfo;

        /// <summary>
        /// 线程安全的数据库上下文集合
        /// </summary>
        private readonly ConcurrentDictionary<Guid, DbContext> dbContexts;

        /// <summary>
        /// 登记错误的数据库上下文
        /// </summary>
        private readonly ConcurrentDictionary<Guid, DbContext> failedDbContexts;

        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        public DbContextPool(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InjectMiniProfiler = App.Settings.InjectMiniProfiler.Value;
            IsPrintDbConnectionInfo = App.Settings.PrintDbConnectionInfo.Value;

            dbContexts = new ConcurrentDictionary<Guid, DbContext>();
            failedDbContexts = new ConcurrentDictionary<Guid, DbContext>();
        }

        /// <summary>
        /// 数据库上下文事务
        /// </summary>
        public IDbContextTransaction DbContextTransaction { get; private set; }

        /// <summary>
        /// 获取所有数据库上下文
        /// </summary>
        /// <returns></returns>
        public ConcurrentDictionary<Guid, DbContext> GetDbContexts()
        {
            return dbContexts;
        }

        /// <summary>
        /// 保存数据库上下文
        /// </summary>
        /// <param name="dbContext"></param>
        public void AddToPool(DbContext dbContext)
        {
            var instanceId = dbContext.ContextId.InstanceId;

            var canAdd = dbContexts.TryAdd(instanceId, dbContext);
            if (canAdd)
            {
                // 订阅数据库上下文操作失败事件
                dbContext.SaveChangesFailed += (s, e) =>
                {
                    // 排除已经存在的数据库上下文
                    var canAdd = failedDbContexts.TryAdd(instanceId, dbContext);
                    if (canAdd)
                    {
                        dynamic context = s as DbContext;

                        // 当前事务
                        var database = context.Database as DatabaseFacade;
                        var currentTransaction = database?.CurrentTransaction;
                        if (currentTransaction != null && context.FailedAutoRollback == true)
                        {
                            // 获取数据库连接信息
                            var connection = database.GetDbConnection();

                            // 回滚事务
                            currentTransaction.Rollback();

                            // 打印事务回滚消息
                            App.PrintToMiniProfiler("transaction", "Rollback", $"[Connection Id: {context.ContextId}] / [Database: {connection.Database}]{(IsPrintDbConnectionInfo ? $" / [Connection String: {connection.ConnectionString}]" : string.Empty)}", isError: true);
                        }
                    }
                };
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
                .Where(u => u.Value != null && u.Value.ChangeTracker.HasChanges() && !failedDbContexts.Contains(u))
                .Select(u => u.Value.SaveChanges()).Count();
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
                .Where(u => u.Value != null && u.Value.ChangeTracker.HasChanges() && !failedDbContexts.Contains(u))
                .Select(u => u.Value.SaveChanges(acceptAllChangesOnSuccess)).Count();
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
                .Where(u => u.Value != null && u.Value.ChangeTracker.HasChanges() && !failedDbContexts.Contains(u))
                .Select(u => u.Value.SaveChangesAsync(cancellationToken));

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
                .Where(u => u.Value != null && u.Value.ChangeTracker.HasChanges() && !failedDbContexts.Contains(u))
                .Select(u => u.Value.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));

            // 等待所有异步完成
            var results = await Task.WhenAll(tasks);
            return results.Length;
        }

        /// <summary>
        /// 打开事务
        /// </summary>
        /// <param name="ensureTransaction"></param>
        /// <returns></returns>
        public void BeginTransaction(bool ensureTransaction = false)
        {
        // 判断 dbContextPool 中是否包含DbContext，如果是，则使用第一个数据库上下文开启事务，并应用于其他数据库上下文
        EnsureTransaction: if (dbContexts.Any())
            {
                // 如果共享事务不为空，则直接共享
                if (DbContextTransaction != null) goto ShareTransaction;

                // 先判断是否已经有上下文开启了事务
                var transactionDbContext = dbContexts.FirstOrDefault(u => u.Value.Database.CurrentTransaction != null);
                if (transactionDbContext.Value != null)
                {
                    DbContextTransaction = transactionDbContext.Value.Database.CurrentTransaction;
                }
                else
                {
                    // 如果没有任何上下文有事务，则将第一个开启事务
                    DbContextTransaction = dbContexts.First().Value.Database.BeginTransaction();
                }

            // 共享事务
            ShareTransaction: ShareTransaction(DbContextTransaction.GetDbTransaction());

                // 打印事务实际开启信息
                App.PrintToMiniProfiler(MiniProfilerCategory, "Began");
            }
            else
            {
                // 判断是否确保事务强制可用（此处是无奈之举）
                if (ensureTransaction)
                {
                    var defaultDbContextLocator = Penetrates.DbContextWithLocatorCached.LastOrDefault();
                    if (defaultDbContextLocator.Key == null) return;

                    // 创建一个新的上下文
                    var newDbContext = Db.GetDbContext(defaultDbContextLocator.Key, _serviceProvider);
                    goto EnsureTransaction;
                }
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="isManualSaveChanges"></param>
        /// <param name="exception"></param>
        /// <param name="withCloseAll">是否自动关闭所有连接</param>
        public void CommitTransaction(bool isManualSaveChanges = true, Exception exception = default, bool withCloseAll = false)
        {
            // 判断是否异常
            if (exception == null)
            {
                try
                {
                    // 将所有数据库上下文修改 SaveChanges();，这里另外判断是否需要手动提交
                    var hasChangesCount = !isManualSaveChanges ? SavePoolNow() : 0;

                    // 如果事务为空，则执行完毕后关闭连接
                    if (DbContextTransaction == null) goto CloseAll;

                    // 提交共享事务
                    DbContextTransaction?.Commit();

                    // 打印事务提交消息
                    App.PrintToMiniProfiler(MiniProfilerCategory, "Completed", $"Transaction Completed! Has {hasChangesCount} DbContext Changes.");
                }
                catch
                {
                    // 回滚事务
                    if (DbContextTransaction?.GetDbTransaction()?.Connection != null) DbContextTransaction?.Rollback();

                    // 打印事务回滚消息
                    App.PrintToMiniProfiler(MiniProfilerCategory, "Rollback", isError: true);

                    throw;
                }
                finally
                {
                    if (DbContextTransaction?.GetDbTransaction()?.Connection != null)
                    {
                        DbContextTransaction = null;
                        DbContextTransaction?.Dispose();
                    }
                }
            }
            else
            {
                // 回滚事务
                if (DbContextTransaction?.GetDbTransaction()?.Connection != null) DbContextTransaction?.Rollback();
                DbContextTransaction?.Dispose();
                DbContextTransaction = null;

                // 打印事务回滚消息
                App.PrintToMiniProfiler(MiniProfilerCategory, "Rollback", isError: true);
            }

        // 关闭所有连接
        CloseAll: if (withCloseAll) CloseAll();
        }

        /// <summary>
        /// 释放所有数据库上下文
        /// </summary>
        public void CloseAll()
        {
            if (!dbContexts.Any()) return;

            foreach (var item in dbContexts)
            {
                var conn = item.Value.Database.GetDbConnection();
                if (conn.State == ConnectionState.Open)
                {
                    var wrapConn = InjectMiniProfiler ? new ProfiledDbConnection(conn, MiniProfiler.Current) : conn;
                    wrapConn.Close();
                }
            }
        }

        /// <summary>
        /// 设置数据库上下文共享事务
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private void ShareTransaction(DbTransaction transaction)
        {
            // 跳过第一个数据库上下文并设置共享事务
            _ = dbContexts
                   .Where(u => u.Value != null && u.Value.Database.CurrentTransaction == null)
                   .Select(u => u.Value.Database.UseTransaction(transaction));
        }
    }
}