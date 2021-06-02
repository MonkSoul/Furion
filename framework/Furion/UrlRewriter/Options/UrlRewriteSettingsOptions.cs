using Furion.ConfigurableOptions;
using Furion.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;

namespace Furion.UrlRewriter
{
    /// <summary>
    /// URL转发选项
    /// </summary>
    [SkipScan]
    public class UrlRewriteSettingsOptions : IConfigurableOptions<UrlRewriteSettingsOptions>
    {
        /// <summary>
        /// 是否启用URL转发规则
        /// </summary>
        public bool UrlRewriteEnable { get; set; }

        /// <summary>
        /// URL转发规则列表
        /// </summary>
        public string[][] UrlRewriteRules { get; set; }

        /// <summary>
        /// 配置后期处理
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(UrlRewriteSettingsOptions options, IConfiguration configuration)
        {
            options.UrlRewriteRules ??= Array.Empty<string[]>();
        }
    }
}