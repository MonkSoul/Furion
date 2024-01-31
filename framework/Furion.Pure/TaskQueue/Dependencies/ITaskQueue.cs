// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.TimeCrontab;

namespace Furion.TaskQueue;

/// <summary>
/// 任务队列接口
/// </summary>
public interface ITaskQueue
{
    /// <summary>
    /// 任务委托执行事件
    /// </summary>
    event EventHandler<TaskHandlerEventArgs> OnExecuted;

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="delay">延迟时间（毫秒）</param>
    /// <param name="channel">任务通道</param>
    /// <returns><see cref="Guid"/></returns>
    Guid Enqueue(Action<IServiceProvider> taskHandler, int delay = 0, string channel = null);

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="delay">延迟时间（毫秒）</param>
    /// <param name="channel">任务通道</param>
    /// <returns><see cref="ValueTask"/></returns>
    ValueTask<Guid> EnqueueAsync(Func<IServiceProvider, CancellationToken, ValueTask> taskHandler, int delay = 0, string channel = null);

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="cronExpression">Cron 表达式</param>
    /// <param name="format"><see cref="CronStringFormat"/></param>
    /// <param name="channel">任务通道</param>
    /// <returns><see cref="Guid"/></returns>
    Guid Enqueue(Action<IServiceProvider> taskHandler, string cronExpression, CronStringFormat format = CronStringFormat.Default, string channel = null);

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="cronExpression">Cron 表达式</param>
    /// <param name="format"><see cref="CronStringFormat"/></param>
    /// <param name="channel">任务通道</param>
    /// <returns><see cref="ValueTask"/></returns>
    ValueTask<Guid> EnqueueAsync(Func<IServiceProvider, CancellationToken, ValueTask> taskHandler, string cronExpression, CronStringFormat format = CronStringFormat.Default, string channel = null);

    /// <summary>
    /// 任务项出队
    /// </summary>
    /// <param name="cancellationToken">取消任务 Token</param>
    /// <returns><see cref="ValueTask"/></returns>
    ValueTask<TaskWrapper> DequeueAsync(CancellationToken cancellationToken);

    /// <summary>
    /// 触发任务队列事件
    /// </summary>
    /// <param name="args">事件参数</param>
    void InvokeEvents(TaskHandlerEventArgs args);
}