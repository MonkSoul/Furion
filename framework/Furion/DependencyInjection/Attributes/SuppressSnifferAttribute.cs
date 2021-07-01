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

using System;

namespace Furion.DependencyInjection
{
    /// <summary>
    /// 不被扫描和发现的特性
    /// </summary>
    /// <remarks>用于程序集扫描类型或方法时候</remarks>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Enum)]
    public class SuppressSnifferAttribute : Attribute
    {
    }
}