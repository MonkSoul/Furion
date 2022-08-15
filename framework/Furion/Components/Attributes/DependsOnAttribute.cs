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

using Furion.Components;
using Furion.Reflection;

namespace System;

/// <summary>
/// 组件依赖配置特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class DependsOnAttribute : Attribute
{
    /// <summary>
    /// 依赖组件列表
    /// </summary>
    private Type[] _dependComponents = Array.Empty<Type>();

    /// <summary>
    /// 连接组件列表
    /// </summary>
    private Type[] _links = Array.Empty<Type>();

    /// <summary>
    /// 构造函数
    /// </summary>
    public DependsOnAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dependComponents">依赖组件列表</param>
    /// <remarks>支持字符串类型程序集/类型配置</remarks>
    public DependsOnAttribute(params object[] dependComponents)
    {
        var components = new List<Type>();

        // 遍历所有依赖组件
        if (dependComponents != null && dependComponents.Length > 0)
        {
            foreach (var component in dependComponents)
            {
                // 如果是类型自动载入
                if (component is Type componentType)
                {
                    components.Add(componentType);
                }
                // 处理字符串配置模式
                else if (component is string typeString)
                {
                    components.Add(Reflect.GetStringType(typeString));
                }
                else throw new InvalidOperationException("Component type can only be `Type` or `String` type of specific format.");
            }
        }

        DependComponents = components.ToArray();
    }

    /// <summary>
    /// 依赖组件列表
    /// </summary>
    public Type[] DependComponents
    {
        get => _dependComponents;
        set
        {
            var components = value ?? Array.Empty<Type>();

            // 检查类型是否实现 IComponent 接口
            foreach (var type in components)
            {
                if (!typeof(IComponent).IsAssignableFrom(type))
                {
                    throw new InvalidOperationException($"The type of `{type.FullName}` must be assignable from `{nameof(IComponent)}`.");
                }
            }

            _dependComponents = components;
        }
    }

    /// <summary>
    /// 链接组件列表
    /// </summary>
    public object[] Links
    {
        get => _links;
        set
        {
            var components = new List<Type>();

            // 遍历所有依赖组件
            if (value != null && value.Length > 0)
            {
                foreach (var component in value)
                {
                    // 如果是类型自动载入
                    if (component is Type componentType)
                    {
                        components.Add(componentType);
                    }
                    // 处理字符串配置模式
                    else if (component is string typeString)
                    {
                        components.Add(Reflect.GetStringType(typeString));
                    }
                    else throw new InvalidOperationException("Component type can only be `Type` or `String` type of specific format.");
                }
            }

            LinkComponents = _links = components.ToArray();
        }
    }

    /// <summary>
    /// 内部链接组件
    /// </summary>
    internal Type[] LinkComponents
    {
        get => _links;
        set
        {
            var components = value ?? Array.Empty<Type>();

            // 检查类型是否实现 IComponent 接口
            foreach (var type in components)
            {
                if (!typeof(IComponent).IsAssignableFrom(type))
                {
                    throw new InvalidOperationException($"The type of `{type.FullName}` must be assignable from `{nameof(IComponent)}`.");
                }
            }
        }
    }
}