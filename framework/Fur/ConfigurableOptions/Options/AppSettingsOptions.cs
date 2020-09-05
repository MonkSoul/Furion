// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur 
// 开源协议：Apache-2.0（https://gitee.com/monksoul/Fur/blob/alpha/LICENSE）

using Microsoft.Extensions.Configuration;

namespace Fur.ConfigurableOptions
{
    /// <summary>
    /// 应用全局配置
    /// </summary>
    public sealed class AppSettingsOptions : IConfigurableOptions<AppSettingsOptions>
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
        public void PostConfigure(AppSettingsOptions options, IConfiguration configuration)
        {
            options.InjectMiniProfiler ??= true;
        }
    }
}