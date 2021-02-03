using Microsoft.AspNetCore.SignalR;

namespace Furion.InstantMessaging
{
    /// <summary>
    /// 即时通讯静态类
    /// </summary>
    public static class IM
    {
        /// <summary>
        /// 获取集线器实例
        /// </summary>
        /// <typeparam name="THub"></typeparam>
        /// <returns></returns>
        public static IHubContext<THub> GetHub<THub>()
            where THub : Hub
        {
            return App.GetService<IHubContext<THub>>();
        }

        /// <summary>
        /// 获取强类型集线器实例
        /// </summary>
        /// <typeparam name="THub"></typeparam>
        /// <typeparam name="TStronglyTyped"></typeparam>
        /// <returns></returns>
        public static IHubContext<THub, TStronglyTyped> GetHub<THub, TStronglyTyped>()
            where THub : Hub<TStronglyTyped>
            where TStronglyTyped : class
        {
            return App.GetService<IHubContext<THub, TStronglyTyped>>();
        }
    }
}