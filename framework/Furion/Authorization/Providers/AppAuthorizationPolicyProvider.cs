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

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Furion.Authorization
{
    /// <summary>
    /// 授权策略提供器
    /// </summary>
    internal sealed class AppAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        /// <summary>
        /// 默认回退策略
        /// </summary>
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public AppAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        /// <summary>
        /// 获取默认策略
        /// </summary>
        /// <returns></returns>
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return FallbackPolicyProvider.GetDefaultPolicyAsync();
        }

        /// <summary>
        /// 获取回退策略
        /// </summary>
        /// <returns></returns>
        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            return FallbackPolicyProvider.GetFallbackPolicyAsync();
        }

        /// <summary>
        /// 获取策略
        /// </summary>
        /// <param name="policyName"></param>
        /// <returns></returns>
        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            // 判断是否是包含授权策略前缀
            if (policyName.StartsWith(Penetrates.AppAuthorizePrefix))
            {
                // 解析策略名并获取策略参数
                var policies = policyName[Penetrates.AppAuthorizePrefix.Length..].Split(',', StringSplitOptions.RemoveEmptyEntries);

                // 添加策略需求
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new AppAuthorizeRequirement(policies));

                return Task.FromResult(policy.Build());
            }

            // 如果策略不匹配，则返回回退策略
            return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}