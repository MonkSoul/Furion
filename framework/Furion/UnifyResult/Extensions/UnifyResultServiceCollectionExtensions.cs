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

using Furion.UnifyResult;
using Microsoft.Extensions.DependencyInjection.Extensions;
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