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
using System.ComponentModel.DataAnnotations;

namespace Furion.CorsAccessor
{
    /// <summary>
    /// 跨域配置选项
    /// </summary>
    public sealed class CorsAccessorSettingsOptions : IConfigurableOptions<CorsAccessorSettingsOptions>
    {
        /// <summary>
        /// 策略名称
        /// </summary>
        [Required]
        public string PolicyName { get; set; }

        /// <summary>
        /// 允许来源域名，没有配置则允许所有来源
        /// </summary>
        public string[] WithOrigins { get; set; }

        /// <summary>
        /// 请求表头，没有配置则允许所有表头
        /// </summary>
        public string[] WithHeaders { get; set; }

        /// <summary>
        /// 响应标头
        /// </summary>
        public string[] WithExposedHeaders { get; set; }

        /// <summary>
        /// 设置跨域允许请求谓词，没有配置则允许所有
        /// </summary>
        public string[] WithMethods { get; set; }

        /// <summary>
        /// 跨域请求中的凭据
        /// </summary>
        public bool? AllowCredentials { get; set; }

        /// <summary>
        /// 设置预检过期时间
        /// </summary>
        public int? SetPreflightMaxAge { get; set; }

        /// <summary>
        /// 后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(CorsAccessorSettingsOptions options, IConfiguration configuration)
        {
            PolicyName ??= "App.Cors.Policy";
            WithOrigins ??= Array.Empty<string>();
            AllowCredentials ??= true;
        }
    }
}