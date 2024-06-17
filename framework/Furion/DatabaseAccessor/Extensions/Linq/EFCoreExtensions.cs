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

using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore;

/// <summary>
/// EntityFramework Core 拓展
/// </summary>
[SuppressSniffer]
public static class EFCoreExtensions
{
    /// <summary>
    /// 根据条件成立再构建 Include 查询
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <typeparam name="TProperty">泛型属性类型</typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="condition">布尔条件</param>
    /// <param name="expression">新的集合对象表达式</param>
    /// <returns></returns>
    public static IQueryable<TSource> Include<TSource, TProperty>(this IQueryable<TSource> sources, bool condition, Expression<Func<TSource, TProperty>> expression) where TSource : class
    {
        return condition ? sources.Include(expression) : sources;
    }

    /// <summary>
    /// 构建 OrderBy 查询（自动处理 N 级）
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="keySelector">表达式</param>
    /// <returns>新的集合对象</returns>
    public static IOrderedQueryable<TSource> FlexOrderBy<TSource, TKey>(this IQueryable<TSource> sources, Expression<Func<TSource, TKey>> keySelector)
    {
        return sources.Expression.Type.GetGenericTypeDefinition() == typeof(IOrderedQueryable<>)
               ? ((IOrderedQueryable<TSource>)sources).ThenBy(keySelector)
               : sources.OrderBy(keySelector);
    }

    /// <summary>
    /// 构建 OrderByDescending 查询（自动处理 N 级）
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="keySelector">表达式</param>
    /// <returns>新的集合对象</returns>
    public static IOrderedQueryable<TSource> FlexOrderByDescending<TSource, TKey>(this IQueryable<TSource> sources, Expression<Func<TSource, TKey>> keySelector)
    {
        return sources.Expression.Type.GetGenericTypeDefinition() == typeof(IOrderedQueryable<>)
               ? ((IOrderedQueryable<TSource>)sources).ThenByDescending(keySelector)
               : sources.OrderByDescending(keySelector);
    }

    /// <summary>
    /// 根据条件成立再构建 OrderBy 查询
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="condition">布尔条件</param>
    /// <param name="keySelector">表达式</param>
    /// <returns>新的集合对象</returns>
    public static IQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> sources, bool condition, Expression<Func<TSource, TKey>> keySelector)
    {
        return condition ? sources.FlexOrderBy(keySelector) : sources;
    }

    /// <summary>
    /// 根据条件成立再构建 OrderByDescending 查询
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="condition">布尔条件</param>
    /// <param name="keySelector">表达式</param>
    /// <returns>新的集合对象</returns>
    public static IQueryable<TSource> OrderByDescending<TSource, TKey>(this IQueryable<TSource> sources, bool condition, Expression<Func<TSource, TKey>> keySelector)
    {
        return condition ? sources.FlexOrderByDescending(keySelector) : sources;
    }

    /// <summary>
    /// 根据条件成立再构建 ThenBy 查询
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="condition">布尔条件</param>
    /// <param name="keySelector">表达式</param>
    /// <returns>新的集合对象</returns>
    public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(this IOrderedQueryable<TSource> sources, bool condition, Expression<Func<TSource, TKey>> keySelector)
    {
        return condition ? sources.ThenBy(keySelector) : sources;
    }

    /// <summary>
    /// 根据条件成立再构建 ThenByDescending 查询
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="condition">布尔条件</param>
    /// <param name="keySelector">表达式</param>
    /// <returns>新的集合对象</returns>
    public static IOrderedQueryable<TSource> ThenByDescending<TSource, TKey>(this IOrderedQueryable<TSource> sources, bool condition, Expression<Func<TSource, TKey>> keySelector)
    {
        return condition ? sources.ThenByDescending(keySelector) : sources;
    }
}