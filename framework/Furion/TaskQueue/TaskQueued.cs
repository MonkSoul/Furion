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
    public static void Enqueue(Action<IServiceProvider> taskHandler, int delay = 0)
    {
        var taskQueue = App.GetRequiredService<ITaskQueue>(App.RootServices);
        taskQueue.Enqueue(taskHandler, delay);
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="delay">延迟时间（毫秒）</param>
    /// <returns><see cref="ValueTask"/></returns>
    public static async ValueTask EnqueueAsync(Func<IServiceProvider, CancellationToken, ValueTask> taskHandler, int delay = 0)
    {
        var taskQueue = App.GetRequiredService<ITaskQueue>(App.RootServices);
        await taskQueue.EnqueueAsync(taskHandler, delay);
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="cronExpression">Cron 表达式</param>
    /// <param name="format"><see cref="CronStringFormat"/></param>
    public static void Enqueue(Action<IServiceProvider> taskHandler, string cronExpression, CronStringFormat format = CronStringFormat.Default)
    {
        var taskQueue = App.GetRequiredService<ITaskQueue>(App.RootServices);
        taskQueue.Enqueue(taskHandler, cronExpression, format);
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="cronExpression">Cron 表达式</param>
    /// <param name="format"><see cref="CronStringFormat"/></param>
    /// <returns><see cref="ValueTask"/></returns>
    public static async ValueTask EnqueueAsync(Func<IServiceProvider, CancellationToken, ValueTask> taskHandler, string cronExpression, CronStringFormat format = CronStringFormat.Default)
    {
        var taskQueue = App.GetRequiredService<ITaskQueue>(App.RootServices);
        await taskQueue.EnqueueAsync(taskHandler, cronExpression, format);
    }
}