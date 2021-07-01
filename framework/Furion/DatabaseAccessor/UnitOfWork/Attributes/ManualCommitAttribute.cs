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
    /// 手动提交 SaveChanges
    /// <para>默认情况下，框架会自动在方法结束之时调用 SaveChanges 方法，贴此特性可以忽略该行为</para>
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
    public sealed class ManualCommitAttribute : Attribute
    {
    }
}