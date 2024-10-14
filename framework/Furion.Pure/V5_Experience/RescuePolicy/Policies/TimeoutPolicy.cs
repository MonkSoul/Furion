// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Diagnostics.CodeAnalysis;

namespace Furion.RescuePolicy;

/// <summary>
///     超时策略
/// </summary>
/// <remarks>
///     <para>若需要测试同步阻塞，建议使用 <c>Task.Delay(...).Wait()</c> 替代 <c>Thread.Sleep(...)</c></para>
/// </remarks>
public sealed class TimeoutPolicy : TimeoutPolicy<object>
{
    /// <summary>
    ///     <inheritdoc cref="TimeoutPolicy" />
    /// </summary>
    public TimeoutPolicy()
    {
    }

    /// <summary>
    ///     <inheritdoc cref="TimeoutPolicy" />
    /// </summary>
    /// <param name="timeout">超时时间（毫秒）</param>
    public TimeoutPolicy(double timeout)
        : base(timeout)
    {
    }

    /// <summary>
    ///     <inheritdoc cref="TimeoutPolicy" />
    /// </summary>
    /// <param name="timeout">超时时间</param>
    public TimeoutPolicy(TimeSpan timeout)
        : base(timeout)
    {
    }
}

/// <summary>
///     超时策略
/// </summary>
/// <remarks>
///     <para>若需要测试同步阻塞，建议使用 <c>Task.Delay(...).Wait()</c> 替代 <c>Thread.Sleep(...)</c></para>
/// </remarks>
/// <typeparam name="TResult">操作返回值类型</typeparam>
public class TimeoutPolicy<TResult> : PolicyBase<TResult>
{
    /// <summary>
    ///     超时输出信息
    /// </summary>
    internal const string TIMEOUT_MESSAGE = "The operation has timed out.";

    /// <summary>
    ///     <inheritdoc cref="TimeoutPolicy{TResult}" />
    /// </summary>
    public TimeoutPolicy()
    {
    }

    /// <summary>
    ///     <inheritdoc cref="TimeoutPolicy{TResult}" />
    /// </summary>
    /// <param name="timeout">超时时间（毫秒）</param>
    public TimeoutPolicy(double timeout) => Timeout = TimeSpan.FromMilliseconds(timeout);

    /// <summary>
    ///     <inheritdoc cref="TimeoutPolicy{TResult}" />
    /// </summary>
    /// <param name="timeout">超时时间</param>
    public TimeoutPolicy(TimeSpan timeout) => Timeout = timeout;

    /// <summary>
    ///     超时时间
    /// </summary>
    public TimeSpan Timeout { get; set; }

    /// <summary>
    ///     超时时操作方法
    /// </summary>
    public Action<TimeoutPolicyContext<TResult>>? TimeoutAction { get; set; }

    /// <summary>
    ///     添加超时时操作方法
    /// </summary>
    /// <param name="timeoutAction">超时时操作方法</param>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public TimeoutPolicy<TResult> OnTimeout(Action<TimeoutPolicyContext<TResult>> timeoutAction)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(timeoutAction);

        TimeoutAction = timeoutAction;

        return this;
    }

    /// <inheritdoc />
    public override TResult? Execute(Func<TResult?> operation,
        CancellationToken cancellationToken = default)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(operation);

        return ExecuteAsync(() => Task.Run(operation, cancellationToken), cancellationToken)
            .GetAwaiter()
            .GetResult();
    }

    /// <inheritdoc />
    public override TResult? Execute(Func<CancellationToken, TResult?> operation,
        CancellationToken cancellationToken = default)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(operation);

        return ExecuteAsync(() => Task.Run(() => operation(cancellationToken), cancellationToken), cancellationToken)
            .GetAwaiter()
            .GetResult();
    }

    /// <inheritdoc />
    public override async Task<TResult?> ExecuteAsync(Func<CancellationToken, Task<TResult?>> operation,
        CancellationToken cancellationToken = default)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(operation);

        // 检查是否配置了超时时间
        if (Timeout == TimeSpan.Zero)
        {
            return await operation(cancellationToken);
        }

        // 创建关联的取消标识
        using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        var stoppingToken = cancellationTokenSource.Token;

        // 设置超时时间
        cancellationTokenSource.CancelAfter(Timeout);

        try
        {
            // 获取操作方法任务
            var operationTask = operation(stoppingToken);

            // 获取提前完成的任务
            var completedTask = await Task.WhenAny(operationTask,
                Task.Delay(System.Threading.Timeout.InfiniteTimeSpan, stoppingToken));

            // 检查是否存在取消请求
            cancellationToken.ThrowIfCancellationRequested();

            // 检查提前完成的任务是否是操作方法任务
            if (completedTask == operationTask)
            {
                // 返回操作方法结果
                return await operationTask;
            }

            // 抛出超时异常
            ThrowTimeoutException();
        }
        catch (OperationCanceledException exception) when (exception.CancellationToken == stoppingToken)
        {
            // 抛出超时异常
            ThrowTimeoutException();
        }

        return default;
    }

    /// <summary>
    ///     抛出超时异常
    /// </summary>
    /// <exception cref="TimeoutException"></exception>
    [DoesNotReturn]
    internal void ThrowTimeoutException()
    {
        // 输出调试事件
        Debugging.Error(TIMEOUT_MESSAGE);

        // 调用重试时操作方法
        TimeoutAction?.Invoke(new TimeoutPolicyContext<TResult> { PolicyName = PolicyName });

        // 抛出超时异常
        throw new TimeoutException(TIMEOUT_MESSAGE);
    }
}