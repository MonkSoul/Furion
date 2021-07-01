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

using Furion.Authorization;
using Furion.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.Authorization
{
    /// <summary>
    /// 策略授权特性
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AppAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="policies">多个策略</param>
        public AppAuthorizeAttribute(params string[] policies)
        {
            if (policies != null && policies.Length > 0) Policies = policies;
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