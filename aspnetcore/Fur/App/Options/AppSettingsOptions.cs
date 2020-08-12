using System.ComponentModel.DataAnnotations;

namespace Fur.Options
{
    /// <summary>
    /// 应用全局配置
    /// </summary>
    [OptionsSettings("AppSettings")]
    public sealed class AppSettingsOptions : IAppOptions
    {
        /// <summary>
        /// 应用类型
        /// </summary>
        [Required]
        public ProjectType Project { get; set; }
    }
}