// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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