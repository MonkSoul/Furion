using Furion.ConfigurableOptions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Furion.DataEncryption.Options
{
    /// <summary>
    /// PBKDF2初始化配置
    /// </summary>
    public class Pbkdf2Options : IConfigurableOptions
    {
        /// <summary>
        /// 初始迭代次数累加值（未进行随机运算前的初始累加值，不等于最终迭代次数）
        /// <para>建议为10 - 150之间，不宜太大，过大将导致最终迭代次数过多，影响性能</para>
        /// <para>初始值：55</para>
        /// </summary>
        public int InitialIterationCount { get; set; } = 55;
        /// <summary>
        /// 默认的伪随机函数
        /// <para>初始值：KeyDerivationPrf.HMACSHA256</para>
        /// </summary>
        public KeyDerivationPrf KeyDerivationPrf { get; set; } = KeyDerivationPrf.HMACSHA256;
        /// <summary>
        /// 派生密钥的长度 (以字节为单位) 
        /// <para>初始值：512 / 8</para>
        /// </summary>
        public int NumBytesRequested { get; set; } = 512 / 8;
    }
}
