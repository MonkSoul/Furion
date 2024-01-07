// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Authorization;

namespace Furion.Authorization;

/// <summary>
/// 策略对应的需求
/// </summary>
[SuppressSniffer]
public sealed class AppAuthorizeRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="policies"></param>
    public AppAuthorizeRequirement(params string[] policies)
    {
        Policies = policies;
    }

    /// <summary>
    /// 策略
    /// </summary>
    public string[] Policies { get; private set; }
}