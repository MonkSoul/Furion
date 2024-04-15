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