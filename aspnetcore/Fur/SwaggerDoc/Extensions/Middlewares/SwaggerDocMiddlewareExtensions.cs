using Fur.ApplicationBase;
using Microsoft.AspNetCore.Builder;

namespace Fur.SwaggerDoc.Extensions.Middlewares
{
    /// <summary>
    /// Swagger 中间件拓展
    /// </summary>
    public static class SwaggerDocMiddlewareExtensions
    {
        #region Swagger UI 中间件拓展 + public static IApplicationBuilder AddFurSwaggerUI(this IApplicationBuilder app)

        /// <summary>
        /// Swagger UI 中间件拓展
        /// </summary>
        /// <param name="app">应用构建器</param>
        /// <returns>新的应用构建器</returns>
        public static IApplicationBuilder AddFurSwaggerUI(this IApplicationBuilder app)
        {
            if (ApplicationCore.GlobalSettings.SwaggerDocOptions.EnableMiniProfiler)
            {
                //app.UseMiniProfiler();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options => SwaggerDocConfigure.Initialize(options));

            return app;
        }

        #endregion Swagger UI 中间件拓展 + public static IApplicationBuilder AddFurSwaggerUI(this IApplicationBuilder app)
    }
}