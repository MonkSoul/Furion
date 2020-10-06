using Fur.DatabaseAccessor;
using Microsoft.Extensions.DependencyInjection;

namespace Fur.EntityFramework.Core
{
    [Startup(600)]
    public sealed class FurEntityFrameworkCoreStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
                options.AddDbPool<FurDbContext>(DbProvider.Sqlite);
            });
        }
    }
}