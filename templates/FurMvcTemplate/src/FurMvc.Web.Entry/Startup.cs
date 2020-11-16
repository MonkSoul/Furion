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
            // 迁移至 FurMvc.Web.Core/WebStartup.cs 中
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 迁移至 FurMvc.Web.Core/WebStartup.cs 中
        }
    }
}