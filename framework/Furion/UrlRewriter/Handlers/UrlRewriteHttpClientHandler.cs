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
    /// 自定义 Url 转发的 HttpClientHandler 对象
    /// </summary>
    [SkipScan]
    public sealed class UrlRewriteHttpClientHandler : HttpClientHandler
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UrlRewriteHttpClientHandler() : base()
        {
            AllowAutoRedirect = false;
            MaxConnectionsPerServer = int.MaxValue;
            UseCookies = false;
        }
    }
}