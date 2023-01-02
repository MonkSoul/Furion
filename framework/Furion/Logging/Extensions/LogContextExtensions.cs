// MIT License
//
// Copyright (c) 2020-2023 百小僧, Baiqian Co.,Ltd and Contributors
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

namespace Furion.Logging;

/// <summary>
/// LogContext 拓展
/// </summary>
[SuppressSniffer]
public static class LogContextExtensions
{
    /// <summary>
    /// 设置上下文数据
    /// </summary>
    /// <param name="logContext"></param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static LogContext Set(this LogContext logContext, object key, object value)
    {
        if (logContext == null || key == null) return logContext;

        logContext.Properties ??= new Dictionary<object, object>();

        if (logContext.Properties.ContainsKey(key)) logContext.Properties.Remove(key);
        logContext.Properties.Add(key, value);
        return logContext;
    }

    /// <summary>
    /// 批量设置上下文数据
    /// </summary>
    /// <param name="logContext"></param>
    /// <param name="properties"></param>
    /// <returns></returns>
    public static LogContext SetRange(this LogContext logContext, IDictionary<object, object> properties)
    {
        if (logContext == null
            || properties == null
            || properties.Count == 0) return logContext;

        foreach (var (key, value) in properties)
        {
            logContext.Set(key, value);
        }

        return logContext;
    }

    /// <summary>
    /// 获取上下文数据
    /// </summary>
    /// <param name="logContext"></param>
    /// <param name="key">键</param>
    /// <returns></returns>
    public static object Get(this LogContext logContext, object key)
    {
        if (logContext == null
            || key == null
            || logContext.Properties == null
            || logContext.Properties.Count == 0) return default;

        var isExists = logContext.Properties.TryGetValue(key, out var value);
        return isExists ? value : null;
    }

    /// <summary>
    /// 获取上下文数据
    /// </summary>
    /// <param name="logContext"></param>
    /// <param name="key">键</param>
    /// <returns></returns>
    public static object Get<T>(this LogContext logContext, object key)
    {
        var value = logContext.Get(key);
        return (T)value;
    }
}