// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.EventBus;

/// <summary>
/// 事件处理程序执行器依赖接口
/// </summary>
public interface IEventHandlerExecutor
{
    /// <summary>
    /// 执行事件处理程序
    /// </summary>
    /// <remarks>在这里可以实现超时控制，失败重试控制等等</remarks>
    /// <param name="context">事件处理程序执行前上下文</param>
    /// <param name="handler">事件处理程序</param>
    /// <returns><see cref="Task"/> 实例</returns>
    Task ExecuteAsync(EventHandlerExecutingContext context, Func<EventHandlerExecutingContext, Task> handler);
}