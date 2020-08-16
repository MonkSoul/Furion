using Fur.Options;

namespace Fur.LazyController
{
    /// <summary>
    /// 惰性控制器配置
    /// </summary>
    [OptionsSettings("AppSettings:LazyControllerSettings")]
    public sealed class LazyControllerSettingsOptions : IAppOptions<LazyControllerSettingsOptions>
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
        /// 支持Mvc控制器类型
        /// </summary>
        public bool SupportedControllerBase { get; set; }

        /// <summary>
        /// 骆驼命名分隔符
        /// </summary>
        public string CamelCaseSeparator { get; set; }

        /// <summary>
        /// 被舍弃的控制器名称前后缀
        /// </summary>
        public string[] AbandonControllerAffixes { get; set; }

        /// <summary>
        /// 被舍弃的行为名称前后缀
        /// </summary>
        public string[] AbandonActionAffixes { get; set; }

        /// <summary>
        /// 默认配置
        /// </summary>
        /// <param name="options"></param>
        public void PostConfigure(LazyControllerSettingsOptions options)
        {
            options.DefaultRoutePrefix = "api";
            options.DefaultHttpMethod = "POST";
            options.LowerCaseRoute = true;
            options.KeepVerb = false;
            options.SupportedControllerBase = false;
            options.CamelCaseSeparator = "-";
            options.AbandonControllerAffixes = new string[]
            {
                "AppServices",
                "AppService",
                "ApiController",
                "Controller",
                "Services",
                "Service"
            };
            options.AbandonActionAffixes = new string[]
            {
                "Async"
            };
        }
    }
}