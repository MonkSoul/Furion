using Fur.Core;
using Fur.EntityFramework.Core;
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
            });

            services.AddControllers()
                .AddDynamicApiControllers()
                .AddFriendlyException()
                .AddDataValidation();

            services.AddDatabaseAccessor(options =>
            {
                options.AddAppDbContextPool<FurDbContext>(Configuration.GetConnectionString("DbConnectionString"));
                options.AddAppDbContextPool<FurDbContext2, FurDbContextLocator2>(Configuration.GetConnectionString("DbConnectionString2"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

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