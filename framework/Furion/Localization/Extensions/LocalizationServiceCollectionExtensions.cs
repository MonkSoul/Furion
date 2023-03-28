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