// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.ConfigurableOptions;
using Microsoft.Extensions.Configuration;
using System;

namespace Fur.DependencyInjection
{
    /// <summary>
    /// 依赖注入配置选项
    /// </summary>
    [OptionsSettings("DependencyInjectionSettings")]
    public sealed class DependencyInjectionSettingsOptions : IConfigurableOptions<DependencyInjectionSettingsOptions>
    {
        /// <summary>
        /// 外部注册定义
        /// </summary>
        public ExternalService[] Definitions { get; set; }

        /// <summary>
        /// 后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(DependencyInjectionSettingsOptions options, IConfiguration configuration)
        {
            options.Definitions ??= Array.Empty<ExternalService>();
        }
    }
}