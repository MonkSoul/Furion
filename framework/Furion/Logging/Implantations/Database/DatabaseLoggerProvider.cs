// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
    /// 数据库日志写入器作用域范围
    /// </summary>
    internal IServiceScope _serviceScope;

    /// <summary>
    /// 数据库日志写入器
    /// </summary>
    private IDatabaseLoggingWriter _databaseLoggingWriter;

    /// <summary>
    /// 长时间运行的后台任务
    /// </summary>
    /// <remarks>实现不间断写入</remarks>
    private Task _processQueueTask;

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

        // 释放数据库写入器作用域范围
        _serviceScope?.Dispose();
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
        // 解析服务作用域工厂服务
        var serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

        // 创建服务作用域
        _serviceScope = serviceScopeFactory.CreateScope();

        // 基于当前作用域创建数据库日志写入器
        _databaseLoggingWriter = _serviceScope.ServiceProvider.GetRequiredService<IDatabaseLoggingWriter>();

        // 创建长时间运行的后台任务，并将日志消息队列中数据写入存储中
        _processQueueTask = Task.Factory.StartNew(state => ((DatabaseLoggerProvider)state).ProcessQueue()
            , this, TaskCreationOptions.LongRunning);
    }

    /// <summary>
    /// 将日志消息写入数据库中
    /// </summary>
    private void ProcessQueue()
    {
        foreach (var logMsg in _logMessageQueue.GetConsumingEnumerable())
        {
            try
            {
                // 调用数据库写入器写入数据库方法
                _databaseLoggingWriter.Write(logMsg, _logMessageQueue.Count == 0);
            }
            catch (Exception ex)
            {
                // 处理数据库写入错误
                if (HandleWriteError != null)
                {
                    var databaseWriteError = new DatabaseWriteError(ex);
                    HandleWriteError(databaseWriteError);
                }
                // 这里不抛出异常，避免中断日志写入
                else { }
            }
            finally
            {
                // 清空日志上下文
                ClearScopeContext(logMsg.LogName);
            }
        }
    }

    /// <summary>
    /// 清空日志上下文
    /// </summary>
    /// <param name="categoryName"></param>
    private void ClearScopeContext(string categoryName)
    {
        var isExist = _databaseLoggers.TryGetValue(categoryName, out var fileLogger);
        if (isExist)
        {
            fileLogger.Context?.Properties?.Clear();
            fileLogger.Context = null;
        }
    }
}