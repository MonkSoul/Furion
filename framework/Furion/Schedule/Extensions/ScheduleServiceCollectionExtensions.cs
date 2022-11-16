// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
                , scheduleOptionsBuilder.UseUtcTimestamp);

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
        var schedulers = scheduleOptionsBuilder.Build(services);

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
                , schedulers
                , scheduleOptionsBuilder.UseUtcTimestamp);

            return schedulerFactory;
        });

        return services;
    }
}