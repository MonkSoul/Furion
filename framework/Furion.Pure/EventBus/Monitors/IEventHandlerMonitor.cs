// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.EventBus;

/// <summary>
/// 事件处理程序监视器
/// </summary>
public interface IEventHandlerMonitor
{
    /// <summary>
    /// 事件处理程序执行前
    /// </summary>
    /// <param name="context">上下文</param>
    /// <returns><see cref="Task"/> 实例</returns>
    Task OnExecutingAsync(EventHandlerExecutingContext context);

    /// <summary>
    /// 事件处理程序执行后
    /// </summary>
    /// <param name="context">上下文</param>
    /// <returns><see cref="Task"/> 实例</returns>
    Task OnExecutedAsync(EventHandlerExecutedContext context);
}