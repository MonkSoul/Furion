// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RescuePolicy;

/// <summary>
/// 异常策略服务
/// </summary>
/// <typeparam name="TResult">操作返回值类型</typeparam>
public interface IExceptionPolicy<TResult>
{
    /// <summary>
    /// 策略名称
    /// </summary>
    string PolicyName { get; set; }

    /// <summary>
    /// 执行同步操作方法
    /// </summary>
    /// <param name="operation">操作方法</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    void Execute(Action operation, CancellationToken cancellationToken = default);

    /// <summary>
    /// 执行异步操作方法
    /// </summary>
    /// <param name="operation">操作方法</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task{TResult}"/></returns>
    Task ExecuteAsync(Func<Task> operation, CancellationToken cancellationToken = default);

    /// <summary>
    /// 执行同步操作方法
    /// </summary>
    /// <param name="operation">操作方法</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><typeparamref name="TResult"/></returns>
    TResult Execute(Func<TResult> operation, CancellationToken cancellationToken = default);

    /// <summary>
    /// 执行异步操作方法
    /// </summary>
    /// <param name="operation">操作方法</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task{TResult}"/></returns>
    Task<TResult> ExecuteAsync(Func<Task<TResult>> operation, CancellationToken cancellationToken = default);
}