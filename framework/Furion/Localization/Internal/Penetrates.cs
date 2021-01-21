using Furion.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Furion.Localization
{
    /// <summary>
    /// 常量、公共方法配置类
    /// </summary>
    [SkipScan]
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
            requestLocalization.SetDefaultCulture(string.IsNullOrEmpty(localizationSettings.DefaultCulture) ? localizationSettings.SupportedCultures[0] : localizationSettings.DefaultCulture)
                   .AddSupportedCultures(localizationSettings.SupportedCultures)
                   .AddSupportedUICultures(localizationSettings.SupportedCultures);

            // 自动根据客户端浏览器的语言实现多语言机制
            requestLocalization.ApplyCurrentCultureToResponseHeaders = true;
        }
    }
}