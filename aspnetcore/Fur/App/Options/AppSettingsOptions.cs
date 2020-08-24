namespace Fur.Options
{
    /// <summary>
    /// 应用全局配置
    /// </summary>
    public sealed class AppSettingsOptions : IAppOptions<AppSettingsOptions>
    {
        /// <summary>
        /// MiniProfiler 插件路径
        /// </summary>
        internal const string MiniProfilerRouteBasePath = "/index-mini-profiler";

        /// <summary>
        /// 应用类型
        /// </summary>
        public ProjectType Project { get; set; }

        /// <summary>
        /// 集成 MiniProfiler 组件
        /// </summary>
        public bool? InjectMiniProfiler { get; set; }

        /// <summary>
        /// 后期配置
        /// </summary>
        /// <param name="options"></param>
        public void PostConfigure(AppSettingsOptions options)
        {
            options.InjectMiniProfiler ??= true;
        }
    }
}