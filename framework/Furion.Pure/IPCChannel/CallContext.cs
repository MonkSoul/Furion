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

using System.Collections.Concurrent;

namespace Furion.IPCChannel;

/// <summary>
/// 提供线程异步流共享数据上下文（尽量在项目需要该操作的类中使用 AsyncLocal 方式使用，而不是调用 CallContext
/// </summary>
/// <typeparam name="T"></typeparam>
/// <remarks>
/// AsyncLocal 遇到 await 关键字时采用拷贝方式创建新的执行上下文并流转
/// 在Task方法内部修改其值，但在任务结束后仍为初始值，这是一种“写时复制”行为，AsyncLocal内部做了两步操作：
///   进行AsyncLocal实例的拷贝副本，但这是浅复制行为而非深复制
///   在设置新的值之前完成复制操作
/// 获取当前线程 Id：Thread.CurrentThread.ManagedThreadId
/// </remarks>
[SuppressSniffer]
public static class CallContext<T>
{
    /// <summary>
    /// 保存本地数据
    /// </summary>
    /// <remarks>这里存在内存溢出问题，因为该定义对象并没有任何释放内存的方式提供，所以尽可能的少使用</remarks>
    private static readonly ConcurrentDictionary<string, AsyncLocal<T>> localValues = new();

    /// <summary>
    /// 设置值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetLocalValue(string key, T value)
    {
        localValues.GetOrAdd(key, _ => new AsyncLocal<T>(args =>
        {
            // args.CurrentValue; // 当前值
            // args.PreviousValue;  // 更改之前值
            // args.ThreadContextChanged;   // 如果上下文发生改变返回 true，否则返回 false
        })).Value = value;
    }

    /// <summary>
    /// 读取值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T GetLocalValue(string key)
    {
        return localValues.TryGetValue(key, out var value) ? value.Value : default;
    }
}

/// <summary>
/// 提供线程异步流共享数据上下文（尽量在项目需要该操作的类中使用 AsyncLocal 方式使用，而不是调用 CallContext
/// </summary>
/// <remarks>
/// AsyncLocal 遇到 await 关键字时采用拷贝方式创建新的执行上下文并流转
/// 在Task方法内部修改其值，但在任务结束后仍为初始值，这是一种“写时复制”行为，AsyncLocal内部做了两步操作：
///   进行AsyncLocal实例的拷贝副本，但这是浅复制行为而非深复制
///   在设置新的值之前完成复制操作
/// 获取当前线程 Id：Thread.CurrentThread.ManagedThreadId
/// </remarks>
[SuppressSniffer]
public static class CallContext
{
    /// <summary>
    /// 保存本地数据
    /// </summary>
    /// <remarks>这里存在内存溢出问题，因为该定义对象并没有任何释放内存的方式提供，所以尽可能的少使用</remarks>
    private static readonly ConcurrentDictionary<string, AsyncLocal<object>> localValues = new();

    /// <summary>
    /// 设置值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetLocalValue(string key, object value)
    {
        localValues.GetOrAdd(key, _ => new AsyncLocal<object>(args =>
        {
            // args.CurrentValue; // 当前值
            // args.PreviousValue;  // 更改之前值
            // args.ThreadContextChanged;   // 如果上下文发生改变返回 true，否则返回 false
        })).Value = value;
    }

    /// <summary>
    /// 读取值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static object GetLocalValue(string key)
    {
        return localValues.TryGetValue(key, out var value) ? value.Value : default;
    }
}