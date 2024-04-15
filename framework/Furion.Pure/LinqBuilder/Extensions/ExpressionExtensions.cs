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

namespace Furion.LinqBuilder;

/// <summary>
/// 表达式拓展类
/// </summary>
[SuppressSniffer]
public static class ExpressionExtensions
{
    /// <summary>
    /// 组合两个表达式
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="expression">表达式1</param>
    /// <param name="extendExpression">表达式2</param>
    /// <param name="mergeWay">组合方式</param>
    /// <returns>新的表达式</returns>
    public static Expression<TSource> Compose<TSource>(this Expression<TSource> expression, Expression<TSource> extendExpression, Func<Expression, Expression, Expression> mergeWay)
    {
        var parameterExpressionSetter = expression.Parameters
            .Select((u, i) => new { u, Parameter = extendExpression.Parameters[i] })
            .ToDictionary(d => d.Parameter, d => d.u);

        var extendExpressionBody = ParameterReplaceExpressionVisitor.ReplaceParameters(parameterExpressionSetter, extendExpression.Body);
        return Expression.Lambda<TSource>(mergeWay(expression.Body, extendExpressionBody), expression.Parameters);
    }

    /// <summary>
    /// 与操作合并两个表达式
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="expression">表达式1</param>
    /// <param name="extendExpression">表达式2</param>
    /// <returns>新的表达式</returns>
    public static Expression<Func<TSource, bool>> And<TSource>(this Expression<Func<TSource, bool>> expression, Expression<Func<TSource, bool>> extendExpression)
    {
        return expression.Compose(extendExpression, Expression.AndAlso);
    }

    /// <summary>
    /// 与操作合并两个表达式，支持索引器
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="expression">表达式1</param>
    /// <param name="extendExpression">表达式2</param>
    /// <returns>新的表达式</returns>
    public static Expression<Func<TSource, int, bool>> And<TSource>(this Expression<Func<TSource, int, bool>> expression, Expression<Func<TSource, int, bool>> extendExpression)
    {
        return expression.Compose(extendExpression, Expression.AndAlso);
    }

    /// <summary>
    /// 根据条件成立再与操作合并两个表达式
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="expression">表达式1</param>
    /// <param name="condition">布尔条件</param>
    /// <param name="extendExpression">表达式2</param>
    /// <returns>新的表达式</returns>
    public static Expression<Func<TSource, bool>> AndIf<TSource>(this Expression<Func<TSource, bool>> expression, bool condition, Expression<Func<TSource, bool>> extendExpression)
    {
        return condition ? expression.Compose(extendExpression, Expression.AndAlso) : expression;
    }

    /// <summary>
    /// 根据条件成立再与操作合并两个表达式，支持索引器
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="expression">表达式1</param>
    /// <param name="condition">布尔条件</param>
    /// <param name="extendExpression">表达式2</param>
    /// <returns>新的表达式</returns>
    public static Expression<Func<TSource, int, bool>> AndIf<TSource>(this Expression<Func<TSource, int, bool>> expression, bool condition, Expression<Func<TSource, int, bool>> extendExpression)
    {
        return condition ? expression.Compose(extendExpression, Expression.AndAlso) : expression;
    }

    /// <summary>
    /// 或操作合并两个表达式
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="expression">表达式1</param>
    /// <param name="extendExpression">表达式2</param>
    /// <returns>新的表达式</returns>
    public static Expression<Func<TSource, bool>> Or<TSource>(this Expression<Func<TSource, bool>> expression, Expression<Func<TSource, bool>> extendExpression)
    {
        return expression.Compose(extendExpression, Expression.OrElse);
    }

    /// <summary>
    /// 或操作合并两个表达式，支持索引器
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="expression">表达式1</param>
    /// <param name="extendExpression">表达式2</param>
    /// <returns>新的表达式</returns>
    public static Expression<Func<TSource, int, bool>> Or<TSource>(this Expression<Func<TSource, int, bool>> expression, Expression<Func<TSource, int, bool>> extendExpression)
    {
        return expression.Compose(extendExpression, Expression.OrElse);
    }

    /// <summary>
    /// 根据条件成立再或操作合并两个表达式
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="expression">表达式1</param>
    /// <param name="condition">布尔条件</param>
    /// <param name="extendExpression">表达式2</param>
    /// <returns>新的表达式</returns>
    public static Expression<Func<TSource, bool>> OrIf<TSource>(this Expression<Func<TSource, bool>> expression, bool condition, Expression<Func<TSource, bool>> extendExpression)
    {
        return condition ? expression.Compose(extendExpression, Expression.OrElse) : expression;
    }

    /// <summary>
    /// 根据条件成立再或操作合并两个表达式，支持索引器
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="expression">表达式1</param>
    /// <param name="condition">布尔条件</param>
    /// <param name="extendExpression">表达式2</param>
    /// <returns>新的表达式</returns>
    public static Expression<Func<TSource, int, bool>> OrIf<TSource>(this Expression<Func<TSource, int, bool>> expression, bool condition, Expression<Func<TSource, int, bool>> extendExpression)
    {
        return condition ? expression.Compose(extendExpression, Expression.OrElse) : expression;
    }

    /// <summary>
    /// 获取Lambda表达式属性名，只限 u=>u.Property 表达式
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="expression">表达式</param>
    /// <returns>属性名</returns>
    public static string GetExpressionPropertyName<TSource>(this Expression<Func<TSource, object>> expression)
    {
        if (expression.Body is UnaryExpression unaryExpression)
        {
            return ((MemberExpression)unaryExpression.Operand).Member.Name;
        }
        else if (expression.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }
        else if (expression.Body is ParameterExpression parameterExpression)
        {
            return parameterExpression.Type.Name;
        }

        throw new InvalidCastException(nameof(expression));
    }

    /// <summary>
    /// 是否是空集合
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="sources">集合对象</param>
    /// <returns>是否为空集合</returns>
    public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> sources)
    {
        return sources == null || !sources.Any();
    }
}