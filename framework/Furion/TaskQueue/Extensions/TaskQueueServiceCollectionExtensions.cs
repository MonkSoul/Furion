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
            var taskQueueHostedService = ActivatorUtilities.CreateInstance<TaskQueueHostedService>(serviceProvider);

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