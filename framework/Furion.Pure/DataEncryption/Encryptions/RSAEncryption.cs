// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.12.0
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Furion.DataEncryption
{
    /// <summary>
    /// RSA 加密
    /// </summary>
    [SuppressSniffer]
    public static class RSAEncryption
    {
        /// <summary>
        /// 生成 RSA 秘钥
        /// </summary>
        /// <param name="keySize">大小必须为 2048 到 16384 之间，且必须能被 8 整除</param>
        /// <returns></returns>
        public static (string publicKey, string privateKey) GenerateSecretKey(int keySize = 2048)
        {
            if (keySize < 2048 || keySize > 16384 || keySize % 8 != 0)
                throw new ArgumentException("The keySize must be between 2048 and 16384 in size and must be divisible by 8.", nameof(keySize));

            using var rsa = new RSACryptoServiceProvider(keySize);

            return (rsa.ToXmlString(false), rsa.ToXmlString(true));
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">明文内容</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static string Encrypt(string text, string publicKey)
        {
            string encryptedContent;
            using var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);

            var encryptedData = rsa.Encrypt(Encoding.Default.GetBytes(text), false);
            encryptedContent = Convert.ToBase64String(encryptedData);

            return encryptedContent;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text">密文内容</param>
        /// <param name="privateKey">私钥</param>
        /// <returns></returns>
        public static string Decrypt(string text, string privateKey)
        {
            string decryptedContent;
            using var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);

            var decryptedData = rsa.Decrypt(Convert.FromBase64String(text), false);
            decryptedContent = Encoding.Default.GetString(decryptedData);
            return decryptedContent;
        }
    }
}