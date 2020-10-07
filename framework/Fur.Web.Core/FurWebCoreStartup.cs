using Fur.UnifyResult;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fur.Web.Core
{
    [AppStartup(800)]
    public sealed class FurWebCoreStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsAccessor();

            services.AddSpecificationDocuments();
            services.AddControllers()
                       .AddDynamicApiControllers()
                       .AddDataValidation()
                       .AddFriendlyException()
                       .AddUnifyResult<RESTfulResult, RESTfulResultProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCorsAccessor();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSpecificationDocuments();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}