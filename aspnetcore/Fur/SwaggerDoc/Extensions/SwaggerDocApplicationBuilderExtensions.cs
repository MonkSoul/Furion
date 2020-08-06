using Fur.SwaggerDoc;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Swagger 中间件拓展
    /// </summary>

    public static class SwaggerDocApplicationBuilderExtensions
    {
        /// <summary>
        /// Swagger UI 中间件拓展
        /// </summary>
        /// <param name="app">应用构建器</param>
        /// <returns>新的应用构建器</returns>
        public static IApplicationBuilder AddFurSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger(options =>
            {
                //options.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(options => SwaggerDocBuilder.Initialize(options));

            return app;
        }
    }
}