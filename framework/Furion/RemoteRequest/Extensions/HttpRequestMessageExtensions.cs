using Furion.ClayObject;
using Furion.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="isEncode"></param>
        public static void AppendQueries(this HttpRequestMessage httpRequest, IDictionary<string, object> queries, bool isEncode = true)
        {
            if (queries == null || queries.Count == 0) return;

            // 获取原始地址
            var finalRequestUrl = httpRequest.RequestUri.OriginalString;

            // 拼接
            var urlParameters = queries.Where(u => u.Value != null)
                            .Select(u => $"{u.Key}={(isEncode ? Uri.EscapeDataString(u.Value?.ToString() ?? string.Empty) : (u.Value?.ToString() ?? string.Empty))}");
            finalRequestUrl += $"{(finalRequestUrl.IndexOf("?") > -1 ? "&" : "?")}{string.Join("&", urlParameters)}";

            // 重新设置地址
            httpRequest.RequestUri = new Uri(finalRequestUrl, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// 追加查询参数
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="queries"></param>
        /// <param name="isEncode"></param>
        public static void AppendQueries(this HttpRequestMessage httpRequest, object queries, bool isEncode = true)
        {
            if (queries == null) return;

            httpRequest.AppendQueries(DictionaryMaker.Make(queries), isEncode);
        }
    }
}