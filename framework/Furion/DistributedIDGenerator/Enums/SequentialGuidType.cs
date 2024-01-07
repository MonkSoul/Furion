// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.ComponentModel;

namespace Furion.DistributedIDGenerator;

/// <summary>
/// 连续 GUID 类型选项
/// </summary>
[SuppressSniffer]
public enum SequentialGuidType
{
    /// <summary>
    /// 标准连续 GUID 字符串
    /// </summary>
    [Description("标准连续 GUID 字符串")]
    SequentialAsString,

    /// <summary>
    /// Byte 数组类型的连续 `GUID` 字符串
    /// </summary>
    [Description("Byte 数组类型的连续 `GUID` 字符串")]
    SequentialAsBinary,

    /// <summary>
    /// 连续部分在末尾展示
    /// </summary>
    [Description("连续部分在末尾展示")]
    SequentialAtEnd
}