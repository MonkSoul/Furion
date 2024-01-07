// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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