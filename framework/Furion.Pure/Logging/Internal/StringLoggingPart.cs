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
[SuppressSniffer]
public sealed partial class StringLoggingPart
{
    /// <summary>
    /// 静态缺省日志部件
    /// </summary>
    public static StringLoggingPart Default() => new();

    /// <summary>
    /// 日志内容
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// 日志级别
    /// </summary>
    public LogLevel Level { get; private set; } = LogLevel.Information;

    /// <summary>
    /// 消息格式化参数
    /// </summary>
    public object[] Args { get; private set; }

    /// <summary>
    /// 事件 Id
    /// </summary>
    public EventId? EventId { get; private set; }

    /// <summary>
    /// 日志分类类型（从依赖注入中解析）
    /// </summary>
    public Type CategoryType { get; private set; } = typeof(System.Running.Logging);

    /// <summary>
    /// 日志分类名（总是创建新的实例）
    /// </summary>
    public string CategoryName { get; private set; }

    /// <summary>
    /// 异常对象
    /// </summary>
    public Exception Exception { get; private set; }

    /// <summary>
    /// 日志对象所在作用域
    /// </summary>
    public IServiceProvider LoggerScoped { get; private set; }
}