using Furion.ConfigurableOptions;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 远程请求设置
    /// </summary>
    public sealed class RemoteRequestSettingsOptions : IConfigurableOptions
    {
        /// <summary>
        /// 客户端定义
        /// </summary>
        public HttpClientConfigure[] HttpClientDefinitions { get; set; }
    }
}