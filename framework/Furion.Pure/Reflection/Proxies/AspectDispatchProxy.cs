// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
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