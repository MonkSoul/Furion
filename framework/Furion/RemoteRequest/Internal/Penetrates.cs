using Furion.Extensions;
using Furion.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 内部公共类
    /// </summary>
    internal static class Penetrates
    {
        /// <summary>
        /// MiniProfiler 分类名
        /// </summary>
        internal const string MiniProfilerCategory = "httpRequest";

        /// <summary>
        /// 设置方法体
        /// </summary>
        /// <param name="request"></param>
        /// <param name="bodyArgs"></param>
        /// <param name="bodyContentTypeOptions"></param>
        /// <param name="jsonNamingPolicyOptions"></param>
        /// <param name="contentType"></param>
        internal static void SetHttpRequestBody(HttpRequestMessage request, object bodyArgs, HttpContentTypeOptions bodyContentTypeOptions, JsonNamingPolicyOptions jsonNamingPolicyOptions, string contentType)
        {
            // 处理 body 内容
            HttpContent httpContent;
            switch (bodyContentTypeOptions)
            {
                case HttpContentTypeOptions.StringContent:
                case HttpContentTypeOptions.JsonStringContent:
                case HttpContentTypeOptions.XmlStringContent:
                    string bodyContent;
                    // application/json;text/json;application/*+json
                    if (bodyContentTypeOptions == HttpContentTypeOptions.JsonStringContent)
                    {
                        // 配置 Json 命名策略
                        var jsonSerializerOptions = JsonSerializerUtility.GetDefaultJsonSerializerOptions();
                        jsonSerializerOptions.PropertyNamingPolicy = jsonNamingPolicyOptions switch
                        {
                            JsonNamingPolicyOptions.CamelCase => JsonNamingPolicy.CamelCase,
                            JsonNamingPolicyOptions.Null => null,
                            _ => null
                        };

                        bodyContent = JsonSerializerUtility.Serialize(bodyArgs, jsonSerializerOptions);
                    }
                    // application/xml;text/xml;application/*+xml
                    else if (bodyContentTypeOptions == HttpContentTypeOptions.XmlStringContent)
                    {
                        var xmlSerializer = new XmlSerializer(bodyArgs.GetType());
                        var buffer = new StringBuilder();

                        using var writer = new StringWriter(buffer);
                        xmlSerializer.Serialize(writer, bodyArgs);

                        bodyContent = buffer.ToString();
                    }
                    // none
                    else bodyContent = bodyArgs.ToString();

                    httpContent = new StringContent(bodyContent, Encoding.UTF8);
                    break;
                // 处理 x-www-form-urlencoded
                case HttpContentTypeOptions.FormUrlEncodedContent:
                    Dictionary<string, string> formDataDic = new();
                    if (bodyArgs is Dictionary<string, string> dic) formDataDic = dic;
                    else
                    {
                        var bodyArgsType = bodyArgs.GetType();

                        // 只有类和匿名类才处理
                        if (bodyArgsType.IsClass || bodyArgsType.IsAnonymous())
                        {
                            var properties = bodyArgsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                            foreach (var prop in properties)
                            {
                                formDataDic.Add(prop.Name, prop.GetValue(bodyArgs)?.ToString());
                            }
                        }
                    }
                    httpContent = new FormUrlEncodedContent(formDataDic);
                    break;
                // 处理 multipart/form-data
                case HttpContentTypeOptions.MultipartFormDataContent:
                default:
                    throw new NotImplementedException("Please use RequestInterceptor to set.");
            }

            // 设置内容
            if (httpContent != null)
            {
                httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                request.Content = httpContent;
            }
        }

        /// <summary>
        /// 创建请求异常信息
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static async Task<HttpRequestException> CreateRequestException(HttpResponseMessage response)
        {
            // 读取错误数据
            var errorMessage = await response.Content.ReadAsStringAsync();

            // 打印失败消息
            App.PrintToMiniProfiler(MiniProfilerCategory, "Failed", errorMessage, isError: true);

            // 抛出请求异常
            return new HttpRequestException(errorMessage);
        }
    }
}