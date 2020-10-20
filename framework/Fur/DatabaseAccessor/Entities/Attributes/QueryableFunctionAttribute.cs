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
using System;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 实体函数配置特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class QueryableFunctionAttribute : DbFunctionAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">函数名</param>
        /// <param name="schema">架构名</param>
        public QueryableFunctionAttribute(string name, string schema = null) : base(name, schema)
        {
            DbContextLocators = Array.Empty<Type>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">函数名</param>
        /// <param name="schema">架构名</param>
        /// <param name="dbContextLocators">数据库上下文定位器</param>
        public QueryableFunctionAttribute(string name, string schema = null, params Type[] dbContextLocators) : base(name, schema)
        {
            DbContextLocators = dbContextLocators ?? Array.Empty<Type>();
        }

        /// <summary>
        /// 数据库上下文定位器
        /// </summary>
        public Type[] DbContextLocators { get; set; }
    }
}