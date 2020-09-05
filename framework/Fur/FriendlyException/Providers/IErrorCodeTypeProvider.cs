// --------------------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur 
// 开源协议：Apache-2.0（https://gitee.com/monksoul/Fur/blob/alpha/LICENSE）
// --------------------------------------------------------------------------------------

using System;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 异常错误代码提供器
    /// </summary>
    public interface IErrorCodeTypeProvider
    {
        /// <summary>
        /// 错误代码定义类型
        /// </summary>
        Type[] Definitions { get; }
    }
}