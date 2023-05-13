// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Furion;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

        // 添加动态 WebAPI 运行时感知服务
        services.AddSingleton<MvcActionDescriptorChangeProvider>()
                .AddSingleton<IActionDescriptorChangeProvider>(provider => provider.GetRequiredService<MvcActionDescriptorChangeProvider>());
        services.AddSingleton<IDynamicApiRuntimeChangeProvider, DynamicApiRuntimeChangeProvider>();

        // 添加配置
        services.AddConfigurableOptions<DynamicApiControllerSettingsOptions>();

        // 配置 Mvc 选项
        services.Configure<MvcOptions>(options =>
        {
            // 添加应用模型转换器
            options.Conventions.Add(new DynamicApiControllerApplicationModelConvention(services));

            // 添加 text/plain 请求 Body 参数支持
            options.InputFormatters.Add(new TextPlainMediaTypeFormatter());
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