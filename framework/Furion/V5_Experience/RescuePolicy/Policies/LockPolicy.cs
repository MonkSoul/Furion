// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RescuePolicy;

/// <summary>
///     并发锁策略
/// </summary>
public sealed class LockPolicy : LockPolicy<object>
{
    /// <summary>
    ///     <inheritdoc cref="LockPolicy" />
    /// </summary>
    public LockPolicy()
    {
    }
}

/// <summary>
///     并发锁策略
/// </summary>
/// <typeparam name="TResult">操作返回值类型</typeparam>
public class LockPolicy<TResult> : PolicyBase<TResult>
{
    /// <summary>
    ///     异步锁对象
    /// </summary>
    internal readonly SemaphoreSlim _asyncLock = new(1);

    /// <summary>
    ///     同步锁对象
    /// </summary>
    internal readonly object _syncLock = new();

    /// <summary>
    ///     <inheritdoc cref="LockPolicy{TResult}" />
    /// </summary>
    public LockPolicy()
    {
    }

    /// <inheritdoc />
    public override TResult? Execute(Func<TResult?> operation,
        CancellationToken cancellationToken = default)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(operation);

        // 对同步锁对象进行加锁，确保同一时间只有一个线程可以进入同步代码块
        lock (_syncLock)
        {
            // 执行操作方法并返回
            return operation();
        }
    }

    /// <inheritdoc />
    public override TResult? Execute(Func<CancellationToken, TResult?> operation,
        CancellationToken cancellationToken = default)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(operation);

        // 对同步锁对象进行加锁，确保同一时间只有一个线程可以进入同步代码块
        lock (_syncLock)
        {
            // 执行操作方法并返回
            return operation(cancellationToken);
        }
    }

    /// <inheritdoc />
    public override async Task<TResult?> ExecuteAsync(Func<CancellationToken, Task<TResult?>> operation,
        CancellationToken cancellationToken = default)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(operation);

        // 获取异步锁，确保同一时间只有一个异步操作可以进入异步代码块
        await _asyncLock.WaitAsync(cancellationToken);

        try
        {
            // 执行操作方法并返回
            return await operation(cancellationToken);
        }
        finally
        {
            // 释放异步锁
            _asyncLock.Release();
        }
    }
}