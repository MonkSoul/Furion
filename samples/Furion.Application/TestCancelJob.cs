using Furion.Schedule;
using Microsoft.Extensions.Logging;

namespace Furion.Application;

//[Period(10000)]
public class TestCancelJob : IJob
{
    private readonly ILogger<TestCancelJob> _logger;
    public TestCancelJob(ILogger<TestCancelJob> logger)
    {
        _logger = logger;
    }

    public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        _logger.LogWarning($"{context}");

        await Task.Delay(10000, stoppingToken);

        _logger.LogWarning("超时作业已执行完成");

        await Task.CompletedTask;
    }
}