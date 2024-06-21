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

    public Task<IEnumerable<SchedulerBuilder>> PreloadAsync(CancellationToken stoppingToken)
    {
        return Task.FromResult(Enumerable.Empty<SchedulerBuilder>());
    }

    public Task<SchedulerBuilder> OnLoadingAsync(SchedulerBuilder builder, CancellationToken stoppingToken)
    {
        return Task.FromResult(builder);
    }

    public Task OnChangedAsync(PersistenceContext context)
    {
        return Task.CompletedTask;
    }

    public Task OnTriggerChangedAsync(PersistenceTriggerContext context)
    {
        return Task.CompletedTask;
    }

    public Task OnExecutionRecordAsync(PersistenceExecutionRecordContext context)
    {
        Console.WriteLine(context.ToString());
        return Task.CompletedTask;
    }
}