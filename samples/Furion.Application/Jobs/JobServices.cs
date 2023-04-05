using Furion.Schedule;

namespace Furion.Application;

/// <summary>
/// 定时任务
/// </summary>
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
        _ = _schedulerFactory.TryGetJob(jobId, out var scheduler);
        scheduler?.Start();
    }

    public void PauseJob(string jobId)
    {
        _ = _schedulerFactory.TryGetJob(jobId, out var scheduler);
        scheduler?.Pause();
    }

    public void AddJob([FromQuery] string jobId)
    {
        if (!string.IsNullOrWhiteSpace(jobId))
        {
            _schedulerFactory.TryAddJob<TestJob>(jobId, new[] { Triggers.Period(10000) }, out _);
        }
        else
        {
            _schedulerFactory.TryAddJob<TestJob>(new[] { Triggers.Period(10000) }, out _);
        }
    }

    public void RunJob([FromQuery] string jobId)
    {
        _schedulerFactory.RunJob(jobId);
    }

    public void RemoveJob(string jobId)
    {
        _ = _schedulerFactory.TryRemoveJob(jobId, out var _);
    }
}