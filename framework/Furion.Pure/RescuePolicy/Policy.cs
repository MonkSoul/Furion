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
/// 异常策略静态类
/// </summary>
public static class Policy
{
    /// <summary>
    /// 添加自定义策略
    /// </summary>
    /// <typeparam name="TPolicy"><see cref="PolicyBase{TResult}"/></typeparam>
    /// <returns><typeparamref name="TPolicy"/></returns>
    public static TPolicy For<TPolicy>()
        where TPolicy : PolicyBase<object>, new()
    {
        return new();
    }

    /// <summary>
    /// 添加自定义策略
    /// </summary>
    /// <typeparam name="TPolicy"><see cref="PolicyBase{TResult}"/></typeparam>
    /// <param name="policy"><typeparamref name="TPolicy"/></param>
    /// <returns><typeparamref name="TPolicy"/></returns>
    public static TPolicy For<TPolicy>(TPolicy policy)
        where TPolicy : PolicyBase<object>
    {
        return policy;
    }

    /// <summary>
    /// 初始化重试策略（默认 3 次）
    /// </summary>
    /// <returns><see cref="RetryPolicy"/></returns>
    public static RetryPolicy Retry()
    {
        return Retry(3);
    }

    /// <summary>
    /// 初始化重试策略
    /// </summary>
    /// <param name="maxRetryCount">最大重试次数</param>
    /// <returns><see cref="RetryPolicy"/></returns>
    public static RetryPolicy Retry(int maxRetryCount)
    {
        return new(maxRetryCount);
    }

    /// <summary>
    /// 初始化超时策略（默认 10 秒）
    /// </summary>
    /// <remarks>
    /// <para>若需要测试同步阻塞，建议使用 <c>Task.Delay(...).Wait()</c> 替代 <c>Thread.Sleep(...)</c></para>
    /// </remarks>
    /// <returns><see cref="TimeoutPolicy"/></returns>
    public static TimeoutPolicy Timeout()
    {
        return Timeout(TimeSpan.FromSeconds(10));
    }

    /// <summary>
    /// 初始化超时策略
    /// </summary>
    /// <remarks>
    /// <para>若需要测试同步阻塞，建议使用 <c>Task.Delay(...).Wait()</c> 替代 <c>Thread.Sleep(...)</c></para>
    /// </remarks>
    /// <param name="timeout">超时时间（毫秒）</param>
    /// <returns><see cref="TimeoutPolicy"/></returns>
    public static TimeoutPolicy Timeout(double timeout)
    {
        return new(timeout);
    }

    /// <summary>
    /// 初始化超时策略
    /// </summary>
    /// <remarks>
    /// <para>若需要测试同步阻塞，建议使用 <c>Task.Delay(...).Wait()</c> 替代 <c>Thread.Sleep(...)</c></para>
    /// </remarks>
    /// <param name="timeout">超时时间</param>
    /// <returns><see cref="TimeoutPolicy"/></returns>
    public static TimeoutPolicy Timeout(TimeSpan timeout)
    {
        return new(timeout);
    }

    /// <summary>
    /// 初始化后备策略
    /// </summary>
    /// <returns><see cref="FallbackPolicy"/></returns>
    public static FallbackPolicy Fallback()
    {
        return new();
    }

    /// <summary>
    /// 初始化后备策略
    /// </summary>
    /// <param name="fallbackAction">后备操作方法</param>
    /// <returns><see cref="FallbackPolicy"/></returns>
    public static FallbackPolicy Fallback(Func<FallbackPolicyContext<object>, object> fallbackAction)
    {
        return new(fallbackAction);
    }

    /// <summary>
    /// 初始化后备策略
    /// </summary>
    /// <param name="fallbackAction">后备操作方法</param>
    /// <returns><see cref="FallbackPolicy"/></returns>
    public static FallbackPolicy Fallback(Action<FallbackPolicyContext<object>> fallbackAction)
    {
        return new(fallbackAction);
    }

    /// <summary>
    /// 初始化组合策略
    /// </summary>
    /// <returns><see cref="CompositePolicy"/></returns>
    public static CompositePolicy Composite()
    {
        return new();
    }

    /// <summary>
    /// 初始化组合策略
    /// </summary>
    /// <param name="policies">策略集合</param>
    /// <returns><see cref="CompositePolicy"/></returns>
    public static CompositePolicy Composite(params PolicyBase<object>[] policies)
    {
        return new(policies);
    }

    /// <summary>
    /// 初始化组合策略
    /// </summary>
    /// <param name="policies">策略集合</param>
    /// <returns><see cref="CompositePolicy"/></returns>
    public static CompositePolicy Composite(IEnumerable<PolicyBase<object>> policies)
    {
        return new(policies);
    }

    /// <summary>
    /// 并发锁策略
    /// </summary>
    /// <returns><see cref="LockPolicy"/></returns>
    public static LockPolicy Lock()
    {
        return new();
    }
}

/// <summary>
/// 异常策略静态类
/// </summary>
/// <typeparam name="TResult">操作返回值类型</typeparam>
public static class Policy<TResult>
{
    /// <summary>
    /// 添加自定义策略
    /// </summary>
    /// <typeparam name="TPolicy"><see cref="PolicyBase{TResult}"/></typeparam>
    /// <returns><typeparamref name="TPolicy"/></returns>
    public static TPolicy For<TPolicy>()
        where TPolicy : PolicyBase<TResult>, new()
    {
        return new();
    }

    /// <summary>
    /// 添加自定义策略
    /// </summary>
    /// <typeparam name="TPolicy"><see cref="PolicyBase{TResult}"/></typeparam>
    /// <param name="policy"><typeparamref name="TPolicy"/></param>
    /// <returns><typeparamref name="TPolicy"/></returns>
    public static TPolicy For<TPolicy>(TPolicy policy)
        where TPolicy : PolicyBase<TResult>
    {
        return policy;
    }

    /// <summary>
    /// 初始化重试策略（默认 3 次）
    /// </summary>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public static RetryPolicy<TResult> Retry()
    {
        return Retry(3);
    }

    /// <summary>
    /// 初始化重试策略
    /// </summary>
    /// <param name="maxRetryCount">最大重试次数</param>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public static RetryPolicy<TResult> Retry(int maxRetryCount)
    {
        return new(maxRetryCount);
    }

    /// <summary>
    /// 初始化超时策略（默认 10 秒）
    /// </summary>
    /// <remarks>
    /// <para>若需要测试同步阻塞，建议使用 <c>Task.Delay(...).Wait()</c> 替代 <c>Thread.Sleep(...)</c></para>
    /// </remarks>
    /// <returns><see cref="TimeoutPolicy{TResult}"/></returns>
    public static TimeoutPolicy<TResult> Timeout()
    {
        return Timeout(TimeSpan.FromSeconds(10));
    }

    /// <summary>
    /// 初始化超时策略
    /// </summary>
    /// <remarks>
    /// <para>若需要测试同步阻塞，建议使用 <c>Task.Delay(...).Wait()</c> 替代 <c>Thread.Sleep(...)</c></para>
    /// </remarks>
    /// <param name="timeout">超时时间（毫秒）</param>
    /// <returns><see cref="TimeoutPolicy{TResult}"/></returns>
    public static TimeoutPolicy<TResult> Timeout(double timeout)
    {
        return new(timeout);
    }

    /// <summary>
    /// 初始化超时策略
    /// </summary>
    /// <remarks>
    /// <para>若需要测试同步阻塞，建议使用 <c>Task.Delay(...).Wait()</c> 替代 <c>Thread.Sleep(...)</c></para>
    /// </remarks>
    /// <param name="timeout">超时时间</param>
    /// <returns><see cref="TimeoutPolicy{TResult}"/></returns>
    public static TimeoutPolicy<TResult> Timeout(TimeSpan timeout)
    {
        return new(timeout);
    }

    /// <summary>
    /// 初始化后备策略
    /// </summary>
    /// <returns><see cref="FallbackPolicy{TResult}"/></returns>
    public static FallbackPolicy<TResult> Fallback()
    {
        return new();
    }

    /// <summary>
    /// 初始化后备策略
    /// </summary>
    /// <param name="fallbackAction">后备操作方法</param>
    /// <returns><see cref="FallbackPolicy{TResult}"/></returns>
    public static FallbackPolicy<TResult> Fallback(Func<FallbackPolicyContext<TResult>, TResult> fallbackAction)
    {
        return new(fallbackAction);
    }

    /// <summary>
    /// 初始化后备策略
    /// </summary>
    /// <param name="fallbackAction">后备操作方法</param>
    /// <returns><see cref="FallbackPolicy{TResult}"/></returns>
    public static FallbackPolicy<TResult> Fallback(Action<FallbackPolicyContext<TResult>> fallbackAction)
    {
        return new(fallbackAction);
    }

    /// <summary>
    /// 初始化组合策略
    /// </summary>
    /// <returns><see cref="CompositePolicy{TResult}"/></returns>
    public static CompositePolicy<TResult> Composite()
    {
        return new();
    }

    /// <summary>
    /// 初始化组合策略
    /// </summary>
    /// <param name="policies">策略集合</param>
    /// <returns><see cref="CompositePolicy{TResult}"/></returns>
    public static CompositePolicy<TResult> Composite(params PolicyBase<TResult>[] policies)
    {
        return new(policies);
    }

    /// <summary>
    /// 初始化组合策略
    /// </summary>
    /// <param name="policies">策略集合</param>
    /// <returns><see cref="CompositePolicy{TResult}"/></returns>
    public static CompositePolicy<TResult> Composite(IEnumerable<PolicyBase<TResult>> policies)
    {
        return new(policies);
    }

    /// <summary>
    /// 初始化并发锁策略
    /// </summary>
    /// <returns><see cref="LockPolicy{TResult}"/></returns>
    public static LockPolicy<TResult> Lock()
    {
        return new();
    }
}