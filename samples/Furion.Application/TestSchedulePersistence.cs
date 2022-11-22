using Furion.Schedule;
using Microsoft.Extensions.Logging;

namespace Furion.Application;

public class TestSchedulerPersistence : IJobPersistence
{
    private readonly ILogger<TestSchedulerPersistence> _logger;

    public TestSchedulerPersistence(ILogger<TestSchedulerPersistence> logger)
    {
        _logger = logger;
    }

    public SchedulerBuilder Preload(SchedulerBuilder builder)
    {
        builder.GetJobBuilder().SetDescription("测试作业描述");
        builder.GetTriggerBuilders().ForEach(b =>
        {
            b.SetDescription("测试触发器描述");
        });

        _logger.LogInformation(builder.ConvertToJSON());

        return builder;
    }

    public void OnChanged(PersistenceContext context)
    {
        _logger.LogInformation("{behavior}：{jobId} {updatedTime}", context.Behavior, context.JobId, context.JobDetail.UpdatedTime);
    }

    public void OnTriggerChanged(PersistenceTriggerContext context)
    {
        _logger.LogInformation("{behavior}：{jobId} {triggerId} {status} {updatedTime}", context.Behavior, context.JobId, context.TriggerId, context.Trigger.Status, context.Trigger.UpdatedTime);
    }
}