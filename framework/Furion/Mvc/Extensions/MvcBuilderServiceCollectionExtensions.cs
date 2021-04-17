using Furion;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Mvc 服务拓展类
    /// </summary>
    [SkipScan]
    public static class MvcBuilderServiceCollectionExtensions
    {
        /// <summary>
        /// 注册 Mvc 过滤器
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="mvcBuilder"></param>
        /// <param name="extraConfigure"></param>
        /// <returns></returns>
        public static IMvcBuilder AddMvcFilter<TFilter>(this IMvcBuilder mvcBuilder, Action<MvcOptions> extraConfigure = default)
            where TFilter : IFilterMetadata
        {
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add<TFilter>();

                // 其他额外配置
                extraConfigure?.Invoke(options);
            });

            return mvcBuilder;
        }

        /// <summary>
        /// 注册 Mvc 过滤器
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="services"></param>
        /// <param name="extraConfigure"></param>
        /// <returns></returns>
        public static IServiceCollection AddMvcFilter<TFilter>(this IServiceCollection services, Action<MvcOptions> extraConfigure = default)
            where TFilter : IFilterMetadata
        {
            // 只有 Web 环境才添加过滤器
            if (App.WebHostEnvironment == null) return services;

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<TFilter>();

                // 其他额外配置
                extraConfigure?.Invoke(options);
            });

            return services;
        }
    }
}