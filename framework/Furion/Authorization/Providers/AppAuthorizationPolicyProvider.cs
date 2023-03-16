// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Furion.Authorization;

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