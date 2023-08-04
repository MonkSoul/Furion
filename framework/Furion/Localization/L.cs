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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Linq.Expressions;

namespace Furion.Localization;

/// <summary>
/// 全局多语言静态类
/// </summary>
[SuppressSniffer]
public static class L
{
    /// <summary>
    /// String 多语言
    /// </summary>
    public static IStringLocalizer Text => App.GetService<IStringLocalizerFactory>(App.RootServices)?.Create();

    /// <summary>
    /// Html 多语言
    /// </summary>
    public static IHtmlLocalizer Html => App.GetService<IHtmlLocalizerFactory>(App.RootServices)?.Create();

    /// <summary>
    /// String 多语言
    /// </summary>
    /// <typeparam name="T">特定类型</typeparam>
    /// <remarks><see cref="IStringLocalizer{T}"/></remarks>
    public static IStringLocalizer<T> TextOf<T>()
    {
        return App.GetService<IStringLocalizer<T>>(App.RootServices);
    }

    /// <summary>
    /// Html 多语言
    /// </summary>
    /// <typeparam name="T">特定类型</typeparam>
    /// <remarks><see cref="IHtmlLocalizer{T}"/></remarks>
    public static IHtmlLocalizer<T> HtmlOf<T>()
    {
        return App.GetService<IHtmlLocalizer<T>>(App.RootServices);
    }

    /// <summary>
    /// 设置当前选择的语言
    /// </summary>
    /// <param name="culture"></param>
    /// <param name="immediately">是否立即对当前线程有效，设置 true 表示立即有效，默认情况下只会影响下一次请求</param>
    public static void SetCulture(string culture, bool immediately = false)
    {
        // 是否立即修改当前线程 UI 区域性
        if (immediately) SetCurrentUICulture(culture);

        var httpContext = App.HttpContext;
        if (httpContext == null) return;

        // 如果 Response 已经完成输出或者是 WebSocket 请求，则禁止写入
        if (httpContext.IsWebSocketRequest() || httpContext.Response.HasStarted) return;

        var cultureInfo = new RequestCulture(culture);

        // 修复 DateTime 问题
        Penetrates.FixedCultureDateTimeFormat(cultureInfo, App.GetOptions<LocalizationSettingsOptions>()?.DateTimeFormatCulture);

        httpContext.Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(cultureInfo),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );
    }

    /// <summary>
    /// 获取当前选择的语言
    /// </summary>
    /// <returns></returns>
    public static RequestCulture GetSelectCulture()
    {
        var httpContext = App.HttpContext;
        if (httpContext == null) return default;

        // 获取请求特性
        var requestCulture = httpContext.Features.Get<IRequestCultureFeature>();
        return requestCulture.RequestCulture;
    }

    /// <summary>
    /// 设置当前线程 UI 区域性
    /// </summary>
    /// <param name="culture"></param>
    /// <remarks>对当前线程（代码）立即有效</remarks>
    /// <returns></returns>
    public static void SetCurrentUICulture(string culture)
    {
        // https://learn.microsoft.com/zh-cn/dotnet/api/system.globalization.cultureinfo.currentuiculture?view=net-6.0
        // 修改线程当前的 UI 区域性
        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        // 修复 DateTime 问题
        Penetrates.FixedCultureDateTimeFormat(CultureInfo.CurrentUICulture
            , App.GetOptions<LocalizationSettingsOptions>()?.DateTimeFormatCulture);
    }

    /// <summary>
    /// 获取当前线程 UI 区域性
    /// </summary>
    /// <returns></returns>
    public static CultureInfo GetCurrentUICulture()
    {
        // https://learn.microsoft.com/zh-cn/dotnet/api/system.globalization.cultureinfo.currentuiculture?view=net-6.0
        return CultureInfo.CurrentUICulture;
    }

    /// <summary>
    /// 获取系统提供的语言列表
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, string> GetCultures()
    {
        var httpContext = App.HttpContext;
        if (httpContext == null) return new Dictionary<string, string>();

        // 获取请求本地特性选项
        var locOptions = httpContext.RequestServices.GetService<IOptions<RequestLocalizationOptions>>().Value;

        // 获取语言符号和名称
        var cultureItems = locOptions.SupportedUICultures
            .ToDictionary(u => u.Name, u => u.DisplayName);

        return cultureItems;
    }

    /// <summary>
    /// 根据实体类属性名获取对应的多语言配置
    /// </summary>
    /// <typeparam name="TResource">通常命名为 SharedResource </typeparam>
    /// <param name="propertyExpression">属性表达式</param>
    /// <returns></returns>
    public static LocalizedString GetString<TResource>(Expression<Func<TResource, string>> propertyExpression)
    {
        return Text.GetString(propertyExpression);
    }

    /// <summary>
    /// 获取指定区域的翻译
    /// </summary>
    /// <param name="name"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public static LocalizedString GetString(string name, string culture)
    {
        // 获取当前的区域
        var currentCulture = GetSelectCulture();

        // 临时设置区域
        CultureInfo.CurrentUICulture = new CultureInfo(culture);
        var cultureString = Text[name];

        // 还原区域
        CultureInfo.CurrentUICulture = currentCulture.Culture;
        return cultureString;
    }
}