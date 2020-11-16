using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FurMvc.Web.Entry
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // 代码迁移至 FurMvc.Web.Core/WebConfigureStartup.cs
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 代码迁移至 FurMvc.Web.Core/WebConfigureStartup.cs
        }
    }
}