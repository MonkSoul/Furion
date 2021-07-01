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

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 工作单元配置特性
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
    public sealed class UnitOfWorkAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UnitOfWorkAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ensureTransaction"></param>
        public UnitOfWorkAttribute(bool ensureTransaction)
        {
            EnsureTransaction = ensureTransaction;
        }

        /// <summary>
        /// 确保事务可用
        /// <para>此方法为了解决静态类方式操作数据库的问题</para>
        /// </summary>
        public bool EnsureTransaction { get; set; } = false;
    }
}