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
        /// 添加事务引擎
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddViewEngine(this IServiceCollection services)
        {
            services.AddTransient<IViewEngine, ViewEngine>();
            return services;
        }
    }
}