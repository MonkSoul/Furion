using Fur.AppCore;
using Fur.AppCore.Attributes;
using Fur.SwaggerDoc;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Swagger 中间件拓展
    /// </summary>
    [NonInflated]
    public static class SwaggerDocApplicationBuilderExtensions
    {
        /// <summary>
        /// Swagger UI 中间件拓展
        /// </summary>
        /// <param name="app">应用构建器</param>
        /// <returns>新的应用构建器</returns>
        public static IApplicationBuilder AddFurSwaggerUI(this IApplicationBuilder app)
        {
            if (App.AppOptions.SwaggerDocOptions.EnableMiniProfiler)
            {
                //app.UseMiniProfiler();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options => SwaggerDocConfigure.Initialize(options));

            return app;
        }
    }
}