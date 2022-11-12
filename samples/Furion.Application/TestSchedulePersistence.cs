using Furion.Schedule;
using Microsoft.Extensions.Logging;

namespace Furion.Application;

public class TestSchedulerPersistence : ISchedulerPersistence
{
    private readonly ILogger<TestSchedulerPersistence> _logger;

    public TestSchedulerPersistence(ILogger<TestSchedulerPersistence> logger)
    {
        _logger = logger;
    }

    public void Preload(SchedulerBuilder builder)
    {
        builder.JobBuilder.SetDescription("测试作业描述");
        builder.TriggerBuilders.ForEach(b =>
        {
            b.SetDescription("测试触发器描述");
        });
    }

    public void Persist(PersistenceContext context)
    {
        _logger.LogInformation("{jobId} {triggerId} {status}", context.JobId, context.TriggerId, context.Trigger.Status);
    }
}