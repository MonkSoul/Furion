using Fur.AppCore.Attributes;
using System;

namespace Fur.Linq.Builders
{
    /// <summary>
    /// Linq/Lambda 构建器
    /// </summary>
    [NonInflated]
    public class LinqBuilder
    {
        /// <summary>
        /// 创建 Linq/Lambda 表达式
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>新的表达式</returns>
        public static Func<TSource, bool> Create<TSource>(Func<TSource, bool> expression) => expression;

        /// <summary>
        /// 创建 Linq/Lambda 表达式，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>新的表达式</returns>
        public static Func<TSource, int, bool> Create<TSource>(Func<TSource, int, bool> expression) => expression;

        /// <summary>
        /// 创建 And 表达式
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Func<TSource, bool> And<TSource>() => u => true;

        /// <summary>
        /// 创建 And 表达式，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Func<TSource, int, bool> IndexAnd<TSource>() => (u, i) => true;

        /// <summary>
        /// 创建 Or 表达式
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Func<TSource, bool> Or<TSource>() => u => false;

        /// <summary>
        /// 创建 Or 表达式，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Func<TSource, int, bool> IndexOr<TSource>() => (u, i) => false;
    }
}