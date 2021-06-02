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

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Furion.UrlRewriter
{
    /// <summary>
    /// URL转发器
    /// </summary>
    public interface IUrlRewriterProxy
    {
        /// <summary>
        /// URL转发
        /// </summary>
        /// <param name="context"></param>
        /// <param name="rewritePath"></param>
        /// <param name="toHost"></param>
        /// <returns></returns>
        Task RewriteUri(HttpContext context, PathString rewritePath, string toHost);
    }
}