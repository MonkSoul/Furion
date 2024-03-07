// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Schedule;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Schedule 模块服务拓展
/// </summary>
[SuppressSniffer]
public static class ScheduleServiceCollectionExtensions
{
    /// <summary>
    /// 添加 Schedule 模块注册
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configureOptionsBuilder">作业调度器配置选项构建器委托</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddSchedule(this IServiceCollection services, Action<ScheduleOptionsBuilder> configureOptionsBuilder)
    {
        // 创建初始作业调度器配置选项构建器
        var scheduleOptionsBuilder = new ScheduleOptionsBuilder();
        configureOptionsBuilder.Invoke(scheduleOptionsBuilder);

        return services.AddSchedule(scheduleOptionsBuilder);
    }

    /// <summary>
    /// 添加 Schedule 模块注册
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="scheduleOptionsBuilder">作业调度器配置选项构建器</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddSchedule(this IServiceCollection services, ScheduleOptionsBuilder scheduleOptionsBuilder = default)
    {
        // 初始化作业调度器配置选项
        scheduleOptionsBuilder ??= new ScheduleOptionsBuilder();

        // 注册内部服务
        services.AddInternalService(scheduleOptionsBuilder);

        // 注册作业调度器后台主机服务
        services.AddHostedService(serviceProvider =>
        {
            // 创建作业调度器后台主机对象
            var scheduleHostedService = ActivatorUtilities.CreateInstance<ScheduleHostedService>(serviceProvider, scheduleOptionsBuilder.ClusterId);

            // 订阅未察觉任务异常事件
            var unobservedTaskExceptionHandler = scheduleOptionsBuilder.UnobservedTaskExceptionHandler;
            if (unobservedTaskExceptionHandler != default)
            {
                scheduleHostedService.UnobservedTaskException += unobservedTaskExceptionHandler;
            }

            return scheduleHostedService;
        });

        return services;
    }

    /// <summary>
    /// 注册内部服务
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="scheduleOptionsBuilder">作业调度器配置选项构建器</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    private static IServiceCollection AddInternalService(this IServiceCollection services, ScheduleOptionsBuilder scheduleOptionsBuilder)
    {
        // 构建作业调度器配置选项
        var schedulerBuilders = scheduleOptionsBuilder.Build(services);

        // 注册空日志服务
        services.AddLogging();

        // 注册作业调度器日志服务
        services.AddSingleton<IScheduleLogger>(serviceProvider =>
        {
            var scheduleLogger = ActivatorUtilities.CreateInstance<ScheduleLogger>(serviceProvider
                , scheduleOptionsBuilder.LogEnabled);

            return scheduleLogger;
        });

        // 注册作业计划工厂服务
        services.AddSingleton<ISchedulerFactory>(serviceProvider =>
        {
            var schedulerFactory = ActivatorUtilities.CreateInstance<SchedulerFactory>(serviceProvider, schedulerBuilders);
            return schedulerFactory;
        });

        // 注册取消作业执行 Token 器
        services.AddSingleton<IJobCancellationToken, JobCancellationToken>();

        return services;
    }
}