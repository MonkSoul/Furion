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

using Furion.Scheduler;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Scheduler 模块服务拓展
/// </summary>
[SuppressSniffer]
public static class SchedulerServiceCollectionExtensions
{
    /// <summary>
    /// 添加 Scheduler 模块注册
    /// </summary>
    /// <param name="services">服务集合对象</param>
    /// <param name="configureOptionsBuilder">作业调度配置选项构建器委托</param>
    /// <returns>服务集合实例</returns>
    public static IServiceCollection AddScheduler(this IServiceCollection services, Action<SchedulerOptionsBuilder> configureOptionsBuilder)
    {
        // 创建初始定时任务配置选项构建器
        var schedulerOptionsBuilder = new SchedulerOptionsBuilder();
        configureOptionsBuilder.Invoke(schedulerOptionsBuilder);

        return services.AddScheduler(schedulerOptionsBuilder);
    }

    /// <summary>
    /// 添加 Scheduler 模块注册
    /// </summary>
    /// <param name="services">服务集合对象</param>
    /// <param name="schedulerOptionsBuilder">定时任务配置选项构建器</param>
    /// <returns>服务集合实例</returns>
    public static IServiceCollection AddScheduler(this IServiceCollection services, SchedulerOptionsBuilder schedulerOptionsBuilder = default)
    {
        // 初始化作业调度配置项
        schedulerOptionsBuilder ??= new SchedulerOptionsBuilder();

        // 注册内部服务
        services.AddInternalService(schedulerOptionsBuilder);

        // 注册作业调度后台主机服务
        services.AddHostedService(serviceProvider =>
        {
            // 创建作业调度后台服务对象
            var schedulerHostedService = ActivatorUtilities.CreateInstance<SchedulerHostedService>(
                serviceProvider
                , schedulerOptionsBuilder.UseUtcTimestamp);

            // 订阅未察觉任务异常事件
            var unobservedTaskExceptionHandler = schedulerOptionsBuilder.UnobservedTaskExceptionHandler;
            if (unobservedTaskExceptionHandler != default)
            {
                schedulerHostedService.UnobservedTaskException += unobservedTaskExceptionHandler;
            }

            return schedulerHostedService;
        });

        return services;
    }

    /// <summary>
    /// 注册内部服务
    /// </summary>
    /// <param name="services">服务集合对象</param>
    /// <param name="schedulerOptionsBuilder">作业调度配置选项构建器</param>
    /// <returns>服务集合实例</returns>
    private static IServiceCollection AddInternalService(this IServiceCollection services, SchedulerOptionsBuilder schedulerOptionsBuilder)
    {
        // 构建作业调度服务
        var jobSchedulers = schedulerOptionsBuilder.Build(services);

        // 注册作业调度日志服务
        services.AddSingleton<ISchedulerLogger>(serviceProvider =>
        {
            var schedulerLogger = ActivatorUtilities.CreateInstance<SchedulerLogger>(serviceProvider
                , schedulerOptionsBuilder.LogEnabled);

            return schedulerLogger;
        });

        // 注册作业调度工厂服务
        services.AddSingleton<ISchedulerFactory>(serviceProvider =>
        {
            var schedulerFactory = ActivatorUtilities.CreateInstance<SchedulerFactory>(serviceProvider
                , jobSchedulers);

            return schedulerFactory;
        });

        return services;
    }
}