// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.ComponentModel.DataAnnotations;

namespace Furion.DataValidation;

/// <summary>
/// 数据验证结果
/// </summary>
[SuppressSniffer]
public sealed class DataValidationResult
{
    /// <summary>
    /// 验证状态
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// 验证结果
    /// </summary>
    public ICollection<ValidationResult> ValidationResults { get; set; }

    /// <summary>
    /// 成员或值
    /// </summary>
    public object MemberOrValue { get; set; }
}