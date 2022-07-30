// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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