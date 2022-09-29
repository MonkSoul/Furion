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

using Furion.Logging;

namespace System;

/// <summary>
/// 日志格式化静态类
/// </summary>
[SuppressSniffer]
public static class LoggerFormatter
{
    /// <summary>
    /// Json 输出格式化
    /// </summary>
    public static readonly Func<LogMessage, string> Json = (logMsg) =>
    {
        return logMsg.Write(writer =>
        {
            writer.WriteStartObject();

            // 输出日志级别
            writer.WriteString("logLevel", logMsg.LogLevel.ToString());

            // 输出日志时间
            writer.WriteString("logDateTime", logMsg.LogDateTime.ToString("o"));

            // 输出日志类别
            writer.WriteString("logName", logMsg.LogName);

            // 输出日志事件 Id
            writer.WriteNumber("eventId", logMsg.EventId.Id);

            // 输出日志消息
            writer.WriteString("message", logMsg.Message);

            // 输出日志所在线程 Id
            writer.WriteNumber("threadId", logMsg.ThreadId);

            // 输出异常信息
            writer.WritePropertyName("exception");
            if (logMsg.Exception == null) writer.WriteNullValue();
            else writer.WriteStringValue(logMsg.Exception.ToString());

            writer.WriteEndObject();
        });
    };
}