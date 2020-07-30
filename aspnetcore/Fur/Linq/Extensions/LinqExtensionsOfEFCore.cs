using Fur.Linq.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fur.Linq.Extensions
{
    /// <summary>
    /// [EF Core] Linq/Lambda 拓展类
    /// </summary>
    public static class LinqExtensionsOfEFCore
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
            => condition ? sources.Where(expression) : sources;

        /// <summary>
        /// [EF Core] 根据条件成立再构建 Where 查询，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="sources">集合对象</param>
        /// <param name="condition">布尔条件</param>
        /// <param name="expression">表达式</param>
        /// <returns>新的集合对象</returns>
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> sources, bool condition, Expression<Func<TSource, int, bool>> expression)
            => condition ? sources.Where(expression) : sources;

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
           => condition ? sources.Include(expression) : sources;

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

            var expression = LinqBuilderOfEFCore.Or<TSource>();
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

            var expression = LinqBuilderOfEFCore.IndexOr<TSource>();
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