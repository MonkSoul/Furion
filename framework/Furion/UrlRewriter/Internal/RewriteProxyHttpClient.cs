using Furion.DependencyInjection;
using System.Net.Http;

namespace Furion.UrlRewriter
{
    /// <summary>
    /// URL转发用的Http代理
    /// </summary>
    [SkipScan]
    public class RewriteProxyHttpClient
    {
        /// <summary>
        /// http请求客户端
        /// </summary>
        public HttpClient Client { get; private set; }

        /// <summary>
        /// Http代理
        /// </summary>
        /// <param name="httpClient"></param>
        public RewriteProxyHttpClient(HttpClient httpClient)
        {
            Client = httpClient;
        }
    }
}