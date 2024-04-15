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