// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fur.LinqBuilder
{
    /// <summary>
    /// linq/Lambda 表达式拓展
    /// </summary>
    [SkipScan]
    public static class LinqExtensions
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
        /// [EF Core] 根据条件成立再构建 Where 查询
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
        /// [EF Core] 根据条件成立再构建 Where 查询，支持索引器
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
        /// [EF Core] 根据条件成立再构建 Include 查询
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <typeparam name="TProperty">泛型属性类型</typeparam>
        /// <param name="sources">集合对象</param>
        /// <param name="condition">布尔条件</param>
        /// <param name="expression">新的集合对象表达式</param>
        /// <returns></returns>
        public static IIncludableQueryable<TSource, TProperty> Include<TSource, TProperty>(this IQueryable<TSource> sources, bool condition, Expression<Func<TSource, TProperty>> expression) where TSource : class
        {
            return condition ? sources.Include(expression) : (IIncludableQueryable<TSource, TProperty>)sources;
        }

        /// <summary>
        /// [EF Core] 与操作合并多个表达式
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
        /// [EF Core] 与操作合并多个表达式，支持索引器
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
        /// [EF Core] 根据条件成立再构建 WhereOr 查询
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
        /// [EF Core] 根据条件成立再构建 WhereOr 查询，支持索引器
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