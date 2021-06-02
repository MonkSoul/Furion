// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.7.9
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

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
        public bool? Enabled { get; set; }

        /// <summary>
        /// URL转发规则列表
        /// </summary>
        public string[][] Rules { get; set; }

        /// <summary>
        /// 配置后期处理
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(UrlRewriteSettingsOptions options, IConfiguration configuration)
        {
            options.Enabled ??= false;
            options.Rules ??= Array.Empty<string[]>();
        }
    }
}