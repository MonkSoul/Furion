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

namespace Furion.Logging;

/// <summary>
/// 日志上下文
/// </summary>
[SuppressSniffer]
public sealed class LogContext
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LogContext()
    {
    }

    /// <summary>
    /// 日志上下文数据
    /// </summary>
    public IDictionary<object, object> Properties { get; set; }

    /// <summary>
    /// 设置上下文数据
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public LogContext Set(object key, object value)
    {
        Properties ??= new Dictionary<object, object>();

        Properties.TryAdd(key, value);
        return this;
    }

    /// <summary>
    /// 获取上下文数据
    /// </summary>
    /// <param name="key">键</param>
    /// <returns></returns>
    public object Get(object key)
    {
        if (Properties == null || Properties.Count == 0) return default;

        var isExists = Properties.TryGetValue(key, out var value);

        return isExists ? value : null;
    }

    /// <summary>
    /// 获取上下文数据
    /// </summary>
    /// <param name="key">键</param>
    /// <returns></returns>
    public object Get<T>(object key)
    {
        var value = Get(key);

        return (T)value;
    }
}