// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Reflection;

namespace Furion.EventBus;

/// <summary>
/// 事件处理程序执行前上下文
/// </summary>
[SuppressSniffer]
public sealed class EventHandlerExecutingContext : EventHandlerContext
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="eventSource">事件源（事件承载对象）</param>
    /// <param name="properties">共享上下文数据</param>
    /// <param name="handlerMethod">触发的方法</param>
    /// <param name="attribute">订阅特性</param>
    internal EventHandlerExecutingContext(IEventSource eventSource
        , IDictionary<object, object> properties
        , MethodInfo handlerMethod
        , EventSubscribeAttribute attribute)
        : base(eventSource, properties, handlerMethod, attribute)
    {
    }

    /// <summary>
    /// 执行前时间
    /// </summary>
    public DateTime ExecutingTime { get; internal set; }
}