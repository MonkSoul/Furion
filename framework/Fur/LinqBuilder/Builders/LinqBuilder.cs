// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using System;

namespace Fur.LinqBuilder
{
    /// <summary>
    /// Linq/Lambda 构建器
    /// </summary>
    public static class LinqBuilder
    {
        /// <summary>
        /// 创建 Linq/Lambda 表达式
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>新的表达式</returns>
        public static Func<TSource, bool> Create<TSource>(Func<TSource, bool> expression)
        {
            return expression;
        }

        /// <summary>
        /// 创建 Linq/Lambda 表达式，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>新的表达式</returns>
        public static Func<TSource, int, bool> Create<TSource>(Func<TSource, int, bool> expression)
        {
            return expression;
        }

        /// <summary>
        /// 创建 And 表达式
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Func<TSource, bool> And<TSource>()
        {
            return u => true;
        }

        /// <summary>
        /// 创建 And 表达式，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Func<TSource, int, bool> IndexAnd<TSource>()
        {
            return (u, i) => true;
        }

        /// <summary>
        /// 创建 Or 表达式
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Func<TSource, bool> Or<TSource>()
        {
            return u => false;
        }

        /// <summary>
        /// 创建 Or 表达式，支持索引器
        /// </summary>
        /// <typeparam name="TSource">泛型类型</typeparam>
        /// <returns>新的表达式</returns>
        public static Func<TSource, int, bool> IndexOr<TSource>()
        {
            return (u, i) => false;
        }
    }
}