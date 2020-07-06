using Autofac;
using Fur.AttachController.Extensions;
using Fur.DependencyInjection;
using Fur.EntityFramework.Core.Extensions;
using Fur.Mvc.Filters;
using Fur.ObjectMapper.Extensions;
using Fur.SwaggerGen.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fur.Web.Host
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            Environment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers().AddFurAttachControllers(Configuration);
            services.AddFurSwaggerGen(Configuration);
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<ExceptionAsyncFilter>();
                options.Filters.Add<ValidateModelAsyncFilter>();
            });
            services.AddFurObjectMapper();
            services.AddFurDbContextPool(Environment, Configuration);


        }

        public void ConfigureContainer(ContainerBuilder builder) => Injection.Initialize(builder);

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.AddFurSwaggerUI();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}