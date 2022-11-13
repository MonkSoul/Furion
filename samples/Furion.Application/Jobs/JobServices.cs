using Furion.Schedule;

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
        var scheduler = _schedulerFactory.GetJob(jobId);
        scheduler?.Start();
    }

    public void PauseJob(string jobId)
    {
        var scheduler = _schedulerFactory.GetJob(jobId);
        scheduler?.Pause();
    }

    public void AddJob([FromQuery] string jobId)
    {
        if (!string.IsNullOrWhiteSpace(jobId))
        {
            _schedulerFactory.AddJob<TestJob>(jobId, Triggers.Period(10000));
        }
        else
        {
            _schedulerFactory.AddJob<TestJob>(Triggers.Period(10000));
        }
    }
}