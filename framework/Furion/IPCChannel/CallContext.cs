// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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