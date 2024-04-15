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

using System.Reflection;

namespace Furion.Reflection;

/// <summary>
/// 方法调用器
/// </summary>
/// <remarks>负责动态调用方法</remarks>
[SuppressSniffer]
public sealed class Invocation
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="targetMethod">接口方法</param>
    /// <param name="args">调用参数</param>
    /// <param name="target">代理实例</param>
    /// <param name="properties">额外数据</param>
    public Invocation(MethodInfo targetMethod
        , object[] args
        , object target
        , Dictionary<object, object> properties)
    {
        Args = args;
        TargetMethod = targetMethod;
        Target = target;
        Properties = properties;

        if (target == null) return;

        // 查找方法定义
        var targetMethodDefined = targetMethod.DeclaringType
            .GetMethods()
            .First(m => m.MetadataToken == targetMethod.MetadataToken);

        // 查找代理方法
        Method = target.GetType().GetMethods().Single(m => m.ToString() == targetMethodDefined.ToString());

        // 处理泛型方法
        if (targetMethod.IsGenericMethod) Method = Method.MakeGenericMethod(targetMethod.GetGenericArguments());
    }

    /// <summary>
    /// 代理实例
    /// </summary>
    public object Target { get; }

    /// <summary>
    /// 调用方法
    /// </summary>
    public MethodInfo Method { get; }

    /// <summary>
    /// 接口方法
    /// </summary>
    private MethodInfo TargetMethod { get; }

    /// <summary>
    /// 调用参数
    /// </summary>
    public object[] Args { get; }

    /// <summary>
    /// 额外数据
    /// </summary>
    public Dictionary<object, object> Properties { get; }

    /// <summary>
    /// 调用同步方法
    /// </summary>
    /// <returns></returns>
    public object Proceed()
    {
        //方法返回值
        var returnType = Method.ReturnType;

        // 处理 Task 和 Task<> 异步方法调用
        if (returnType == typeof(Task) || returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
        {
            // 调用方法并返回 Task 类型
            var task = (Task)Method.Invoke(Target, Args);

            // 创建 TaskCompletionSource 实例，用于控制 Task 什么时候结束、取消、错误
            var taskCompletionSource = new TaskCompletionSource<object>();
            task.ContinueWith(t =>
            {
                // 异步执行失败处理
                if (t.IsFaulted) taskCompletionSource.TrySetException(t.Exception);
                // 异步被取消处理
                else if (t.IsCanceled) taskCompletionSource.TrySetCanceled();
                // 异步成功返回处理
                else taskCompletionSource.TrySetResult(returnType == typeof(Task) ? null : ((dynamic)t).Result);
            });

            return taskCompletionSource.Task;
        }
        // 处理同步方法
        else return Method.Invoke(Target, Args);
    }

    /// <summary>
    /// 调用异步方法
    /// </summary>
    /// <returns></returns>
    public Task ProceedAsync()
    {
        return (Task)Proceed();
    }

    /// <summary>
    /// 调用异步方法带返回值
    /// </summary>
    /// <typeparam name="T">泛型值</typeparam>
    /// <returns><see cref="Task{TResult}"/></returns>
    public async Task<T> ProceedAsync<T>()
    {
        return (T)await (Task<object>)Proceed();
    }
}