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

using Furion.DependencyInjection;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace Furion.DataEncryption
{
    /// <summary>
    /// PBKDF2 密钥派生算法（密码加密专用）
    /// </summary>
    [SuppressSniffer]
    public static class PBKDF2Encryption
    {
        /// <summary>
        /// 初始迭代次数累加值
        /// </summary>
        private static readonly int _initialIterationCount;

        /// <summary>
        /// 默认的伪随机函数
        /// </summary>
        private static readonly KeyDerivationPrf _keyDerivation;

        /// <summary>
        /// 输出密钥的长度
        /// </summary>
        private static readonly int _numBytesRequested;

        /// <summary>
        /// 构造函数
        /// </summary>
        static PBKDF2Encryption()
        {
            // 读取配置
            var options = App.GetOptions<PBKDF2EncryptionSettingsOptions>();

            _initialIterationCount = options?.InitialIterationCount ?? 110;
            _keyDerivation = options?.KeyDerivation ?? KeyDerivationPrf.HMACSHA256;
            _numBytesRequested = options?.NumBytesRequested ?? 512 / 8;
        }

        /// <summary>
        /// 使用 PBKDF2 算法执行密钥派生，推荐所有密码都用此方法进行加密，使用 <see cref="Compare" /> 进行验证
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Encrypt(string text)
        {
            // 生成salt
            var salt = Guid.NewGuid().ToByteArray();
            // 加密密码，返回base64
            return Convert.ToBase64String(Encrypt(text, salt, _keyDerivation));
        }

        /// <summary>
        /// 使用 PBKDF2 验证文本（字符串）是否正确
        /// </summary>
        /// <param name="text">待验证的原始文本（字符串）</param>
        /// <param name="encryptText">已加密的 base64 字符串</param>
        /// <returns></returns>
        public static bool Compare(string text, string encryptText)
        {
            var decodeEncryptText = Convert.FromBase64String(encryptText);

            var salt = new byte[128 / 8];
            Buffer.BlockCopy(decodeEncryptText, 0, salt, 0, salt.Length);

            var encryptSubkey = new byte[_numBytesRequested];
            Buffer.BlockCopy(decodeEncryptText, salt.Length + 1, encryptSubkey, 0, encryptSubkey.Length);

            var actualSubkey = Encrypt(text, salt, (KeyDerivationPrf)decodeEncryptText[salt.Length]);

            return CryptographicOperations.FixedTimeEquals(actualSubkey, decodeEncryptText);
        }

        /// <summary>
        /// 使用 PBKDF2 进行加密
        /// </summary>
        /// <param name="text">待加密的文本</param>
        /// <param name="salt">salt</param>
        /// <param name="prf">加密使用的伪随机函数</param>
        /// <returns></returns>
        private static byte[] Encrypt(string text, byte[] salt, KeyDerivationPrf prf)
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
                password: text,
                salt: salt,
                prf: prf,
                iterationCount: (salt[dstOffset + 1] + _initialIterationCount) << 6,
                numBytesRequested: _numBytesRequested);

            Buffer.BlockCopy(encryptByte, 0, output, salt.Length + 1, encryptByte.Length);

            return output;
        }
    }
}