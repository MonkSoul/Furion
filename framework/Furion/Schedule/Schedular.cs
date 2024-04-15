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

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

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

    /// <summary>
    /// 编译 C# 类定义代码
    /// </summary>
    /// <param name="csharpCode">字符串代码</param>
    /// <param name="assemblyName">自定义程序集名称</param>
    /// <param name="additionalAssemblies">附加的程序集</param>
    /// <returns><see cref="Assembly"/></returns>
    public static Assembly CompileCSharpClassCode(string csharpCode, string assemblyName = default, params Assembly[] additionalAssemblies)
    {
        return App.CompileCSharpClassCode(csharpCode, assemblyName, additionalAssemblies);
    }
}