using Fur.ConfigurableOptions;
using Microsoft.Extensions.Configuration;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 友好异常配置选项
    /// </summary>
    public sealed class FriendlyExceptionSettingsOptions : IConfigurableOptions<FriendlyExceptionSettingsOptions>
    {
        /// <summary>
        /// 隐藏错误码
        /// </summary>
        public bool? HideErrorCode { get; set; }

        /// <summary>
        /// 默认错误码
        /// </summary>
        public string DefaultErrorCode { get; set; }

        /// <summary>
        /// 默认错误消息
        /// </summary>
        public string DefaultErrorMessage { get; set; }

        /// <summary>
        /// 选项后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(FriendlyExceptionSettingsOptions options, IConfiguration configuration)
        {
            options.HideErrorCode ??= false;
            options.DefaultErrorCode ??= string.Empty;
            options.DefaultErrorMessage ??= "Internal Server Error";
        }
    }
}