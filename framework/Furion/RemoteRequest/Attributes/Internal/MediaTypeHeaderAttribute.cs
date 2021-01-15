using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 设置内容类型
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MediaTypeHeaderAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value"></param>
        public MediaTypeHeaderAttribute(string value)
        {
            Value = value;
        }

        /// <summary>
        /// 内容类型
        /// </summary>
        public string Value { get; set; }
    }
}