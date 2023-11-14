// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司
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

namespace Furion.RescuePolicy;

/// <summary>
/// 组合策略
/// </summary>
[SuppressSniffer]
public sealed class CompositePolicy : CompositePolicy<object>
{
    /// <summary>
    /// <inheritdoc cref="CompositePolicy"/>
    /// </summary>
    public CompositePolicy()
        : base()
    {
    }

    /// <summary>
    /// <inheritdoc cref="CompositePolicy"/>
    /// </summary>
    /// <param name="policies">策略集合</param>
    public CompositePolicy(params PolicyBase<object>[] policies)
        : base(policies)
    {
    }

    /// <summary>
    /// <inheritdoc cref="CompositePolicy"/>
    /// </summary>
    /// <param name="policies">策略集合</param>
    public CompositePolicy(IEnumerable<PolicyBase<object>> policies)
        : base(policies)
    {
    }
}

/// <summary>
/// 组合策略
/// </summary>
/// <typeparam name="TResult">操作返回值类型</typeparam>
[SuppressSniffer]
public class CompositePolicy<TResult> : PolicyBase<TResult>
{
    /// <summary>
    /// <inheritdoc cref="CompositePolicy{TResult}"/>
    /// </summary>
    public CompositePolicy()
    {
        Policies = [];
    }

    /// <summary>
    /// <inheritdoc cref="CompositePolicy{TResult}"/>
    /// </summary>
    /// <param name="policies">策略集合</param>
    public CompositePolicy(params PolicyBase<TResult>[] policies)
        : this()
    {
        Join(policies);
    }

    /// <summary>
    /// <inheritdoc cref="CompositePolicy{TResult}"/>
    /// </summary>
    /// <param name="policies">策略集合</param>
    public CompositePolicy(IEnumerable<PolicyBase<TResult>> policies)
        : this()
    {
        Join(policies);
    }

    /// <summary>
    /// 策略集合
    /// </summary>
    public List<PolicyBase<TResult>> Policies { get; init; }

    /// <summary>
    /// 执行失败时操作方法
    /// </summary>
    public Action<CompositePolicyContext<TResult>> ExecutionFailureAction { get; set; }

    /// <summary>
    /// 添加策略
    /// </summary>
    /// <param name="policies">策略集合</param>
    /// <returns><see cref="CompositePolicy{TResult}"/></returns>
    public CompositePolicy<TResult> Join(params PolicyBase<TResult>[] policies)
    {
        // 检查策略集合合法性
        EnsureLegalData(policies);

        Policies.AddRange(policies);

        return this;
    }

    /// <summary>
    /// 添加策略
    /// </summary>
    /// <param name="policies">策略集合</param>
    /// <returns><see cref="CompositePolicy{TResult}"/></returns>
    public CompositePolicy<TResult> Join(IEnumerable<PolicyBase<TResult>> policies)
    {
        return Join(policies.ToArray());
    }

    /// <summary>
    /// 添加执行失败时操作方法
    /// </summary>
    /// <param name="executionFailureAction">执行失败时操作方法</param>
    /// <returns><see cref="CompositePolicy{TResult}"/></returns>
    public CompositePolicy<TResult> OnExecutionFailure(Action<CompositePolicyContext<TResult>> executionFailureAction)
    {
        // 空检查
        if (executionFailureAction is null) throw new ArgumentNullException(nameof(executionFailureAction));

        ExecutionFailureAction = executionFailureAction;

        return this;
    }

    /// <inheritdoc />
    public async override Task<TResult> ExecuteAsync(Func<Task<TResult>> operation, CancellationToken cancellationToken = default)
    {
        // 空检查
        if (operation is null) throw new ArgumentNullException(nameof(operation));

        // 检查策略集合合法性
        EnsureLegalData(Policies);

        // 检查是否配置了策略集合
        if (Policies is { Count: 0 })
        {
            return await operation();
        }

        // 生成异步操作方法级联委托
        var cascadeExecuteAsync = Policies
            .Select(p => new Func<Func<Task<TResult>>, CancellationToken, Task<TResult>>(p.ExecuteAsync))
            .Aggregate(ExecutePolicyChain);

        // 调用异步操作方法级联委托
        return await cascadeExecuteAsync(operation, cancellationToken);
    }

    /// <summary>
    /// 执行策略链
    /// </summary>
    /// <param name="previous"><see cref="Func{T1, T2, TResult}"/></param>
    /// <param name="current"><see cref="Func{T1, T2, TResult}"/></param>
    /// <returns><see cref="Func{T1, T2, TResult}"/></returns>
    internal Func<Func<Task<TResult>>, CancellationToken, Task<TResult>> ExecutePolicyChain(
        Func<Func<Task<TResult>>, CancellationToken, Task<TResult>> previous
        , Func<Func<Task<TResult>>, CancellationToken, Task<TResult>> current)
    {
        return async (opt, token) =>
        {
            object policy = null;
            try
            {
                // 执行前一个策略
                return await previous(async () =>
                {
                    try
                    {
                        // 执行当前策略
                        return await current(opt, token);
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
                ExecutionFailureAction?.Invoke(new((PolicyBase<TResult>)policy!)
                {
                    PolicyName = PolicyName,
                    Exception = exception
                });

                throw;
            }
        };
    }

    /// <summary>
    /// 检查策略集合合法性
    /// </summary>
    /// <param name="policies">策略集合</param>
    /// <exception cref="ArgumentException"></exception>
    internal static void EnsureLegalData(IEnumerable<PolicyBase<TResult>> policies)
    {
        // 空检查
        if (policies is null) throw new ArgumentNullException(nameof(policies));

        // 子项空检查
        if (policies.Any(policy => policy is null))
        {
            throw new ArgumentException("The policy collection contains a null value.", nameof(policies));
        }
    }
}