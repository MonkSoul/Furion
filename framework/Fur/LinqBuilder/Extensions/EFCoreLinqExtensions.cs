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
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fur.LinqBuilder
{
    /// <summary>
    /// [EF Core] Linq/Lambda 拓展类
    /// </summary>
    [NonBeScan]
    public static class EFCoreLinqExtensions
    {
        /// <summary>
        /// [EF Core] 根据条件成立再构建 Where 查询
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="sources">集合对象</param>
        /// <param name="condition">布尔条件</param>
        /// <param name="expression">表达式</param>
        /// <returns>新的集合对象</returns>
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> sources, bool condition, Expression<Func<TSource, bool>> expression)
        {
            return condition ? sources.Where(expression) : sources;
        }

        /// <summary>
        /// [EF Core] 根据条件成立再构建 Where 查询，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="sources">集合对象</param>
        /// <param name="condition">布尔条件</param>
        /// <param name="expression">表达式</param>
        /// <returns>新的集合对象</returns>
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> sources, bool condition, Expression<Func<TSource, int, bool>> expression)
        {
            return condition ? sources.Where(expression) : sources;
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
        public static IQueryable<TSource> IncludeIf<TSource, TProperty>(this IQueryable<TSource> sources, bool condition, Expression<Func<TSource, TProperty>> expression) where TSource : class
        {
            return condition ? sources.Include(expression) : sources;
        }

        /// <summary>
        /// [EF Core] 与操作合并多个表达式
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="sources">集合对象</param>
        /// <param name="expressions">表达式数组</param>
        /// <returns>新的集合对象</returns>
        public static IQueryable<TSource> WhereOr<TSource>(this IQueryable<TSource> sources, params Expression<Func<TSource, bool>>[] expressions)
        {
            if (expressions == null || !expressions.Any()) return sources;
            if (expressions.Length == 1) return sources.Where(expressions[0]);

            var expression = EFCoreLinqBuilder.Or<TSource>();
            foreach (var _expression in expressions)
            {
                expression = expression.Or(_expression);
            }
            return sources.Where(expression);
        }

        /// <summary>
        /// [EF Core] 与操作合并多个表达式，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="sources">集合对象</param>
        /// <param name="expressions">表达式数组</param>
        /// <returns>新的集合对象</returns>
        public static IQueryable<TSource> WhereOr<TSource>(this IQueryable<TSource> sources, params Expression<Func<TSource, int, bool>>[] expressions)
        {
            if (expressions == null || !expressions.Any()) return sources;
            if (expressions.Length == 1) return sources.Where(expressions[0]);

            var expression = EFCoreLinqBuilder.IndexOr<TSource>();
            foreach (var _expression in expressions)
            {
                expression = expression.Or(_expression);
            }
            return sources.Where(expression);
        }

        /// <summary>
        /// [EF Core] 根据条件成立再构建 WhereOr 查询
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="sources">集合对象</param>
        /// <param name="conditionExpressions">条件表达式</param>
        /// <returns>新的集合对象</returns>
        public static IQueryable<TSource> WhereOrIf<TSource>(this IQueryable<TSource> sources, params (bool condition, Expression<Func<TSource, bool>> expression)[] conditionExpressions)
        {
            var expressions = new List<Expression<Func<TSource, bool>>>();
            foreach (var (condition, expression) in conditionExpressions)
            {
                if (condition) expressions.Add(expression);
            }
            return WhereOr(sources, expressions.ToArray());
        }

        /// <summary>
        /// [EF Core] 根据条件成立再构建 WhereOr 查询，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="sources">集合对象</param>
        /// <param name="conditionExpressions">条件表达式</param>
        /// <returns>新的集合对象</returns>
        public static IQueryable<TSource> WhereOrIf<TSource>(this IQueryable<TSource> sources, params (bool condition, Expression<Func<TSource, int, bool>> expression)[] conditionExpressions)
        {
            var expressions = new List<Expression<Func<TSource, int, bool>>>();
            foreach (var (condition, expression) in conditionExpressions)
            {
                if (condition) expressions.Add(expression);
            }
            return WhereOr(sources, expressions.ToArray());
        }
    }
}