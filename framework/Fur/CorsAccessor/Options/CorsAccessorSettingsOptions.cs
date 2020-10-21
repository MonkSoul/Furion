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
using System.ComponentModel.DataAnnotations;

namespace Fur.CorsAccessor
{
    /// <summary>
    /// 跨域配置选项
    /// </summary>
    [OptionsSettings("CorsAccessorSettings")]
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
            PolicyName ??= "FurAllowSpecificOrigins";
            WithOrigins ??= new[] { "http://localhost:4200" };
            AllowCredentials ??= true;
        }
    }
}