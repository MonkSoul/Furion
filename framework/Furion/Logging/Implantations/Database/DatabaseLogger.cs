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

namespace Furion.Logging;

/// <summary>
/// 数据库日志记录器
/// </summary>
/// <remarks>https://docs.microsoft.com/zh-cn/dotnet/core/extensions/custom-logging-provider</remarks>
[SuppressSniffer]
public sealed class DatabaseLogger : ILogger
{
    /// <summary>
    /// 排除日志分类名
    /// </summary>
    /// <remarks>避免数据库日志死循环</remarks>
    private static readonly string[] ExcludesOfLogCategoryName = new string[]
    {
        "Microsoft.EntityFrameworkCore"
    };

    /// <summary>
    /// 记录器类别名称
    /// </summary>
    private readonly string _logName;

    /// <summary>
    /// 数据库记录器提供器
    /// </summary>
    private readonly DatabaseLoggerProvider _databaseLoggerProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logName">记录器类别名称</param>
    /// <param name="databaseLoggerProvider">数据库记录器提供器</param>
    public DatabaseLogger(string logName, DatabaseLoggerProvider databaseLoggerProvider)
    {
        _logName = logName;
        _databaseLoggerProvider = databaseLoggerProvider;
    }

    /// <summary>
    /// 日志上下文
    /// </summary>
    public LogContext Context { get; internal set; }

    /// <summary>
    /// 开始逻辑操作范围
    /// </summary>
    /// <typeparam name="TState">标识符类型参数</typeparam>
    /// <param name="state">要写入的项/对象</param>
    /// <returns><see cref="IDisposable"/></returns>
    public IDisposable BeginScope<TState>(TState state)
    {
        // 设置日志上下文
        if (state is LogContext context)
        {
            if (Context == null) Context = new LogContext().SetRange(context.Properties);
            else Context.SetRange(context.Properties);
        }

        return default;
    }

    /// <summary>
    /// 检查是否已启用给定日志级别
    /// </summary>
    /// <param name="logLevel">日志级别</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= _databaseLoggerProvider.MinimumLevel;
    }

    /// <summary>
    /// 写入日志项
    /// </summary>
    /// <typeparam name="TState">标识符类型参数</typeparam>
    /// <param name="logLevel">日志级别</param>
    /// <param name="eventId">事件 Id</param>
    /// <param name="state">要写入的项/对象</param>
    /// <param name="exception">异常对象</param>
    /// <param name="formatter">日志格式化器</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void Log<TState>(LogLevel logLevel
        , EventId eventId
        , TState state
        , Exception exception
        , Func<TState, Exception, string> formatter)
    {
        // 判断日志级别是否有效
        if (!IsEnabled(logLevel)) return;

        // 排除数据库自身日志
        if (ExcludesOfLogCategoryName.Any(u => _logName.StartsWith(u))) return;

        // 检查日志格式化器
        if (formatter == null) throw new ArgumentNullException(nameof(formatter));

        // 获取格式化后的消息
        var message = formatter(state, exception);
        var logMsg = new LogMessage(_logName, logLevel, eventId, message, exception, Context);

        // 是否自定义了日志筛选器，如果是则检查是否条件
        if (_databaseLoggerProvider.LoggerOptions.WriteFilter?.Invoke(logMsg) == false) return;

        // 写入日志队列
        _databaseLoggerProvider.WriteToQueue(logMsg);
    }
}