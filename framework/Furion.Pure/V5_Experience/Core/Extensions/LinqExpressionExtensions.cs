// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Linq.Expressions;

namespace Furion.Extensions;

/// <summary>
///     <see cref="Expression" /> 拓展类
/// </summary>
internal static class LinqExpressionExtensions
{
    /// <summary>
    ///     根据条件成立构建 <c>Where</c> 表达式
    /// </summary>
    /// <param name="source">
    ///     <see cref="IEnumerable{TSource}" />
    /// </param>
    /// <param name="condition">条件</param>
    /// <param name="predicate"><c>Where</c> 表达式</param>
    /// <typeparam name="TSource">集合元素类型</typeparam>
    /// <returns>
    ///     <see cref="IEnumerable{TSource}" />
    /// </returns>
    internal static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source
        , bool condition
        , Func<TSource, bool> predicate)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(predicate);

        return !condition
            ? source
            : source.Where(predicate);
    }

    /// <summary>
    ///     解析表达式属性名称
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    /// <param name="propertySelector">
    ///     <see cref="Expression{TDelegate}" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    /// <exception cref="ArgumentException"></exception>
    internal static string GetPropertyName<T, TProperty>(this Expression<Func<T, TProperty?>> propertySelector) =>
        propertySelector.Body switch
        {
            // 检查 Lambda 表达式的主体是否是 MemberExpression 类型
            MemberExpression memberExpression => GetPropertyName<T>(memberExpression),

            // 如果主体是 UnaryExpression 类型，则继续解析
            UnaryExpression { Operand: MemberExpression nestedMemberExpression } => GetPropertyName<T>(
                nestedMemberExpression),

            _ => throw new ArgumentException("Expression is not valid for property selection.")
        };

    /// <summary>
    ///     解析表达式属性名称
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="memberExpression">
    ///     <see cref="MemberExpression" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    /// <exception cref="ArgumentException"></exception>
    internal static string GetPropertyName<T>(MemberExpression memberExpression)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(memberExpression);

        // 获取属性声明类型
        var propertyType = memberExpression.Member.DeclaringType;

        // 检查是否越界访问属性
        if (propertyType != typeof(T))
        {
            throw new ArgumentException("Invalid property selection.");
        }

        // 返回属性名称
        return memberExpression.Member.Name;
    }
}