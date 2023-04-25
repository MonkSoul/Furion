// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Furion.FriendlyException;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Logging;

namespace Furion.TaskQueue;

/// <summary>
/// 任务队列后台主机服务
/// </summary>
/// <remarks>用于长时间监听任务项入队后进行出队调用</remarks>
internal sealed class TaskQueueHostedService : BackgroundService
{
    /// <summary>
    /// 避免由 CLR 的终结器捕获该异常从而终止应用程序，让所有未觉察异常被觉察
    /// </summary>
    internal event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException;

    /// <summary>
    /// 日志对象
    /// </summary>
    private readonly ILogger<TaskQueueService> _logger;

    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 后台任务队列
    /// </summary>
    private readonly ITaskQueue _taskQueue;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志对象</param>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="taskQueue">后台任务队列</param>
    public TaskQueueHostedService(ILogger<TaskQueueService> logger
        , IServiceProvider serviceProvider
        , ITaskQueue taskQueue)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _taskQueue = taskQueue;
    }

    /// <summary>
    /// 执行后台任务
    /// </summary>
    /// <param name="stoppingToken">后台主机服务停止时取消任务 Token</param>
    /// <returns>Task</returns>
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("TaskQueue hosted service is running.");

        // 注册后台主机服务停止监听
        stoppingToken.Register(() =>
            _logger.LogDebug($"TaskQueue hosted service is stopping."));

        // 监听服务是否取消
        while (!stoppingToken.IsCancellationRequested)
        {
            // 执行具体任务
            await BackgroundProcessing(stoppingToken);
        }

        _logger.LogCritical($"TaskQueue hosted service is stopped.");
    }

    /// <summary>
    /// 后台调用处理程序
    /// </summary>
    /// <param name="stoppingToken">后台主机服务停止时取消任务 Token</param>
    /// <returns><see cref="Task"/></returns>
    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
        // 出队
        var taskHandler = await _taskQueue.DequeueAsync(stoppingToken);

        Parallel.For(0, 1, async _ =>
        {
            try
            {
                // 调用任务处理程序并配置出错执行重试
                await Retry.InvokeAsync(async () =>
                {
                    // 调用任务处理委托
                    await taskHandler(_serviceProvider, stoppingToken);
                }
                , 3
                , 1000
                , retryAction: (total, times) =>
                {
                    // 输出重试日志
                    _logger.LogWarning("Retrying {times}/{total} times for {TaskHandler}", times, total, taskHandler?.ToString());
                });
            }
            catch (Exception ex)
            {
                // 输出异常日志
                _logger.LogError(ex, "Error occurred executing in {TaskHandler}.", taskHandler?.ToString());

                // 捕获 Task 任务异常信息并统计所有异常
                if (UnobservedTaskException != default)
                {
                    var args = new UnobservedTaskExceptionEventArgs(
                        ex as AggregateException ?? new AggregateException(ex));

                    UnobservedTaskException.Invoke(this, args);
                }
            }
        });
    }
}