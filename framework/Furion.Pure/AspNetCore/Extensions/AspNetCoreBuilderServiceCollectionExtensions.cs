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
using Furion.AspNetCore;
using Furion.SensitiveDetection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Concurrent;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// ASP.NET Core 服务拓展类
/// </summary>
[SuppressSniffer]
public static class AspNetCoreBuilderServiceCollectionExtensions
{
    /// <summary>
    /// 注册 Mvc 过滤器
    /// </summary>
    /// <typeparam name="TFilter"></typeparam>
    /// <param name="mvcBuilder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IMvcBuilder AddMvcFilter<TFilter>(this IMvcBuilder mvcBuilder, Action<MvcOptions> configure = default)
        where TFilter : IFilterMetadata
    {
        mvcBuilder.Services.AddMvcFilter<TFilter>(configure);

        return mvcBuilder;
    }

    /// <summary>
    /// 注册 Mvc 过滤器
    /// </summary>
    /// <typeparam name="TFilter"></typeparam>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddMvcFilter<TFilter>(this IServiceCollection services, Action<MvcOptions> configure = default)
        where TFilter : IFilterMetadata
    {
        // 非 Web 环境跳过注册
        if (App.WebHostEnvironment == default) return services;

        services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add<TFilter>();

            // 其他额外配置
            configure?.Invoke(options);
        });

        return services;
    }

    /// <summary>
    /// 注册 Mvc 过滤器
    /// </summary>
    /// <param name="services"></param>
    /// <param name="filter"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddMvcFilter(this IServiceCollection services, IFilterMetadata filter, Action<MvcOptions> configure = default)
    {
        // 非 Web 环境跳过注册
        if (App.WebHostEnvironment == default) return services;

        services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add(filter);

            // 其他额外配置
            configure?.Invoke(options);
        });

        return services;
    }

    /// <summary>
    /// 添加 [FromConvert] 模型绑定
    /// </summary>
    /// <param name="mvcBuilder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IMvcBuilder AddFromConvertBinding(this IMvcBuilder mvcBuilder, Action<ConcurrentDictionary<Type, Type>> configure = default)
    {
        mvcBuilder.Services.AddFromConvertBinding(configure);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加 [FromConvert] 模型绑定
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddFromConvertBinding(this IServiceCollection services, Action<ConcurrentDictionary<Type, Type>> configure = default)
    {
        // 非 Web 环境跳过注册
        if (App.WebHostEnvironment == default) return services;

        // 定义模型绑定转换器集合
        var modelBinderConverts = new ConcurrentDictionary<Type, Type>();
        modelBinderConverts.TryAdd(typeof(DateTime), typeof(DateTimeModelConvertBinder));
        modelBinderConverts.TryAdd(typeof(DateTimeOffset), typeof(DateTimeOffsetModelConvertBinder));

        // 配置 Mvc 选项
        services.Configure<MvcOptions>(options =>
        {
            // 添加模型绑定器
            options.ModelBinderProviders.Insert(0, new FromConvertBinderProvider(modelBinderConverts));
        });

        // 调用外部方法
        configure?.Invoke(modelBinderConverts);

        return services;
    }
}