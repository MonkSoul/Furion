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

using Furion.FriendlyException;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Logging;
using System.Reflection;
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
    /// 事件处理程序集合
    /// </summary>
    private readonly ConcurrentDictionary<EventHandlerWrapper, EventHandlerWrapper> _eventHandlers = new();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志对象</param>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="eventSourceStorer">事件源存储器</param>
    /// <param name="eventSubscribers">事件订阅者集合</param>
    /// <param name="useUtcTimestamp">是否使用 Utc 时间</param>
    /// <param name="fuzzyMatch">是否启用模糊匹配事件消息</param>
    /// <param name="gcCollect">是否启用执行完成触发 GC 回收</param>
    /// <param name="logEnabled">是否启用日志记录</param>
    public EventBusHostedService(ILogger<EventBusService> logger
        , IServiceProvider serviceProvider
        , IEventSourceStorer eventSourceStorer
        , IEnumerable<IEventSubscriber> eventSubscribers
        , bool useUtcTimestamp
        , bool fuzzyMatch
        , bool gcCollect
        , bool logEnabled)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
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
        var eventHandlersThatShouldRun = _eventHandlers.Where(t => t.Key.ShouldRun(eventSource.EventId)).OrderByDescending(u => u.Value.Order).Select(u => u.Key);

        // 空订阅
        if (!eventHandlersThatShouldRun.Any())
        {
            Log(LogLevel.Warning, "Subscriber with event ID <{EventId}> was not found.", new[] { eventSource.EventId });

            return;
        }

        // 创建一个任务工厂并保证执行任务都使用当前的计划程序
        var taskFactory = new TaskFactory(System.Threading.Tasks.TaskScheduler.Current);

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
                    if (eventSource.CancellationToken.IsCancellationRequested)
                    {
                        throw new OperationCanceledException();
                    }

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