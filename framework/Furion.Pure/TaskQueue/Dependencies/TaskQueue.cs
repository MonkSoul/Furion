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
    /// 队列通道
    /// </summary>
    private readonly Channel<Func<IServiceProvider, CancellationToken, ValueTask>> _queue;

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
        _queue = Channel.CreateBounded<Func<IServiceProvider, CancellationToken, ValueTask>>(boundedChannelOptions);
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="delay">延迟时间（毫秒）</param>
    public void Enqueue(Action<IServiceProvider> taskHandler, int delay = 0)
    {
        // 空检查
        if (taskHandler == default)
        {
            throw new ArgumentNullException(nameof(taskHandler));
        }

        _ = EnqueueAsync((serviceProvider, token) =>
          {
              taskHandler(serviceProvider);
              return ValueTask.CompletedTask;
          }, delay).AsTask();
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="delay">延迟时间（毫秒）</param>
    /// <returns><see cref="ValueTask"/></returns>
    public async ValueTask EnqueueAsync(Func<IServiceProvider, CancellationToken, ValueTask> taskHandler, int delay = 0)
    {
        // 空检查
        if (taskHandler == default)
        {
            throw new ArgumentNullException(nameof(taskHandler));
        }

        // 写入管道队列
        await _queue.Writer.WriteAsync(async (serviceProvider, cancellationToken) =>
        {
            if (delay > 0)
            {
                await Task.Delay(delay, cancellationToken);
            }

            await taskHandler(serviceProvider, cancellationToken);
        });
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="cronExpression">Cron 表达式</param>
    /// <param name="format"><see cref="CronStringFormat"/></param>
    public void Enqueue(Action<IServiceProvider> taskHandler, string cronExpression, CronStringFormat format = CronStringFormat.Default)
    {
        var totalMilliseconds = Crontab.Parse(cronExpression, format)
                                            .GetSleepMilliseconds(DateTime.Now);

        Enqueue(taskHandler, (int)totalMilliseconds);
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="cronExpression">Cron 表达式</param>
    /// <param name="format"><see cref="CronStringFormat"/></param>
    /// <returns><see cref="ValueTask"/></returns>
    public ValueTask EnqueueAsync(Func<IServiceProvider, CancellationToken, ValueTask> taskHandler, string cronExpression, CronStringFormat format = CronStringFormat.Default)
    {
        var totalMilliseconds = Crontab.Parse(cronExpression, format)
                                            .GetSleepMilliseconds(DateTime.Now);

        return EnqueueAsync(taskHandler, (int)totalMilliseconds);
    }

    /// <summary>
    /// 任务项出队
    /// </summary>
    /// <param name="cancellationToken">取消任务 Token</param>
    /// <returns><see cref="ValueTask"/></returns>
    public async ValueTask<Func<IServiceProvider, CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken)
    {
        // 读取管道队列
        var taskHandler = await _queue.Reader.ReadAsync(cancellationToken);
        return taskHandler;
    }
}