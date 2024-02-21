// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.DataValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 友好异常服务拓展类
/// </summary>
[SuppressSniffer]
public static class DataValidationServiceCollectionExtensions
{
    /// <summary>
    /// 添加全局数据验证
    /// </summary>
    /// <typeparam name="TValidationMessageTypeProvider">验证类型消息提供器</typeparam>
    /// <param name="mvcBuilder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IMvcBuilder AddDataValidation<TValidationMessageTypeProvider>(this IMvcBuilder mvcBuilder, Action<DataValidationOptions> configure = null)
        where TValidationMessageTypeProvider : class, IValidationMessageTypeProvider
    {
        // 添加全局数据验证
        mvcBuilder.Services.AddDataValidation<TValidationMessageTypeProvider>(configure);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加全局数据验证
    /// </summary>
    /// <typeparam name="TValidationMessageTypeProvider">验证类型消息提供器</typeparam>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddDataValidation<TValidationMessageTypeProvider>(this IServiceCollection services, Action<DataValidationOptions> configure = null)
        where TValidationMessageTypeProvider : class, IValidationMessageTypeProvider
    {
        // 添加全局数据验证
        services.AddDataValidation(configure);

        // 单例注册验证消息提供器
        services.TryAddSingleton<IValidationMessageTypeProvider, TValidationMessageTypeProvider>();

        return services;
    }

    /// <summary>
    /// 添加全局数据验证
    /// </summary>
    /// <param name="mvcBuilder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IMvcBuilder AddDataValidation(this IMvcBuilder mvcBuilder, Action<DataValidationOptions> configure = null)
    {
        mvcBuilder.Services.AddDataValidation(configure);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加全局数据验证
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddDataValidation(this IServiceCollection services, Action<DataValidationOptions> configure = null)
    {
        // 解决服务重复注册问题
        if (services.Any(u => u.ServiceType == typeof(IConfigureOptions<ValidationTypeMessageSettingsOptions>)))
        {
            return services;
        }

        // 添加验证配置文件支持
        services.AddConfigurableOptions<ValidationTypeMessageSettingsOptions>();

        // 载入服务配置选项
        var configureOptions = new DataValidationOptions();
        configure?.Invoke(configureOptions);

        // 判断是否启用全局
        if (configureOptions.GlobalEnabled)
        {
            // 启用了全局验证，则默认关闭原生 ModelStateInvalidFilter 验证
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressMapClientErrors = configureOptions.SuppressMapClientErrors;
                options.SuppressModelStateInvalidFilter = configureOptions.SuppressModelStateInvalidFilter;
            });

            // 添加全局数据验证
            services.AddMvcFilter<DataValidationFilter>(options =>
            {
                // 关闭空引用对象验证
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = configureOptions.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes;
            });

            // 添加全局数据验证（Razor Pages）
            services.AddMvcFilter<DataValidationPageFilter>(options =>
            {
                // 关闭空引用对象验证
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = configureOptions.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes;
            });
        }

        return services;
    }
}