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

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Furion.Localization;

/// <summary>
/// 常量、公共方法配置类
/// </summary>
internal static class Penetrates
{
    /// <summary>
    /// 设置请求多语言对象
    /// </summary>
    /// <param name="requestLocalization"></param>
    /// <param name="localizationSettings"></param>
    internal static void SetRequestLocalization(RequestLocalizationOptions requestLocalization, LocalizationSettingsOptions localizationSettings)
    {
        // 如果设置了默认语言，则取默认语言，否则取第一个
        requestLocalization.SetDefaultCulture(
                    string.IsNullOrWhiteSpace(localizationSettings.DefaultCulture)
                        ? (localizationSettings.SupportedCultures != null && localizationSettings.SupportedCultures.Length > 0 ? localizationSettings.SupportedCultures[0] : localizationSettings.DefaultCulture)
                        : localizationSettings.DefaultCulture)
               .AddSupportedCultures(localizationSettings.SupportedCultures)
               .AddSupportedUICultures(localizationSettings.SupportedCultures);

        // 自动根据客户端浏览器的语言实现多语言机制
        requestLocalization.ApplyCurrentCultureToResponseHeaders = true;

        // 修复 DateTime 问题 https://gitee.com/dotnetchina/Furion/issues/I6RUOU
        if (!string.IsNullOrWhiteSpace(localizationSettings.DateTimeFormatCulture))
        {
            var standardCulture = new CultureInfo(localizationSettings.DateTimeFormatCulture);

            // 修复默认区域语言
            FixedCultureDateTimeFormat(requestLocalization.DefaultRequestCulture, standardCulture);

            // 修复所有支持的区域语言
            foreach (var culture in requestLocalization.SupportedCultures)
            {
                FixedCultureDateTimeFormat(culture, standardCulture);
            }

            // 修复线程区域语言
            foreach (var culture in requestLocalization.SupportedUICultures)
            {
                FixedCultureDateTimeFormat(culture, standardCulture);
            }
        }
    }

    /// <summary>
    /// 修复多语言引起的 DateTime.Now 问题
    /// </summary>
    /// <param name="culture"></param>
    /// <param name="targetCulture"></param>
    internal static void FixedCultureDateTimeFormat(CultureInfo culture, CultureInfo targetCulture)
    {
        culture.DateTimeFormat = targetCulture.DateTimeFormat;
    }

    /// <summary>
    /// 修复多语言引起的 DateTime.Now 问题
    /// </summary>
    /// <param name="culture"></param>
    /// <param name="targetCulture"></param>
    internal static void FixedCultureDateTimeFormat(RequestCulture culture, CultureInfo targetCulture)
    {
        culture.Culture.DateTimeFormat = targetCulture.DateTimeFormat;
        culture.UICulture.DateTimeFormat = targetCulture.DateTimeFormat;
    }

    /// <summary>
    /// 修复多语言引起的 DateTime.Now 问题
    /// </summary>
    /// <param name="culture"></param>
    /// <param name="targetCulture"></param>
    internal static void FixedCultureDateTimeFormat(CultureInfo culture, string targetCulture)
    {
        if (!string.IsNullOrWhiteSpace(targetCulture))
        {
            FixedCultureDateTimeFormat(culture, new CultureInfo(targetCulture));
        }
    }

    /// <summary>
    /// 修复多语言引起的 DateTime.Now 问题
    /// </summary>
    /// <param name="culture"></param>
    /// <param name="targetCulture"></param>
    internal static void FixedCultureDateTimeFormat(RequestCulture culture, string targetCulture)
    {
        if (!string.IsNullOrWhiteSpace(targetCulture))
        {
            FixedCultureDateTimeFormat(culture, new CultureInfo(targetCulture));
        }
    }
}