// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.EventBus;

/// <summary>
/// 事件订阅者依赖接口
/// </summary>
/// <remarks>
/// <para>可自定义事件处理方法，但须符合 Func{EventSubscribeExecutingContext, Task} 签名</para>
/// <para>通常只做依赖查找，不做服务调用</para>
/// </remarks>
public interface IEventSubscriber
{
    /*
     * // 事件处理程序定义规范
     * [EventSubscribe(YourEventID)]
     * public Task YourHandler(EventHandlerExecutingContext context)
     * {
     *     // To Do...
     * }
     */
}