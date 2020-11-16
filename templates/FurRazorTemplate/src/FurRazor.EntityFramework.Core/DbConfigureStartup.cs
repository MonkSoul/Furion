using Fur;
using Microsoft.Extensions.DependencyInjection;

namespace FurRazor.EntityFramework.Core
{
    public class DbConfigureStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
            }, "FurRazor.Database.Migrations");
        }
    }
}