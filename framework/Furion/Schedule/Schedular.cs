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

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Furion.Schedule;

/// <summary>
/// 调度作业服务静态类
/// </summary>
[SuppressSniffer]
public static class ScheduleServe
{
    /// <summary>
    /// 初始化 Schedule 服务
    /// </summary>
    /// <remarks>仅限不能依赖注入的服务使用</remarks>
    /// <param name="configureOptionsBuilder">作业调度器配置选项构建器委托</param>
    /// <returns><see cref="IDisposable"/></returns>
    public static IDisposable Run(Action<ScheduleOptionsBuilder> configureOptionsBuilder)
    {
        // 创建初始作业调度器配置选项构建器
        var scheduleOptionsBuilder = new ScheduleOptionsBuilder();
        configureOptionsBuilder.Invoke(scheduleOptionsBuilder);

        return Run(scheduleOptionsBuilder);
    }

    /// <summary>
    /// 初始化 Schedule 服务
    /// </summary>
    /// <remarks>仅限不能依赖注入的服务使用</remarks>
    /// <param name="scheduleOptionsBuilder">作业调度器配置选项构建器</param>
    public static IDisposable Run(ScheduleOptionsBuilder scheduleOptionsBuilder = default)
    {
        // 创建服务集合
        var services = new ServiceCollection();

        // 注册初始服务
        services.AddConsoleFormatter();

        // 注册 Schedule 服务
        services.AddSchedule(scheduleOptionsBuilder);

        // 构建服务并解析 ScheduleHostedService
        var serviceProvider = services.BuildServiceProvider();
        var scheduleHostedService = serviceProvider.GetServices<IHostedService>()
            .Single(s => s.GetType().Name == nameof(ScheduleHostedService));

        // 启动服务
        var cancellationTokenSource = new CancellationTokenSource();
        scheduleHostedService.StartAsync(cancellationTokenSource.Token);

        return serviceProvider;
    }
}

/// <summary>
/// 作业调度器静态类
/// </summary>
[SuppressSniffer]
public static class Schedular
{
    /// <summary>
    /// 获取作业计划工厂
    /// </summary>
    /// <returns><see cref="ISchedulerFactory"/></returns>
    public static ISchedulerFactory GetFactory()
    {
        return App.GetRequiredService<ISchedulerFactory>(App.RootServices);
    }

    /// <summary>
    /// 获取作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <returns><see cref="IScheduler"/></returns>
    public static IScheduler GetJob(string jobId)
    {
        return GetFactory().GetJob(jobId);
    }

    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <remarks>主要用于作业触发器参数，作业信息额外数据序列化</remarks>
    /// <param name="obj">对象</param>
    /// <returns><see cref="string"/></returns>
    public static string Serialize(object obj)
    {
        return Penetrates.Serialize(obj);
    }

    /// <summary>
    /// 反序列化对象
    /// </summary>
    /// <remarks>主要用于作业触发器参数，作业信息额外数据序列化</remarks>
    /// <param name="json">JSON 字符串</param>
    /// <returns>T</returns>
    public static T Deserialize<T>(string json)
    {
        return Penetrates.Deserialize<T>(json);
    }
}