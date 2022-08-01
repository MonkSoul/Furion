// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Furion;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 动态接口控制器拓展类
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
        mvcBuilder.Services.AddDynamicApiControllers();

        return mvcBuilder;
    }

    /// <summary>
    /// 添加动态接口控制器服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDynamicApiControllers(this IServiceCollection services)
    {
        var partManager = services.FirstOrDefault(s => s.ServiceType == typeof(ApplicationPartManager))?.ImplementationInstance as ApplicationPartManager
            ?? throw new InvalidOperationException($"`{nameof(AddDynamicApiControllers)}` must be invoked after `{nameof(MvcServiceCollectionExtensions.AddControllers)}`.");

        // 解决项目类型为 <Project Sdk="Microsoft.NET.Sdk"> 不能加载 API 问题，默认支持 <Project Sdk="Microsoft.NET.Sdk.Web">
        foreach (var assembly in App.Assemblies)
        {
            if (partManager.ApplicationParts.Any(u => u.Name != assembly.GetName().Name))
            {
                partManager.ApplicationParts.Add(new AssemblyPart(assembly));
            }
        }

        // 载入模块化/插件程序集部件
        if (App.ExternalAssemblies.Any())
        {
            foreach (var assembly in App.ExternalAssemblies)
            {
                if (partManager.ApplicationParts.Any(u => u.Name != assembly.GetName().Name))
                {
                    partManager.ApplicationParts.Add(new AssemblyPart(assembly));
                }
            }
        }

        // 添加控制器特性提供器
        partManager.FeatureProviders.Add(new DynamicApiControllerFeatureProvider());

        // 添加配置
        services.AddConfigurableOptions<DynamicApiControllerSettingsOptions>();

        // 配置 Mvc 选项
        services.Configure<MvcOptions>(options =>
        {
            // 添加应用模型转换器
            options.Conventions.Add(new DynamicApiControllerApplicationModelConvention());
        });

        return services;
    }

    /// <summary>
    /// 添加外部程序集部件集合
    /// </summary>
    /// <param name="mvcBuilder">Mvc构建器</param>
    /// <param name="assemblies"></param>
    /// <returns>Mvc构建器</returns>
    public static IMvcBuilder AddExternalAssemblyParts(this IMvcBuilder mvcBuilder, IEnumerable<Assembly> assemblies)
    {
        var partManager = mvcBuilder.PartManager;
        // 载入程序集部件
        if (partManager != null && assemblies != null && assemblies.Any())
        {
            foreach (var assembly in assemblies)
            {
                if (partManager.ApplicationParts.Any(u => u.Name != assembly.GetName().Name))
                {
                    mvcBuilder.AddApplicationPart(assembly);
                }
            }
        }

        return mvcBuilder;
    }
}