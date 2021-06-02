// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.7.9
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

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
    public class UrlRewriteProxy : IUrlRewriterProxy
    {
        /// <summary>
        /// 无需转发的请求头信息
        /// </summary>
        private static readonly string[] NotForwardedHttpHeaders = new[] { "Connection", "Host" };

        /// <summary>
        /// 终结重定向
        /// </summary>
        private static readonly string[] NotResponseHttpHeaders = new string[] { "Transfer-Encoding", "Location" };

        /// <summary>
        /// Url 重写代理 HttpClient 对象
        /// </summary>
        private readonly UrlRewriteProxyHttpClient _urlRewriteProxyHttpClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="urlRewriteProxyHttpClient"></param>
        public UrlRewriteProxy(UrlRewriteProxyHttpClient urlRewriteProxyHttpClient)
        {
            _urlRewriteProxyHttpClient = urlRewriteProxyHttpClient;
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

                // 构建 HttpRequestMessage 对象
                var requestMessage = CreateHttpRequestMessage(context, targetUri);

                await SendAsync(context, requestMessage);
            }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        private async Task SendAsync(HttpContext context, HttpRequestMessage requestMessage)
        {
            using var responseMessage = await _urlRewriteProxyHttpClient.Client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted);
            context.Response.StatusCode = (int)responseMessage.StatusCode;

            context.Response.Headers["Furion-UrlRewrite"] = requestMessage.RequestUri?.ToString();

            // 添加目标主机头
            foreach (var header in responseMessage.Headers.Where(x => !NotResponseHttpHeaders.Contains(x.Key)))
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }

            // 添加目标内容头
            foreach (var header in responseMessage.Content.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }

            // 拷贝内容
            await responseMessage.Content.CopyToAsync(context.Response.Body);
        }

        /// <summary>
        /// 构建 Http 请求体
        /// </summary>
        /// <param name="context"></param>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        private static HttpRequestMessage CreateHttpRequestMessage(HttpContext context, Uri targetUri)
        {
            var requestMessage = new HttpRequestMessage();

            // 复制原有的请求头和内容信息到新的请求中
            CopyRequestContentAndHeaders(context, requestMessage);

            requestMessage.RequestUri = targetUri;
            requestMessage.Headers.Host = targetUri.Host;
            requestMessage.Method = GetHttpMethod(context.Request.Method);
            return requestMessage;
        }

        /// <summary>
        /// 复制请求头和内容信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requestMessage"></param>
        private static void CopyRequestContentAndHeaders(HttpContext context, HttpRequestMessage requestMessage)
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
                if (NotForwardedHttpHeaders.Contains(header.Key)) continue;

                if (header.Key != "User-Agent")
                {
                    if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()) && requestMessage.Content != null)
                    {
                        requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                    }
                }
                else
                {
                    var userAgent = header.Value.Count > 0 ? (header.Value[0] + " Furion:" + context.TraceIdentifier) : string.Empty;

                    if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, userAgent) && requestMessage.Content != null)
                    {
                        requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, userAgent);
                    }
                }
            }
        }

        /// <summary>
        /// 解析 HttpMethod 对象
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        private static HttpMethod GetHttpMethod(string httpMethod)
        {
            if (HttpMethods.IsDelete(httpMethod)) return HttpMethod.Delete;
            if (HttpMethods.IsGet(httpMethod)) return HttpMethod.Get;
            if (HttpMethods.IsHead(httpMethod)) return HttpMethod.Head;
            if (HttpMethods.IsOptions(httpMethod)) return HttpMethod.Options;
            if (HttpMethods.IsPost(httpMethod)) return HttpMethod.Post;
            if (HttpMethods.IsPut(httpMethod)) return HttpMethod.Put;
            if (HttpMethods.IsTrace(httpMethod)) return HttpMethod.Trace;

            return new HttpMethod(httpMethod);
        }
    }
}