using Furion.JsonSerialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// HttpClient 对象组装部件
    /// </summary>
    public sealed partial class HttpClientPart
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; private set; }

        /// <summary>
        /// Url 地址模板
        /// </summary>
        public Dictionary<string, object> Templates { get; private set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public HttpMethod Method { get; private set; }

        /// <summary>
        /// 请求报文头
        /// </summary>
        public Dictionary<string, object> Headers { get; private set; }

        /// <summary>
        /// 查询参数
        /// </summary>
        public Dictionary<string, object> Queries { get; private set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; private set; }

        /// <summary>
        /// 请求报文 Body 参数
        /// </summary>
        public object Body { get; private set; }

        /// <summary>
        /// 请求报文 Body 内容类型
        /// </summary>
        public string ContentType { get; private set; } = "application/json";

        /// <summary>
        /// 内容编码
        /// </summary>
        public Encoding ContentEncoding { get; private set; } = Encoding.UTF8;

        /// <summary>
        /// 设置 Body Bytes 类型
        /// </summary>
        public List<(string Name, byte[] Bytes, string FileName)> BodyBytes { get; private set; } = new List<(string Name, byte[] Bytes, string FileName)>();

        /// <summary>
        /// Json 序列化提供器
        /// </summary>
        public (Type ProviderType, object JsonSerializerOptions) JsonSerialization { get; private set; } = (typeof(SystemTextJsonSerializerProvider), default);

        /// <summary>
        /// 是否启用模型验证
        /// </summary>
        public (bool Enabled, bool IncludeNull) ValidationState { get; private set; } = (false, false);

        /// <summary>
        /// 构建请求对象拦截器
        /// </summary>
        public List<Action<HttpRequestMessage>> RequestInterceptors { get; private set; } = new List<Action<HttpRequestMessage>>();

        /// <summary>
        /// 创建客户端对象拦截器
        /// </summary>
        public List<Action<HttpClient>> HttpClientInterceptors { get; private set; } = new List<Action<HttpClient>>();

        /// <summary>
        /// 请求成功拦截器
        /// </summary>
        public List<Action<HttpResponseMessage>> ResponseInterceptors { get; private set; } = new List<Action<HttpResponseMessage>>();

        /// <summary>
        /// 请求异常拦截器
        /// </summary>
        public List<Action<HttpResponseMessage, string>> ExceptionInterceptors { get; private set; } = new List<Action<HttpResponseMessage, string>>();
    }
}