// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.7.9
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using System.Net.Http;

namespace Furion.UrlRewriter
{
    /// <summary>
    /// URL 转发用的 Http 代理
    /// </summary>
    [SkipScan]
    public sealed class UrlRewriteProxyHttpClient
    {
        /// <summary>
        /// Http代理
        /// </summary>
        /// <param name="httpClient"></param>
        public UrlRewriteProxyHttpClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        /// <summary>
        /// http请求客户端
        /// </summary>
        public HttpClient Client { get; private set; }
    }
}