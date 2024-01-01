using Furion.Schedule;
using Microsoft.Extensions.Logging;
using System.Text.Json;

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

    public SchedulerBuilder OnLoading(SchedulerBuilder builder)
    {
        return builder;
    }

    public void OnChanged(PersistenceContext context)
    {
    }

    public void OnTriggerChanged(PersistenceTriggerContext context)
    {
    }

    public void OnExecutionRecord(TriggerTimeline timeline)
    {
        Console.WriteLine(JsonSerializer.Serialize(timeline));
    }
}