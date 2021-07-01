// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

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
    [SuppressSniffer]
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
            // 非 Web 环境跳过注册
            if (App.WebHostEnvironment == default) return mvcBuilder;

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
            // 非 Web 环境跳过注册
            if (App.WebHostEnvironment == default) return services;

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