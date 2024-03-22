// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.TimeCrontab;
using System.Threading.Channels;

namespace Furion.TaskQueue;

/// <summary>
/// 任务队列默认实现
/// </summary>
internal sealed partial class TaskQueue : ITaskQueue
{
    /// <summary>
    /// 任务委托执行事件
    /// </summary>
    public event EventHandler<TaskHandlerEventArgs> OnExecuted;

    /// <summary>
    /// 队列通道
    /// </summary>
    private readonly Channel<TaskWrapper> _queue;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="capacity">队列通道默认容量，超过该容量进入等待</param>
    public TaskQueue(int capacity)
    {
        // 配置通道，设置超出默认容量后进入等待
        var boundedChannelOptions = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        // 创建有限容量通道
        _queue = Channel.CreateBounded<TaskWrapper>(boundedChannelOptions);
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="delay">延迟时间（毫秒）</param>
    /// <param name="channel">任务通道</param>
    /// <param name="taskId">任务 Id</param>
    /// <param name="concurrent">是否采用并行执行</param>
    /// <param name="runOnceIfDelaySet">配置是否设置了延迟执行后立即执行一次</param>
    /// <returns><see cref="object"/></returns>
    public object Enqueue(Action<IServiceProvider> taskHandler, int delay = 0, string channel = null, object taskId = null, bool? concurrent = null, bool runOnceIfDelaySet = false)
    {
        // 空检查
        if (taskHandler == default)
        {
            throw new ArgumentNullException(nameof(taskHandler));
        }

        return EnqueueAsync((serviceProvider, token) =>
        {
            taskHandler(serviceProvider);
            return ValueTask.CompletedTask;
        }, delay, channel, taskId, concurrent, runOnceIfDelaySet)
        .AsTask().GetAwaiter().GetResult();
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="delay">延迟时间（毫秒）</param>
    /// <param name="channel">任务通道</param>
    /// <param name="taskId">任务 Id</param>
    /// <param name="concurrent">是否采用并行执行</param>
    /// <param name="runOnceIfDelaySet">配置是否设置了延迟执行后立即执行一次</param>
    /// <returns><see cref="ValueTask"/></returns>
    public async ValueTask<object> EnqueueAsync(Func<IServiceProvider, CancellationToken, ValueTask> taskHandler, int delay = 0, string channel = null, object taskId = null, bool? concurrent = null, bool runOnceIfDelaySet = false)
    {
        // 空检查
        if (taskHandler == default)
        {
            throw new ArgumentNullException(nameof(taskHandler));
        }

        // 创建任务 ID
        var newTaskId = taskId ?? Guid.NewGuid();

        // 写入管道队列
        await _queue.Writer.WriteAsync(new TaskWrapper
        {
            TaskId = newTaskId,
            Channel = channel ?? string.Empty,
            Handler = async (serviceProvider, cancellationToken) =>
            {
                if (delay > 0)
                {
                    // 配置是否设置了延迟执行后立即执行一次
                    if (runOnceIfDelaySet)
                    {
                        await taskHandler(serviceProvider, cancellationToken);
                    }

                    await Task.Delay(delay, cancellationToken);
                }

                await taskHandler(serviceProvider, cancellationToken);
            },
            Concurrent = concurrent
        });

        return newTaskId;
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="cronExpression">Cron 表达式</param>
    /// <param name="channel">任务通道</param>
    /// <param name="format"><see cref="CronStringFormat"/></param>
    /// <param name="taskId">任务 Id</param>
    /// <param name="concurrent">是否采用并行执行</param>
    /// <param name="runOnceIfDelaySet">配置是否设置了延迟执行后立即执行一次</param>
    /// <returns><see cref="object"/></returns>
    public object Enqueue(Action<IServiceProvider> taskHandler, string cronExpression, CronStringFormat format = CronStringFormat.Default, string channel = null, object taskId = null, bool? concurrent = null, bool runOnceIfDelaySet = false)
    {
        var totalMilliseconds = Crontab.Parse(cronExpression, format)
                                            .GetSleepMilliseconds(DateTime.Now);

        return Enqueue(taskHandler, (int)totalMilliseconds, channel, taskId, concurrent, runOnceIfDelaySet);
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="cronExpression">Cron 表达式</param>
    /// <param name="format"><see cref="CronStringFormat"/></param>
    /// <param name="channel">任务通道</param>
    /// <param name="taskId">任务 Id</param>
    /// <param name="concurrent">是否采用并行执行</param>
    /// <param name="runOnceIfDelaySet">配置是否设置了延迟执行后立即执行一次</param>
    /// <returns><see cref="ValueTask"/></returns>
    public ValueTask<object> EnqueueAsync(Func<IServiceProvider, CancellationToken, ValueTask> taskHandler, string cronExpression, CronStringFormat format = CronStringFormat.Default, string channel = null, object taskId = null, bool? concurrent = null, bool runOnceIfDelaySet = false)
    {
        var totalMilliseconds = Crontab.Parse(cronExpression, format)
                                            .GetSleepMilliseconds(DateTime.Now);

        return EnqueueAsync(taskHandler, (int)totalMilliseconds, channel, taskId, concurrent, runOnceIfDelaySet);
    }

    /// <summary>
    /// 任务项出队
    /// </summary>
    /// <param name="cancellationToken">取消任务 Token</param>
    /// <returns><see cref="ValueTask"/></returns>
    public async ValueTask<TaskWrapper> DequeueAsync(CancellationToken cancellationToken)
    {
        // 读取管道队列
        var taskWrapper = await _queue.Reader.ReadAsync(cancellationToken);
        return taskWrapper;
    }

    /// <summary>
    /// 触发任务队列事件
    /// </summary>
    /// <param name="args">事件参数</param>
    public void InvokeEvents(TaskHandlerEventArgs args)
    {
        try
        {
            OnExecuted?.Invoke(this, args);
        }
        catch { }
    }
}