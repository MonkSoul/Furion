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
    /// <remarks>超过 n 条待处理消息，第 n+1 条将进入等待，默认为 12000</remarks>
    public int ChannelCapacity { get; set; } = 12000;

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
            services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IEventSubscriber), eventSubscriber));
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