// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
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

using System.Linq.Expressions;

namespace Furion.LinqBuilder;

/// <summary>
/// EF Core Linq 拓展
/// </summary>
[SuppressSniffer]
public static class LinqExpression
{
    /// <summary>
    /// 创建 Linq/Lambda 表达式
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="expression">表达式</param>
    /// <returns>新的表达式</returns>
    public static Expression<Func<TSource, bool>> Create<TSource>(Expression<Func<TSource, bool>> expression)
    {
        return expression;
    }

    /// <summary>
    /// 创建 Linq/Lambda 表达式，支持索引器
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="expression">表达式</param>
    /// <returns>新的表达式</returns>
    public static Expression<Func<TSource, int, bool>> Create<TSource>(Expression<Func<TSource, int, bool>> expression)
    {
        return expression;
    }

    /// <summary>
    /// 创建 And 表达式
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <returns>新的表达式</returns>
    public static Expression<Func<TSource, bool>> And<TSource>()
    {
        return u => true;
    }

    /// <summary>
    /// 创建 And 表达式，支持索引器
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <returns>新的表达式</returns>
    public static Expression<Func<TSource, int, bool>> IndexAnd<TSource>()
    {
        return (u, i) => true;
    }

    /// <summary>
    /// 创建 Or 表达式
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <returns>新的表达式</returns>
    public static Expression<Func<TSource, bool>> Or<TSource>()
    {
        return u => false;
    }

    /// <summary>
    /// 创建 Or 表达式，支持索引器
    /// </summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <returns>新的表达式</returns>
    public static Expression<Func<TSource, int, bool>> IndexOr<TSource>()
    {
        return (u, i) => false;
    }
}