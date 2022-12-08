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
        taskQueueOptionsBuilder.Build(services);

        // 注册后台任务队列接口/实例为单例，采用工厂方式创建
        services.AddSingleton<ITaskQueue>(_ =>
        {
            // 创建后台队列实例
            return new TaskQueue(taskQueueOptionsBuilder.ChannelCapacity);
        });

        return services;
    }
}