// --------------------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur 
// 开源协议：Apache-2.0（https://gitee.com/monksoul/Fur/blob/alpha/LICENSE）
// --------------------------------------------------------------------------------------

using Fur;
using Fur.ConfigurableOptions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 应用服务集合拓展类
    /// </summary>
    public static class AppServiceCollectionExtensions
    {
        /// <summary>
        /// 添加应用配置
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure">服务配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddApp(this IServiceCollection services, Action<IServiceCollection> configure = null)
        {
            App.Services = services;
            services.AddConfigurableOptions<AppSettingsOptions>();

            // 注册 IHttpContextAccessor
            services.AddHttpContextAccessor();

            // 注册MiniProfiler 组件
            if (App.Settings.InjectMiniProfiler == true)
            {
                services.AddMiniProfiler(options =>
                {
                    options.RouteBasePath = AppSettingsOptions.MiniProfilerRouteBasePath;
                }).AddEntityFramework();
            }

            configure?.Invoke(services);
            return services;
        }
    }
}