// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Furion.Components;
using Furion.Reflection;

namespace System;

/// <summary>
/// 组件依赖配置特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
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