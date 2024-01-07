// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.ConfigurableOptions;

namespace Furion.DataValidation;

/// <summary>
/// 验证消息配置选项
/// </summary>
public sealed class ValidationTypeMessageSettingsOptions : IConfigurableOptions
{
    /// <summary>
    /// 验证消息配置表
    /// </summary>
    public object[][] Definitions { get; set; }
}