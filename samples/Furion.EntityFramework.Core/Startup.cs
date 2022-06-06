using Microsoft.Extensions.DependencyInjection;

namespace Furion.EntityFramework.Core;

[AppStartup(600)]
public sealed class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseAccessor(options =>
        {
            options.AddDbPool<DefaultDbContext>();
        }, "Furion.Database.Migrations");   // 设置 CodeFirst 生成迁移文件项目名
    }
}