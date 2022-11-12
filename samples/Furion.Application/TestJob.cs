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

    public async Task ExecuteAsync(JobHandlerExecutingContext context, CancellationToken cancellationToken)
    {
        _logger.LogWarning("{Description} {JobId} {JobTrigger}", context.JobDetail.Description, context.JobId, context.Trigger);
        await Task.CompletedTask;
    }
}
