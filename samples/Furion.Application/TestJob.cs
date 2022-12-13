using Furion.Schedule;
using Microsoft.Extensions.Logging;

namespace Furion.Application;

//[Period(10000)]
public class TestJob : IJob
{
    private readonly ILogger<TestJob> _logger;
    public TestJob(ILogger<TestJob> logger)
    {
        _logger = logger;
    }

    public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        _logger.LogWarning($"{context}");
        await Task.CompletedTask;
    }
}