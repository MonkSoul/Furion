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

namespace Furion.EventBus;

/// <summary>
/// 事件处理程序执行后上下文
/// </summary>
[SuppressSniffer]
public sealed class EventHandlerExecutedContext : EventHandlerContext
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="eventSource">事件源（事件承载对象）</param>
    /// <param name="properties">共享上下文数据</param>
    internal EventHandlerExecutedContext(IEventSource eventSource, IDictionary<object, object> properties)
        : base(eventSource, properties)
    {
    }

    /// <summary>
    /// 执行后时间
    /// </summary>
    public DateTime ExecutedTime { get; internal set; }

    /// <summary>
    /// 异常信息
    /// </summary>
    public InvalidOperationException Exception { get; internal set; }
}