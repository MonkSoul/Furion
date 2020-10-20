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

namespace Fur.Authorization
{
    /// <summary>
    /// Jwt
    /// </summary>
    [OptionsSettings("JWTSettings")]
    public sealed class JWTSettingsOptions : IConfigurableOptions<JWTSettingsOptions>
    {
        /// <summary>
        /// 验证签发方密钥
        /// </summary>
        public bool? ValidateIssuerSigningKey { get; set; }

        /// <summary>
        /// 签发方密钥
        /// </summary>
        public string IssuerSigningKey { get; set; }

        /// <summary>
        /// 验证签发方
        /// </summary>
        public bool? ValidateIssuer { get; set; }

        /// <summary>
        /// 签发方
        /// </summary>
        public string ValidIssuer { get; set; }

        /// <summary>
        /// 验证签收方
        /// </summary>
        public bool? ValidateAudience { get; set; }

        /// <summary>
        /// 签收方
        /// </summary>
        public string ValidAudience { get; set; }

        /// <summary>
        /// 验证生存期
        /// </summary>
        public bool? ValidateLifetime { get; set; }

        /// <summary>
        /// 过期时间容错值，解决服务器端时间不同步问题（秒）
        /// </summary>
        public long? ClockSkew { get; set; }

        /// <summary>
        /// 过期时间（分钟）
        /// </summary>
        public long? ExpiredTime { get; set; }

        /// <summary>
        /// 选项后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(JWTSettingsOptions options, IConfiguration configuration)
        {
            options.ValidateIssuerSigningKey ??= true;
            if (options.ValidateIssuerSigningKey == true)
            {
                options.IssuerSigningKey ??= "U2FsdGVkX1+6H3D8Q//yQMhInzTdRZI9DbUGetbyaag=";
            }
            options.ValidateIssuer ??= true;
            if (options.ValidateIssuer == true)
            {
                options.ValidIssuer = "dotnetchina";
            }
            options.ValidateAudience ??= true;
            if (options.ValidateAudience == true)
            {
                options.ValidAudience = "powerby Fur";
            }
            options.ValidateLifetime ??= true;
            if (options.ValidateLifetime == true)
            {
                options.ClockSkew ??= 10;
            }
            options.ExpiredTime ??= 20;
        }
    }
}