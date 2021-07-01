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
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;

namespace Furion.DataEncryption
{
    /// <summary>
    /// PBKDF2 初始化配置
    /// </summary>
    public sealed class PBKDF2EncryptionSettingsOptions : IConfigurableOptions<PBKDF2EncryptionSettingsOptions>
    {
        /// <summary>
        /// 初始迭代次数累加值（未进行随机运算前的初始累加值，不等于最终迭代次数）
        /// <para>建议为70 - 200之间，硬件性能好可以继续增加，此值越高，安全性越高，性能越低</para>
        /// <para>初始值：110</para>
        /// </summary>
        public int? InitialIterationCount { get; set; }

        /// <summary>
        /// 加密算法规则
        /// <para>初始值：KeyDerivationPrf.HMACSHA256</para>
        /// </summary>
        public KeyDerivationPrf? KeyDerivation { get; set; }

        /// <summary>
        /// 派生密钥的长度 (以字节为单位)
        /// <para>初始值：512 / 8</para>
        /// </summary>
        public int? NumBytesRequested { get; set; }

        /// <summary>
        /// 选项后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(PBKDF2EncryptionSettingsOptions options, IConfiguration configuration)
        {
            options.InitialIterationCount ??= 110;
            options.KeyDerivation ??= KeyDerivationPrf.HMACSHA256;
            options.NumBytesRequested ??= 512 / 8;
        }
    }
}