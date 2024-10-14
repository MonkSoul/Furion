// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RescuePolicy;

/// <summary>
///     组合策略
/// </summary>
public sealed class CompositePolicy : CompositePolicy<object>
{
    /// <summary>
    ///     <inheritdoc cref="CompositePolicy" />
    /// </summary>
    public CompositePolicy()
    {
    }

    /// <summary>
    ///     <inheritdoc cref="CompositePolicy" />
    /// </summary>
    /// <param name="policies">策略集合</param>
    public CompositePolicy(params PolicyBase<object>[] policies)
        : base(policies)
    {
    }

    /// <summary>
    ///     <inheritdoc cref="CompositePolicy" />
    /// </summary>
    /// <param name="policies">策略集合</param>
    public CompositePolicy(IEnumerable<PolicyBase<object>> policies)
        : base(policies)
    {
    }
}

/// <summary>
///     组合策略
/// </summary>
/// <typeparam name="TResult">操作返回值类型</typeparam>
public class CompositePolicy<TResult> : PolicyBase<TResult>
{
    /// <summary>
    ///     <inheritdoc cref="CompositePolicy{TResult}" />
    /// </summary>
    public CompositePolicy() => Policies = [];

    /// <summary>
    ///     <inheritdoc cref="CompositePolicy{TResult}" />
    /// </summary>
    /// <param name="policies">策略集合</param>
    public CompositePolicy(params PolicyBase<TResult>[] policies)
        : this() =>
        Join(policies);

    /// <summary>
    ///     <inheritdoc cref="CompositePolicy{TResult}" />
    /// </summary>
    /// <param name="policies">策略集合</param>
    public CompositePolicy(IEnumerable<PolicyBase<TResult>> policies)
        : this() =>
        Join(policies);

    /// <summary>
    ///     策略集合
    /// </summary>
    public List<PolicyBase<TResult>> Policies { get; init; }

    /// <summary>
    ///     执行失败时操作方法
    /// </summary>
    public Action<CompositePolicyContext<TResult>>? ExecutionFailureAction { get; set; }

    /// <summary>
    ///     添加策略
    /// </summary>
    /// <param name="policies">策略集合</param>
    /// <returns>
    ///     <see cref="CompositePolicy{TResult}" />
    /// </returns>
    public CompositePolicy<TResult> Join(params PolicyBase<TResult>[] policies)
    {
        // 检查策略集合合法性
        EnsureLegalData(policies);

        Policies.AddRange(policies);

        return this;
    }

    /// <summary>
    ///     添加策略
    /// </summary>
    /// <param name="policies">策略集合</param>
    /// <returns>
    ///     <see cref="CompositePolicy{TResult}" />
    /// </returns>
    public CompositePolicy<TResult> Join(IEnumerable<PolicyBase<TResult>> policies) => Join(policies.ToArray());

    /// <summary>
    ///     添加执行失败时操作方法
    /// </summary>
    /// <param name="executionFailureAction">执行失败时操作方法</param>
    /// <returns>
    ///     <see cref="CompositePolicy{TResult}" />
    /// </returns>
    public CompositePolicy<TResult> OnExecutionFailure(Action<CompositePolicyContext<TResult>> executionFailureAction)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(executionFailureAction);

        ExecutionFailureAction = executionFailureAction;

        return this;
    }

    /// <inheritdoc />
    public override async Task<TResult?> ExecuteAsync(Func<CancellationToken, Task<TResult?>> operation,
        CancellationToken cancellationToken = default)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(operation);

        // 检查策略集合合法性
        EnsureLegalData(Policies);

        // 检查是否配置了策略集合
        if (Policies is { Count: 0 })
        {
            return await operation(cancellationToken);
        }

        // 生成异步操作方法级联委托
        var cascadeExecuteAsync = Policies
            .Select(p =>
                new Func<Func<CancellationToken, Task<TResult?>>, CancellationToken, Task<TResult?>>(p.ExecuteAsync))
            .Aggregate(ExecutePolicyChain);

        // 调用异步操作方法级联委托
        return await cascadeExecuteAsync(operation, cancellationToken);
    }

    /// <summary>
    ///     执行策略链
    /// </summary>
    /// <param name="previous">
    ///     <see cref="Func{T1, T2, TResult}" />
    /// </param>
    /// <param name="current">
    ///     <see cref="Func{T1, T2, TResult}" />
    /// </param>
    /// <returns>
    ///     <see cref="Func{T1, T2, TResult}" />
    /// </returns>
    internal Func<Func<CancellationToken, Task<TResult?>>, CancellationToken, Task<TResult?>> ExecutePolicyChain(
        Func<Func<CancellationToken, Task<TResult?>>, CancellationToken, Task<TResult?>> previous
        , Func<Func<CancellationToken, Task<TResult?>>, CancellationToken, Task<TResult?>> current) =>
        async (opt, token) =>
        {
            object? policy = null;
            try
            {
                // 执行前一个策略
                return await previous(async outerToken =>
                {
                    try
                    {
                        // 执行当前策略
                        return await current(opt, outerToken);
                    }
                    // 检查内部策略是否已被取消
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (System.Exception)
                    {
                        // 记录执行异常的策略
                        policy ??= current.Target;

                        throw;
                    }
                }, token);
            }
            // 检查内部策略是否已被取消
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (System.Exception exception)
            {
                // 记录执行异常的策略
                policy ??= previous.Target;

                // 调用执行失败时操作方法
                ExecutionFailureAction?.Invoke(new CompositePolicyContext<TResult>((PolicyBase<TResult>)policy!)
                {
                    PolicyName = PolicyName, Exception = exception
                });

                throw;
            }
        };

    /// <summary>
    ///     检查策略集合合法性
    /// </summary>
    /// <param name="policies">策略集合</param>
    /// <exception cref="ArgumentException"></exception>
    internal static void EnsureLegalData(IEnumerable<PolicyBase<TResult>?> policies)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(policies);

        // 子项空检查
        if (policies.Any(policy => policy is null))
        {
            throw new ArgumentException("The policy collection contains a null value.", nameof(policies));
        }
    }
}