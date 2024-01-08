using Furion.Schedule;
using Microsoft.Extensions.DependencyInjection;

namespace Furion.Application;

public class JobFactory : IJobFactory
{
    public IJob CreateJob(IServiceProvider serviceProvider, JobFactoryContext context)
    {
        return ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, context.JobType) as IJob;

        // 如果通过 services.AddSingleton<YourJob>(); 或 serivces.AddScoped<YourJob>();
        //return serviceProvider.GetRequiredService(context.JobType) as IJob;
    }
}