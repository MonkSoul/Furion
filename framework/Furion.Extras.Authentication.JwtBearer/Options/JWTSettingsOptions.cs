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

namespace Furion.Authorization
{
    /// <summary>
    /// Jwt 配置
    /// </summary>
    public sealed class JWTSettingsOptions
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
        /// 加密算法
        /// </summary>
        public string Algorithm { get; set; }
    }
}