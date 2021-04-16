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
        /// <param name="scoped"></param>
        /// <returns></returns>
        public static IHubContext<THub> GetHub<THub>(IServiceProvider scoped = default)
            where THub : Hub
        {
            return App.GetService<IHubContext<THub>>(scoped);
        }

        /// <summary>
        /// 获取强类型集线器实例
        /// </summary>
        /// <typeparam name="THub"></typeparam>
        /// <typeparam name="TStronglyTyped"></typeparam>
        /// <param name="scoped"></param>
        /// <returns></returns>
        public static IHubContext<THub, TStronglyTyped> GetHub<THub, TStronglyTyped>(IServiceProvider scoped = default)
            where THub : Hub<TStronglyTyped>
            where TStronglyTyped : class
        {
            return App.GetService<IHubContext<THub, TStronglyTyped>>(scoped);
        }
    }
}