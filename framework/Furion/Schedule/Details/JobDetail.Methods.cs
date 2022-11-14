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

namespace Furion.Schedule;

/// <summary>
/// 作业信息
/// </summary>
public partial class JobDetail
{
    /// <summary>
    /// 获取作业额外数据
    /// </summary>
    /// <param name="key">键</param>
    /// <returns><see cref="object"/></returns>
    public object GetProperty(string key)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        if (!RuntimeProperties.ContainsKey(key)) return default;

        return RuntimeProperties[key];
    }

    /// <summary>
    /// 获取作业额外数据
    /// </summary>
    /// <typeparam name="T">结果泛型类型</typeparam>
    /// <param name="key">键</param>
    /// <returns>T 类型</returns>
    public T GetProperty<T>(string key)
    {
        var value = GetProperty(key);
        if (value == null) return default;
        return (T)value;
    }

    /// <summary>
    /// 添加作业额外数据
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns><see cref="JobDetail"/></returns>
    public JobDetail AddProperty(string key, object value)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        RuntimeProperties.TryAdd(key, value);
        Properties = Penetrates.Serialize(RuntimeProperties);

        return this;
    }

    /// <summary>
    /// 添加或更新作业额外数据
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns><see cref="JobDetail"/></returns>
    public JobDetail AddOrUpdateProperty(string key, object value)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        if (RuntimeProperties.ContainsKey(key)) RuntimeProperties[key] = value;
        else RuntimeProperties.TryAdd(key, value);

        Properties = Penetrates.Serialize(RuntimeProperties);

        return this;
    }

    /// <summary>
    /// 删除作业额外数据
    /// </summary>
    /// <param name="key">键</param>
    /// <returns><see cref="JobDetail"/></returns>
    public JobDetail RemoveProperty(string key)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        if (RuntimeProperties.ContainsKey(key))
        {
            RuntimeProperties.Remove(key);
            Properties = Penetrates.Serialize(RuntimeProperties);
        }

        return this;
    }

    /// <summary>
    /// 清空作业额外数据
    /// </summary>
    /// <returns><see cref="JobDetail"/></returns>
    public JobDetail ClearProperties()
    {
        RuntimeProperties.Clear();
        Properties = Penetrates.Serialize(RuntimeProperties);

        return this;
    }
}