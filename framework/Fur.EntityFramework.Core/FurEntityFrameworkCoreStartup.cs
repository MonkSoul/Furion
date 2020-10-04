using Fur.DatabaseAccessor;
using Microsoft.Extensions.DependencyInjection;

namespace Fur.EntityFramework.Core
{
    [Startup(600)]
    public sealed class FurEntityFrameworkCoreStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // 配置数据库上下文，支持N个数据库
            services.AddDatabaseAccessor(options =>
            {
                options.AddDbPool<FurDbContext>(DbProvider.Sqlite);
            });
        }
    }
}