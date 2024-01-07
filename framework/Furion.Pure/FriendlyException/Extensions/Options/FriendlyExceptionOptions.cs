// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.FriendlyException;

/// <summary>
/// AddInject 友好异常配置选项
/// </summary>
public sealed class FriendlyExceptionOptions
{
    /// <summary>
    /// 是否启用全局友好异常
    /// </summary>
    public bool GlobalEnabled { get; set; } = true;
}