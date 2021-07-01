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
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///动态接口控制器拓展类
    /// </summary>
    [SuppressSniffer]
    public static class DynamicApiControllerServiceCollectionExtensions
    {
        /// <summary>
        /// 添加动态接口控制器服务
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <returns>Mvc构建器</returns>
        public static IMvcBuilder AddDynamicApiControllers(this IMvcBuilder mvcBuilder)
        {
            // 添加基础服务
            AddBaseServices(mvcBuilder.Services);

            // 配置 Mvc 选项
            mvcBuilder.AddMvcOptions(options =>
            {
                ConfigureMvcBuilder(options);
            });

            return mvcBuilder;
        }

        /// <summary>
        /// 添加外部程序集部件集合
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <param name="assemblies"></param>
        /// <returns>Mvc构建器</returns>
        public static IMvcBuilder AddExternalAssemblyParts(this IMvcBuilder mvcBuilder, IEnumerable<Assembly> assemblies)
        {
            // 载入程序集部件
            if (assemblies != null && assemblies.Any())
            {
                foreach (var assembly in assemblies)
                {
                    mvcBuilder.AddApplicationPart(assembly);
                }
            }

            return mvcBuilder;
        }

        /// <summary>
        /// 添加动态接口控制器服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns>Mvc构建器</returns>
        public static IServiceCollection AddDynamicApiControllers(this IServiceCollection services)
        {
            // 添加基础服务
            AddBaseServices(services);

            // 配置 Mvc 选项
            services.Configure<MvcOptions>(options =>
            {
                ConfigureMvcBuilder(options);
            });

            return services;
        }

        /// <summary>
        /// 添加基础服务
        /// </summary>
        /// <param name="services"></param>
        private static void AddBaseServices(IServiceCollection services)
        {
            var partManager = services.FirstOrDefault(s => s.ServiceType == typeof(ApplicationPartManager))?.ImplementationInstance as ApplicationPartManager
                ?? throw new InvalidOperationException($"`{nameof(AddDynamicApiControllers)}` must be invoked after `{nameof(MvcServiceCollectionExtensions.AddControllers)}`.");

            // 载入模块化/插件程序集部件
            if (App.ExternalAssemblies.Any())
            {
                foreach (var assembly in App.ExternalAssemblies)
                {
                    partManager.ApplicationParts.Add(new AssemblyPart(assembly));
                }
            }

            // 添加控制器特性提供器
            partManager.FeatureProviders.Add(new DynamicApiControllerFeatureProvider());

            // 添加配置
            services.AddConfigurableOptions<DynamicApiControllerSettingsOptions>();
        }

        /// <summary>
        /// 配置 Mvc 构建器
        /// </summary>
        /// <param name="options"></param>
        private static void ConfigureMvcBuilder(MvcOptions options)
        {
            // 添加应用模型转换器
            options.Conventions.Add(new DynamicApiControllerApplicationModelConvention());

            // 添加 Xml 支持
            options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
        }
    }
}