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
using System;
using System.Security.Cryptography;
using System.Text;

namespace Furion.DataEncryption
{
    /// <summary>
    ///     RSA密钥对结构体
    /// </summary>
    public struct RsaSecretKey
    {
        /// <summary>
        ///     初始化秘钥
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="publicKey"></param>
        public RsaSecretKey(string privateKey, string publicKey)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

        /// <summary>
        ///     公钥
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        ///     私钥
        /// </summary>
        public string PrivateKey { get; set; }
    }


    /// <summary>
    ///     RSA加密函数
    /// </summary>
    [SuppressSniffer]
    public class RSAEncryption
    {
        /// <summary>
        ///     生成RSA秘钥
        /// </summary>
        /// <param name="keySize">大小必须为384到16384之间，且必须能被8整除</param>
        /// <returns></returns>
        public static RsaSecretKey GenerateRsaSecretKey(int keySize)
        {
            if (keySize < 384 || keySize > 16384 || keySize % 8 != 0)
                throw new ArgumentException("keySize must be between 384 and 16384 in size and must be divisible by 8.",
                    "keySize");
            var rsaKey = new RsaSecretKey();
            using (var rsa = new RSACryptoServiceProvider(keySize))
            {
                rsaKey.PrivateKey = rsa.ToXmlString(true);
                rsaKey.PublicKey = rsa.ToXmlString(false);
            }

            return rsaKey;
        }


        /// <summary>
        ///     加密
        /// </summary>
        /// <param name="content">明文内容</param>
        /// <param name="xmlPublicKey">公钥</param>
        /// <returns></returns>
        public static string Encrypt(string content, string xmlPublicKey)
        {
            try
            {
                string encryptedContent;
                using var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                var encryptedData = rsa.Encrypt(Encoding.Default.GetBytes(content), false);
                encryptedContent = Convert.ToBase64String(encryptedData);

                return encryptedContent;
            }
            catch (Exception)
            {
                throw new Exception("Encryption error.Please check that the xmlPublicKey is valid.");
            }
        }

        /// <summary>
        ///     解密
        /// </summary>
        /// <param name="content">密文内容</param>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <returns></returns>
        public static string Decrypt(string content, string xmlPrivateKey)
        {
            try
            {
                string decryptedContent;
                using var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                var decryptedData = rsa.Decrypt(Convert.FromBase64String(content), false);
                decryptedContent = Encoding.Default.GetString(decryptedData);
                return decryptedContent;
            }
            catch (Exception)
            {
                throw new Exception("Decryption error. Please check whether the xmlPrivateKey and ciphertext match.");
            }
        }
    }
}