using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace FurionRazorApi.EntityFramework.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseAccessor(options =>
        {
            options.AddDbPool<DefaultDbContext>();
        }, "FurionRazorApi.Database.Migrations");
    }
}
