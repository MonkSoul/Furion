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

using Furion.FriendlyException;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Logging;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Furion.EventBus;

/// <summary>
/// 事件总线后台主机服务
/// </summary>
internal sealed class EventBusHostedService : BackgroundService
{
    /// <summary>
    /// GC 回收默认间隔
    /// </summary>
    private const int GC_COLLECT_INTERVAL_SECONDS = 3;

    /// <summary>
    /// 避免由 CLR 的终结器捕获该异常从而终止应用程序，让所有未觉察异常被觉察
    /// </summary>
    internal event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException;

    /// <summary>
    /// 日志对象
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 事件源存储器
    /// </summary>
    private readonly IEventSourceStorer _eventSourceStorer;

    /// <summary>
    /// 事件发布服务
    /// </summary>
    private readonly IEventPublisher _eventPublisher;

    /// <summary>
    /// 事件处理程序集合
    /// </summary>
    private readonly ConcurrentDictionary<EventHandlerWrapper, EventHandlerWrapper> _eventHandlers = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志对象</param>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="eventSourceStorer">事件源存储器</param>
    /// <param name="eventPublisher">事件发布服务</param>
    /// <param name="eventSubscribers">事件订阅者集合</param>
    /// <param name="useUtcTimestamp">是否使用 Utc 时间</param>
    /// <param name="fuzzyMatch">是否启用模糊匹配事件消息</param>
    /// <param name="gcCollect">是否启用执行完成触发 GC 回收</param>
    /// <param name="logEnabled">是否启用日志记录</param>
    public EventBusHostedService(ILogger<EventBusService> logger
        , IServiceProvider serviceProvider
        , IEventSourceStorer eventSourceStorer
        , IEventPublisher eventPublisher
        , IEnumerable<IEventSubscriber> eventSubscribers
        , bool useUtcTimestamp
        , bool fuzzyMatch
        , bool gcCollect
        , bool logEnabled)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _eventPublisher = eventPublisher;
        _eventSourceStorer = eventSourceStorer;

        Monitor = serviceProvider.GetService<IEventHandlerMonitor>();
        Executor = serviceProvider.GetService<IEventHandlerExecutor>();
        UseUtcTimestamp = useUtcTimestamp;
        FuzzyMatch = fuzzyMatch;
        GCCollect = gcCollect;
        LogEnabled = logEnabled;

        var bindingAttr = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        // 逐条获取事件处理程序并进行包装
        foreach (var eventSubscriber in eventSubscribers)
        {
            // 获取事件订阅者类型
            var eventSubscriberType = eventSubscriber.GetType();

            // 查找所有公开且贴有 [EventSubscribe] 的实例方法
            var eventHandlerMethods = eventSubscriberType.GetMethods(bindingAttr)
                .Where(u => u.IsDefined(typeof(EventSubscribeAttribute), false));

            // 遍历所有事件订阅者处理方法
            foreach (var eventHandlerMethod in eventHandlerMethods)
            {
                // 将方法转换成 Func<EventHandlerExecutingContext, Task> 委托
                var handler = (Func<EventHandlerExecutingContext, Task>)eventHandlerMethod.CreateDelegate(typeof(Func<EventHandlerExecutingContext, Task>), eventSubscriber);

                // 处理同一个事件处理程序支持多个事件 Id 情况
                var eventSubscribeAttributes = eventHandlerMethod.GetCustomAttributes<EventSubscribeAttribute>(false);

                // 逐条包装并添加到 _eventHandlers 集合中
                foreach (var eventSubscribeAttribute in eventSubscribeAttributes)
                {
                    var wrapper = new EventHandlerWrapper(eventSubscribeAttribute.EventId)
                    {
                        Handler = handler,
                        HandlerMethod = eventHandlerMethod,
                        Attribute = eventSubscribeAttribute,
                        Pattern = CheckIsSetFuzzyMatch(eventSubscribeAttribute.FuzzyMatch) ? new Regex(eventSubscribeAttribute.EventId, RegexOptions.Singleline) : default,
                        GCCollect = CheckIsSetGCCollect(eventSubscribeAttribute.GCCollect),
                        Order = eventSubscribeAttribute.Order
                    };

                    _eventHandlers.TryAdd(wrapper, wrapper);
                }
            }
        }
    }

    /// <summary>
    /// 事件处理程序监视器
    /// </summary>
    private IEventHandlerMonitor Monitor { get; }

    /// <summary>
    /// 事件处理程序执行器
    /// </summary>
    private IEventHandlerExecutor Executor { get; }

    /// <summary>
    /// 是否使用 UTC 时间
    /// </summary>
    private bool UseUtcTimestamp { get; }

    /// <summary>
    /// 是否启用模糊匹配事件消息
    /// </summary>
    private bool FuzzyMatch { get; }

    /// <summary>
    /// 是否启用执行完成触发 GC 回收
    /// </summary>
    private bool GCCollect { get; }

    /// <summary>
    /// 是否启用日志记录
    /// </summary>
    private bool LogEnabled { get; }

    /// <summary>
    /// 最近一次收集时间
    /// </summary>
    private DateTime? LastGCCollectTime { get; set; }

    /// <summary>
    /// 执行后台任务
    /// </summary>
    /// <param name="stoppingToken">后台主机服务停止时取消任务 Token</param>
    /// <returns><see cref="Task"/> 实例</returns>
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Log(LogLevel.Information, "EventBus hosted service is running.");

        // 注册后台主机服务停止监听
        stoppingToken.Register(() =>
           Log(LogLevel.Debug, $"EventBus hosted service is stopping."));

        // 监听服务是否取消
        while (!stoppingToken.IsCancellationRequested)
        {
            // 执行具体任务
            await BackgroundProcessing(stoppingToken);
        }

        Log(LogLevel.Critical, $"EventBus hosted service is stopped.");
    }

    /// <summary>
    /// 后台调用处理程序
    /// </summary>
    /// <param name="stoppingToken">后台主机服务停止时取消任务 Token</param>
    /// <returns><see cref="Task"/> 实例</returns>
    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
        // 从事件存储器中读取一条
        var eventSource = await _eventSourceStorer.ReadAsync(stoppingToken);

        // 处理动态新增/删除事件订阅器
        if (eventSource is EventSubscribeOperateSource subscribeOperateSource)
        {
            ManageEventSubscribers(subscribeOperateSource);

            return;
        }

        // 空检查
        if (string.IsNullOrWhiteSpace(eventSource?.EventId))
        {
            Log(LogLevel.Warning, "Invalid EventId, EventId cannot be <null> or an empty string.");

            return;
        }

        // 查找事件 Id 匹配的事件处理程序
        var eventHandlersThatShouldRun = _eventHandlers.Where(t => t.Key.ShouldRun(eventSource.EventId)).OrderByDescending(u => u.Value.Order)
            .Select(u => u.Key)
            .ToList();

        // 空订阅
        if (!eventHandlersThatShouldRun.Any())
        {
            Log(LogLevel.Warning, "Subscriber with event ID <{EventId}> was not found.", new[] { eventSource.EventId });

            return;
        }

        // 检查是否配置只消费一次
        if (eventSource.IsConsumOnce)
        {
            var randomId = RandomNumberGenerator.GetInt32(0, eventHandlersThatShouldRun.Count);
            eventHandlersThatShouldRun = [eventHandlersThatShouldRun.ElementAt(randomId)];
        }

        // 创建一个任务工厂并保证执行任务都使用当前的计划程序
        var taskFactory = new TaskFactory(TaskScheduler.Current);

        // 创建共享上下文数据对象
        var properties = new Dictionary<object, object>();

        // 通过并行方式提高吞吐量并解决 Thread.Sleep 问题
        Parallel.ForEach(eventHandlersThatShouldRun, (eventHandlerThatShouldRun) =>
        {
            // 创建新的线程执行
            taskFactory.StartNew(async () =>
            {
                // 获取特性信息，可能为 null
                var eventSubscribeAttribute = eventHandlerThatShouldRun.Attribute;

                // 创建执行前上下文
                var eventHandlerExecutingContext = new EventHandlerExecutingContext(eventSource, properties, eventHandlerThatShouldRun.HandlerMethod, eventSubscribeAttribute)
                {
                    ExecutingTime = UseUtcTimestamp ? DateTime.UtcNow : DateTime.Now
                };

                // 执行异常对象
                InvalidOperationException executionException = default;

                try
                {
                    // 处理任务取消
                    eventSource.CancellationToken.ThrowIfCancellationRequested();

                    // 调用执行前监视器
                    if (Monitor != default)
                    {
                        await Monitor.OnExecutingAsync(eventHandlerExecutingContext);
                    }

                    // 判断是否自定义了执行器
                    if (Executor == default)
                    {
                        // 判断是否自定义了重试失败回调服务
                        var fallbackPolicyService = eventSubscribeAttribute?.FallbackPolicy == null
                            ? null
                            : _serviceProvider.GetService(eventSubscribeAttribute.FallbackPolicy) as IEventFallbackPolicy;

                        // 调用事件处理程序并配置出错执行重试
                        await Retry.InvokeAsync(async () =>
                        {
                            await eventHandlerThatShouldRun.Handler!(eventHandlerExecutingContext);
                        }
                        , eventSubscribeAttribute?.NumRetries ?? 0
                        , eventSubscribeAttribute?.RetryTimeout ?? 1000
                        , exceptionTypes: eventSubscribeAttribute?.ExceptionTypes
                        , fallbackPolicy: fallbackPolicyService == null ? null : async (ex) => await fallbackPolicyService.CallbackAsync(eventHandlerExecutingContext, ex)
                        , retryAction: (total, times) =>
                        {
                            // 输出重试日志
                            _logger.LogWarning("Retrying {times}/{total} times for {EventId}", times, total, eventSource.EventId);
                        });
                    }
                    else
                    {
                        await Executor.ExecuteAsync(eventHandlerExecutingContext, eventHandlerThatShouldRun.Handler!);
                    }

                    // 触发事件处理程序事件
                    _eventPublisher.InvokeEvents(new(eventSource, true));
                }
                catch (Exception ex)
                {
                    // 输出异常日志
                    Log(LogLevel.Error, "Error occurred executing in {EventId}.", new[] { eventSource.EventId }, ex);

                    // 标记异常
                    executionException = new InvalidOperationException(string.Format("Error occurred executing in {0}.", eventSource.EventId), ex);

                    // 捕获 Task 任务异常信息并统计所有异常
                    if (UnobservedTaskException != default)
                    {
                        var args = new UnobservedTaskExceptionEventArgs(
                            ex as AggregateException ?? new AggregateException(ex));

                        UnobservedTaskException.Invoke(this, args);
                    }

                    // 触发事件处理程序事件
                    _eventPublisher.InvokeEvents(new(eventSource, false)
                    {
                        Exception = ex
                    });
                }
                finally
                {
                    // 调用执行后监视器
                    if (Monitor != default)
                    {
                        // 创建执行后上下文
                        var eventHandlerExecutedContext = new EventHandlerExecutedContext(eventSource, properties, eventHandlerThatShouldRun.HandlerMethod, eventSubscribeAttribute)
                        {
                            ExecutedTime = UseUtcTimestamp ? DateTime.UtcNow : DateTime.Now,
                            Exception = executionException
                        };

                        await Monitor.OnExecutedAsync(eventHandlerExecutedContext);
                    }

                    // 判断是否执行完成后调用 GC 回收
                    var nowTime = DateTime.UtcNow;
                    if (eventHandlerThatShouldRun.GCCollect && (LastGCCollectTime == null || (nowTime - LastGCCollectTime.Value).TotalSeconds > GC_COLLECT_INTERVAL_SECONDS))
                    {
                        LastGCCollectTime = nowTime;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }
            }, stoppingToken);
        });
    }

    /// <summary>
    /// 管理事件订阅器动态
    /// </summary>
    /// <param name="subscribeOperateSource"></param>
    private void ManageEventSubscribers(EventSubscribeOperateSource subscribeOperateSource)
    {
        // 获取实际订阅事件 Id
        var eventId = subscribeOperateSource.SubscribeEventId;

        // 确保事件订阅 Id 和传入的特性 EventId 一致
        if (subscribeOperateSource.Attribute != null && subscribeOperateSource.Attribute.EventId != eventId) throw new InvalidOperationException("Ensure that the <eventId> is consistent with the <EventId> attribute of the EventSubscribeAttribute object.");

        // 处理动态新增
        if (subscribeOperateSource.Operate == EventSubscribeOperates.Append)
        {
            var wrapper = new EventHandlerWrapper(eventId)
            {
                Attribute = subscribeOperateSource.Attribute,
                HandlerMethod = subscribeOperateSource.HandlerMethod,
                Handler = subscribeOperateSource.Handler,
                Pattern = CheckIsSetFuzzyMatch(subscribeOperateSource.Attribute?.FuzzyMatch) ? new Regex(eventId, RegexOptions.Singleline) : default,
                GCCollect = CheckIsSetGCCollect(subscribeOperateSource.Attribute?.GCCollect),
                Order = subscribeOperateSource.Attribute?.Order ?? 0
            };

            // 追加到集合中
            var succeeded = _eventHandlers.TryAdd(wrapper, wrapper);

            // 输出日志
            if (succeeded)
            {
                Log(LogLevel.Information, "Subscriber with event ID <{EventId}> was appended successfully.", new[] { eventId });
            }
        }
        // 处理动态删除
        else if (subscribeOperateSource.Operate == EventSubscribeOperates.Remove)
        {
            // 删除所有匹配事件 Id 的处理程序
            foreach (var wrapper in _eventHandlers.Keys)
            {
                if (wrapper.EventId != eventId) continue;

                var succeeded = _eventHandlers.TryRemove(wrapper, out _);
                if (!succeeded) continue;

                // 输出日志
                Log(LogLevel.Warning, "Subscriber<{Name}> with event ID <{EventId}> was remove.", new[] { wrapper.HandlerMethod?.Name, eventId });
            }
        }
    }

    /// <summary>
    /// 检查是否开启模糊匹配事件 Id 功能
    /// </summary>
    /// <param name="fuzzyMatch"></param>
    /// <returns></returns>
    private bool CheckIsSetFuzzyMatch(object fuzzyMatch)
    {
        return fuzzyMatch == null
            ? FuzzyMatch
            : Convert.ToBoolean(fuzzyMatch);
    }

    /// <summary>
    /// 检查是否开启执行完成触发 GC 回收
    /// </summary>
    /// <param name="gcCollect"></param>
    /// <returns></returns>
    private bool CheckIsSetGCCollect(object gcCollect)
    {
        return gcCollect == null
            ? GCCollect
            : Convert.ToBoolean(gcCollect);
    }

    /// <summary>
    /// 记录日志
    /// </summary>
    /// <param name="logLevel">日志级别</param>
    /// <param name="message">消息</param>
    /// <param name="args">参数</param>
    /// <param name="ex">异常</param>
    private void Log(LogLevel logLevel, string message, object[] args = default, Exception ex = default)
    {
        // 如果未启用日志记录则直接返回
        if (!LogEnabled) return;

        if (logLevel == LogLevel.Error)
        {
            _logger.LogError(ex, message, args);
        }
        else
        {
            _logger.Log(logLevel, message, args);
        }
    }
}