// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DistributedIDGenerator;

/// <summary>
/// 连续 GUID 配置
/// </summary>
[SuppressSniffer]
public sealed class SequentialGuidSettings
{
    /// <summary>
    /// 当前时间
    /// </summary>
    public DateTimeOffset? TimeNow { get; set; }

    /// <summary>
    /// LittleEndianBinary 16 格式化
    /// </summary>
    public bool LittleEndianBinary16Format { get; set; }
}