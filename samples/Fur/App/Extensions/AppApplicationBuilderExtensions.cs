using Fur;
using Fur.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 应用中间件拓展类（由框架内部调用）
    /// </summary>
    [SkipScan]
    public static class AppApplicationBuilderExtensions
    {
        /// <summary>
        /// 注入基础中间件（带Swagger）
        /// </summary>
        /// <param name="app"></param>
        /// <param name="routePrefix"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseInject(this IApplicationBuilder app, string routePrefix = default)
        {
            app.UseSpecificationDocuments(routePrefix);
            return app;
        }

        /// <summary>
        /// 注入基础中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseInjectBase(this IApplicationBuilder app)
        {
            return app;
        }

        /// <summary>
        /// 添加应用中间件
        /// </summary>
        /// <param name="app">应用构建器</param>
        /// <param name="configure">应用配置</param>
        /// <returns>应用构建器</returns>
        internal static IApplicationBuilder UseApp(this IApplicationBuilder app, Action<IApplicationBuilder> configure = null)
        {
            // 启用 MiniProfiler组件
            if (App.Settings.InjectMiniProfiler == true)
            {
                app.UseMiniProfiler();
            }

            // 调用自定义服务
            configure?.Invoke(app);
            return app;
        }
    }
}