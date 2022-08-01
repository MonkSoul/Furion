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

using Furion.Logging;

namespace Microsoft.Extensions.Logging;

/// <summary>
/// <see cref="ILogger"/> 拓展
/// </summary>
[SuppressSniffer]
public static class ILoggerExtensions
{
    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    public static ILogger ScopeContext(this ILogger logger, IDictionary<object, object> properties)
    {
        logger.BeginScope(new LogContext { Properties = properties });

        return logger;
    }

    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static ILogger ScopeContext(this ILogger logger, Action<LogContext> configure)
    {
        var logContext = new LogContext();
        configure?.Invoke(logContext);

        logger.BeginScope(logContext);

        return logger;
    }

    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static ILogger ScopeContext(this ILogger logger, LogContext context)
    {
        logger.BeginScope(context);

        return logger;
    }
}