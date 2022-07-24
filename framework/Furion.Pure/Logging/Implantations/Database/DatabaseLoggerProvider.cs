using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Furion.Logging;

/// <summary>
/// 数据库日志记录器提供程序
/// </summary>
/// <remarks>https://docs.microsoft.com/zh-cn/dotnet/core/extensions/custom-logging-provider</remarks>
[SuppressSniffer, ProviderAlias("Database")]
public sealed class DatabaseLoggerProvider : ILoggerProvider
{
    /// <summary>
    /// 存储多日志分类日志记录器
    /// </summary>
    private readonly ConcurrentDictionary<string, DatabaseLogger> _databaseLoggers = new();

    /// <summary>
    /// 日志消息队列（线程安全）
    /// </summary>
    private readonly BlockingCollection<LogMessage> _logMessageQueue = new(1024);

    /// <summary>
    /// 服务提供器
    /// </summary>
    private IServiceProvider _serviceProvider;

    /// <summary>
    /// 长时间运行的后台任务
    /// </summary>
    /// <remarks>实现不间断写入</remarks>
    private Task _processQueueTask;

    /// <summary>
    /// 构造函数
    /// </summary>
    public DatabaseLoggerProvider()
        : this(new DatabaseLoggerOptions())
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="databaseLoggerOptions">数据库日志记录器配置选项</param>
    public DatabaseLoggerProvider(DatabaseLoggerOptions databaseLoggerOptions)
    {
        LoggerOptions = databaseLoggerOptions;
    }

    /// <summary>
    /// 最低日志记录级别
    /// </summary>
    public LogLevel MinimumLevel
    {
        get => LoggerOptions.MinimumLevel;
        set { LoggerOptions.MinimumLevel = value; }
    }

    /// <summary>
    /// 自定义数据库日志写入错误程序
    /// </summary>
    /// <remarks>主要解决日志在写入过程出现异常问题</remarks>
    /// <example>
    /// options.HandleWriteError = (err) => {
    ///     // do anything
    /// };
    /// </example>
    public Action<DatabaseWriteError> HandleWriteError
    {
        get => LoggerOptions.HandleWriteError;
        set { LoggerOptions.HandleWriteError = value; }
    }

    /// <summary>
    /// 数据库日志记录器配置选项
    /// </summary>
    internal DatabaseLoggerOptions LoggerOptions { get; private set; }

    /// <summary>
    /// 创建数据库日志记录器
    /// </summary>
    /// <param name="categoryName">日志分类名</param>
    /// <returns><see cref="ILogger"/></returns>
    public ILogger CreateLogger(string categoryName)
    {
        return _databaseLoggers.GetOrAdd(categoryName, name => new DatabaseLogger(name, this));
    }

    /// <summary>
    /// 释放非托管资源
    /// </summary>
    /// <remarks>控制日志消息队列</remarks>
    public void Dispose()
    {
        // 标记日志消息队列停止写入
        _logMessageQueue.CompleteAdding();

        try
        {
            // 设置 1.5秒的缓冲时间，避免还有日志消息没有完成写入文件中
            _processQueueTask.Wait(1500);
        }
        catch (TaskCanceledException) { }
        catch (AggregateException ex) when (ex.InnerExceptions.Count == 1 && ex.InnerExceptions[0] is TaskCanceledException) { }

        // 清空数据库日志记录器
        _databaseLoggers.Clear();
    }

    /// <summary>
    /// 将日志消息写入队列中等待后台任务出队写入数据库
    /// </summary>
    /// <param name="logMsg">结构化日志消息</param>
    internal void WriteToQueue(LogMessage logMsg)
    {
        // 只有队列可持续入队才写入
        if (!_logMessageQueue.IsAddingCompleted)
        {
            try
            {
                _logMessageQueue.Add(logMsg);
                return;
            }
            catch (InvalidOperationException) { }
        }
    }

    /// <summary>
    /// 设置服务提供器
    /// </summary>
    /// <param name="serviceProvider"></param>
    internal void SetServiceProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        // 创建长时间运行的后台任务，并将日志消息队列中数据写入文件中
        _processQueueTask = Task.Factory.StartNew(state => ((DatabaseLoggerProvider)state).ProcessQueue()
            , this, TaskCreationOptions.LongRunning);
    }

    /// <summary>
    /// 将日志消息写入数据库中
    /// </summary>
    private void ProcessQueue()
    {
        foreach (var message in _logMessageQueue.GetConsumingEnumerable())
        {
            IServiceScope serviceScope = null;
            IDatabaseLoggingWriter databaseLoggingWriter = null;

            try
            {
                // 解析服务作用域工厂服务
                var serviceScopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();

                // 创建服务作用域
                serviceScope = serviceScopeFactory.CreateScope();

                // 基于当前作用域创建数据库日志写入器
                databaseLoggingWriter = serviceScope.ServiceProvider.GetRequiredService<IDatabaseLoggingWriter>();

                // 调用数据库写入器写入数据库方法
                databaseLoggingWriter.Write(message, _logMessageQueue.Count == 0);
            }
            catch (Exception ex)
            {
                // 处理文件写入错误
                if (HandleWriteError != null)
                {
                    var databaseWriteError = new DatabaseWriteError(ex);
                    HandleWriteError(databaseWriteError);
                }
                // 其他直接抛出异常
                else throw;
            }
            finally
            {
                // 释放作用域
                serviceScope?.Dispose();
            }
        }
    }
}