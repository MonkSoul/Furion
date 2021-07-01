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
using System.ComponentModel;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 数据库上下文模式
    /// </summary>
    [SuppressSniffer]
    public enum DbContextMode
    {
        /// <summary>
        /// 缓存模型数据库上下文
        /// <para>
        /// OnModelCreating 只会初始化一次
        /// </para>
        /// </summary>
        [Description("缓存模型数据库上下文")]
        Cached,

        /// <summary>
        /// 动态模型数据库上下文
        /// <para>
        /// OnModelCreating 每次都会调用
        /// </para>
        /// </summary>
        [Description("动态模型数据库上下文")]
        Dynamic
    }
}