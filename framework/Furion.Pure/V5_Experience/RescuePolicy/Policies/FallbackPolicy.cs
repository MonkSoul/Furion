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
///     后备策略
/// </summary>
public sealed class FallbackPolicy : FallbackPolicy<object>
{
    /// <summary>
    ///     <inheritdoc cref="FallbackPolicy" />
    /// </summary>
    public FallbackPolicy()
    {
    }

    /// <summary>
    ///     <inheritdoc cref="FallbackPolicy" />
    /// </summary>
    public FallbackPolicy(Func<FallbackPolicyContext<object>, object?> fallbackAction)
        : base(fallbackAction)
    {
    }

    /// <summary>
    ///     <inheritdoc cref="FallbackPolicy" />
    /// </summary>
    public FallbackPolicy(Action<FallbackPolicyContext<object>> fallbackAction)
        : base(fallbackAction)
    {
    }
}

/// <summary>
///     后备策略
/// </summary>
/// <typeparam name="TResult">操作返回值类型</typeparam>
public class FallbackPolicy<TResult> : PolicyBase<TResult>
{
    /// <summary>
    ///     后备输出信息
    /// </summary>
    internal const string FALLBACK_MESSAGE = "Operation execution failed! The backup operation will be called shortly.";

    /// <summary>
    ///     <inheritdoc cref="FallbackPolicy{TResult}" />
    /// </summary>
    public FallbackPolicy()
    {
    }

    /// <summary>
    ///     <inheritdoc cref="FallbackPolicy{TResult}" />
    /// </summary>
    public FallbackPolicy(Func<FallbackPolicyContext<TResult>, TResult?> fallbackAction) => OnFallback(fallbackAction);

    /// <summary>
    ///     <inheritdoc cref="FallbackPolicy{TResult}" />
    /// </summary>
    public FallbackPolicy(Action<FallbackPolicyContext<TResult>> fallbackAction) => OnFallback(fallbackAction);

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
    public List<Func<FallbackPolicyContext<TResult>, bool>>? ResultConditions { get; set; }

    /// <summary>
    ///     后备操作方法
    /// </summary>
    public Func<FallbackPolicyContext<TResult>, TResult?>? FallbackAction { get; set; }

    /// <summary>
    ///     添加捕获异常类型
    /// </summary>
    /// <typeparam name="TException">
    ///     <see cref="System.Exception" />
    /// </typeparam>
    /// <returns>
    ///     <see cref="FallbackPolicy{TResult}" />
    /// </returns>
    public FallbackPolicy<TResult> Handle<TException>()
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
    ///     <see cref="FallbackPolicy{TResult}" />
    /// </returns>
    public FallbackPolicy<TResult> Handle<TException>(Func<TException, bool> exceptionCondition)
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
    ///     <see cref="FallbackPolicy{TResult}" />
    /// </returns>
    public FallbackPolicy<TResult> Or<TException>()
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
    ///     <see cref="FallbackPolicy{TResult}" />
    /// </returns>
    public FallbackPolicy<TResult> Or<TException>(Func<TException, bool> exceptionCondition)
        where TException : System.Exception =>
        Handle(exceptionCondition);

    /// <summary>
    ///     添加捕获内部异常类型
    /// </summary>
    /// <typeparam name="TException">
    ///     <see cref="System.Exception" />
    /// </typeparam>
    /// <returns>
    ///     <see cref="FallbackPolicy{TResult}" />
    /// </returns>
    public FallbackPolicy<TResult> HandleInner<TException>()
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
    ///     <see cref="FallbackPolicy{TResult}" />
    /// </returns>
    public FallbackPolicy<TResult> HandleInner<TException>(Func<TException, bool> exceptionCondition)
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
    ///     <see cref="FallbackPolicy{TResult}" />
    /// </returns>
    public FallbackPolicy<TResult> OrInner<TException>()
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
    ///     <see cref="FallbackPolicy{TResult}" />
    /// </returns>
    public FallbackPolicy<TResult> OrInner<TException>(Func<TException, bool> exceptionCondition)
        where TException : System.Exception =>
        HandleInner(exceptionCondition);

    /// <summary>
    ///     添加操作结果条件
    /// </summary>
    /// <param name="resultCondition">操作结果条件</param>
    /// <returns>
    ///     <see cref="FallbackPolicy{TResult}" />
    /// </returns>
    public FallbackPolicy<TResult> HandleResult(Func<FallbackPolicyContext<TResult>, bool> resultCondition)
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
    ///     <see cref="FallbackPolicy{TResult}" />
    /// </returns>
    public FallbackPolicy<TResult> OrResult(Func<FallbackPolicyContext<TResult>, bool> resultCondition) =>
        HandleResult(resultCondition);

    /// <summary>
    ///     添加后备操作方法
    /// </summary>
    /// <param name="fallbackAction">后备操作方法</param>
    /// <returns>
    ///     <see cref="FallbackPolicy{TResult}" />
    /// </returns>
    public FallbackPolicy<TResult> OnFallback(Func<FallbackPolicyContext<TResult>, TResult?> fallbackAction)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(fallbackAction);

        FallbackAction = fallbackAction;

        return this;
    }

    /// <summary>
    ///     添加后备操作方法
    /// </summary>
    /// <param name="fallbackAction">后备操作方法</param>
    /// <returns>
    ///     <see cref="FallbackPolicy{TResult}" />
    /// </returns>
    public FallbackPolicy<TResult> OnFallback(Action<FallbackPolicyContext<TResult>> fallbackAction)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(fallbackAction);

        return OnFallback(context =>
        {
            fallbackAction(context);

            return default;
        });
    }

    /// <summary>
    ///     检查是否满足捕获异常的条件
    /// </summary>
    /// <param name="context">
    ///     <see cref="FallbackPolicyContext{TResult}" />
    /// </param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    internal bool ShouldHandle(FallbackPolicyContext<TResult> context)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(context);

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

        // 初始化后备策略上下文
        var context = new FallbackPolicyContext<TResult> { PolicyName = PolicyName };

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
            return ReturnOrThrowIfException(context);
        }

        // 输出调试事件
        Debugging.Error(FALLBACK_MESSAGE);

        // 调用后备操作方法
        return FallbackAction is not null
            ? FallbackAction(context)
            :
            // 返回结果或抛出异常
            ReturnOrThrowIfException(context);
    }

    /// <summary>
    ///     检查是否满足捕获异常的条件
    /// </summary>
    /// <param name="context">
    ///     <see cref="FallbackPolicyContext{TResult}" />
    /// </param>
    /// <param name="exceptionTypes">捕获异常类型集合</param>
    /// <param name="exception">
    ///     <see cref="System.Exception" />
    /// </param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    internal bool CanHandleException(FallbackPolicyContext<TResult> context
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
    ///     <see cref="FallbackPolicyContext{TResult}" />
    /// </param>
    /// <returns>
    ///     <typeparamref name="TResult" />
    /// </returns>
    internal static TResult? ReturnOrThrowIfException(FallbackPolicyContext<TResult> context)
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