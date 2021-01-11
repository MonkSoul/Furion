using Furion.DependencyInjection;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 响应类型
    /// </summary>
    [SkipScan]
    public enum ResponseType
    {
        /// <summary>
        /// 对象类型
        /// </summary>
        Object,

        /// <summary>
        /// 文本
        /// </summary>
        Text,

        /// <summary>
        /// 流
        /// </summary>
        Stream,

        /// <summary>
        /// Byte 数组
        /// </summary>
        ByteArray
    }
}