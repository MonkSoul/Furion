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