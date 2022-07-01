// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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

            // 检查类型是否实现 IStartupComponent 接口
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
}