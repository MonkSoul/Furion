using Fur.AppBasic;
using Microsoft.AspNetCore.Builder;

namespace Fur.SwaggerDoc.Extensions.Middlewares
{
    /// <summary>
    /// Swagger 中间件拓展
    /// </summary>
    public static class SwaggerDocMiddlewareExtensions
    {
        /// <summary>
        /// Swagger UI 中间件拓展
        /// </summary>
        /// <param name="app">应用构建器</param>
        /// <returns>新的应用构建器</returns>
        public static IApplicationBuilder AddFurSwaggerUI(this IApplicationBuilder app)
        {
            if (App.FurOptions.FurSwaggerDocOptions.EnableMiniProfiler)
            {
                //app.UseMiniProfiler();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options => SwaggerDocConfigure.Initialize(options));

            return app;
        }
    }
}