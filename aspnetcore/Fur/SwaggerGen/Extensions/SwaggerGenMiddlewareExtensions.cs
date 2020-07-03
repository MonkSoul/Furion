using Fur.SwaggerGen.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Fur.SwaggerGen.Extensions
{
    /// <summary>
    /// Swagger 中间件拓展
    /// </summary>
    public static class SwaggerGenMiddlewareExtensions
    {
        #region Swagger UI 中间件拓展 +/* public static IApplicationBuilder AddFurSwaggerUI(this IApplicationBuilder app)

        /// <summary>
        /// Swagger UI 中间件拓展
        /// </summary>
        /// <param name="app">应用构建器</param>
        /// <returns>新的应用构建器</returns>
        public static IApplicationBuilder AddFurSwaggerUI(this IApplicationBuilder app)
        {
            var swaggerOptions = app.ApplicationServices.GetService<IOptionsMonitor<SwaggerOptions>>().CurrentValue;
            if (swaggerOptions.EnableMiniProfiler)
            {
                app.UseMiniProfiler();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options => SwaggerConfigure.Initialize(options));

            return app;
        }

        #endregion Swagger UI 中间件拓展 +/* public static IApplicationBuilder AddFurSwaggerUI(this IApplicationBuilder app)
    }
}