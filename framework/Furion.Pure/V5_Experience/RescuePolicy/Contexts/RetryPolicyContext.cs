// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RescuePolicy;

/// <summary>
///     重试策略上下文
/// </summary>
/// <typeparam name="TResult">操作返回值类型</typeparam>
public sealed class RetryPolicyContext<TResult> : PolicyContextBase
{
    /// <summary>
    ///     <inheritdoc cref="RetryPolicyContext{TResult}" />
    /// </summary>
    internal RetryPolicyContext()
    {
    }

    /// <inheritdoc cref="System.Exception" />
    public System.Exception? Exception { get; internal set; }

    /// <summary>
    ///     操作返回值
    /// </summary>
    public TResult? Result { get; internal set; }

    /// <summary>
    ///     当前重试次数
    /// </summary>
    public int RetryCount { get; internal set; }

    /// <summary>
    ///     附加属性
    /// </summary>
    public IDictionary<object, object?>? Properties { get; set; }

    /// <summary>
    ///     递增上下文数据
    /// </summary>
    internal void Increment() => RetryCount++;
}