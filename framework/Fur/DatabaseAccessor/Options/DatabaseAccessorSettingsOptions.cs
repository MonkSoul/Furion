// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.ConfigurableOptions;
using Microsoft.Extensions.Configuration;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库配置选项
    /// </summary>
    [OptionsSettings("AppSettings:DatabaseAccessorSettings")]
    public sealed class DatabaseAccessorSettingsOptions : IConfigurableOptions<DatabaseAccessorSettingsOptions>
    {
        /// <summary>
        /// 是否启用多租户支持
        /// </summary>
        public bool? EnabledMultiTenant { get; set; }

        /// <summary>
        /// 多租户实现方式
        /// </summary>
        public MultiTenantPattern? MultiTenantOptions { get; set; }

        /// <summary>
        /// 选项后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(DatabaseAccessorSettingsOptions options, IConfiguration configuration)
        {
            EnabledMultiTenant ??= false;
            MultiTenantOptions ??= MultiTenantPattern.None;
        }
    }
}