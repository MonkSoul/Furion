using Furion.DependencyInjection;
using System.Reflection;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 参数及参数值
    /// </summary>
    [SkipScan]
    internal class ParameterValue
    {
        /// <summary>
        /// 参数
        /// </summary>
        internal ParameterInfo Parameter { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        internal object Value { get; set; }

        /// <summary>
        /// 是否 Url 地址参数
        /// </summary>
        internal bool IsUrlParameter { get; set; }

        /// <summary>
        /// 是否 Body参数
        /// </summary>
        internal bool IsBodyParameter { get; set; }
    }
}