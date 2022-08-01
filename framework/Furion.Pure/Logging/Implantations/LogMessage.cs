// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
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
/// 日志结构化消息
/// </summary>
[SuppressSniffer]
public struct LogMessage
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logName">记录器类别名称</param>
    /// <param name="logLevel">日志级别</param>
    /// <param name="eventId">事件 Id</param>
    /// <param name="message">日志消息</param>
    /// <param name="exception">异常对象</param>
    /// <param name="context">日志上下文</param>
    internal LogMessage(string logName
        , LogLevel logLevel
        , EventId eventId
        , string message
        , Exception exception
        , LogContext context)
    {
        LogName = logName;
        Message = message;
        LogLevel = logLevel;
        EventId = eventId;
        Exception = exception;
        Context = context;
    }

    /// <summary>
    /// 记录器类别名称
    /// </summary>
    public readonly string LogName;

    /// <summary>
    /// 日志级别
    /// </summary>
    public readonly LogLevel LogLevel;

    /// <summary>
    /// 事件 Id
    /// </summary>
    public readonly EventId EventId;

    /// <summary>
    /// 日志消息
    /// </summary>
    public readonly string Message;

    /// <summary>
    /// 异常对象
    /// </summary>
    public readonly Exception Exception;

    /// <summary>
    /// 日志上下文
    /// </summary>
    public LogContext Context;
}