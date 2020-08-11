using Fur.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fur.Options
{
    /// <summary>
    /// 应用全局配置
    /// </summary>
    [Options("AppSettings")]
    public sealed class AppSettingsOptions : IAppOptions
    {
        /// <summary>
        /// 框架版本
        /// </summary>
        public const string Version = "1.0.0";

        /// <summary>
        /// 应用类型
        /// </summary>
        [Required]
        public ProjectOptions Project { get; set; }
    }
}