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

using Furion.EventBus;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// EventBus 模块服务拓展
/// </summary>
[SuppressSniffer]
public static class EventBusServiceCollectionExtensions
{
    /// <summary>
    /// 添加 EventBus 模块注册
    /// </summary>
    /// <param name="services">服务集合对象</param>
    /// <param name="configureOptionsBuilder">事件总线配置选项构建器委托</param>
    /// <returns>服务集合实例</returns>
    public static IServiceCollection AddEventBus(this IServiceCollection services, Action<EventBusOptionsBuilder> configureOptionsBuilder)
    {
        // 创建初始事件总线配置选项构建器
        var eventBusOptionsBuilder = new EventBusOptionsBuilder();
        configureOptionsBuilder.Invoke(eventBusOptionsBuilder);

        return services.AddEventBus(eventBusOptionsBuilder);
    }

    /// <summary>
    /// 添加 EventBus 模块注册
    /// </summary>
    /// <param name="services">服务集合对象</param>
    /// <param name="eventBusOptionsBuilder">事件总线配置选项构建器</param>
    /// <returns>服务集合实例</returns>
    public static IServiceCollection AddEventBus(this IServiceCollection services, EventBusOptionsBuilder eventBusOptionsBuilder = default)
    {
        // 初始化事件总线配置项
        eventBusOptionsBuilder ??= new EventBusOptionsBuilder();

        // 注册内部服务
        services.AddInternalService(eventBusOptionsBuilder);

        // 构建事件总线服务
        eventBusOptionsBuilder.Build(services);

        // 通过工厂模式创建
        services.AddHostedService(serviceProvider =>
        {
            // 创建事件总线后台服务对象
            var eventBusHostedService = ActivatorUtilities.CreateInstance<EventBusHostedService>(
                serviceProvider
                , eventBusOptionsBuilder.UseUtcTimestamp
                , eventBusOptionsBuilder.FuzzyMatch
                , eventBusOptionsBuilder.GCCollect
                , eventBusOptionsBuilder.LogEnabled);

            // 订阅未察觉任务异常事件
            var unobservedTaskExceptionHandler = eventBusOptionsBuilder.UnobservedTaskExceptionHandler;
            if (unobservedTaskExceptionHandler != default)
            {
                eventBusHostedService.UnobservedTaskException += unobservedTaskExceptionHandler;
            }

            return eventBusHostedService;
        });

        return services;
    }

    /// <summary>
    /// 注册内部服务
    /// </summary>
    /// <param name="services">服务集合对象</param>
    /// <param name="eventBusOptionsBuilder">事件总线配置选项构建器</param>
    /// <returns>服务集合实例</returns>
    private static IServiceCollection AddInternalService(this IServiceCollection services, EventBusOptionsBuilder eventBusOptionsBuilder)
    {
        // 创建默认内存通道事件源对象
        var defaultStorerOfChannel = new ChannelEventSourceStorer(eventBusOptionsBuilder.ChannelCapacity);

        // 注册后台任务队列接口/实例为单例，采用工厂方式创建
        services.AddSingleton<IEventSourceStorer>(_ =>
        {
            return defaultStorerOfChannel;
        });

        // 注册默认内存通道事件发布者
        services.AddSingleton<IEventPublisher, ChannelEventPublisher>();

        // 注册事件总线工厂
        services.AddSingleton<IEventBusFactory, EventBusFactory>();

        return services;
    }
}