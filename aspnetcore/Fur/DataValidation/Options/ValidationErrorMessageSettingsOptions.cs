using Fur.Options;

namespace Fur.DataValidation
{
    /// <summary>
    /// 验证消息配置选项
    /// </summary>
    [OptionsSettings("AppSettings:ValidationErrorMessageSettings")]
    public sealed class ValidationErrorMessageSettingsOptions : IAppOptions
    {
        /// <summary>
        /// 验证消息配置表
        /// </summary>
        public object[][] Definitions { get; set; }
    }
}