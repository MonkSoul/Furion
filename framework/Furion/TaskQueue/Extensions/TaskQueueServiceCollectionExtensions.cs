// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Furion.TaskQueue;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
        services.TryAddSingleton<ITaskQueue>(_ =>
        {
            // 创建后台队列实例
            return new TaskQueue(taskQueueOptionsBuilder.ChannelCapacity);
        });

        return services;
    }
}