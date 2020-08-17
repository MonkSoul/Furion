using System.ComponentModel.DataAnnotations;

namespace Fur.Options
{
    /// <summary>
    /// 应用全局配置
    /// </summary>
    [OptionsSettings("AppSettings")]
    public sealed class AppSettingsOptions : IAppOptions<AppSettingsOptions>
    {
        /// <summary>
        /// 应用类型
        /// </summary>
        [Required]
        public ProjectType Project { get; set; }

        /// <summary>
        /// 默认配置
        /// </summary>
        /// <param name="options"></param>
        public void PostConfigure(AppSettingsOptions options)
        {
            options.Project = ProjectType.RESTfulAPI;
        }
    }
}