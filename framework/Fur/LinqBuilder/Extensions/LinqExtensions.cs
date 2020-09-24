// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：Gitee：https://gitee.com/monksoul/Fur 
// 				   Github：https://github.com/monksoul/Fur 
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fur.LinqBuilder
{
    /// <summary>
    /// Linq/Lambda 拓展类
    /// </summary>
    [NonBeScan]
    public static class LinqExtensions
    {
        /// <summary>
        /// 根据条件成立再构建 Where 查询
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="sources">集合对象</param>
        /// <param name="condition">布尔条件</param>
        /// <param name="expression">表达式</param>
        /// <returns>新的集合对象</returns>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> sources, bool condition, Func<TSource, bool> expression)
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
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> sources, bool condition, Func<TSource, int, bool> expression)
        {
            return condition ? sources.Where(expression) : sources;
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
}