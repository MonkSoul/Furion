using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace FurionApi.EntityFramework.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
            }, "FurionApi.Database.Migrations");
        }
    }
}