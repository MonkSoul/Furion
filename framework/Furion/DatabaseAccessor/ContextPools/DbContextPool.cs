// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Transactions;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库上下文池
/// </summary>
[SuppressSniffer]
public class DbContextPool : IDbContextPool, IDisposable
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
    private readonly ConcurrentDictionary<Guid, DbContext> _dbContexts;

    /// <summary>
    /// 登记错误的数据库上下文
    /// </summary>
    private readonly ConcurrentDictionary<Guid, DbContext> _failedDbContexts;

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

        _dbContexts = new ConcurrentDictionary<Guid, DbContext>();
        _failedDbContexts = new ConcurrentDictionary<Guid, DbContext>();
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
        return _dbContexts;
    }

    /// <summary>
    /// 保存数据库上下文
    /// </summary>
    /// <param name="dbContext"></param>
    public void AddToPool(DbContext dbContext)
    {
        // 跳过非关系型数据库
        if (!dbContext.Database.IsRelational()) return;

        var instanceId = dbContext.ContextId.InstanceId;
        if (!_dbContexts.TryAdd(instanceId, dbContext)) return;

        // 订阅数据库上下文操作失败事件
        dbContext.SaveChangesFailed += (s, e) =>
        {
            // 排除已经存在的数据库上下文
            if (!_failedDbContexts.TryAdd(instanceId, dbContext)) return;

            // 当前事务
            dynamic context = s as DbContext;
            var database = context.Database as DatabaseFacade;
            var currentTransaction = database?.CurrentTransaction;

            // 只有事务不等于空且支持自动回滚
            if (!(currentTransaction != null && context.FailedAutoRollback == true)) return;

            // 获取数据库连接信息
            var connection = database.GetDbConnection();

            // 回滚事务
            currentTransaction.Rollback();

            // 打印事务回滚消息
            App.PrintToMiniProfiler("transaction", "Rollback", $"[Connection Id: {context.ContextId}] / [Database: {connection.Database}]{(IsPrintDbConnectionInfo ? $" / [Connection String: {connection.ConnectionString}]" : string.Empty)}", isError: true);
        };
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
        return _dbContexts
            .Where(u => u.Value != null && !CheckDbContextDispose(u.Value) && u.Value.ChangeTracker.HasChanges() && !_failedDbContexts.Contains(u))
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
        return _dbContexts
            .Where(u => u.Value != null && !CheckDbContextDispose(u.Value) && u.Value.ChangeTracker.HasChanges() && !_failedDbContexts.Contains(u))
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
        var tasks = _dbContexts
            .Where(u => u.Value != null && !CheckDbContextDispose(u.Value) && u.Value.ChangeTracker.HasChanges() && !_failedDbContexts.Contains(u))
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
        var tasks = _dbContexts
            .Where(u => u.Value != null && !CheckDbContextDispose(u.Value) && u.Value.ChangeTracker.HasChanges() && !_failedDbContexts.Contains(u))
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
        // 判断是否启用了分布式环境事务，如果是，则跳过
        if (Transaction.Current != null) return;

        // 判断 dbContextPool 中是否包含DbContext，如果是，则使用第一个数据库上下文开启事务，并应用于其他数据库上下文
        EnsureTransaction: if (_dbContexts.Any())
        {
            // 如果共享事务不为空，则直接共享
            if (DbContextTransaction != null) goto ShareTransaction;

            // 先判断是否已经有上下文开启了事务
            var transactionDbContext = _dbContexts.FirstOrDefault(u => u.Value.Database.CurrentTransaction != null);

            DbContextTransaction = transactionDbContext.Value != null
                   ? transactionDbContext.Value.Database.CurrentTransaction
                   // 如果没有任何上下文有事务，则将第一个开启事务
                   : _dbContexts.First().Value.Database.BeginTransaction();

        // 共享事务
        ShareTransaction: ShareTransaction(DbContextTransaction.GetDbTransaction());

            // 打印事务实际开启信息
            App.PrintToMiniProfiler(MiniProfilerCategory, "Began");
        }
        else
        {
            // 判断是否确保事务强制可用（此处是无奈之举）
            if (!ensureTransaction) return;

            var defaultDbContextLocator = Penetrates.DbContextDescriptors.LastOrDefault();
            if (defaultDbContextLocator.Key == null) return;

            // 创建一个新的上下文
            var newDbContext = Db.GetDbContext(defaultDbContextLocator.Key, _serviceProvider);
            DbContextTransaction = newDbContext.Database.BeginTransaction();
            goto EnsureTransaction;
        }
    }

    /// <summary>
    /// 提交事务
    /// </summary>
    /// <param name="withCloseAll">是否自动关闭所有连接</param>
    public void CommitTransaction(bool withCloseAll = false)
    {
        // 判断是否启用了分布式环境事务，如果是，则跳过
        if (Transaction.Current != null) return;

        try
        {
            // 将所有数据库上下文修改 SaveChanges();，这里另外判断是否需要手动提交
            var hasChangesCount = SavePoolNow();

            // 如果事务为空，则执行完毕后关闭连接
            if (DbContextTransaction == null)
            {
                if (withCloseAll) CloseAll();
                return;
            }

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

        // 关闭所有连接
        if (withCloseAll) CloseAll();
    }

    /// <summary>
    /// 回滚事务
    /// </summary>
    /// <param name="withCloseAll">是否自动关闭所有连接</param>
    public void RollbackTransaction(bool withCloseAll = false)
    {
        // 判断是否启用了分布式环境事务，如果是，则跳过
        if (Transaction.Current != null) return;

        // 回滚事务
        if (DbContextTransaction?.GetDbTransaction()?.Connection != null) DbContextTransaction?.Rollback();
        DbContextTransaction?.Dispose();
        DbContextTransaction = null;

        // 打印事务回滚消息
        App.PrintToMiniProfiler(MiniProfilerCategory, "Rollback", isError: true);

        // 关闭所有连接
        if (withCloseAll) CloseAll();
    }

    /// <summary>
    /// 释放所有数据库上下文
    /// </summary>
    public void CloseAll()
    {
        if (!_dbContexts.Any()) return;

        foreach (var item in _dbContexts)
        {
            if (CheckDbContextDispose(item.Value)) continue;

            var conn = item.Value.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open) continue;

            conn.Close();
            // 打印数据库关闭信息
            App.PrintToMiniProfiler("sql", $"Close", $"Connection Close()");
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
        _ = _dbContexts
               .Where(u => u.Value != null && !CheckDbContextDispose(u.Value) && ((dynamic)u.Value).UseUnitOfWork == true && u.Value.Database.CurrentTransaction == null)
               .Select(u => u.Value.Database.UseTransaction(transaction))
               .Count();
    }

    /// <summary>
    /// 释放所有上下文
    /// </summary>
    public void Dispose()
    {
        _dbContexts.Clear();
    }

    /// <summary>
    /// 判断数据库上下文是否释放
    /// </summary>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    private static bool CheckDbContextDispose(DbContext dbContext)
    {
        // 反射获取 _disposed 字段，判断数据库上下文是否已释放
        var _disposedField = typeof(DbContext).GetField("_disposed", BindingFlags.Instance | BindingFlags.NonPublic);
        if (_disposedField == null) return false;

        var _disposed = Convert.ToBoolean(_disposedField.GetValue(dbContext));
        return _disposed;
    }
}