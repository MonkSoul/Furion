using Furion.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Furion.UrlRewriter
{
    /// <summary>
    /// URL转发器实现
    /// </summary>
    [SkipScan]
    public class UrlRewriteProxy : IUrlRewriter
    {
        // 无需转发的请求头信息
        private static readonly string[] NotForwardedHttpHeaders = new[] { "Connection", "Host" };

        // 终结重定向
        private static readonly string[] NotResponseHttpHeaders = new string[] { "Transfer-Encoding", "Location" };

        private RewriteProxyHttpClient _rewriteProxyHttpClient;

        /// <summary>
        /// URL转发器
        /// </summary>
        /// <param name="proxyHttpClient"></param>
        public UrlRewriteProxy(RewriteProxyHttpClient proxyHttpClient)
        {
            _rewriteProxyHttpClient = proxyHttpClient;
        }

        /// <summary>
        /// URL转发
        /// </summary>
        /// <param name="context"></param>
        /// <param name="rewritePath"></param>
        /// <param name="toHost"></param>
        /// <returns></returns>
        public async Task RewriteUri(HttpContext context, PathString rewritePath, string toHost)
        {
            // 再次判断访问是否含有前缀
            if (context.Request.Path.StartsWithSegments(rewritePath))
            {
                // 替换请求地址为转发的新地址
                var newUri = context.Request.Path.Value.Remove(0, rewritePath.Value.Length) + context.Request.QueryString;
                var targetUri = new Uri(toHost + newUri);

                var requestMessage = GenerateProxifiedRequest(context, targetUri);

                await SendAsync(context, requestMessage);
            }
        }

        private async Task SendAsync(HttpContext context, HttpRequestMessage requestMessage)
        {
            using (var responseMessage = await _rewriteProxyHttpClient.Client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted))
            {
                context.Response.StatusCode = (int)responseMessage.StatusCode;

                context.Response.Headers["Furion-UrlRewrite"] = requestMessage.RequestUri?.ToString();

                foreach (var header in responseMessage.Headers.Where(x => !NotResponseHttpHeaders.Contains(x.Key)))
                {
                    context.Response.Headers[header.Key] = header.Value.ToArray();
                }

                foreach (var header in responseMessage.Content.Headers)
                {
                    context.Response.Headers[header.Key] = header.Value.ToArray();
                }

                await responseMessage.Content.CopyToAsync(context.Response.Body);
            }
        }

        /// <summary>
        /// 构建Http请求体
        /// </summary>
        /// <param name="context"></param>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        private HttpRequestMessage GenerateProxifiedRequest(HttpContext context, Uri targetUri)
        {
            var requestMessage = new HttpRequestMessage();

            // 复制原有的请求头和内容信息到新的请求中
            CopyRequestContentAndHeaders(context, requestMessage);

            requestMessage.RequestUri = targetUri;
            requestMessage.Headers.Host = targetUri.Host;
            requestMessage.Method = GetMethod(context.Request.Method);
            return requestMessage;
        }

        /// <summary>
        /// 复制请求头和内容信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requestMessage"></param>
        private void CopyRequestContentAndHeaders(HttpContext context, HttpRequestMessage requestMessage)
        {
            var requestMethod = context.Request.Method;
            if (!HttpMethods.IsGet(requestMethod)
                 && !HttpMethods.IsHead(requestMethod)
                 && !HttpMethods.IsDelete(requestMethod)
                 && !HttpMethods.IsTrace(requestMethod))
            {
                // 复制请求内容
                var streamContent = new StreamContent(context.Request.Body);
                requestMessage.Content = streamContent;
            }

            // 复制请求头信息
            foreach (var header in context.Request.Headers)
            {
                if (!NotForwardedHttpHeaders.Contains(header.Key))
                {
                    if (header.Key != "User-Agent")
                    {
                        if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()) && requestMessage.Content != null)
                        {
                            requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                        }
                    }
                    else
                    {
                        string userAgent = header.Value.Count > 0 ? (header.Value[0] + " Furion:" + context.TraceIdentifier) : string.Empty;

                        if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, userAgent) && requestMessage.Content != null)
                        {
                            requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, userAgent);
                        }
                    }
                }
            }
        }

        private HttpMethod GetMethod(string method)
        {
            if (HttpMethods.IsDelete(method)) return HttpMethod.Delete;
            if (HttpMethods.IsGet(method)) return HttpMethod.Get;
            if (HttpMethods.IsHead(method)) return HttpMethod.Head;
            if (HttpMethods.IsOptions(method)) return HttpMethod.Options;
            if (HttpMethods.IsPost(method)) return HttpMethod.Post;
            if (HttpMethods.IsPut(method)) return HttpMethod.Put;
            if (HttpMethods.IsTrace(method)) return HttpMethod.Trace;
            return new HttpMethod(method);
        }
    }
}