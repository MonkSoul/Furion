// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;
using Microsoft.Extensions.Configuration;
using System;

namespace Furion.Localization
{
    /// <summary>
    /// 多语言配置选项
    /// </summary>
    public sealed class LocalizationSettingsOptions : IConfigurableOptions<LocalizationSettingsOptions>
    {
        /// <summary>
        /// 资源路径
        /// </summary>
        public string ResourcesPath { get; set; }

        /// <summary>
        /// 支持的语言列表
        /// </summary>
        public string[] SupportedCultures { get; set; }

        /// <summary>
        /// 默认的语言
        /// </summary>
        public string DefaultCulture { get; set; }

        /// <summary>
        /// 选项后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(LocalizationSettingsOptions options, IConfiguration configuration)
        {
            ResourcesPath ??= "Resources";
            SupportedCultures ??= Array.Empty<string>();
            DefaultCulture ??= string.Empty;
        }
    }
}