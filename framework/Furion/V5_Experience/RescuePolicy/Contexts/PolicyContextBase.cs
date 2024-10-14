// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RescuePolicy;

/// <summary>
///     策略上下文抽象基类
/// </summary>
public abstract class PolicyContextBase
{
    /// <summary>
    ///     策略名称
    /// </summary>
    public string? PolicyName { get; set; }
}