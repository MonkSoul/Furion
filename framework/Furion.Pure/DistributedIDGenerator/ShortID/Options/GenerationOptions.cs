// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DistributedIDGenerator;

/// <summary>
/// 短 ID 生成配置选项
/// </summary>
[SuppressSniffer]
public class GenerationOptions
{
    /// <summary>
    /// 是否使用数字
    /// <para>默认 false</para>
    /// </summary>
    public bool UseNumbers { get; set; }

    /// <summary>
    /// 是否使用特殊字符
    /// <para>默认 true</para>
    /// </summary>
    public bool UseSpecialCharacters { get; set; } = true;

    /// <summary>
    /// 设置短 ID 长度
    /// </summary>
    public int Length { get; set; } = RandomHelpers.GenerateNumberInRange(Constants.MinimumAutoLength, Constants.MaximumAutoLength);
}