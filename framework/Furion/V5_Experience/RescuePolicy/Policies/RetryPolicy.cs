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
///     重试策略
/// </summary>
public sealed class RetryPolicy : RetryPolicy<object>
{
    /// <summary>
    ///     <inheritdoc cref="RetryPolicy" />
    /// </summary>
    public RetryPolicy()
    {
    }

    /// <summary>
    ///     <inheritdoc cref="RetryPolicy" />
    /// </summary>
    /// <param name="maxRetryCount">最大重试次数</param>
    public RetryPolicy(int maxRetryCount)
        : base(maxRetryCount)
    {
    }
}

/// <summary>
///     重试策略
/// </summary>
/// <typeparam name="TResult">操作返回值类型</typeparam>
public class RetryPolicy<TResult> : PolicyBase<TResult>
{
    /// <summary>
    ///     等待重试输出信息
    /// </summary>
    internal const string WAIT_RETRY_MESSAGE = "Retry after {0} seconds.";

    /// <summary>
    ///     重试输出信息
    /// </summary>
    internal const string RETRY_MESSAGE = "Retrying for the {0}nd time.";

    /// <summary>
    ///     <inheritdoc cref="RetryPolicy{TResult}" />
    /// </summary>
    public RetryPolicy()
    {
    }

    /// <summary>
    ///     <inheritdoc cref="RetryPolicy{TResult}" />
    /// </summary>
    /// <param name="maxRetryCount">最大重试次数</param>
    public RetryPolicy(int maxRetryCount) => MaxRetryCount = maxRetryCount;

    /// <summary>
    ///     最大重试次数
    /// </summary>
    public int MaxRetryCount { get; set; }

    /// <summary>
    ///     重试等待时间集合
    /// </summary>
    public TimeSpan[]? RetryIntervals { get; set; }

    /// <summary>
    ///     捕获的异常集合
    /// </summary>
    public HashSet<Type>? HandleExceptions { get; set; }

    /// <summary>
    ///     捕获的内部异常集合
    /// </summary>
    public HashSet<Type>? HandleInnerExceptions { get; set; }

    /// <summary>
    ///     操作结果条件集合
    /// </summary>
    public List<Func<RetryPolicyContext<TResult>, bool>>? ResultConditions { get; set; }

    /// <summary>
    ///     等待重试时操作方法
    /// </summary>
    public Action<RetryPolicyContext<TResult>, TimeSpan>? WaitRetryAction { get; set; }

    /// <summary>
    ///     重试时操作方法
    /// </summary>
    public Action<RetryPolicyContext<TResult>>? RetryingAction { get; set; }

    /// <summary>
    ///     添加捕获异常类型
    /// </summary>
    /// <typeparam name="TException">
    ///     <see cref="System.Exception" />
    /// </typeparam>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> Handle<TException>()
        where TException : System.Exception
    {
        HandleExceptions ??= [];
        HandleExceptions.Add(typeof(TException));

        return this;
    }

    /// <summary>
    ///     添加捕获异常类型
    /// </summary>
    /// <typeparam name="TException">
    ///     <see cref="System.Exception" />
    /// </typeparam>
    /// <param name="exceptionCondition">异常条件</param>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> Handle<TException>(Func<TException, bool> exceptionCondition)
        where TException : System.Exception
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(exceptionCondition);

        // 添加捕获异常类型和条件
        Handle<TException>();
        HandleResult(context => context.Exception is TException exception && exceptionCondition(exception));

        return this;
    }

    /// <summary>
    ///     添加捕获异常类型
    /// </summary>
    /// <typeparam name="TException">
    ///     <see cref="System.Exception" />
    /// </typeparam>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> Or<TException>()
        where TException : System.Exception =>
        Handle<TException>();

    /// <summary>
    ///     添加捕获异常类型
    /// </summary>
    /// <typeparam name="TException">
    ///     <see cref="System.Exception" />
    /// </typeparam>
    /// <param name="exceptionCondition">异常条件</param>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> Or<TException>(Func<TException, bool> exceptionCondition)
        where TException : System.Exception =>
        Handle(exceptionCondition);

    /// <summary>
    ///     添加捕获内部异常类型
    /// </summary>
    /// <typeparam name="TException">
    ///     <see cref="System.Exception" />
    /// </typeparam>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> HandleInner<TException>()
        where TException : System.Exception
    {
        HandleInnerExceptions ??= [];
        HandleInnerExceptions.Add(typeof(TException));

        return this;
    }

    /// <summary>
    ///     添加捕获内部异常类型
    /// </summary>
    /// <typeparam name="TException">
    ///     <see cref="System.Exception" />
    /// </typeparam>
    /// <param name="exceptionCondition">异常条件</param>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> HandleInner<TException>(Func<TException, bool> exceptionCondition)
        where TException : System.Exception
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(exceptionCondition);

        // 添加捕获内部异常类型和条件
        HandleInner<TException>();
        HandleResult(context =>
            context.Exception?.InnerException is TException exception && exceptionCondition(exception));

        return this;
    }

    /// <summary>
    ///     添加捕获内部异常类型
    /// </summary>
    /// <typeparam name="TException">
    ///     <see cref="System.Exception" />
    /// </typeparam>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> OrInner<TException>()
        where TException : System.Exception =>
        HandleInner<TException>();

    /// <summary>
    ///     添加捕获内部异常类型
    /// </summary>
    /// <typeparam name="TException">
    ///     <see cref="System.Exception" />
    /// </typeparam>
    /// <param name="exceptionCondition">异常条件</param>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> OrInner<TException>(Func<TException, bool> exceptionCondition)
        where TException : System.Exception =>
        HandleInner(exceptionCondition);

    /// <summary>
    ///     添加操作结果条件
    /// </summary>
    /// <param name="resultCondition">操作结果条件</param>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> HandleResult(Func<RetryPolicyContext<TResult>, bool> resultCondition)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(resultCondition);

        ResultConditions ??= [];
        ResultConditions.Add(resultCondition);

        return this;
    }

    /// <summary>
    ///     添加操作结果条件
    /// </summary>
    /// <param name="resultCondition">操作结果条件</param>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> OrResult(Func<RetryPolicyContext<TResult>, bool> resultCondition) =>
        HandleResult(resultCondition);

    /// <summary>
    ///     添加重试等待时间
    /// </summary>
    /// <param name="retryIntervals">重试等待时间</param>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> WaitAndRetry(params TimeSpan[] retryIntervals)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(retryIntervals);

        RetryIntervals = retryIntervals;

        return this;
    }

    /// <summary>
    ///     永久重试
    /// </summary>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> Forever()
    {
        MaxRetryCount = int.MaxValue;

        return this;
    }

    /// <summary>
    ///     永久重试并添加重试等待时间
    /// </summary>
    /// <param name="retryIntervals">重试等待时间</param>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> WaitAndRetryForever(params TimeSpan[] retryIntervals) =>
        WaitAndRetry(retryIntervals)
            .Forever();

    /// <summary>
    ///     添加等待重试时操作方法
    /// </summary>
    /// <param name="waitRetryAction">等待重试时操作方法</param>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> OnWaitRetry(Action<RetryPolicyContext<TResult>, TimeSpan> waitRetryAction)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(waitRetryAction);

        WaitRetryAction = waitRetryAction;

        return this;
    }

    /// <summary>
    ///     添加重试时操作方法
    /// </summary>
    /// <param name="retryingAction">重试时操作方法</param>
    /// <returns>
    ///     <see cref="RetryPolicy{TResult}" />
    /// </returns>
    public RetryPolicy<TResult> OnRetrying(Action<RetryPolicyContext<TResult>> retryingAction)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(retryingAction);

        RetryingAction = retryingAction;

        return this;
    }

    /// <summary>
    ///     检查是否满足捕获异常的条件
    /// </summary>
    /// <param name="context">
    ///     <see cref="RetryPolicyContext{TResult}" />
    /// </param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    internal bool ShouldHandle(RetryPolicyContext<TResult> context)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(context);

        // 检查最大重试次数是否大于等于 0
        if (MaxRetryCount <= 0)
        {
            return false;
        }

        // 检查重试次数是否大于最大重试次数减
        if (context.RetryCount > MaxRetryCount - 1)
        {
            return false;
        }

        // 检查是否满足捕获异常的条件
        if (CanHandleException(context, HandleExceptions, context.Exception)
            || CanHandleException(context, HandleInnerExceptions, context.Exception?.InnerException))
        {
            return true;
        }

        // 检查是否满足操作结果条件
        return ResultConditions is { Count: > 0 } && ResultConditions.Any(condition => condition(context));
    }

    /// <inheritdoc />
    public override async Task<TResult?> ExecuteAsync(Func<CancellationToken, Task<TResult?>> operation,
        CancellationToken cancellationToken = default)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(operation);

        // 初始化重试策略上下文
        var context = new RetryPolicyContext<TResult> { PolicyName = PolicyName };

        // 无限循环直到满足条件退出
        while (true)
        {
            try
            {
                // 获取操作方法执行结果
                context.Result = await operation(cancellationToken);
            }
            catch (System.Exception exception)
            {
                // 设置策略上下文异常信息
                context.Exception = exception;
            }

            // 检查是否存在取消请求
            cancellationToken.ThrowIfCancellationRequested();

            // 检查是否满足捕获异常的条件
            if (!ShouldHandle(context))
            {
                // 返回结果或抛出异常
                return ReturnOrThrowIfException(context);
            }

            // 递增上下文数据
            context.Increment();

            // 检查是否配置了重试时间
            if (RetryIntervals is { Length: > 0 })
            {
                // 解析延迟时间戳
                var delay = RetryIntervals[context.RetryCount % RetryIntervals.Length];

                // 输出调试事件
                Debugging.Info(WAIT_RETRY_MESSAGE, delay.TotalSeconds);

                // 调用等待重试时操作方法
                WaitRetryAction?.Invoke(context, delay);

                // 延迟指定时间再操作
                await Task.Delay(delay, cancellationToken);
            }

            // 输出调试事件
            Debugging.Warn(RETRY_MESSAGE, context.RetryCount);

            // 调用重试时操作方法
            RetryingAction?.Invoke(context);
        }
    }

    /// <summary>
    ///     检查是否满足捕获异常的条件
    /// </summary>
    /// <param name="context">
    ///     <see cref="RetryPolicyContext{TResult}" />
    /// </param>
    /// <param name="exceptionTypes">捕获异常类型集合</param>
    /// <param name="exception">
    ///     <see cref="System.Exception" />
    /// </param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    internal bool CanHandleException(RetryPolicyContext<TResult> context
        , HashSet<Type>? exceptionTypes
        , System.Exception? exception)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(context);

        // 空检查
        if (exception is null)
        {
            return false;
        }

        // 检查是否满足捕获异常的条件
        if (exceptionTypes is not (null or { Count: 0 })
            && !exceptionTypes.Any(ex => ex.IsInstanceOfType(exception)))
        {
            return false;
        }

        // 检查是否满足操作结果条件
        return ResultConditions is not { Count: > 0 } || ResultConditions.Any(condition => condition(context));
    }

    /// <summary>
    ///     返回结果或抛出异常
    /// </summary>
    /// <param name="context">
    ///     <see cref="RetryPolicyContext{TResult}" />
    /// </param>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    internal static TResult? ReturnOrThrowIfException(RetryPolicyContext<TResult> context)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(context);

        // 空检查
        if (context.Exception is not null)
        {
            throw context.Exception;
        }

        return context.Result;
    }
}