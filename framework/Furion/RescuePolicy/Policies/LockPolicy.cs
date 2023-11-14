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
/// 并发锁策略
/// </summary>
[SuppressSniffer]
public sealed class LockPolicy : LockPolicy<object>
{
    /// <summary>
    /// <inheritdoc cref="LockPolicy"/>
    /// </summary>
    public LockPolicy()
        : base()
    {
    }
}

/// <summary>
/// 并发锁策略
/// </summary>
/// <typeparam name="TResult">操作返回值类型</typeparam>
[SuppressSniffer]
public class LockPolicy<TResult> : PolicyBase<TResult>
{
    /// <summary>
    /// 同步锁对象
    /// </summary>
    internal readonly object _syncLock = new();

    /// <summary>
    /// 异步锁对象
    /// </summary>
    internal readonly SemaphoreSlim _asyncLock = new(1);

    /// <summary>
    /// <inheritdoc cref="LockPolicy{TResult}"/>
    /// </summary>
    public LockPolicy()
    {
    }

    /// <inheritdoc />
    public override TResult Execute(Func<TResult> operation, CancellationToken cancellationToken = default)
    {
        // 空检查
        if (operation is null) throw new ArgumentNullException(nameof(operation));

        // 对同步锁对象进行加锁，确保同一时间只有一个线程可以进入同步代码块
        lock (_syncLock)
        {
            // 执行操作方法并返回
            return operation();
        }
    }

    /// <inheritdoc />
    public async override Task<TResult> ExecuteAsync(Func<Task<TResult>> operation, CancellationToken cancellationToken = default)
    {
        // 空检查
        if (operation is null) throw new ArgumentNullException(nameof(operation));

        // 获取异步锁，确保同一时间只有一个异步操作可以进入异步代码块
        await _asyncLock.WaitAsync(cancellationToken);

        try
        {
            // 执行操作方法并返回
            return await operation();
        }
        finally
        {
            // 释放异步锁
            _asyncLock.Release();
        }
    }
}