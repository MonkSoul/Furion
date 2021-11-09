using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace FurionBlazor.EntityFramework.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseAccessor(options =>
        {
            options.AddDbPool<DefaultDbContext>();
        }, "FurionBlazor.Database.Migrations");
    }
}
