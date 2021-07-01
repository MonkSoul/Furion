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
using Microsoft.EntityFrameworkCore;
using System;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 实体函数配置特性
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
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