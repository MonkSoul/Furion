using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace FurionApp.EntityFramework.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseAccessor(options =>
        {
            options.AddDbPool<DefaultDbContext>();
        }, "FurionApp.Database.Migrations");
    }
}
