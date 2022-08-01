// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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