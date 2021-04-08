using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 远程请求静态类
    /// </summary>
    [SkipScan]
    public static class Http
    {
        /// <summary>
        /// 获取远程请求代理
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>ISqlRepository</returns>
        public static THttpDispatchProxy GetRemoteRequestProxy<THttpDispatchProxy>(IServiceProvider serviceProvider = default)
            where THttpDispatchProxy : class, IHttpDispatchProxy
        {
            return App.GetService<THttpDispatchProxy>(serviceProvider);
        }
    }
}