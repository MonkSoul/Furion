// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DistributedIDGenerator;

/// <summary>
/// 短 ID 约束
/// </summary>
internal static class Constants
{
    /// <summary>
    /// 最小长度
    /// </summary>
    public const int MinimumAutoLength = 8;

    /// <summary>
    /// 最大长度
    /// </summary>
    public const int MaximumAutoLength = 14;

    /// <summary>
    /// 最小可选字符长度
    /// </summary>
    public const int MinimumCharacterSetLength = 50;
}