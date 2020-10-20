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
using Microsoft.AspNetCore.Authorization;
using System;

namespace Fur.Authorization
{
    /// <summary>
    /// 策略授权特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AppAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="policies">多个策略</param>
        public AppAuthorizeAttribute(params string[] policies)
        {
            Policies = policies;
        }

        /// <summary>
        /// 策略
        /// </summary>
        public string[] Policies
        {
            get => Policy[Penetrates.AppAuthorizePrefix.Length..].Split(',', StringSplitOptions.RemoveEmptyEntries);
            internal set => Policy = $"{Penetrates.AppAuthorizePrefix}{string.Join(',', value)}";
        }
    }
}