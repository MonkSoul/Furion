// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
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