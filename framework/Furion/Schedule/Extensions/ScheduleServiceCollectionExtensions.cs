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
            var scheduleHostedService = ActivatorUtilities.CreateInstance<ScheduleHostedService>(
                serviceProvider
                , scheduleOptionsBuilder.UseUtcTimestamp
                , scheduleOptionsBuilder.ClusterId);

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
            var schedulerFactory = ActivatorUtilities.CreateInstance<SchedulerFactory>(serviceProvider
                , schedulerBuilders
                , scheduleOptionsBuilder.UseUtcTimestamp);

            return schedulerFactory;
        });

        return services;
    }
}