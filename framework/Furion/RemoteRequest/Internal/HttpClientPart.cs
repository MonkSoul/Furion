using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// HttpClient 对象组装部件
    /// </summary>
    internal sealed class HttpClientPart
    {
        /// <summary>
        /// 请求方式
        /// </summary>
        internal HttpMethod HttpMethod { get; private set; }

        /// <summary>
        /// 请求报文头
        /// </summary>
        internal Dictionary<string, string> Headers { get; private set; }

        /// <summary>
        /// 查询参数
        /// </summary>
        internal Dictionary<string, object> QueryArgs { get; private set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        internal string ClientName { get; private set; }

        /// <summary>
        /// 请求报文 Body 参数
        /// </summary>
        internal object BodyArgs { get; private set; }

        /// <summary>
        /// 请求报文 Body 内容类型
        /// </summary>
        internal string ContentType { get; private set; }

        /// <summary>
        /// Json 序列化提供器
        /// </summary>
        internal (Type, object) SerializationProvider { get; private set; }

        /// <summary>
        /// 是否启用模型状态
        /// </summary>
        internal bool EnabledModelState { get; private set; }

        /// <summary>
        /// 请求之前
        /// </summary>
        internal Action<HttpRequestMessage> OnRequesting { get; private set; }

        /// <summary>
        /// 请求成功之前
        /// </summary>
        internal Action<HttpResponseMessage> OnResponsing { get; private set; }

        /// <summary>
        /// 请求异常
        /// </summary>
        internal Func<object> OnException { get; private set; }
    }
}