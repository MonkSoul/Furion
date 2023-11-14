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
/// 重试策略
/// </summary>
[SuppressSniffer]
public sealed class RetryPolicy : RetryPolicy<object>
{
    /// <summary>
    /// <inheritdoc cref="RetryPolicy"/>
    /// </summary>
    public RetryPolicy()
        : base()
    {
    }

    /// <summary>
    /// <inheritdoc cref="RetryPolicy"/>
    /// </summary>
    /// <param name="maxRetryCount">最大重试次数</param>
    public RetryPolicy(int maxRetryCount)
        : base(maxRetryCount)
    {
    }
}

/// <summary>
/// 重试策略
/// </summary>
/// <typeparam name="TResult">操作返回值类型</typeparam>
[SuppressSniffer]
public class RetryPolicy<TResult> : PolicyBase<TResult>
{
    /// <summary>
    /// <inheritdoc cref="RetryPolicy{TResult}"/>
    /// </summary>
    public RetryPolicy()
    {
    }

    /// <summary>
    /// <inheritdoc cref="RetryPolicy{TResult}"/>
    /// </summary>
    /// <param name="maxRetryCount">最大重试次数</param>
    public RetryPolicy(int maxRetryCount)
    {
        MaxRetryCount = maxRetryCount;
    }

    /// <summary>
    /// 最大重试次数
    /// </summary>
    public int MaxRetryCount { get; set; }

    /// <summary>
    /// 重试等待时间集合
    /// </summary>
    public TimeSpan[] RetryIntervals { get; set; }

    /// <summary>
    /// 捕获的异常集合
    /// </summary>
    public HashSet<Type> HandleExceptions { get; set; }

    /// <summary>
    /// 捕获的内部异常集合
    /// </summary>
    public HashSet<Type> HandleInnerExceptions { get; set; }

    /// <summary>
    /// 操作结果条件集合
    /// </summary>
    public List<Func<RetryPolicyContext<TResult>, bool>> ResultConditions { get; set; }

    /// <summary>
    /// 等待重试时操作方法
    /// </summary>
    public Action<RetryPolicyContext<TResult>, TimeSpan> WaitRetryAction { get; set; }

    /// <summary>
    /// 重试时操作方法
    /// </summary>
    public Action<RetryPolicyContext<TResult>> RetryingAction { get; set; }

    /// <summary>
    /// 添加捕获异常类型
    /// </summary>
    /// <typeparam name="TException"><see cref="System.Exception"/></typeparam>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> Handle<TException>()
        where TException : System.Exception
    {
        HandleExceptions ??= [];
        HandleExceptions.Add(typeof(TException));

        return this;
    }

    /// <summary>
    /// 添加捕获异常类型
    /// </summary>
    /// <typeparam name="TException"><see cref="System.Exception"/></typeparam>
    /// <param name="exceptionCondition">异常条件</param>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> Handle<TException>(Func<TException, bool> exceptionCondition)
        where TException : System.Exception
    {
        // 空检查
        if (exceptionCondition is null) throw new ArgumentNullException(nameof(exceptionCondition));

        // 添加捕获异常类型和条件
        Handle<TException>();
        HandleResult(context => context.Exception is TException exception && exceptionCondition(exception));

        return this;
    }

    /// <summary>
    /// 添加捕获异常类型
    /// </summary>
    /// <typeparam name="TException"><see cref="System.Exception"/></typeparam>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> Or<TException>()
        where TException : System.Exception
    {
        return Handle<TException>();
    }

    /// <summary>
    /// 添加捕获异常类型
    /// </summary>
    /// <typeparam name="TException"><see cref="System.Exception"/></typeparam>
    /// <param name="exceptionCondition">异常条件</param>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> Or<TException>(Func<TException, bool> exceptionCondition)
        where TException : System.Exception
    {
        return Handle(exceptionCondition);
    }

    /// <summary>
    /// 添加捕获内部异常类型
    /// </summary>
    /// <typeparam name="TException"><see cref="System.Exception"/></typeparam>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> HandleInner<TException>()
        where TException : System.Exception
    {
        HandleInnerExceptions ??= [];
        HandleInnerExceptions.Add(typeof(TException));

        return this;
    }

    /// <summary>
    /// 添加捕获内部异常类型
    /// </summary>
    /// <typeparam name="TException"><see cref="System.Exception"/></typeparam>
    /// <param name="exceptionCondition">异常条件</param>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> HandleInner<TException>(Func<TException, bool> exceptionCondition)
        where TException : System.Exception
    {
        // 空检查
        if (exceptionCondition is null) throw new ArgumentNullException(nameof(exceptionCondition));

        // 添加捕获内部异常类型和条件
        HandleInner<TException>();
        HandleResult(context => context.Exception?.InnerException is TException exception && exceptionCondition(exception));

        return this;
    }

    /// <summary>
    /// 添加捕获内部异常类型
    /// </summary>
    /// <typeparam name="TException"><see cref="System.Exception"/></typeparam>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> OrInner<TException>()
        where TException : System.Exception
    {
        return HandleInner<TException>();
    }

    /// <summary>
    /// 添加捕获内部异常类型
    /// </summary>
    /// <typeparam name="TException"><see cref="System.Exception"/></typeparam>
    /// <param name="exceptionCondition">异常条件</param>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> OrInner<TException>(Func<TException, bool> exceptionCondition)
        where TException : System.Exception
    {
        return HandleInner(exceptionCondition);
    }

    /// <summary>
    /// 添加操作结果条件
    /// </summary>
    /// <param name="resultCondition">操作结果条件</param>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> HandleResult(Func<RetryPolicyContext<TResult>, bool> resultCondition)
    {
        // 空检查
        if (resultCondition is null) throw new ArgumentNullException(nameof(resultCondition));

        ResultConditions ??= [];
        ResultConditions.Add(resultCondition);

        return this;
    }

    /// <summary>
    /// 添加操作结果条件
    /// </summary>
    /// <param name="resultCondition">操作结果条件</param>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> OrResult(Func<RetryPolicyContext<TResult>, bool> resultCondition)
    {
        return HandleResult(resultCondition);
    }

    /// <summary>
    /// 添加重试等待时间
    /// </summary>
    /// <param name="retryIntervals">重试等待时间</param>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> WaitAndRetry(params TimeSpan[] retryIntervals)
    {
        // 空检查
        if (retryIntervals is null) throw new ArgumentNullException(nameof(retryIntervals));

        RetryIntervals = retryIntervals;

        return this;
    }

    /// <summary>
    /// 永久重试
    /// </summary>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> Forever()
    {
        MaxRetryCount = int.MaxValue;

        return this;
    }

    /// <summary>
    /// 永久重试并添加重试等待时间
    /// </summary>
    /// <param name="retryIntervals">重试等待时间</param>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> WaitAndRetryForever(params TimeSpan[] retryIntervals)
    {
        return WaitAndRetry(retryIntervals)
            .Forever();
    }

    /// <summary>
    /// 添加等待重试时操作方法
    /// </summary>
    /// <param name="waitRetryAction">等待重试时操作方法</param>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> OnWaitRetry(Action<RetryPolicyContext<TResult>, TimeSpan> waitRetryAction)
    {
        // 空检查
        if (waitRetryAction is null) throw new ArgumentNullException(nameof(waitRetryAction));

        WaitRetryAction = waitRetryAction;

        return this;
    }

    /// <summary>
    /// 添加重试时操作方法
    /// </summary>
    /// <param name="retryingAction">重试时操作方法</param>
    /// <returns><see cref="RetryPolicy{TResult}"/></returns>
    public RetryPolicy<TResult> OnRetrying(Action<RetryPolicyContext<TResult>> retryingAction)
    {
        // 空检查
        if (retryingAction is null) throw new ArgumentNullException(nameof(retryingAction));

        RetryingAction = retryingAction;

        return this;
    }

    /// <summary>
    /// 检查是否满足捕获异常的条件
    /// </summary>
    /// <param name="context"><see cref="RetryPolicyContext{TResult}"/></param>
    /// <returns><see cref="bool"/></returns>
    internal bool ShouldHandle(RetryPolicyContext<TResult> context)
    {
        // 空检查
        if (context is null) throw new ArgumentNullException(nameof(context));

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
        if (ResultConditions is { Count: > 0 })
        {
            return ResultConditions.Any(condition => condition(context));
        }

        return false;
    }

    /// <inheritdoc />
    public async override Task<TResult> ExecuteAsync(Func<Task<TResult>> operation, CancellationToken cancellationToken = default)
    {
        // 空检查
        if (operation is null) throw new ArgumentNullException(nameof(operation));

        // 初始化重试策略上下文
        var context = new RetryPolicyContext<TResult>
        {
            PolicyName = PolicyName
        };

        // 无限循环直到满足条件退出
        while (true)
        {
            try
            {
                // 获取操作方法执行结果
                context.Result = await operation();
            }
            catch (System.Exception exception)
            {
                // 设置策略上下文异常信息
                context.Exception = exception;
            }

            // 检查是否存在取消请求
            cancellationToken.ThrowIfCancellationRequested();

            // 检查是否满足捕获异常的条件
            if (ShouldHandle(context))
            {
                // 递增上下文数据
                context.Increment();

                // 检查是否配置了重试时间
                if (RetryIntervals is { Length: > 0 })
                {
                    // 解析延迟时间戳
                    var delay = RetryIntervals[context.RetryCount % RetryIntervals.Length];

                    // 调用等待重试时操作方法
                    WaitRetryAction?.Invoke(context, delay);

                    // 延迟指定时间再操作
                    await Task.Delay(delay, cancellationToken);
                }

                // 调用重试时操作方法
                RetryingAction?.Invoke(context);

                continue;
            }

            // 返回结果或抛出异常
            return ReturnOrThrowIfException(context);
        }
    }

    /// <summary>
    /// 检查是否满足捕获异常的条件
    /// </summary>
    /// <param name="context"><see cref="RetryPolicyContext{TResult}"/></param>
    /// <param name="exceptionTypes">捕获异常类型集合</param>
    /// <param name="exception"><see cref="System.Exception"/></param>
    /// <returns><see cref="bool"/></returns>
    internal bool CanHandleException(RetryPolicyContext<TResult> context
        , HashSet<Type> exceptionTypes
        , System.Exception exception)
    {
        // 空检查
        if (context is null) throw new ArgumentNullException(nameof(context));

        // 空检查
        if (exception is null)
        {
            return false;
        }

        // 检查是否满足捕获异常的条件
        if (exceptionTypes is null or { Count: 0 }
            || exceptionTypes.Any(ex => ex.IsInstanceOfType(exception)))
        {
            // 检查是否满足操作结果条件
            if (ResultConditions is { Count: > 0 })
            {
                return ResultConditions.Any(condition => condition(context));
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// 返回结果或抛出异常
    /// </summary>
    /// <param name="context"><see cref="RetryPolicyContext{TResult}"/></param>
    /// <returns><typeparamref name="TResult"/></returns>
    internal static TResult ReturnOrThrowIfException(RetryPolicyContext<TResult> context)
    {
        // 空检查
        if (context is null) throw new ArgumentNullException(nameof(context));

        // 空检查
        if (context.Exception is not null)
        {
            throw context.Exception;
        }

        return context.Result;
    }
}