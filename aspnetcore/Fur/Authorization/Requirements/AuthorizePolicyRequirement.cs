using Microsoft.AspNetCore.Authorization;

namespace Fur.Authorization
{
    /// <summary>
    /// 策略对应的需求
    /// </summary>
    public sealed class AuthorizePolicyRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="policies"></param>
        public AuthorizePolicyRequirement(params string[] policies)
            => Policies = policies;

        /// <summary>
        /// 策略
        /// </summary>
        public string[] Policies { get; private set; }
    }
}