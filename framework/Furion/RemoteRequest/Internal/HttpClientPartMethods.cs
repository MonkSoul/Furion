using Furion.DataValidation;
using Furion.Extensions;
using Furion.JsonSerialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// HttpClient 对象组装部件
    /// </summary>
    public sealed partial class HttpClientPart
    {
        /// <summary>
        /// MiniProfiler 分类名
        /// </summary>
        private const string MiniProfilerCategory = "httpclient";

        /// <summary>
        /// 发送 GET 请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T> GetAsAsync<T>(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Get).SendAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 GET 请求返回 Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> GetAsStreamAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Get).SendAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 GET 请求返回 String
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetAsStringAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Get).SendAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 GET 请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> GetAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Get).SendAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 POST 请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T> PostAsAsync<T>(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Post).SendAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 POST 请求返回 Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> PostAsStreamAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Post).SendAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 POST 请求返回 String
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> PostAsStringAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Post).SendAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 POST 请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> PostAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Post).SendAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 PUT 请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T> PutAsAsync<T>(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Put).SendAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 PUT 请求返回 Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> PutAsStreamAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Put).SendAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 PUT 请求返回 String
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> PutAsStringAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Put).SendAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 PUT 请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> PutAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Put).SendAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 DELETE 请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T> DeleteAsAsync<T>(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Delete).SendAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 DELETE 请求返回 Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> DeleteAsStreamAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Delete).SendAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 DELETE 请求返回 String
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> DeleteAsStringAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Delete).SendAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 DELETE 请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> DeleteAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Delete).SendAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 PATCH 请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T> PatchAsAsync<T>(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Patch).SendAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 PATCH 请求返回 Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> PatchAsStreamAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Patch).SendAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 Patch 请求返回 String
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> PatchAsStringAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Patch).SendAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 PATCH 请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> PatchAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Patch).SendAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 HEAD 请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T> HeadAsAsync<T>(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Head).SendAsAsync<T>(cancellationToken);
        }

        /// <summary>
        /// 发送 HEAD 请求返回 Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> HeadAsStreamAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Head).SendAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 Head 请求返回 String
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> HeadAsStringAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Head).SendAsStringAsync(cancellationToken);
        }

        /// <summary>
        /// 发送 HEAD 请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> HeadAsync(CancellationToken cancellationToken = default)
        {
            return SetHttpMethod(HttpMethod.Head).SendAsync(cancellationToken);
        }

        /// <summary>
        /// 发送请求返回 T 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<T> SendAsAsync<T>(CancellationToken cancellationToken = default)
        {
            // 如果 T 是 HttpRequestMessage 类型，则返回
            if (typeof(T) == typeof(HttpRequestMessage))
            {
                var httpResponseMessage = await SendAsync(cancellationToken);
                return (T)(object)httpResponseMessage;
            }
            if (typeof(T) == typeof(string))
            {
                var str = await SendAsStringAsync(cancellationToken);
                return (T)(object)str;
            }

            // 解析 Json 序列化提供器
            var jsonSerializer = App.GetService(JsonSerialization.ProviderType ?? typeof(SystemTextJsonSerializerProvider)) as IJsonSerializerProvider;

            // 读取流内容并转换成字符串
            var stream = await SendAsStreamAsync(cancellationToken);

            // 如果 T 是 Stream 类型，则返回
            if (typeof(T) == typeof(Stream)) return (T)(object)stream;

            using var streamReader = new StreamReader(stream);
            var text = await streamReader.ReadToEndAsync();
            // 释放流
            await stream.DisposeAsync();

            // 反序列化流
            var result = jsonSerializer.Deserialize<T>(text, JsonSerialization.JsonSerializerOptions);
            return result;
        }

        /// <summary>
        /// 发送请求返回 Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Stream> SendAsStreamAsync(CancellationToken cancellationToken = default)
        {
            var response = await SendAsync(cancellationToken);

            // 如果配置了异常拦截器，且请求不成功，则返回 T 默认值
            if (ExceptionInterceptors != null && ExceptionInterceptors.Count > 0 && !response.IsSuccessStatusCode) return default;

            // 读取响应流
            var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            return stream;
        }

        /// <summary>
        /// 发送请求返回 String
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> SendAsStringAsync(CancellationToken cancellationToken = default)
        {
            var response = await SendAsync(cancellationToken);

            // 如果配置了异常拦截器，且请求不成功，则返回 T 默认值
            if (ExceptionInterceptors != null && ExceptionInterceptors.Count > 0 && !response.IsSuccessStatusCode) return default;

            // 读取响应报文
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            return content;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(CancellationToken cancellationToken = default)
        {
            // 检查是否配置了请求方法
            if (Method == null) throw new NullReferenceException(nameof(Method));

            // 检查请求地址
            if (string.IsNullOrEmpty(RequestUrl)) throw new NullReferenceException(RequestUrl);

            // 处理模板问题
            ReplaceRequestUrlTemplates();

            // 构建请求对象
            var request = new HttpRequestMessage(Method, RequestUrl);
            request.AppendQueries(Queries);

            // 设置请求报文头
            if (Headers != null && Headers.Count > 0)
            {
                foreach (var header in Headers)
                {
                    if (header.Value != null) request.Headers.Add(header.Key, header.Value.ToString());
                }
            }

            // 验证模型参数（只作用于 body 类型）
            if (ValidationState.Enabled)
            {
                // 判断是否启用 Null 验证且 body 值为 null
                if (ValidationState.IncludeNull && Body == null) throw new InvalidOperationException($"{nameof(Body)} can not be null.");

                // 验证模型
                Body?.Validate();
            }

            // 设置 HttpContent
            SetHttpContent(request);

            // 配置请求拦截
            RequestInterceptors.ForEach(u =>
            {
                u?.Invoke(request);
            });

            // 创建客户端请求工厂
            var clientFactory = App.GetService<IHttpClientFactory>();
            if (clientFactory == null) throw new InvalidOperationException("please add `services.AddRemoteRequest()` in Startup.cs.");

            // 创建 HttpClient 对象
            using var httpClient = string.IsNullOrEmpty(ClientName)
                                         ? clientFactory.CreateClient()
                                         : clientFactory.CreateClient(ClientName);

            // 配置 HttpClient 拦截
            HttpClientInterceptors.ForEach(u =>
            {
                u?.Invoke(httpClient);
            });

            // 打印发送请求
            App.PrintToMiniProfiler(MiniProfilerCategory, "Sending", $"[{Method}] {httpClient.BaseAddress?.OriginalString}{request.RequestUri?.OriginalString}");

            // 发送请求
            var response = await httpClient.SendAsync(request, cancellationToken);

            // 请求成功
            if (response.IsSuccessStatusCode)
            {
                // 打印成功请求
                App.PrintToMiniProfiler(MiniProfilerCategory, "Succeeded", $"[StatusCode: {response.StatusCode}] Succeeded");

                // 调用成功拦截器
                ResponseInterceptors.ForEach(u =>
                {
                    u?.Invoke(response);
                });
            }
            // 请求异常
            else
            {
                // 读取错误消息
                var errors = await response.Content.ReadAsStringAsync(cancellationToken);

                // 打印失败请求
                App.PrintToMiniProfiler(MiniProfilerCategory, "Failed", $"[StatusCode: {response.StatusCode}] {errors}", true);

                // 抛出异常
                if (ExceptionInterceptors == null || ExceptionInterceptors.Count == 0) throw new HttpRequestException(errors);
                // 调用异常拦截器
                else ExceptionInterceptors.ForEach(u =>
                {
                    u?.Invoke(response, errors);
                });
            }

            return response;
        }

        /// <summary>
        /// 设置 HttpContent
        /// </summary>
        /// <param name="request"></param>
        private void SetHttpContent(HttpRequestMessage request)
        {
            // GET/HEAD 请求不支持设置 Body 请求
            if (Body == null || Method == HttpMethod.Get || Method == HttpMethod.Head) return;

            HttpContent httpContent = null;

            // 处理各种 Body 类型
            switch (ContentType)
            {
                case "multipart/form-data":
                    var multipartFormDataContent = new MultipartFormDataContent();

                    // 添加 Bytes 类型
                    foreach (var (Name, Bytes, FileName) in BodyBytes)
                    {
                        if (string.IsNullOrEmpty(FileName))
                            multipartFormDataContent.Add(new ByteArrayContent(Bytes), Name);
                        else
                            multipartFormDataContent.Add(new ByteArrayContent(Bytes), Name, FileName);
                    }

                    // 处理其他类型
                    var dic = ConvertBodyToDictionary();
                    if (dic == null || dic.Count == 0)
                    {
                        foreach (var (key, value) in dic)
                        {
                            multipartFormDataContent.Add(new StringContent(value, ContentEncoding), string.Format("\"{0}\"", key));
                        }
                    }

                    if (multipartFormDataContent.Any()) httpContent = multipartFormDataContent;

                    break;

                case "application/json":
                case "text/json":
                case "application/*+json":
                    httpContent = new StringContent(SerializerObject(Body), ContentEncoding);
                    break;

                case "application/x-www-form-urlencoded":
                    // 解析字典
                    var keyValues = ConvertBodyToDictionary();

                    if (keyValues == null || keyValues.Count == 0) return;

                    httpContent = new FormUrlEncodedContent(keyValues);
                    break;
            }

            if (httpContent != null)
            {
                // 设置内容类型
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(ContentType);

                // 设置 HttpContent
                request.Content = httpContent;
            }
        }

        /// <summary>
        /// 转换 Body 为 字典类型
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> ConvertBodyToDictionary()
        {
            Dictionary<string, string> keyValues = null;

            // 处理各种情况
            if (Body is Dictionary<string, string> dic) keyValues = dic;
            else if (Body is Dictionary<string, object> dicObj) keyValues = dicObj.ToDictionary(u => u.Key, u => SerializerObject(u.Value));
            else keyValues = Body.ToDictionary<string>();
            return keyValues;
        }

        /// <summary>
        /// 模板正则表达式
        /// </summary>
        private const string templatePattern = @"\{(?<p>.+?)\}";

        /// <summary>
        /// 替换 Url 地址模板参数
        /// </summary>
        /// <returns></returns>
        private void ReplaceRequestUrlTemplates()
        {
            // 如果模板为空，则跳过
            if (Templates == null || Templates.Count == 0) return;

            // 判断请求地址是否包含模板
            if (!Regex.IsMatch(RequestUrl, templatePattern)) return;

            // 获取所有匹配的模板
            var templateValues = Regex.Matches(RequestUrl, templatePattern)
                                                       .Select(u => new
                                                       {
                                                           Template = u.Groups["p"].Value,
                                                           Value = MatchTemplateValue(u.Groups["p"].Value, Templates)
                                                       });

            // 循环替换模板
            foreach (var item in templateValues)
            {
                RequestUrl = RequestUrl.Replace($"{{{item.Template}}}", HttpUtility.UrlEncode(item.Value?.ToString() ?? string.Empty));
            }
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private string SerializerObject(object body)
        {
            // 解析序列化工具
            var jsonSerializer = App.GetService(JsonSerialization.ProviderType) as IJsonSerializerProvider;
            return jsonSerializer.Serialize(body, JsonSerialization.JsonSerializerOptions);
        }

        /// <summary>
        /// 匹配模板值
        /// </summary>
        /// <param name="template"></param>
        /// <param name="templateData"></param>
        /// <returns></returns>
        private static object MatchTemplateValue(string template, Dictionary<string, object> templateData)
        {
            string tmpl;
            if (!template.Contains(".", StringComparison.CurrentCulture)) tmpl = template;
            else tmpl = template.Split('.', StringSplitOptions.RemoveEmptyEntries).First();

            var templateValue = templateData.ContainsKey(tmpl) ? templateData[tmpl] : default;
            return ResolveTemplateValue(template, templateValue);
        }

        /// <summary>
        /// 解析模板的值
        /// </summary>
        /// <param name="template"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static object ResolveTemplateValue(string template, object data)
        {
            // 根据 . 分割模板
            var propertyCrumbs = template.Split('.', StringSplitOptions.RemoveEmptyEntries);
            return GetValue(propertyCrumbs, data);

            // 静态本地函数
            static object GetValue(string[] propertyCrumbs, object data)
            {
                if (data == null || propertyCrumbs == null || propertyCrumbs.Length <= 1) return data;
                var dataType = data.GetType();

                // 如果是基元类型则直接返回
                if (dataType.IsRichPrimitive()) return data;
                object value = null;

                // 递归获取下一级模板值
                for (int i = 1; i < propertyCrumbs.Length; i++)
                {
                    var propery = dataType.GetProperty(propertyCrumbs[i]);
                    if (propery == null) break;

                    value = propery.GetValue(data);
                    if (i + 1 < propertyCrumbs.Length)
                    {
                        value = GetValue(propertyCrumbs.Skip(i).ToArray(), value);
                    }
                    else break;
                }

                return value;
            }
        }
    }
}