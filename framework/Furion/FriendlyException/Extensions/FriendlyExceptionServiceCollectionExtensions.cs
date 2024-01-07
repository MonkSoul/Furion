// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.FriendlyException;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 友好异常服务拓展类
/// </summary>
[SuppressSniffer]
public static class FriendlyExceptionServiceCollectionExtensions
{
    /// <summary>
    /// 添加友好异常服务拓展服务
    /// </summary>
    /// <typeparam name="TErrorCodeTypeProvider">异常错误码提供器</typeparam>
    /// <param name="mvcBuilder">Mvc构建器</param>
    /// <param name="configure">是否启用全局异常过滤器</param>
    /// <returns></returns>
    public static IMvcBuilder AddFriendlyException<TErrorCodeTypeProvider>(this IMvcBuilder mvcBuilder, Action<FriendlyExceptionOptions> configure = null)
        where TErrorCodeTypeProvider : class, IErrorCodeTypeProvider
    {
        mvcBuilder.Services.AddFriendlyException<TErrorCodeTypeProvider>(configure);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加友好异常服务拓展服务
    /// </summary>
    /// <typeparam name="TErrorCodeTypeProvider">异常错误码提供器</typeparam>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddFriendlyException<TErrorCodeTypeProvider>(this IServiceCollection services, Action<FriendlyExceptionOptions> configure = null)
        where TErrorCodeTypeProvider : class, IErrorCodeTypeProvider
    {
        // 添加全局异常过滤器
        services.AddFriendlyException(configure);

        // 单例注册异常状态码提供器
        services.AddSingleton<IErrorCodeTypeProvider, TErrorCodeTypeProvider>();

        return services;
    }

    /// <summary>
    /// 添加友好异常服务拓展服务
    /// </summary>
    /// <param name="mvcBuilder">Mvc构建器</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IMvcBuilder AddFriendlyException(this IMvcBuilder mvcBuilder, Action<FriendlyExceptionOptions> configure = null)
    {
        mvcBuilder.Services.AddFriendlyException(configure);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加友好异常服务拓展服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddFriendlyException(this IServiceCollection services, Action<FriendlyExceptionOptions> configure = null)
    {
        // 添加友好异常配置文件支持
        services.AddConfigurableOptions<FriendlyExceptionSettingsOptions>();

        // 添加异常配置文件支持
        services.AddConfigurableOptions<ErrorCodeMessageSettingsOptions>();

        // 载入服务配置选项
        var configureOptions = new FriendlyExceptionOptions();
        configure?.Invoke(configureOptions);

        // 添加全局异常过滤器
        if (configureOptions.GlobalEnabled)
            services.AddMvcFilter<FriendlyExceptionFilter>();

        return services;
    }
}