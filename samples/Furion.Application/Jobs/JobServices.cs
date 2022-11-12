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
        var jobScheduler = _schedulerFactory.GetJobScheduler(jobId);
        jobScheduler?.Start();
    }

    public void PauseJob(string jobId)
    {
        var jobScheduler = _schedulerFactory.GetJobScheduler(jobId);
        jobScheduler?.Pause();
    }

    public void AddJob(string jobId)
    {
        _schedulerFactory.AddJob<TestJob>(jobId, Trigger.Period(10000));
    }
}