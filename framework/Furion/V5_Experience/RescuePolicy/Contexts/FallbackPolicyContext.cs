// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RescuePolicy;

/// <summary>
///     后备策略上下文
/// </summary>
/// <typeparam name="TResult">操作返回值类型</typeparam>
public sealed class FallbackPolicyContext<TResult> : PolicyContextBase
{
    /// <summary>
    ///     <inheritdoc cref="FallbackPolicyContext{TResult}" />
    /// </summary>
    internal FallbackPolicyContext()
    {
    }

    /// <inheritdoc cref="System.Exception" />
    public System.Exception? Exception { get; internal set; }

    /// <summary>
    ///     操作返回值
    /// </summary>
    public TResult? Result { get; internal set; }
}