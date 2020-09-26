using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fur.EntityFramework.Core;
using Fur.UnifyResult;

namespace Fur.Web.MvcSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApp(options =>
            {
                options.AddControllersWithViews()
                    // .AddDynamicApiControllers()
                    .AddDataValidation()
                    .AddFriendlyException()
                    .AddUnifyResult<RESTfulResult, RESTfulResultProvider>();


                options.AddControllers();//MVC
                options.AddRazorPages();//Razor Page
                //options.AddControllersWithViews();
                options.AddDatabaseAccessor(dboptions =>
                {
                    dboptions.AddSqlitePool<FurDbContext>();//默认Sqlite 数据库
                  //  dboptions.AddSqlServerPool<FurDbContext>();//默认SqlServer 数据库
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseApp(options =>
            //{
            //    options.UseSpecificationDocuments();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapAreaControllerRoute(
                    name: "areas", "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
