using Microsoft.Extensions.DependencyInjection;

namespace Furion.EntityFramework.Core;

[AppStartup(600)]
public sealed class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseAccessor(options =>
        {
            options.AddDbPool<DefaultDbContext>();
        });
    }
}
