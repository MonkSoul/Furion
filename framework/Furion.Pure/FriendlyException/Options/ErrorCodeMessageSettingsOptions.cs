// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.ConfigurableOptions;

namespace Furion.FriendlyException;

/// <summary>
/// 异常配置选项，最优的方式是采用后期配置，也就是所有异常状态码先不设置（推荐）
/// </summary>
public sealed class ErrorCodeMessageSettingsOptions : IConfigurableOptions
{
    /// <summary>
    /// 异常状态码配置列表
    /// </summary>
    public object[][] Definitions { get; set; }
}