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
using System.Linq;
using System.Linq.Expressions;

namespace Fur.LinqBuilder
{
    /// <summary>
    /// linq/Lambda 表达式拓展
    /// </summary>
    [NonBeScan]
    public static class LinqExpressionExtensions
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
    }
}