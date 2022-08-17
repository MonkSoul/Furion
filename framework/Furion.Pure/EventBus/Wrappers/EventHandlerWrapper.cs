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

namespace Furion.EventBus;

/// <summary>
/// 事件处理程序包装类
/// </summary>
/// <remarks>主要用于主机服务启动时将所有处理程序和事件 Id 进行包装绑定</remarks>
internal sealed class EventHandlerWrapper
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="eventId">事件Id</param>
    internal EventHandlerWrapper(string eventId)
    {
        EventId = eventId;
    }

    /// <summary>
    /// 事件 Id
    /// </summary>
    internal string EventId { get; set; }

    /// <summary>
    /// 事件处理程序
    /// </summary>
    internal Func<EventHandlerExecutingContext, Task> Handler { get; set; }

    /// <summary>
    /// 是否符合条件执行处理程序
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <returns><see cref="bool"/></returns>
    internal bool ShouldRun(string eventId)
    {
        return EventId == eventId;
    }
}