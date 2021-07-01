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
using System.ComponentModel;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 拦截类型
    /// </summary>
    [SuppressSniffer]
    public enum InterceptorTypes
    {
        /// <summary>
        /// HttpClient 拦截
        /// </summary>
        [Description("HttpClient 拦截")]
        Client,

        /// <summary>
        /// HttpRequestMessage 拦截
        /// </summary>
        [Description("HttpRequestMessage 拦截")]
        Request,

        /// <summary>
        /// HttpResponseMessage 拦截
        /// </summary>
        [Description("HttpResponseMessage 拦截")]
        Response,

        /// <summary>
        /// 异常拦截
        /// </summary>
        [Description("异常拦截")]
        Exception
    }
}