using Fur.Options;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 异常配置选项，最优的方式是采用后期配置，也就是所有异常状态码先不设置（推荐）
    /// </summary>
    [OptionsSettings("AppSettings:ErrorCodesSettings")]
    public sealed class ErrorCodesSettingsOptions : IAppOptions
    {
        /// <summary>
        /// 异常状态码配置列表
        /// </summary>
        public object[][] Datas { get; set; }
    }
}