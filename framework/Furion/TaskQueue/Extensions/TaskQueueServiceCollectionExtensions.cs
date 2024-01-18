// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.TaskQueue;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// TaskQueue 模块服务拓展
/// </summary>
[SuppressSniffer]
public static class TaskQueueServiceCollectionExtensions
{
    /// <summary>
    /// 添加 TaskQueue 模块注册
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configureOptionsBuilder">任务队列配置选项构建器委托</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddTaskQueue(this IServiceCollection services, Action<TaskQueueOptionsBuilder> configureOptionsBuilder)
    {
        // 创建初始任务队列配置选项构建器
        var taskQueueOptionsBuilder = new TaskQueueOptionsBuilder();
        configureOptionsBuilder.Invoke(taskQueueOptionsBuilder);

        return services.AddTaskQueue(taskQueueOptionsBuilder);
    }

    /// <summary>
    /// 添加 TaskQueue 模块注册
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="taskQueueOptionsBuilder">任务队列配置选项构建器</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddTaskQueue(this IServiceCollection services, TaskQueueOptionsBuilder taskQueueOptionsBuilder = default)
    {
        // 初始化任务队列配置选项
        taskQueueOptionsBuilder ??= new TaskQueueOptionsBuilder();

        // 注册内部服务
        services.AddInternalService(taskQueueOptionsBuilder);

        // 注册任务队列后台主机服务
        services.AddHostedService(serviceProvider =>
        {
            // 创建任务队列后台主机对象
            var taskQueueHostedService = ActivatorUtilities.CreateInstance<TaskQueueHostedService>(serviceProvider
                , taskQueueOptionsBuilder.Concurrent
                , taskQueueOptionsBuilder.NumRetries
                , taskQueueOptionsBuilder.RetryTimeout);

            // 订阅未察觉任务异常事件
            var unobservedTaskExceptionHandler = taskQueueOptionsBuilder.UnobservedTaskExceptionHandler;
            if (unobservedTaskExceptionHandler != default)
            {
                taskQueueHostedService.UnobservedTaskException += unobservedTaskExceptionHandler;
            }

            return taskQueueHostedService;
        });

        return services;
    }

    /// <summary>
    /// 注册内部服务
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="taskQueueOptionsBuilder">任务队列配置选项构建器</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    private static IServiceCollection AddInternalService(this IServiceCollection services, TaskQueueOptionsBuilder taskQueueOptionsBuilder)
    {
        // 构建任务队列配置选项
        taskQueueOptionsBuilder.Build();

        // 注册后台任务队列接口/实例为单例，采用工厂方式创建
        services.AddSingleton<ITaskQueue>(_ =>
        {
            // 创建后台队列实例
            return new TaskQueue(taskQueueOptionsBuilder.ChannelCapacity);
        });

        return services;
    }
}