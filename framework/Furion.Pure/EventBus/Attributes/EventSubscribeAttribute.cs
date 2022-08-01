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

using Furion.Extensitions.EventBus;

namespace Furion.EventBus;

/// <summary>
/// 事件处理程序特性
/// </summary>
/// <remarks>
/// <para>作用于 <see cref="IEventSubscriber"/> 实现类实例方法</para>
/// <para>支持多个事件 Id 触发同一个事件处理程序</para>
/// </remarks>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public sealed class EventSubscribeAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="eventId">事件 Id</param>
    /// <remarks>只支持事件类型和 Enum 类型</remarks>
    public EventSubscribeAttribute(object eventId)
    {
        if (eventId is string)
        {
            EventId = eventId as string;
        }
        else if (eventId is Enum)
        {
            EventId = (eventId as Enum).ParseToString();
        }
        else throw new ArgumentException("Only support string or Enum data type.");
    }

    /// <summary>
    /// 事件 Id
    /// </summary>
    public string EventId { get; set; }
}