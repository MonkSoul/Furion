// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion;
using Furion.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 多语言服务拓展类
/// </summary>
[SuppressSniffer]
public static class LocalizationServiceCollectionExtensions
{
    /// <summary>
    /// 配置多语言服务
    /// </summary>
    /// <param name="mvcBuilder"></param>
    /// <param name="customizeConfigure">如果传入该参数，则使用自定义多语言机制</param>
    /// <returns></returns>
    public static IMvcBuilder AddAppLocalization(this IMvcBuilder mvcBuilder, Action<LocalizationSettingsOptions> customizeConfigure = default)
    {
        // 添加多语言配置选项
        mvcBuilder.Services.AddAppLocalization(customizeConfigure);

        // 获取多语言配置选项
        var localizationSettings = App.GetConfig<LocalizationSettingsOptions>("LocalizationSettings", true);

        // 配置视图多语言和验证多语言
        mvcBuilder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                 .AddDataAnnotationsLocalization(options =>
                 {
                     options.DataAnnotationLocalizerProvider = (type, factory) =>
                         factory.Create(localizationSettings.LanguageFilePrefix, localizationSettings.AssemblyName);
                 });

        return mvcBuilder;
    }

    /// <summary>
    /// 配置多语言服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="customizeConfigure">如果传入该参数，则使用自定义多语言机制</param>
    /// <returns></returns>
    public static IServiceCollection AddAppLocalization(this IServiceCollection services, Action<LocalizationSettingsOptions> customizeConfigure = default)
    {
        // 添加多语言配置选项
        services.AddConfigurableOptions<LocalizationSettingsOptions>();

        // 获取多语言配置选项
        var localizationSettings = App.GetConfig<LocalizationSettingsOptions>("LocalizationSettings", true);

        // 注册默认多语言服务
        if (customizeConfigure == null)
        {
            services.AddLocalization(options =>
            {
                if (!string.IsNullOrWhiteSpace(localizationSettings.ResourcesPath))
                    options.ResourcesPath = localizationSettings.ResourcesPath;
            });
        }
        // 使用自定义
        else customizeConfigure.Invoke(localizationSettings);

        // 注册请求多语言配置选项
        services.Configure<RequestLocalizationOptions>(options =>
        {
            Penetrates.SetRequestLocalization(options, localizationSettings);
        });

        // 处理多语言在 Razor 视图中文乱码问题
        services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

        return services;
    }
}