using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace FurionMvc.EntityFramework.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
            }, "FurionMvc.Database.Migrations");
        }
    }
}