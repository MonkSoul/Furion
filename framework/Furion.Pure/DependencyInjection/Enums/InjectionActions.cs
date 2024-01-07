// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.ComponentModel;

namespace Furion.DependencyInjection;

/// <summary>
/// 服务注册方式
/// </summary>
[SuppressSniffer]
public enum InjectionActions
{
    /// <summary>
    /// 如果存在则覆盖
    /// </summary>
    [Description("存在则覆盖")]
    Add,

    /// <summary>
    /// 如果存在则跳过，默认方式
    /// </summary>
    [Description("存在则跳过")]
    TryAdd
}