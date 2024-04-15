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