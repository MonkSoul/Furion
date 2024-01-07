// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.ComponentModel;

namespace Furion.DependencyInjection;

/// <summary>
/// 注册类型
/// </summary>
[SuppressSniffer]
public enum RegisterType
{
    /// <summary>
    /// 瞬时
    /// </summary>
    [Description("瞬时")]
    Transient,

    /// <summary>
    /// 作用域
    /// </summary>
    [Description("作用域")]
    Scoped,

    /// <summary>
    /// 单例
    /// </summary>
    [Description("单例")]
    Singleton
}