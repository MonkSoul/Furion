// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.EntityFrameworkCore.Query;
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
    public static IIncludableQueryable<TSource, TProperty> Include<TSource, TProperty>(this IQueryable<TSource> sources, bool condition, Expression<Func<TSource, TProperty>> expression) where TSource : class
    {
        return condition ? sources.Include(expression) : (IIncludableQueryable<TSource, TProperty>)sources;
    }
}