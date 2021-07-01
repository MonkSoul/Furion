// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 配置请求报文头
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
    public class HeadersAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public HeadersAttribute(string key, object value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <remarks>用于将参数添加到请求报文头中，如 Token </remarks>
        public HeadersAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <remarks>用于将参数添加到请求报文头中，如 Token </remarks>
        /// <param name="alias">别名</param>
        public HeadersAttribute(string alias)
        {
            Key = alias;
        }

        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
    }
}