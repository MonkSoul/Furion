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
/// 构建字符串日志部分类
/// </summary>
public sealed partial class StringLoggingPart
{
    /// <summary>
    /// Information
    /// </summary>
    public void LogInformation()
    {
        SetLevel(LogLevel.Information).Log();
    }

    /// <summary>
    /// Warning
    /// </summary>
    public void LogWarning()
    {
        SetLevel(LogLevel.Warning).Log();
    }

    /// <summary>
    /// Error
    /// </summary>
    public void LogError()
    {
        SetLevel(LogLevel.Error).Log();
    }

    /// <summary>
    /// Debug
    /// </summary>
    public void LogDebug()
    {
        SetLevel(LogLevel.Debug).Log();
    }

    /// <summary>
    /// Trace
    /// </summary>
    public void LogTrace()
    {
        SetLevel(LogLevel.Trace).Log();
    }

    /// <summary>
    /// Critical
    /// </summary>
    public void LogCritical()
    {
        SetLevel(LogLevel.Critical).Log();
    }

    /// <summary>
    /// 写入日志
    /// </summary>
    /// <returns></returns>
    public void Log()
    {
        if (Message == null) return;

        ILoggerFactory loggerFactory = null;
        ILogger logger;
        var hasException = false;

        try
        {
            // 处理传入分类名
            if (!string.IsNullOrWhiteSpace(CategoryName))
            {
                loggerFactory = InternalApp.RunningOfHost
                    ? App.GetService<ILoggerFactory>(LoggerScoped ?? App.RootServices)
                    : CreateDisposeLoggerFactory();

                logger = loggerFactory.CreateLogger(CategoryName);
            }
            else
            {
                logger = InternalApp.RunningOfHost
                    ? App.GetService(typeof(ILogger<>).MakeGenericType(CategoryType), LoggerScoped ?? App.RootServices) as ILogger
                    : loggerFactory.CreateLogger(typeof(System.Running.Logging).FullName) as ILogger;
            }
        }
        catch
        {
            hasException = true;

            loggerFactory = CreateDisposeLoggerFactory();
            logger = loggerFactory.CreateLogger(CategoryName);
        }

        // 如果没有异常且事件 Id 为空
        if (Exception == null && EventId == null)
        {
            logger.Log(Level, Message, Args);
        }
        // 如果存在异常且事件 Id 为空
        else if (Exception != null && EventId == null)
        {
            logger.Log(Level, Exception, Message, Args);
        }
        // 如果异常为空且事件 Id 不为空
        else if (Exception == null && EventId != null)
        {
            logger.Log(Level, EventId.Value, Message, Args);
        }
        // 如果存在异常且事件 Id 不为空
        else if (Exception != null && EventId != null)
        {
            logger.Log(Level, EventId.Value, Exception, Message, Args);
        }
        else { }

        // 释放临时日志工厂
        if (InternalApp.RunningOfHost == false || hasException == true)
        {
            loggerFactory.Dispose();
        }
    }

    /// <summary>
    /// 创建待释放的日志工厂
    /// </summary>
    /// <returns></returns>
    private static ILoggerFactory CreateDisposeLoggerFactory()
    {
        return LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
    }
}