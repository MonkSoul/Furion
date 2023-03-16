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

using Furion.LinqBuilder;
using System.Linq.Expressions;

namespace System.Linq;

/// <summary>
/// IEnumerable 拓展
/// </summary>
[SuppressSniffer]
public static class IEnumerableExtensions
{
    /// <summary>
    /// 根据条件成立再构建 Where 查询
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="condition">布尔条件</param>
    /// <param name="expression">表达式</param>
    /// <returns>新的集合对象</returns>
    public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> sources, bool condition, Expression<Func<TSource, bool>> expression)
    {
        return condition ? Queryable.Where(sources, expression) : sources;
    }

    /// <summary>
    /// 根据条件成立再构建 Where 查询，支持索引器
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="condition">布尔条件</param>
    /// <param name="expression">表达式</param>
    /// <returns>新的集合对象</returns>
    public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> sources, bool condition, Expression<Func<TSource, int, bool>> expression)
    {
        return condition ? Queryable.Where(sources, expression) : sources;
    }

    /// <summary>
    /// 与操作合并多个表达式
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="expressions">表达式数组</param>
    /// <returns>新的集合对象</returns>
    public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> sources, params Expression<Func<TSource, bool>>[] expressions)
    {
        if (expressions == null || !expressions.Any()) return sources;
        if (expressions.Length == 1) return Queryable.Where(sources, expressions[0]);

        var expression = LinqExpression.Or<TSource>();
        foreach (var _expression in expressions)
        {
            expression = expression.Or(_expression);
        }
        return Queryable.Where(sources, expression);
    }

    /// <summary>
    /// 与操作合并多个表达式，支持索引器
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="expressions">表达式数组</param>
    /// <returns>新的集合对象</returns>
    public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> sources, params Expression<Func<TSource, int, bool>>[] expressions)
    {
        if (expressions == null || !expressions.Any()) return sources;
        if (expressions.Length == 1) return Queryable.Where(sources, expressions[0]);

        var expression = LinqExpression.IndexOr<TSource>();
        foreach (var _expression in expressions)
        {
            expression = expression.Or(_expression);
        }
        return Queryable.Where(sources, expression);
    }

    /// <summary>
    /// 根据条件成立再构建 WhereOr 查询
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="conditionExpressions">条件表达式</param>
    /// <returns>新的集合对象</returns>
    public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> sources, params (bool condition, Expression<Func<TSource, bool>> expression)[] conditionExpressions)
    {
        var expressions = new List<Expression<Func<TSource, bool>>>();
        foreach (var (condition, expression) in conditionExpressions)
        {
            if (condition) expressions.Add(expression);
        }
        return Where(sources, expressions.ToArray());
    }

    /// <summary>
    /// 根据条件成立再构建 WhereOr 查询，支持索引器
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="conditionExpressions">条件表达式</param>
    /// <returns>新的集合对象</returns>
    public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> sources, params (bool condition, Expression<Func<TSource, int, bool>> expression)[] conditionExpressions)
    {
        var expressions = new List<Expression<Func<TSource, int, bool>>>();
        foreach (var (condition, expression) in conditionExpressions)
        {
            if (condition) expressions.Add(expression);
        }
        return Where(sources, expressions.ToArray());
    }

    /// <summary>
    /// 根据条件成立再构建 Where 查询
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="condition">布尔条件</param>
    /// <param name="expression">表达式</param>
    /// <returns>新的集合对象</returns>
    public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> sources, bool condition, Func<TSource, bool> expression)
    {
        return condition ? sources.Where(expression) : sources;
    }

    /// <summary>
    /// 根据条件成立再构建 Where 查询，支持索引器
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="condition">布尔条件</param>
    /// <param name="expression">表达式</param>
    /// <returns>新的集合对象</returns>
    public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> sources, bool condition, Func<TSource, int, bool> expression)
    {
        return condition ? sources.Where(expression) : sources;
    }
}