using Furion.DependencyInjection;
using System.ComponentModel;

namespace Furion.DistributedIDGenerator
{
    /// <summary>
    /// 连续 GUID 类型选项
    /// </summary>
    [SkipScan]
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
}