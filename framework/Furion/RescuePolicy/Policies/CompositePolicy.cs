// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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