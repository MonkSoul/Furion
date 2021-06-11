// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.8.5
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using System;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// Sql 执行代理基特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class SqlProxyAttribute : Attribute
    {
        /// <summary>
        /// 数据库上下文定位器
        /// </summary>
        public Type DbContextLocator { get; set; }
    }
}