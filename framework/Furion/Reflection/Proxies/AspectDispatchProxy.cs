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
/// 异步代理分发类
/// </summary>
public abstract class AspectDispatchProxy
{
    /// <summary>
    /// 创建代理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProxy"></typeparam>
    /// <returns></returns>
    public static T Create<T, TProxy>() where TProxy : AspectDispatchProxy
    {
        return (T)AspectDispatchProxyGenerator.CreateProxyInstance(typeof(TProxy), typeof(T));
    }

    /// <summary>
    /// 执行同步代理
    /// </summary>
    /// <param name="method"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public abstract object Invoke(MethodInfo method, object[] args);

    /// <summary>
    /// 执行异步代理
    /// </summary>
    /// <param name="method"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public abstract Task InvokeAsync(MethodInfo method, object[] args);

    /// <summary>
    /// 执行异步返回 Task{T} 代理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public abstract Task<T> InvokeAsyncT<T>(MethodInfo method, object[] args);
}