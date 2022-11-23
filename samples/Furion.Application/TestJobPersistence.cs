using Furion.Schedule;
using Microsoft.Extensions.Logging;

namespace Furion.Application;

public class TestJobPersistence : IJobPersistence
{
    private readonly ILogger<TestJobPersistence> _logger;

    public TestJobPersistence(ILogger<TestJobPersistence> logger)
    {
        _logger = logger;
    }

    public IEnumerable<SchedulerBuilder> Preload()
    {
        return Array.Empty<SchedulerBuilder>();
    }

    public SchedulerBuilder OnLoaded(string jobId, SchedulerBuilder builder)
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