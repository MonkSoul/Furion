using Fur.ApplicationBase;
using Fur.SwaggerGen;
using Microsoft.AspNetCore.Builder;

namespace Fur.SwaggerGen.Extensions.Middlewares
{
    /// <summary>
    /// Swagger 中间件拓展
    /// </summary>
    public static class SwaggerGenMiddlewareExtensions
    {
        #region Swagger UI 中间件拓展 + public static IApplicationBuilder AddFurSwaggerUI(this IApplicationBuilder app)

        /// <summary>
        /// Swagger UI 中间件拓展
        /// </summary>
        /// <param name="app">应用构建器</param>
        /// <returns>新的应用构建器</returns>
        public static IApplicationBuilder AddFurSwaggerUI(this IApplicationBuilder app)
        {
            if (ApplicationCore.GlobalSettings.SwaggerOptions.EnableMiniProfiler)
            {
                //app.UseMiniProfiler();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options => SwaggerConfigure.Initialize(options));

            return app;
        }

        #endregion Swagger UI 中间件拓展 + public static IApplicationBuilder AddFurSwaggerUI(this IApplicationBuilder app)
    }
}