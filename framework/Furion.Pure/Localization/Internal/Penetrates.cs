// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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