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

using System.ComponentModel;

namespace System;

/// <summary>
/// 组件上下文
/// </summary>
[SuppressSniffer]
public sealed class ComponentContext
{
    /// <summary>
    /// 组件类型
    /// </summary>
    public Type ComponentType { get; internal set; }

    /// <summary>
    /// 上级组件上下文
    /// </summary>
    public ComponentContext CalledContext { get; internal set; }

    /// <summary>
    /// 根组件上下文
    /// </summary>
    public ComponentContext RootContext { get; internal set; }

    /// <summary>
    /// 依赖组件列表
    /// </summary>
    public Type[] DependComponents { get; internal set; }

    /// <summary>
    /// 链接组件列表
    /// </summary>
    public Type[] LinkComponents { get; internal set; }

    /// <summary>
    /// 上下文数据
    /// </summary>
    private Dictionary<string, object> Properties { get; set; } = new();

    /// <summary>
    /// 是否是根组件
    /// </summary>
    internal bool IsRoot { get; set; } = false;

    /// <summary>
    /// 设置组件属性参数
    /// </summary>
    /// <typeparam name="TComponent">派生自 <see cref="IComponent"/></typeparam>
    /// <param name="value">组件参数</param>
    /// <returns></returns>
    public Dictionary<string, object> SetProperty<TComponent>(object value)
        where TComponent : class, IComponent, new()
    {
        return SetProperty(typeof(TComponent), value);
    }

    /// <summary>
    /// 设置组件属性参数
    /// </summary>
    /// <param name="componentType">组件类型</param>
    /// <param name="value">组件参数</param>
    /// <returns></returns>
    public Dictionary<string, object> SetProperty(Type componentType, object value)
    {
        return SetProperty(componentType.FullName, value);
    }

    /// <summary>
    /// 设置组件属性参数
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">组件参数</param>
    /// <returns></returns>
    public Dictionary<string, object> SetProperty(string key, object value)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        var properties = RootContext == null ? Properties : RootContext.Properties;

        if (!properties.ContainsKey(key))
        {
            properties.Add(key, value);
        }
        else properties[key] = value;

        return properties;
    }

    /// <summary>
    /// 获取组件属性参数
    /// </summary>
    /// <typeparam name="TComponent">派生自 <see cref="IComponent"/></typeparam>
    /// <typeparam name="TComponentOptions">组件参数类型</typeparam>
    /// <returns></returns>
    public TComponentOptions GetProperty<TComponent, TComponentOptions>()
        where TComponent : class, IComponent, new()
    {
        return GetProperty<TComponentOptions>(typeof(TComponent));
    }

    /// <summary>
    /// 获取组件属性参数
    /// </summary>
    /// <typeparam name="TComponentOptions">组件参数类型</typeparam>
    /// <param name="componentType">组件类型</param>
    /// <returns></returns>
    public TComponentOptions GetProperty<TComponentOptions>(Type componentType)
    {
        return GetProperty<TComponentOptions>(componentType.FullName);
    }

    /// <summary>
    /// 获取组件属性参数
    /// </summary>
    /// <typeparam name="TComponentOptions">组件参数类型</typeparam>
    /// <param name="key">键</param>
    /// <returns></returns>
    public TComponentOptions GetProperty<TComponentOptions>(string key)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        var properties = RootContext == null ? Properties : RootContext.Properties;

        return !properties.ContainsKey(key)
            ? default
            : (TComponentOptions)properties[key];
    }

    /// <summary>
    /// 获取组件所有依赖参数
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, object> GetProperties()
    {
        return RootContext == null ? Properties : RootContext.Properties;
    }
}