// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using System;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 禁用工作单元特性
    /// </summary>
    /// <remarks>
    /// <para>慎用！一旦贴了此特性，单次请求中有任何异常代码，对数据库的任何更改将不会回滚。</para>
    /// <para>支持方法中贴此特性</para>
    /// <para>注意：只对请求中的起始方法起作用</para>
    /// </remarks>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public sealed class NonTransactAttribute : Attribute
    {
    }
}