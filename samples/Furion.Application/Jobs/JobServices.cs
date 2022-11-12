using Furion.Scheduler;

namespace Furion.Application;

[ApiDescriptionSettings("Job")]
public class JobServices : IDynamicApiController
{
    private readonly ISchedulerFactory _schedulerFactory;
    public JobServices(ISchedulerFactory schedulerFactory)
    {
        _schedulerFactory = schedulerFactory;
    }
    public void StartJob(string jobId)
    {
        var jobScheduler = _schedulerFactory.GetJob(jobId);
        jobScheduler?.Start();
    }

    public void PauseJob(string jobId)
    {
        var jobScheduler = _schedulerFactory.GetJob(jobId);
        jobScheduler?.Pause();
    }

    public void AddJob([FromQuery] string jobId)
    {
        if (!string.IsNullOrWhiteSpace(jobId))
        {
            _schedulerFactory.AddJob<TestJob>(jobId, Trigger.Period(10000));
        }
        else
        {
            _schedulerFactory.AddJob<TestJob>(Trigger.Period(10000));
        }
    }
}