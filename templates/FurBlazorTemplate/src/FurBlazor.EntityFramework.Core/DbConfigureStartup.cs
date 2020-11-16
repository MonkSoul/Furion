using Fur;
using Microsoft.Extensions.DependencyInjection;

namespace FurBlazor.EntityFramework.Core
{
    public class DbConfigureStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
            }, "FurBlazor.Database.Migrations");
        }
    }
}