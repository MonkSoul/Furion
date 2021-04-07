using Furion.ConfigurableOptions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;

namespace Furion.DataEncryption
{
    /// <summary>
    /// PBKDF2 初始化配置
    /// </summary>
    public sealed class PBKDF2SettingsOptions : IConfigurableOptions<PBKDF2SettingsOptions>
    {
        /// <summary>
        /// 初始迭代次数累加值（未进行随机运算前的初始累加值，不等于最终迭代次数）
        /// <para>建议为70 - 200之间，硬件性能好可以继续增加，此值越高，安全性越高，性能越低</para>
        /// <para>初始值：110</para>
        /// </summary>
        public int? InitialIterationCount { get; set; }

        /// <summary>
        /// 默认的伪随机函数
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
        public void PostConfigure(PBKDF2SettingsOptions options, IConfiguration configuration)
        {
            options.InitialIterationCount ??= 110;
            options.KeyDerivation ??= KeyDerivationPrf.HMACSHA256;
            options.NumBytesRequested ??= 512 / 8;
        }
    }
}