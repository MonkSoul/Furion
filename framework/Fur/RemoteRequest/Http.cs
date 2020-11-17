using Fur.DependencyInjection;

namespace Fur.RemoteRequest
{
    /// <summary>
    /// 请求静态类
    /// </summary>
    [SkipScan]
    public static class Http
    {
        /// <summary>
        /// 获取 Http 代理
        /// </summary>
        /// <returns>ISqlRepository</returns>
        public static THttpDispatchProxy GetHttpDispatchProxy<THttpDispatchProxy>()
            where THttpDispatchProxy : class, IHttpDispatchProxy
        {
            return App.GetService<THttpDispatchProxy>();
        }
    }
}