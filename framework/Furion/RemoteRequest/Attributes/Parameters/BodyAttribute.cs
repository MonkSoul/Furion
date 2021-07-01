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
    /// 配置Body参数
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Parameter)]
    public class BodyAttribute : ParameterBaseAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BodyAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contentType"></param>
        public BodyAttribute(string contentType)
        {
            ContentType = contentType;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="encoding"></param>
        public BodyAttribute(string contentType, string encoding)
        {
            ContentType = contentType;
            Encoding = encoding;
        }

        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; } = "application/json";

        /// <summary>
        /// 内容编码
        /// </summary>
        public string Encoding { get; set; } = "UTF-8";
    }
}