using Fur.ConfigurableOptions;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 异常配置选项，最优的方式是采用后期配置，也就是所有异常状态码先不设置（推荐）
    /// </summary>
    public sealed class ErrorCodeMessageSettingsOptions : IConfigurableOptions
    {
        /// <summary>
        /// 异常状态码配置列表
        /// </summary>
        public object[][] Definitions { get; set; }
    }
}