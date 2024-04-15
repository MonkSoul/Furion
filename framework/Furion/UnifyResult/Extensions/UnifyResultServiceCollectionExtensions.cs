// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Furion.UnifyResult;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 规范化结果服务拓展
/// </summary>
[SuppressSniffer]
public static class UnifyResultServiceCollectionExtensions
{
    /// <summary>
    /// 添加规范化结果服务
    /// </summary>
    /// <param name="mvcBuilder"></param>
    /// <returns></returns>
    public static IMvcBuilder AddUnifyResult(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.Services.AddUnifyResult<RESTfulResultProvider>();

        return mvcBuilder;
    }

    /// <summary>
    /// 添加规范化结果服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddUnifyResult(this IServiceCollection services)
    {
        services.AddUnifyResult<RESTfulResultProvider>();

        return services;
    }

    /// <summary>
    /// 添加规范化结果服务
    /// </summary>
    /// <typeparam name="TUnifyResultProvider"></typeparam>
    /// <param name="mvcBuilder"></param>
    /// <returns></returns>
    public static IMvcBuilder AddUnifyResult<TUnifyResultProvider>(this IMvcBuilder mvcBuilder)
        where TUnifyResultProvider : class, IUnifyResultProvider
    {
        mvcBuilder.Services.AddUnifyResult<TUnifyResultProvider>();

        return mvcBuilder;
    }

    /// <summary>
    /// 添加规范化结果服务
    /// </summary>
    /// <typeparam name="TUnifyResultProvider"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddUnifyResult<TUnifyResultProvider>(this IServiceCollection services)
        where TUnifyResultProvider : class, IUnifyResultProvider
    {
        // 解决服务重复注册问题
        if (services.Any(u => u.ServiceType == typeof(IConfigureOptions<UnifyResultSettingsOptions>)))
        {
            return services;
        }

        // 添加配置
        services.AddConfigurableOptions<UnifyResultSettingsOptions>();

        // 是否启用规范化结果
        UnifyContext.EnabledUnifyHandler = true;

        // 添加规范化提供器
        services.AddUnifyProvider<TUnifyResultProvider>(string.Empty);

        // 添加成功规范化结果筛选器
        services.AddMvcFilter<SucceededUnifyResultFilter>();

        return services;
    }

    /// <summary>
    /// 替换默认的规范化结果
    /// </summary>
    /// <typeparam name="TUnifyResultProvider"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddUnifyProvider<TUnifyResultProvider>(this IServiceCollection services)
        where TUnifyResultProvider : class, IUnifyResultProvider
    {
        return services.AddUnifyProvider<TUnifyResultProvider>(string.Empty);
    }

    /// <summary>
    /// 添加规范化提供器
    /// </summary>
    /// <typeparam name="TUnifyResultProvider"></typeparam>
    /// <param name="services"></param>
    /// <param name="providerName"></param>
    /// <returns></returns>
    public static IServiceCollection AddUnifyProvider<TUnifyResultProvider>(this IServiceCollection services, string providerName)
        where TUnifyResultProvider : class, IUnifyResultProvider
    {
        providerName ??= string.Empty;

        var providerType = typeof(TUnifyResultProvider);

        // 添加规范化提供器
        services.TryAddSingleton(providerType, providerType);

        // 获取规范化提供器模型，不能为空
        var resultType = providerType.GetCustomAttribute<UnifyModelAttribute>().ModelType;

        // 创建规范化元数据
        var metadata = new UnifyMetadata
        {
            ProviderName = providerName,
            ProviderType = providerType,
            ResultType = resultType
        };

        // 添加或替换规范化配置
        UnifyContext.UnifyProviders.AddOrUpdate(providerName, _ => metadata, (_, _) => metadata);

        return services;
    }

    /// <summary>
    /// 添加规范化序列化配置
    /// </summary>
    /// <param name="mvcBuilder"></param>
    /// <param name="providerName"></param>
    /// <param name="serializerSettings"></param>
    /// <returns></returns>
    public static IMvcBuilder AddUnifyJsonOptions(this IMvcBuilder mvcBuilder, string providerName, object serializerSettings)
    {
        mvcBuilder.Services.AddUnifyJsonOptions(providerName, serializerSettings);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加规范化序列化配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="providerName"></param>
    /// <param name="serializerSettings"></param>
    /// <returns></returns>
    public static IServiceCollection AddUnifyJsonOptions(this IServiceCollection services, string providerName, object serializerSettings)
    {
        if (string.IsNullOrWhiteSpace(providerName)) throw new ArgumentNullException(nameof(providerName));

        // 添加或替换规范化序列化配置
        UnifyContext.UnifySerializerSettings.AddOrUpdate(providerName, _ => serializerSettings, (_, _) => serializerSettings);

        return services;
    }
}