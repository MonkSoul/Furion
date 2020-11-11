using Fur.DependencyInjection;
using System;

namespace Fur.RemoteRequest
{
    /// <summary>
    /// 设置请问报文头
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class HeaderAttribute : Attribute
    {
        /// <summary>
        /// 构造演示
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public HeaderAttribute(string key, string value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}