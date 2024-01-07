// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RescuePolicy;

/// <summary>
/// 组合策略上下文
/// </summary>
/// <typeparam name="TResult">操作返回值类型</typeparam>
[SuppressSniffer]
public sealed class CompositePolicyContext<TResult> : PolicyContextBase
{
    /// <summary>
    /// <inheritdoc cref="CompositePolicyContext{TResult}"/>
    /// </summary>
    /// <param name="policy"><see cref="PolicyBase{TResult}"/></param>
    internal CompositePolicyContext(PolicyBase<TResult> policy)
    {
        // 空检查
        if (policy is null) throw new ArgumentNullException(nameof(policy));

        Policy = policy;
    }

    /// <inheritdoc cref="PolicyBase{TResult}" />
    public PolicyBase<TResult> Policy { get; init; }

    /// <inheritdoc cref="System.Exception"/>
    public System.Exception Exception { get; internal set; }
}