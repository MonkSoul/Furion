// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.ConfigurableOptions;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Fur.DynamicApiController
{
    /// <summary>
    /// 动态接口控制器配置
    /// </summary>
    [OptionsSettings("DynamicApiControllerSettings")]
    public sealed class DynamicApiControllerSettingsOptions : IConfigurableOptions<DynamicApiControllerSettingsOptions>
    {
        /// <summary>
        /// 默认路由前缀
        /// </summary>
        public string DefaultRoutePrefix { get; set; }

        /// <summary>
        /// 默认请求谓词
        /// </summary>
        [Required]
        public string DefaultHttpMethod { get; set; }

        /// <summary>
        /// 默认模块（区域）名称
        /// </summary>
        public string DefaultModule { get; set; }

        /// <summary>
        /// 小写路由
        /// </summary>
        public bool? LowercaseRoute { get; set; }

        /// <summary>
        /// 保留行为名称谓词
        /// </summary>
        public bool? KeepVerb { get; set; }

        /// <summary>
        /// 保留名称
        /// </summary>
        public bool? KeepName { get; set; }

        /// <summary>
        /// 骆驼命名分隔符
        /// </summary>
        public string CamelCaseSeparator { get; set; }

        /// <summary>
        /// 版本号分隔符
        /// </summary>
        [Required]
        public string VersionSeparator { get; set; }

        /// <summary>
        /// 模型转查询参数（只有GET、HEAD请求有效）
        /// </summary>
        public bool? ModelToQuery { get; set; }

        /// <summary>
        /// 支持Mvc控制器处理
        /// </summary>
        public bool? SupportedMvcController { get; set; }

        /// <summary>
        /// 被舍弃的控制器名称前后缀
        /// </summary>
        public string[] AbandonControllerAffixes { get; set; }

        /// <summary>
        /// 被舍弃的行为名称前后缀
        /// </summary>
        public string[] AbandonActionAffixes { get; set; }

        /// <summary>
        /// 选项后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(DynamicApiControllerSettingsOptions options, IConfiguration configuration)
        {
            options.DefaultRoutePrefix ??= "api";
            options.DefaultHttpMethod ??= "POST";
            options.LowercaseRoute ??= true;
            options.KeepVerb ??= false;
            options.KeepName ??= false;
            options.CamelCaseSeparator ??= "-";
            options.VersionSeparator ??= "@";
            options.ModelToQuery ??= false;
            options.SupportedMvcController ??= false;
            options.AbandonControllerAffixes ??= new string[]
            {
                "AppServices",
                "AppService",
                "ApiController",
                "Controller",
                "Services",
                "Service"
            };
            options.AbandonActionAffixes ??= new string[]
            {
                "Async"
            };
        }
    }
}