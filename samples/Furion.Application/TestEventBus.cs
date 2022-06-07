using Furion.EventBus;
using Furion.Extensitions.EventBus;
using Microsoft.Extensions.Logging;

namespace Furion.Application;

public class TestEventBus : IDynamicApiController
{
    private readonly IEventPublisher _eventPublisher;
    public TestEventBus(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    // 发布 ToDo:Create 消息
    public async Task CreateDoTo()
    {
        await _eventPublisher.PublishAsync("ToDo:Create");
    }

    // 发布枚举消息
    public async Task CreateEnum()
    {
        await _eventPublisher.PublishAsync(ValidationTypes.Numeric);
    }
}

// 实现 IEventSubscriber 接口
public class ToDoEventSubscriber : IEventSubscriber, ISingleton
{
    private readonly ILogger<ToDoEventSubscriber> _logger;
    public ToDoEventSubscriber(ILogger<ToDoEventSubscriber> logger)
    {
        _logger = logger;
    }

    [EventSubscribe("ToDo:Create")]
    public async Task CreateToDo(EventHandlerExecutingContext context)
    {
        var todo = context.Source;
        _logger.LogInformation("创建一个 ToDo：{Name}", todo.Payload);
        await Task.CompletedTask;
    }

    [EventSubscribe(ValidationTypes.Numeric)]
    public async Task CreateEnum(EventHandlerExecutingContext context)
    {
        var eventEnum = context.Source.EventId.ParseToEnum();
        await Task.CompletedTask;
    }
}
