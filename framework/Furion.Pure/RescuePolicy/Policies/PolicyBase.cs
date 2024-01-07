// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RescuePolicy;

/// <summary>
/// 策略抽象基类
/// </summary>
/// <typeparam name="TResult">操作返回值类型</typeparam>
[SuppressSniffer]
public abstract class PolicyBase<TResult> : IExceptionPolicy<TResult>
{
    /// <inheritdoc />
    public string PolicyName { get; set; }

    /// <inheritdoc />
    public virtual void Execute(Action operation, CancellationToken cancellationToken = default)
    {
        // 空检查
        if (operation is null) throw new ArgumentNullException(nameof(operation));

        // 执行同步操作方法
        Execute(() =>
        {
            operation();

            return default;
        }, cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task ExecuteAsync(Func<Task> operation, CancellationToken cancellationToken = default)
    {
        // 空检查
        if (operation is null) throw new ArgumentNullException(nameof(operation));

        // 执行异步操作方法
        await ExecuteAsync(async () =>
        {
            await operation();

            return default;
        }, cancellationToken);
    }

    /// <inheritdoc />
    public virtual TResult Execute(Func<TResult> operation, CancellationToken cancellationToken = default)
    {
        // 空检查
        if (operation is null) throw new ArgumentNullException(nameof(operation));

        return ExecuteAsync(() => Task.FromResult(operation()), cancellationToken)
            .GetAwaiter()
            .GetResult();
    }

    /// <inheritdoc />
    public abstract Task<TResult> ExecuteAsync(Func<Task<TResult>> operation, CancellationToken cancellationToken = default);
}