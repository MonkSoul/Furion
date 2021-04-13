using Furion.DependencyInjection;
using Furion.ViewEngine;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 视图引擎服务拓展类
    /// </summary>
    [SkipScan]
    public static class ViewEngineServiceCollectionExtensions
    {
        /// <summary>
        /// 添加视图引擎
        /// </summary>
        /// <param name="services"></param>
        /// <param name="templateSaveDir"></param>
        /// <returns></returns>
        public static IServiceCollection AddViewEngine(this IServiceCollection services, string templateSaveDir = default)
        {
            if (!string.IsNullOrWhiteSpace(templateSaveDir)) Penetrates.TemplateSaveDir = templateSaveDir;

            services.AddTransient<IViewEngine, ViewEngine>();
            return services;
        }
    }
}