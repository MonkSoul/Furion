using Furion.DependencyInjection;
using Furion.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 多语言中间件拓展
    /// </summary>
    [SkipScan]
    public static class LocalizationApplicationBuilderExtensions
    {
        /// <summary>
        /// 配置多语言中间件拓展
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAppLocalization(this IApplicationBuilder app)
        {
            // 获取多语言配置选项
            var localizationSettings = app.ApplicationServices.GetRequiredService<IOptions<LocalizationSettingsOptions>>().Value;

            // 如果没有配置多语言选项，则不注册服务
            if (localizationSettings.SupportedCultures == null || localizationSettings.SupportedCultures.Length == 0) return app;

            var requestLocalization = new RequestLocalizationOptions();
            Penetrates.SetRequestLocalization(requestLocalization, localizationSettings);

            // 设置多语言请求中间件
            app.UseRequestLocalization(requestLocalization);

            return app;
        }
    }
}