using Fur.EntityFramework.Core;
using Fur.UnifyResult;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fur.Web.Entry
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
            services.AddApp(options =>
            {
                options.AddSpecificationDocuments();
                options.AddControllers()
                           .AddDynamicApiControllers()
                           .AddDataValidation()
                           .AddFriendlyException()
                           .AddUnifyResult<RESTfulResult, RESTfulResultProvider>();

                // 配置数据库上下文，支持N个数据库
                options.AddDatabaseAccessor(options =>
                {
                    options.AddSqlitePool<FurDbContext>();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseApp(options =>
            {
                options.UseSpecificationDocuments();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}