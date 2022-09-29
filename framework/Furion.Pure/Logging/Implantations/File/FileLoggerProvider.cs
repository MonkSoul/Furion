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

using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Furion.Logging;

/// <summary>
/// 文件日志记录器提供程序
/// </summary>
/// <remarks>https://docs.microsoft.com/zh-cn/dotnet/core/extensions/custom-logging-provider</remarks>
[SuppressSniffer, ProviderAlias("File")]
public sealed class FileLoggerProvider : ILoggerProvider
{
    /// <summary>
    /// 存储多日志分类日志记录器
    /// </summary>
    private readonly ConcurrentDictionary<string, FileLogger> _fileLoggers = new();

    /// <summary>
    /// 日志消息队列（线程安全）
    /// </summary>
    private readonly BlockingCollection<LogMessage> _logMessageQueue = new(1024);

    /// <summary>
    /// 记录日志所有滚动文件名
    /// </summary>
    /// <remarks>只有 <see cref="MaxRollingFiles"/> 和 <see cref="FileSizeLimitBytes"/> 大于 0 有效</remarks>
    internal readonly ConcurrentDictionary<string, FileInfo> _rollingFileNames = new();

    /// <summary>
    /// 文件日志写入器
    /// </summary>
    private readonly FileLoggingWriter _fileLoggingWriter;

    /// <summary>
    /// 长时间运行的后台任务
    /// </summary>
    /// <remarks>实现不间断写入</remarks>
    private readonly Task _processQueueTask;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fileName">日志文件名</param>
    public FileLoggerProvider(string fileName)
        : this(fileName, true)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fileName">日志文件名</param>
    /// <param name="append">追加到已存在日志文件或覆盖它们</param>
    public FileLoggerProvider(string fileName, bool append)
        : this(fileName, new FileLoggerOptions() { Append = append })
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fileName">日志文件名</param>
    /// <param name="fileLoggerOptions">文件日志记录器配置选项</param>
    public FileLoggerProvider(string fileName, FileLoggerOptions fileLoggerOptions)
    {
        // 支持文件名嵌入系统环境变量，格式为：%SystemDrive%，%SystemRoot%，处理 Windows 和 Linux 路径分隔符不一致问题
        FileName = Environment.ExpandEnvironmentVariables(fileName).Replace('\\', '/');
        LoggerOptions = fileLoggerOptions;

        // 创建文件日志写入器
        _fileLoggingWriter = new FileLoggingWriter(this);

        // 创建长时间运行的后台任务，并将日志消息队列中数据写入文件中
        _processQueueTask = Task.Factory.StartNew(state => ((FileLoggerProvider)state).ProcessQueue()
            , this, TaskCreationOptions.LongRunning);
    }

    /// <summary>
    /// 文件名
    /// </summary>
    internal string FileName;

    /// <summary>
    /// 文件日志记录器配置选项
    /// </summary>
    internal FileLoggerOptions LoggerOptions { get; private set; }

    /// <summary>
    /// 追加到已存在日志文件或覆盖它们
    /// </summary>
    internal bool Append => LoggerOptions.Append;

    /// <summary>
    /// 控制每一个日志文件最大存储大小，默认无限制，单位是 B，也就是 1024 才等于 1KB
    /// </summary>
    /// <remarks>如果指定了该值，那么日志文件大小超出了该配置就会创建的日志文件，新创建的日志文件命名规则：文件名+[递增序号].log</remarks>
    internal long FileSizeLimitBytes => LoggerOptions.FileSizeLimitBytes;

    /// <summary>
    /// 控制最大创建的日志文件数量，默认无限制，配合 <see cref="FileSizeLimitBytes"/> 使用
    /// </summary>
    /// <remarks>如果指定了该值，那么超出该值将从最初日志文件中从头写入覆盖</remarks>
    internal int MaxRollingFiles => LoggerOptions.MaxRollingFiles;

    /// <summary>
    /// 创建文件日志记录器
    /// </summary>
    /// <param name="categoryName">日志分类名</param>
    /// <returns><see cref="ILogger"/></returns>
    public ILogger CreateLogger(string categoryName)
    {
        return _fileLoggers.GetOrAdd(categoryName, name => new FileLogger(name, this));
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

        // 清空文件日志记录器
        _fileLoggers.Clear();

        // 清空滚动文件名记录器
        _rollingFileNames.Clear();

        // 释放内部文件写入器
        _fileLoggingWriter.Close();
    }

    /// <summary>
    /// 将日志消息写入队列中等待后台任务出队写入文件
    /// </summary>
    /// <param name="logMsg">日志消息</param>
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
    /// 将日志消息写入文件中
    /// </summary>
    private void ProcessQueue()
    {
        foreach (var logMsg in _logMessageQueue.GetConsumingEnumerable())
        {
            _fileLoggingWriter.Write(logMsg, _logMessageQueue.Count == 0);

            // 清空日志上下文
            ClearScopeContext(logMsg.LogName);
        }
    }

    /// <summary>
    /// 清空日志上下文
    /// </summary>
    /// <param name="categoryName"></param>
    private void ClearScopeContext(string categoryName)
    {
        var isExist = _fileLoggers.TryGetValue(categoryName, out var fileLogger);
        if (isExist)
        {
            fileLogger.Context?.Properties?.Clear();
            fileLogger.Context = null;
        }
    }
}