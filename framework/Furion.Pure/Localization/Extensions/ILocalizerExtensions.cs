using Microsoft.AspNetCore.Mvc.Localization;
using System.Linq.Expressions;

namespace Microsoft.Extensions.Localization;

/// <summary>
/// <see cref="IStringLocalizer"/> 和 <see cref="IHtmlLocalizer"/> 拓展
/// </summary>
[SuppressSniffer]
public static class ILocalizerExtensions
{
    /// <summary>
    /// 根据实体类属性名获取对应的多语言配置
    /// </summary>
    /// <typeparam name="TResource">通常命名为 SharedResource </typeparam>
    /// <param name="stringLocalizer"><see cref="IStringLocalizer"/></param>
    /// <param name="propertyExpression">属性表达式</param>
    /// <returns></returns>
    public static LocalizedString GetString<TResource>(this IStringLocalizer stringLocalizer, Expression<Func<TResource, string>> propertyExpression)
    {
        return stringLocalizer[(propertyExpression.Body as MemberExpression).Member.Name];
    }
}