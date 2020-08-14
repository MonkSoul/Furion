using Fur.Options;

namespace Fur.SimulateController
{
    /// <summary>
    /// 模刻控制器配置
    /// </summary>
    [OptionsSettings("AppSettings:SimulateSettings")]
    public sealed class SimulateSettingsOptions : IAppOptions<SimulateSettingsOptions>
    {
        /// <summary>
        /// 默认路由前缀
        /// </summary>
        public string DefaultRoutePrefix { get; set; }

        /// <summary>
        /// 默认请求谓词
        /// </summary>
        public string DefaultHttpMethod { get; set; }

        /// <summary>
        /// 默认区域（模块）名称
        /// </summary>
        public string DefaultAreaName { get; set; }

        /// <summary>
        /// 小写路由
        /// </summary>
        public bool LowerCaseRoute { get; set; }

        /// <summary>
        /// 保留行为名称谓词
        /// </summary>
        public bool KeepVerb { get; set; }

        /// <summary>
        /// 移除控制器名前后缀
        /// </summary>
        public string[] ControllerNameAffixes { get; set; }

        /// <summary>
        /// 移除行为名前后缀
        /// </summary>
        public string[] ActionNameAffixes { get; set; }

        /// <summary>
        /// 默认配置
        /// </summary>
        /// <param name="options"></param>
        public void PostConfigure(SimulateSettingsOptions options)
        {
            options.DefaultRoutePrefix = "api";
            options.DefaultHttpMethod = "POST";
            options.LowerCaseRoute = true;
            options.KeepVerb = false;
            options.ControllerNameAffixes = new string[]
            {
                "AppServices",
                "AppService",
                "ApiController",
                "Controller",
                "Services",
                "Service"
            };
            options.ActionNameAffixes = new string[]
            {
                "Async"
            };
        }
    }
}