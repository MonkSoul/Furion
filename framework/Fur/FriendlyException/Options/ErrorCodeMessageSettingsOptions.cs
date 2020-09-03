// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Fur.ConfigurableOptions;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 异常配置选项，最优的方式是采用后期配置，也就是所有异常状态码先不设置（推荐）
    /// </summary>
    [OptionsSettings("AppSettings:ErrorCodeMessageSettings")]
    public sealed class ErrorCodeMessageSettingsOptions : IConfigurableOptions
    {
        /// <summary>
        /// 异常状态码配置列表
        /// </summary>
        public object[][] Definitions { get; set; }
    }
}