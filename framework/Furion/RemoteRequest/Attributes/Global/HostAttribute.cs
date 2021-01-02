using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 设置主机配置
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public class HostAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baseAddress"></param>
        public HostAttribute(string baseAddress)
        {
            BaseAddress = HandleBaseAddress(baseAddress);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="port"></param>
        public HostAttribute(string baseAddress, int port)
        {
            BaseAddress = HandleBaseAddress(baseAddress);
            Port = port;
        }

        /// <summary>
        /// 基础地址
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 处理Url地址斜杆
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <returns></returns>
        private string HandleBaseAddress(string baseAddress)
        {
            if (baseAddress.EndsWith("/"))
            {
                return baseAddress[0..^1];
            }

            return baseAddress;
        }
    }
}