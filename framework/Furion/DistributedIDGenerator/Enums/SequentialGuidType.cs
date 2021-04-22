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
        /// 格式化为字符串
        /// </summary>
        [Description("格式化为字符串")]
        SequentialAsString,

        /// <summary>
        /// 格式化成 Byte 数组
        /// </summary>
        [Description("格式化成 Byte 数组")]
        SequentialAsBinary,

        /// <summary>
        /// 序列化部分在末尾部分
        /// </summary>
        [Description("序列化部分在末尾部分")]
        SequentialAtEnd
    }
}