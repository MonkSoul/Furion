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
        /// <para>建议为70 - 200之间，硬件性能好可以继续增加，此值越高，安全性越高，性能越低</para>
        /// <para>初始值：110</para>
        /// </summary>
        public int InitialIterationCount { get; set; } = 110;
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
