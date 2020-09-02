// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Microsoft.AspNetCore.Authorization;
using System;

namespace Fur.Authorization
{
    /// <summary>
    /// 策略授权特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AuthorizePolicyAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="policies">多个策略</param>
        public AuthorizePolicyAttribute(params string[] policies)
        {
            Policies = policies;
        }

        /// <summary>
        /// 策略
        /// </summary>
        public string[] Policies
        {
            get => Policy[Penetrates.AuthorizePolicyPrefix.Length..].Split(',', StringSplitOptions.RemoveEmptyEntries);
            set => Policy = $"{Penetrates.AuthorizePolicyPrefix}${string.Join(',', value)}";
        }
    }
}