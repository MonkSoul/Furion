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
        /// Json
        /// </summary>
        Json,

        /// <summary>
        /// 文本
        /// </summary>
        Text,

        /// <summary>
        /// 流
        /// </summary>
        Stream,

        /// <summary>
        /// Byte
        /// </summary>
        Byte
    }
}