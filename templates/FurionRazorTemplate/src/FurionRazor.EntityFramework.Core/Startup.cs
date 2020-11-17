using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace FurionRazor.EntityFramework.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
            }, "FurionRazor.Database.Migrations");
        }
    }
}