using Furion.DependencyInjection;
using Furion.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Net.Http
{
    /// <summary>
    /// HttpRequestMessage 拓展
    /// </summary>
    [SkipScan]
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// 追加查询参数
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="queries"></param>
        public static void AppendQueries(this HttpRequestMessage httpRequest, Dictionary<string, object> queries)
        {
            if (queries == null || queries.Count == 0) return;

            // 获取原始地址
            var finalRequestUrl = httpRequest.RequestUri.OriginalString;

            // 拼接
            var urlParameters = queries.Where(u => u.Value != null).Select(u => $"{u.Key}={HttpUtility.UrlEncode(u.Value?.ToString() ?? string.Empty)}");
            finalRequestUrl += $"{(finalRequestUrl.IndexOf("?") > -1 ? "&" : "?")}{string.Join("&", urlParameters)}";

            // 重新设置地址
            httpRequest.RequestUri = new Uri(finalRequestUrl);
        }

        /// <summary>
        /// 追加查询参数
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="queries"></param>
        public static void AppendQueries(this HttpRequestMessage httpRequest, object queries)
        {
            httpRequest.AppendQueries(queries?.ToDictionary<object>());
        }
    }
}