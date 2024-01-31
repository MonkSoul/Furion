// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.TimeCrontab;

namespace Furion.TaskQueue;

/// <summary>
/// 任务队列静态类
/// </summary>
[SuppressSniffer]
public static class TaskQueued
{
    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="delay">延迟时间（毫秒）</param>
    /// <param name="channel">任务通道</param>
    /// <returns><see cref="Guid"/></returns>
    public static Guid Enqueue(Action<IServiceProvider> taskHandler, int delay = 0, string channel = null)
    {
        var taskQueue = App.GetRequiredService<ITaskQueue>(App.RootServices);
        return taskQueue.Enqueue(taskHandler, delay, channel);
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="delay">延迟时间（毫秒）</param>
    /// <param name="channel">任务通道</param>
    /// <returns><see cref="ValueTask"/></returns>
    public static async ValueTask<Guid> EnqueueAsync(Func<IServiceProvider, CancellationToken, ValueTask> taskHandler, int delay = 0, string channel = null)
    {
        var taskQueue = App.GetRequiredService<ITaskQueue>(App.RootServices);
        return await taskQueue.EnqueueAsync(taskHandler, delay, channel);
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="cronExpression">Cron 表达式</param>
    /// <param name="channel">任务通道</param>
    /// <param name="format"><see cref="CronStringFormat"/></param>
    /// <returns><see cref="Guid"/></returns>
    public static Guid Enqueue(Action<IServiceProvider> taskHandler, string cronExpression, CronStringFormat format = CronStringFormat.Default, string channel = null)
    {
        var taskQueue = App.GetRequiredService<ITaskQueue>(App.RootServices);
        return taskQueue.Enqueue(taskHandler, cronExpression, format, channel);
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="cronExpression">Cron 表达式</param>
    /// <param name="format"><see cref="CronStringFormat"/></param>
    /// <param name="channel">任务通道</param>
    /// <returns><see cref="ValueTask"/></returns>
    public static async ValueTask<Guid> EnqueueAsync(Func<IServiceProvider, CancellationToken, ValueTask> taskHandler, string cronExpression, CronStringFormat format = CronStringFormat.Default, string channel = null)
    {
        var taskQueue = App.GetRequiredService<ITaskQueue>(App.RootServices);
        return await taskQueue.EnqueueAsync(taskHandler, cronExpression, format, channel);
    }
}