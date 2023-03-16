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

using Furion.DataValidation;
using Microsoft.AspNetCore.Mvc;

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
        services.AddSingleton<IValidationMessageTypeProvider, TValidationMessageTypeProvider>();

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