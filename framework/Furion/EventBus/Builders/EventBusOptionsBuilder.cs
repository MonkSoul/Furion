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

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Furion.EventBus;

/// <summary>
/// 事件总线配置选项构建器
/// </summary>
[SuppressSniffer]
public sealed class EventBusOptionsBuilder
{
    /// <summary>
    /// 事件订阅者类型集合
    /// </summary>
    private readonly List<Type> _eventSubscribers = new();

    /// <summary>
    /// 事件发布者类型
    /// </summary>
    private Type _eventPublisher;

    /// <summary>
    /// 事件存储器实现工厂
    /// </summary>
    private Func<IServiceProvider, IEventSourceStorer> _eventSourceStorerImplementationFactory;

    /// <summary>
    /// 事件处理程序监视器
    /// </summary>
    private Type _eventHandlerMonitor;

    /// <summary>
    /// 事件处理程序执行器
    /// </summary>
    private Type _eventHandlerExecutor;

    /// <summary>
    /// 事件重试策略类型集合
    /// </summary>
    private readonly List<Type> _fallbackPolicyTypes = new();

    /// <summary>
    /// 默认内置事件源存储器内存通道容量
    /// </summary>
    /// <remarks>超过 n 条待处理消息，第 n+1 条将进入等待，默认为 3000</remarks>
    public int ChannelCapacity { get; set; } = 3000;

    /// <summary>
    /// 是否使用 UTC 时间戳，默认 false
    /// </summary>
    public bool UseUtcTimestamp { get; set; } = false;

    /// <summary>
    /// 是否启用模糊匹配消息
    /// </summary>
    /// <remarks>支持正则表达式</remarks>
    public bool FuzzyMatch { get; set; } = false;

    /// <summary>
    /// 是否启用执行完成触发 GC 回收
    /// </summary>
    public bool GCCollect { get; set; } = true;

    /// <summary>
    /// 是否启用日志记录
    /// </summary>
    public bool LogEnabled { get; set; } = true;

    /// <summary>
    /// 重试失败策略配置
    /// </summary>
    public Type FallbackPolicy { get; set; }

    /// <summary>
    /// 未察觉任务异常事件处理程序
    /// </summary>
    public EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskExceptionHandler { get; set; }

    /// <summary>
    /// 注册事件订阅者
    /// </summary>
    /// <typeparam name="TEventSubscriber">实现自 <see cref="IEventSubscriber"/></typeparam>
    /// <returns><see cref="EventBusOptionsBuilder"/> 实例</returns>
    public EventBusOptionsBuilder AddSubscriber<TEventSubscriber>()
        where TEventSubscriber : class, IEventSubscriber
    {
        _eventSubscribers.Add(typeof(TEventSubscriber));
        return this;
    }

    /// <summary>
    /// 注册事件订阅者
    /// </summary>
    /// <param name="eventSubscriberType"><see cref="IEventSubscriber"/> 派生类型</param>
    /// <returns><see cref="EventBusOptionsBuilder"/> 实例</returns>
    public EventBusOptionsBuilder AddSubscriber(Type eventSubscriberType)
    {
        // 类型检查
        if (!typeof(IEventSubscriber).IsAssignableFrom(eventSubscriberType) || eventSubscriberType.IsInterface) throw new InvalidOperationException("The <eventSubscriberType> is not implement the IEventSubscriber interface.");

        _eventSubscribers.Add(eventSubscriberType);
        return this;
    }

    /// <summary>
    /// 批量注册事件订阅者
    /// </summary>
    /// <param name="assemblies">程序集</param>
    /// <returns><see cref="EventBusOptionsBuilder"/> 实例</returns>
    public EventBusOptionsBuilder AddSubscribers(params Assembly[] assemblies)
    {
        if (assemblies == null || assemblies.Length == 0)
        {
            throw new InvalidOperationException("The assemblies can be not null or empty.");
        }

        // 获取所有导出类型（非接口，非抽象类且实现 IEventSubscriber）接口
        var subscribers = assemblies.SelectMany(ass =>
              ass.GetExportedTypes()
                 .Where(t => t.IsPublic && t.IsClass && !t.IsInterface && !t.IsAbstract && typeof(IEventSubscriber).IsAssignableFrom(t)));

        foreach (var subscriber in subscribers)
        {
            _eventSubscribers.Add(subscriber);
        }

        return this;
    }

    /// <summary>
    /// 替换事件发布者
    /// </summary>
    /// <typeparam name="TEventPublisher">实现自 <see cref="IEventPublisher"/></typeparam>
    /// <returns><see cref="EventBusOptionsBuilder"/> 实例</returns>
    public EventBusOptionsBuilder ReplacePublisher<TEventPublisher>()
        where TEventPublisher : class, IEventPublisher
    {
        _eventPublisher = typeof(TEventPublisher);
        return this;
    }

    /// <summary>
    /// 替换事件源存储器
    /// </summary>
    /// <param name="implementationFactory">自定义事件源存储器工厂</param>
    /// <returns><see cref="EventBusOptionsBuilder"/> 实例</returns>
    public EventBusOptionsBuilder ReplaceStorer(Func<IServiceProvider, IEventSourceStorer> implementationFactory)
    {
        _eventSourceStorerImplementationFactory = implementationFactory;
        return this;
    }

    /// <summary>
    /// 替换事件源存储器（如果初始化失败则回退为默认的）
    /// </summary>
    /// <param name="createStorer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public EventBusOptionsBuilder ReplaceStorerOrFallback(Func<IEventSourceStorer> createStorer)
    {
        // 空检查
        if (createStorer == null) throw new ArgumentNullException(nameof(createStorer));

        try
        {
            // 创建事件源存储器
            var storer = createStorer.Invoke();

            // 替换事件源存储器
            ReplaceStorer(_ => storer);
        }
        catch { }

        return this;
    }

    /// <summary>
    /// 替换事件源存储器（如果初始化失败则回退为默认的）
    /// </summary>
    /// <param name="createStorer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public EventBusOptionsBuilder ReplaceStorerOrFallback(Func<IServiceProvider, IEventSourceStorer> createStorer)
    {
        // 空检查
        if (createStorer == null) throw new ArgumentNullException(nameof(createStorer));

        // 替换事件源存储器
        ReplaceStorer(serviceProvider =>
        {
            try
            {
                return createStorer.Invoke(serviceProvider);
            }
            catch
            {
                return new ChannelEventSourceStorer(ChannelCapacity);
            }
        });

        return this;
    }

    /// <summary>
    /// 注册事件处理程序监视器
    /// </summary>
    /// <typeparam name="TEventHandlerMonitor">实现自 <see cref="IEventHandlerMonitor"/></typeparam>
    /// <returns><see cref="EventBusOptionsBuilder"/> 实例</returns>
    public EventBusOptionsBuilder AddMonitor<TEventHandlerMonitor>()
        where TEventHandlerMonitor : class, IEventHandlerMonitor
    {
        _eventHandlerMonitor = typeof(TEventHandlerMonitor);
        return this;
    }

    /// <summary>
    /// 注册事件处理程序执行器
    /// </summary>
    /// <typeparam name="TEventHandlerExecutor">实现自 <see cref="IEventHandlerExecutor"/></typeparam>
    /// <returns><see cref="EventBusOptionsBuilder"/> 实例</returns>
    public EventBusOptionsBuilder AddExecutor<TEventHandlerExecutor>()
        where TEventHandlerExecutor : class, IEventHandlerExecutor
    {
        _eventHandlerExecutor = typeof(TEventHandlerExecutor);
        return this;
    }

    /// <summary>
    /// 注册事件重试策略
    /// </summary>
    /// <typeparam name="TEventFallbackPolicy">实现自 <see cref="IEventFallbackPolicy"/></typeparam>
    /// <returns><see cref="EventBusOptionsBuilder"/> 实例</returns>
    public EventBusOptionsBuilder AddFallbackPolicy<TEventFallbackPolicy>()
        where TEventFallbackPolicy : class, IEventFallbackPolicy
    {
        _fallbackPolicyTypes.Add(typeof(TEventFallbackPolicy));
        return this;
    }

    /// <summary>
    /// 注册事件重试策略
    /// </summary>
    /// <param name="fallbackPolicyType"><see cref="IEventFallbackPolicy"/> 派生类型</param>
    /// <returns><see cref="EventBusOptionsBuilder"/> 实例</returns>
    public EventBusOptionsBuilder AddFallbackPolicy(Type fallbackPolicyType)
    {
        // 类型检查
        if (!typeof(IEventFallbackPolicy).IsAssignableFrom(fallbackPolicyType) || fallbackPolicyType.IsInterface) throw new InvalidOperationException("The <fallbackPolicyType> is not implement the IEventFallbackPolicy interface.");

        _fallbackPolicyTypes.Add(fallbackPolicyType);
        return this;
    }

    /// <summary>
    /// 构建事件总线配置选项
    /// </summary>
    /// <param name="services">服务集合对象</param>
    internal void Build(IServiceCollection services)
    {
        // 注册事件订阅者
        foreach (var eventSubscriber in _eventSubscribers)
        {
            services.AddSingleton(typeof(IEventSubscriber), eventSubscriber);
        }

        // 替换事件发布者
        if (_eventPublisher != default)
        {
            services.Replace(ServiceDescriptor.Singleton(typeof(IEventPublisher), _eventPublisher));
        }

        // 替换事件存储器
        if (_eventSourceStorerImplementationFactory != default)
        {
            services.Replace(ServiceDescriptor.Singleton(_eventSourceStorerImplementationFactory));
        }

        // 注册事件监视器
        if (_eventHandlerMonitor != default)
        {
            services.AddSingleton(typeof(IEventHandlerMonitor), _eventHandlerMonitor);
        }

        // 注册事件执行器
        if (_eventHandlerExecutor != default)
        {
            services.AddSingleton(typeof(IEventHandlerExecutor), _eventHandlerExecutor);
        }

        // 注册事件重试策略
        foreach (var fallbackPolicyType in _fallbackPolicyTypes)
        {
            services.AddSingleton(fallbackPolicyType);
        }
    }
}