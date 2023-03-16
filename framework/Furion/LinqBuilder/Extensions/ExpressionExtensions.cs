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