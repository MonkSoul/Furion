using Fur;
using Microsoft.Extensions.DependencyInjection;

namespace FurApi.EntityFramework.Core
{
    public class DbConfigureStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
            }, "FurApi.Database.Migrations");
        }
    }
}