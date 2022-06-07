using Microsoft.Extensions.DependencyInjection;

namespace Furion.Application;

[AppStartup(900)]
public sealed class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddConfigurableOptions<AppInfoOptions>();
    }
}