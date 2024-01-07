// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.ComponentModel;

namespace Furion.DependencyInjection;

/// <summary>
/// 注册范围
/// </summary>
[SuppressSniffer]
public enum InjectionPatterns
{
    /// <summary>
    /// 只注册自己
    /// </summary>
    [Description("只注册自己")]
    Self,

    /// <summary>
    /// 第一个接口
    /// </summary>
    [Description("只注册第一个接口")]
    FirstInterface,

    /// <summary>
    /// 自己和第一个接口，默认值
    /// </summary>
    [Description("自己和第一个接口")]
    SelfWithFirstInterface,

    /// <summary>
    /// 所有接口
    /// </summary>
    [Description("所有接口")]
    ImplementedInterfaces,

    /// <summary>
    /// 注册自己包括所有接口
    /// </summary>
    [Description("自己包括所有接口")]
    All
}