// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Fur.DataEncryption
{
    /// <summary>
    /// AES 加解密
    /// </summary>
    [SkipScan]
    public class AESEncryption
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">加密文本</param>
        /// <param name="skey">密钥</param>
        /// <returns></returns>
        public static string Encrypt(string text, string skey)
        {
            var encryptKey = Encoding.UTF8.GetBytes(skey);

            using var aesAlg = Aes.Create();
            using var encryptor = aesAlg.CreateEncryptor(encryptKey, aesAlg.IV);
            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))

            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(text);
            }

            var iv = aesAlg.IV;
            var decryptedContent = msEncrypt.ToArray();
            var result = new byte[iv.Length + decryptedContent.Length];

            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="hash">加密后字符串</param>
        /// <param name="skey">密钥</param>
        /// <returns></returns>
        public static string Decrypt(string hash, string skey)
        {
            var fullCipher = Convert.FromBase64String(hash);

            var iv = new byte[16];
            var cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
            var decryptKey = Encoding.UTF8.GetBytes(skey);

            using var aesAlg = Aes.Create();
            using var decryptor = aesAlg.CreateDecryptor(decryptKey, iv);
            string result;
            using (var msDecrypt = new MemoryStream(cipher))
            {
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);

                result = srDecrypt.ReadToEnd();
            }

            return result;
        }
    }
}