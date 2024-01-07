// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion;
using Furion.Localization;

namespace Microsoft.AspNetCore.Mvc.Localization;

/// <summary>
/// IHtmlLocalizerFactory 拓展类
/// </summary>
[SuppressSniffer]
public static class IHtmlLocalizerFactoryExtensions
{
    /// <summary>
    /// 创建默认多语言工厂
    /// </summary>
    /// <param name="htmlLocalizerFactory"></param>
    /// <returns></returns>
    public static IHtmlLocalizer Create(this IHtmlLocalizerFactory htmlLocalizerFactory)
    {
        var localizationSettings = App.GetOptions<LocalizationSettingsOptions>();
        return htmlLocalizerFactory.Create(localizationSettings.LanguageFilePrefix, localizationSettings.AssemblyName);
    }
}