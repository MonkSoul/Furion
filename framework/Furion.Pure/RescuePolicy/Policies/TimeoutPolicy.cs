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

using System.Diagnostics.CodeAnalysis;

namespace Furion.RescuePolicy;

/// <summary>
/// 超时策略
/// </summary>
/// <remarks>
/// <para>若需要测试同步阻塞，建议使用 <c>Task.Delay(...).Wait()</c> 替代 <c>Thread.Sleep(...)</c></para>
/// </remarks>
[SuppressSniffer]
public sealed class TimeoutPolicy : TimeoutPolicy<object>
{
    /// <summary>
    /// <inheritdoc cref="TimeoutPolicy"/>
    /// </summary>
    public TimeoutPolicy()
        : base()
    {
    }

    /// <summary>
    /// <inheritdoc cref="TimeoutPolicy"/>
    /// </summary>
    /// <param name="timeout">超时时间（毫秒）</param>
    public TimeoutPolicy(double timeout)
        : base(timeout)
    {
    }

    /// <summary>
    /// <inheritdoc cref="TimeoutPolicy"/>
    /// </summary>
    /// <param name="timeout">超时时间</param>
    public TimeoutPolicy(TimeSpan timeout)
        : base(timeout)
    {
    }
}

/// <summary>
/// 超时策略
/// </summary>
/// <remarks>
/// <para>若需要测试同步阻塞，建议使用 <c>Task.Delay(...).Wait()</c> 替代 <c>Thread.Sleep(...)</c></para>
/// </remarks>
/// <typeparam name="TResult">操作返回值类型</typeparam>
[SuppressSniffer]
public class TimeoutPolicy<TResult> : PolicyBase<TResult>
{
    /// <summary>
    /// 超时输出信息
    /// </summary>
    internal const string TIMEOUT_MESSAGE = "The operation has timed out.";

    /// <summary>
    /// <inheritdoc cref="TimeoutPolicy{TResult}"/>
    /// </summary>
    public TimeoutPolicy()
    {
    }

    /// <summary>
    /// <inheritdoc cref="TimeoutPolicy{TResult}"/>
    /// </summary>
    /// <param name="timeout">超时时间（毫秒）</param>
    public TimeoutPolicy(double timeout)
    {
        Timeout = TimeSpan.FromMilliseconds(timeout);
    }

    /// <summary>
    /// <inheritdoc cref="TimeoutPolicy{TResult}"/>
    /// </summary>
    /// <param name="timeout">超时时间</param>
    public TimeoutPolicy(TimeSpan timeout)
    {
        Timeout = timeout;
    }

    /// <summary>
    /// 超时时间
    /// </summary>
    public TimeSpan Timeout { get; set; }

    /// <summary>
    /// 超时时操作方法
    /// </summary>
    public Action<TimeoutPolicyContext<TResult>> TimeoutAction { get; set; }

    /// <summary>
    /// 添加超时时操作方法
    /// </summary>
    /// <param name="timeoutAction">超时时操作方法</param>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public TimeoutPolicy<TResult> OnTimeout(Action<TimeoutPolicyContext<TResult>> timeoutAction)
    {
        // 空检查
        if (timeoutAction is null) throw new ArgumentNullException(nameof(timeoutAction));

        TimeoutAction = timeoutAction;

        return this;
    }

    /// <inheritdoc />
    public override TResult Execute(Func<TResult> operation, CancellationToken cancellationToken = default)
    {
        // 空检查
        if (operation is null) throw new ArgumentNullException(nameof(operation));

        return ExecuteAsync(() => Task.Run(() => operation()), cancellationToken)
            .GetAwaiter()
            .GetResult();
    }

    /// <inheritdoc />
    public async override Task<TResult> ExecuteAsync(Func<Task<TResult>> operation, CancellationToken cancellationToken = default)
    {
        // 空检查
        if (operation is null) throw new ArgumentNullException(nameof(operation));

        // 检查是否配置了超时时间
        if (Timeout == TimeSpan.Zero)
        {
            return await operation();
        }

        // 创建关键的取消标记
        using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        // 设置超时时间
        cancellationTokenSource.CancelAfter(Timeout);

        try
        {
            // 获取操作方法任务
            var operationTask = operation();

            // 获取提前完成的任务
            var completedTask = await Task.WhenAny(operationTask, Task.Delay(Timeout, cancellationTokenSource.Token));

            // 检查是否存在取消请求
            cancellationToken.ThrowIfCancellationRequested();

            // 检查提前完成的任务是否是操作方法任务
            if (completedTask == operationTask)
            {
                // 返回操作方法结果
                return await operationTask;
            }
            else
            {
                // 抛出超时异常
                ThrowTimeoutException();
            }
        }
        catch (OperationCanceledException exception) when (exception.CancellationToken == cancellationTokenSource.Token)
        {
            // 抛出超时异常
            ThrowTimeoutException();
        }

        return default;
    }

    /// <summary>
    /// 抛出超时异常
    /// </summary>
    /// <exception cref="TimeoutException"></exception>
    [DoesNotReturn]
    internal void ThrowTimeoutException()
    {
        // 调用重试时操作方法
        TimeoutAction?.Invoke(new()
        {
            PolicyName = PolicyName
        });

        // 抛出超时异常
        throw new TimeoutException(TIMEOUT_MESSAGE);
    }
}