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
    /// 远程请求静态类
    /// </summary>
    [SuppressSniffer]
    public static class Http
    {
        /// <summary>
        /// 获取远程请求代理
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>IHttpDispatchProxy</returns>
        public static THttpDispatchProxy GetRemoteRequestProxy<THttpDispatchProxy>(IServiceProvider serviceProvider = default)
            where THttpDispatchProxy : class, IHttpDispatchProxy
        {
            return App.GetService<THttpDispatchProxy>(serviceProvider);
        }
    }
}