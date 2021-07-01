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

using Microsoft.AspNetCore.SignalR;
using System;

namespace Furion.InstantMessaging
{
    /// <summary>
    /// 即时通信静态类
    /// </summary>
    public static class IM
    {
        /// <summary>
        /// 获取集线器实例
        /// </summary>
        /// <typeparam name="THub"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IHubContext<THub> GetHub<THub>(IServiceProvider serviceProvider = default)
            where THub : Hub
        {
            return App.GetService<IHubContext<THub>>(serviceProvider);
        }

        /// <summary>
        /// 获取强类型集线器实例
        /// </summary>
        /// <typeparam name="THub"></typeparam>
        /// <typeparam name="TStronglyTyped"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IHubContext<THub, TStronglyTyped> GetHub<THub, TStronglyTyped>(IServiceProvider serviceProvider = default)
            where THub : Hub<TStronglyTyped>
            where TStronglyTyped : class
        {
            return App.GetService<IHubContext<THub, TStronglyTyped>>(serviceProvider);
        }
    }
}