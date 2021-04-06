using System;
using System.Security.Cryptography;

using Furion.DataEncryption.Options;
using Furion.Logging.Extensions;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Furion.DataEncryption
{
    /// <summary>
    /// Pbkdf2 密钥派生算法（密码加密专用）
    /// </summary>
    public static class Pbkdf2Encryption
    {
        /// <summary>
        /// 初始迭代次数累加值
        /// </summary>
        private static readonly int _initialIterationCount;
        /// <summary>
        /// 默认的伪随机函数
        /// </summary>
        private static readonly KeyDerivationPrf _keyDerivationPrf;
        /// <summary>
        /// 输出密钥的长度
        /// </summary>
        private static readonly int _numBytesRequested;

        /// <summary>
        /// 构造函数
        /// </summary>
        static Pbkdf2Encryption()
        {
            var options = new Pbkdf2Options();
            // 读取 startup 自定义的 Pbkdf2Options 配置，如果为null，则使用默认值
            try { options = App.GetOptions<Pbkdf2Options>() ?? new Pbkdf2Options(); } catch (Exception ex) { ex.Message.LogError("Pbkdf2Encryption", ex, ex.Message); }

            _initialIterationCount = options.InitialIterationCount;
            _keyDerivationPrf = options.KeyDerivationPrf;
            _numBytesRequested = options.NumBytesRequested;
        }

        /// <summary>
        /// 使用 Pbkdf2 算法执行密钥派生，推荐所有密码都用此方法进行加密，使用 <see cref="Pbkdf2Verify" /> 进行验证
        /// </summary>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public static string Pbkdf2(string passWord)
        {
            // 生成salt
            var salt = Guid.NewGuid().ToByteArray();
            // 加密密码，返回base64
            return Convert.ToBase64String(Pbkdf2(passWord, salt, _keyDerivationPrf));
        }
        /// <summary>
        /// 使用 Pbkdf2 验证密码（字符串）是否正确
        /// </summary>
        /// <param name="encryptPassWord">已加密的 base64 字符串</param>
        /// <param name="passWord">待验证的原始密码（字符串）</param>
        /// <returns></returns>
        public static bool Pbkdf2Verify(string encryptPassWord, string passWord)
        {
            var decodeEncryptPassWord = Convert.FromBase64String(encryptPassWord);

            var salt = new byte[128 / 8];
            Buffer.BlockCopy(decodeEncryptPassWord, 0, salt, 0, salt.Length);

            var encryptSubkey = new byte[_numBytesRequested];
            Buffer.BlockCopy(decodeEncryptPassWord, salt.Length + 1, encryptSubkey, 0, encryptSubkey.Length);

            var actualSubkey = Pbkdf2(passWord, salt, (KeyDerivationPrf)decodeEncryptPassWord[salt.Length]);

            return CryptographicOperations.FixedTimeEquals(actualSubkey, decodeEncryptPassWord);
        }
        /// <summary>
        /// 使用PBKDF2进行加密
        /// </summary>
        /// <param name="passWord">待加密的密码</param>
        /// <param name="salt">salt</param>
        /// <param name="prf">加密使用的伪随机函数</param>
        /// <returns></returns>
        private static byte[] Pbkdf2(string passWord, byte[] salt, KeyDerivationPrf prf)
        {
            var output = new byte[salt.Length + 1 + _numBytesRequested];
            // 将salt添加到输出结果中
            Buffer.BlockCopy(salt, 0, output, 0, salt.Length);
            // 添加使用的伪随机函数
            output[salt.Length] = (byte)prf;

            // 字节偏移量
            var dstOffset = salt[3] % 10 + 2;
            // 打乱salt
            Buffer.BlockCopy(salt, 0, salt, dstOffset, dstOffset / 2);

            // 加密结果
            var encryptByte = KeyDerivation.Pbkdf2(
                password: passWord,
                salt: salt,
                prf: prf,
                iterationCount: (salt[dstOffset + 1] + _initialIterationCount) << 6,
                numBytesRequested: _numBytesRequested);

            Buffer.BlockCopy(encryptByte, 0, output, salt.Length + 1, encryptByte.Length);

            return output;
        }
    }
}
