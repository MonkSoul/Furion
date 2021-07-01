// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using System;
using System.Linq.Expressions;

namespace Furion.LinqBuilder
{
    /// <summary>
    /// EF Core Linq 拓展
    /// </summary>
    [SuppressSniffer]
    public static class LinqExpression
    {
        /// <summary>
        /// [EF Core] 创建 Linq/Lambda 表达式
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>新的表达式</returns>
        public static Expression<Func<TSource, bool>> Create<TSource>(Expression<Func<TSource, bool>> expression)
        {
            return expression;
        }

        /// <summary>
        /// [EF Core] 创建 Linq/Lambda 表达式，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>新的表达式</returns>
        public static Expression<Func<TSource, int, bool>> Create<TSource>(Expression<Func<TSource, int, bool>> expression)
        {
            return expression;
        }

        /// <summary>
        /// [EF Core] 创建 And 表达式
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Expression<Func<TSource, bool>> And<TSource>()
        {
            return u => true;
        }

        /// <summary>
        /// [EF Core] 创建 And 表达式，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Expression<Func<TSource, int, bool>> IndexAnd<TSource>()
        {
            return (u, i) => true;
        }

        /// <summary>
        /// [EF Core] 创建 Or 表达式
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Expression<Func<TSource, bool>> Or<TSource>()
        {
            return u => false;
        }

        /// <summary>
        /// [EF Core] 创建 Or 表达式，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Expression<Func<TSource, int, bool>> IndexOr<TSource>()
        {
            return (u, i) => false;
        }
    }
}