namespace Fur.Options
{
    /// <summary>
    /// 应用全局配置
    /// </summary>
    public sealed class AppSettingsOptions : IAppOptions
    {
        /// <summary>
        /// 应用类型
        /// </summary>
        public ProjectType Project { get; set; }
    }
}