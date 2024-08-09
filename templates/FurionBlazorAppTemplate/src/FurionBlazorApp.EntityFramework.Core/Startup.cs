using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace FurionBlazorApp.EntityFramework.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseAccessor(options =>
        {
            options.AddDbPool<DefaultDbContext>();
        }, "FurionBlazorApp.Database.Migrations");
    }
}
