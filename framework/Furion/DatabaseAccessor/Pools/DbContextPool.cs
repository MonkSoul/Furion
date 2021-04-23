using Furion.DependencyInjection;
using Microsoft.EntityFrameworkCore;
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
    [SkipScan]
    public class DbContextPool : IDbContextPool
    {
        /// <summary>
        /// MiniProfiler 分类名
        /// </summary>
        private const string MiniProfilerCategory = "transaction";

        /// <summary>
        /// MiniProfiler 组件状态
        /// </summary>
        private readonly bool InjectMiniProfiler;

        /// <summary>
        /// 是否打印数据库连接信息
        /// </summary>
        private readonly bool IsPrintDbConnectionInfo;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DbContextPool()
        {
            InjectMiniProfiler = App.Settings.InjectMiniProfiler.Value;
            IsPrintDbConnectionInfo = App.Settings.PrintDbConnectionInfo.Value;

            dbContexts = new ConcurrentDictionary<Guid, DbContext>();
            failedDbContexts = new ConcurrentDictionary<Guid, DbContext>();
        }

        /// <summary>
        /// 线程安全的数据库上下文集合
        /// </summary>
        private readonly ConcurrentDictionary<Guid, DbContext> dbContexts;

        /// <summary>
        /// 登记错误的数据库上下文
        /// </summary>
        private readonly ConcurrentDictionary<Guid, DbContext> failedDbContexts;

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
                        var context = s as DbContext;

                        // 当前事务
                        var database = context.Database;
                        var currentTransaction = database.CurrentTransaction;
                        if (currentTransaction != null)
                        {
                            // 获取数据库连接信息
                            var connection = database.GetDbConnection();

                            // 回滚事务
                            currentTransaction.Rollback();

                            // 打印事务回滚消息
                            App.PrintToMiniProfiler(MiniProfilerCategory, "Rollback", $"[Connection Id: {context.ContextId}] / [Database: {connection.Database}]{(IsPrintDbConnectionInfo ? $" / [Connection String: {connection.ConnectionString}]" : string.Empty)}", isError: true);
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
        /// 设置数据库上下文共享事务
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public void ShareTransaction(int skipCount, DbTransaction transaction)
        {
            // 跳过第一个数据库上下文并设置共享事务
            _ = dbContexts
                   .Where(u => u.Value != null)
                   .Skip(skipCount)
                   .Select(u => u.Value.Database.UseTransaction(transaction));
        }

        /// <summary>
        /// 设置数据库上下文共享事务
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ShareTransactionAsync(int skipCount, DbTransaction transaction, CancellationToken cancellationToken = default)
        {
            // 跳过第一个数据库上下文并设置贡献事务
            var tasks = dbContexts
                .Where(u => u.Value != null)
                .Skip(skipCount)
                .Select(u => u.Value.Database.UseTransactionAsync(transaction, cancellationToken));

            await Task.WhenAll(tasks);
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
    }
}