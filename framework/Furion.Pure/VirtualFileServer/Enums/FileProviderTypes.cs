// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.ComponentModel;

namespace Furion.VirtualFileServer;

/// <summary>
/// 文件提供器类型
/// </summary>
[Description("文件提供器类型")]
public enum FileProviderTypes
{
    /// <summary>
    /// 物理文件
    /// </summary>
    [Description("物理文件")]
    Physical,

    /// <summary>
    /// 嵌入资源文件
    /// </summary>
    [Description("嵌入资源文件")]
    Embedded
}