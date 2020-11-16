using Fur;
using Microsoft.Extensions.DependencyInjection;

namespace FurApp.EntityFramework.Core
{
    public class DbConfigureStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
            }, "FurApp.Database.Migrations");
        }
    }
}