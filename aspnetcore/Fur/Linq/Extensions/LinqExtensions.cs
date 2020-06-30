using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fur.Linq.Extensions
{
    /// <summary>
    /// Linq/Lambda 拓展类
    /// </summary>
    public static class LinqExtensions
    {
        #region 根据条件成立再构建 Where 查询 +/* public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> sources, bool condition, Func<TSource, bool> expression)
        /// <summary>
        /// 根据条件成立再构建 Where 查询
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="sources">集合对象</param>
        /// <param name="condition">布尔条件</param>
        /// <param name="expression">表达式</param>
        /// <returns>新的集合对象</returns>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> sources, bool condition, Func<TSource, bool> expression)
            => condition ? sources.Where(expression) : sources;
        #endregion

        #region 根据条件成立再构建 Where 查询，支持索引器 +/* public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> sources, bool condition, Func<TSource, int, bool> expression)
        /// <summary>
        /// 根据条件成立再构建 Where 查询，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="sources">集合对象</param>
        /// <param name="condition">布尔条件</param>
        /// <param name="expression">表达式</param>
        /// <returns>新的集合对象</returns>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> sources, bool condition, Func<TSource, int, bool> expression)
            => condition ? sources.Where(expression) : sources;
        #endregion

        #region 获取Lambda表达式属性名，只限 u=>u.Property 表达式 +/* public static string GetExpressionPropertyName<TSource>(this Expression<Func<TSource, object>> expression)
        /// <summary>
        /// 获取Lambda表达式属性名，只限 u=>u.Property 表达式
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>属性名</returns>
        public static string GetExpressionPropertyName<TSource>(this Expression<Func<TSource, object>> expression)
        {
            var expressionBody = expression.Body.ToString();
            return expressionBody.Substring(expressionBody.LastIndexOf(".") + 1);
        }
        #endregion

        #region 是否是空集合 +/* public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> sources)
        /// <summary>
        /// 是否是空集合
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="sources">集合对象</param>
        /// <returns>是否为空集合</returns>
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> sources)
            => sources == null || sources.Count() == 0;
        #endregion
    }
}
