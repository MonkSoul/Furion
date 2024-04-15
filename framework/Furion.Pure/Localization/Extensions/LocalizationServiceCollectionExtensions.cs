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