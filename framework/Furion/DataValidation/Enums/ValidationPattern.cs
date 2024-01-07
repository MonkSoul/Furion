// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// 验证逻辑
/// </summary>
[SuppressSniffer]
public enum ValidationPattern
{
    /// <summary>
    /// 全部都要验证通过
    /// </summary>
    [Description("全部验证通过才为真")]
    AllOfThem,

    /// <summary>
    /// 至少一个验证通过
    /// </summary>
    [Description("有一个通过就为真")]
    AtLeastOne
}