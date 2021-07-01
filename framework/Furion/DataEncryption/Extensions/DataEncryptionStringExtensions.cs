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

namespace Furion.DataEncryption.Extensions
{
    /// <summary>
    /// DataEncryption 字符串加密拓展
    /// </summary>
    [SuppressSniffer]
    public static class DataEncryptionStringExtensions
    {
        /// <summary>
        /// 字符串的 MD5
        /// </summary>
        /// <param name="text"></param>
        /// <param name="uppercase">是否输出大写加密，默认 false</param>
        /// <returns>string</returns>
        public static string ToMD5Encrypt(this string text, bool uppercase = false)
        {
            return MD5Encryption.Encrypt(text, uppercase);
        }

        /// <summary>
        /// 字符串的 MD5
        /// </summary>
        /// <param name="text"></param>
        /// <param name="hash"></param>
        /// <param name="uppercase">是否输出大写加密，默认 false</param>
        /// <returns>string</returns>
        public static bool ToMD5Compare(this string text, string hash, bool uppercase = false)
        {
            return MD5Encryption.Compare(text, hash, uppercase);
        }

        /// <summary>
        /// 字符串 AES 加密
        /// </summary>
        /// <param name="text">需要加密的字符串</param>
        /// <param name="skey"></param>
        /// <returns>string</returns>
        public static string ToAESEncrypt(this string text, string skey)
        {
            return AESEncryption.Encrypt(text, skey);
        }

        /// <summary>
        /// 字符串 AES 解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="skey"></param>
        /// <returns>string</returns>
        public static string ToAESDecrypt(this string text, string skey)
        {
            return AESEncryption.Decrypt(text, skey);
        }

        /// <summary>
        /// 字符串 DESC 加密
        /// </summary>
        /// <param name="text">需要加密的字符串</param>
        /// <param name="skey">密钥</param>
        /// <param name="uppercase">是否输出大写加密，默认 false</param>
        /// <returns>string</returns>
        public static string ToDESCEncrypt(this string text, string skey, bool uppercase = false)
        {
            return DESCEncryption.Encrypt(text, skey, uppercase);
        }

        /// <summary>
        /// 字符串 DESC 解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="skey">密钥</param>
        /// <param name="uppercase">是否输出大写加密，默认 false</param>
        /// <returns>string</returns>
        public static string ToDESCDecrypt(this string text, string skey, bool uppercase = false)
        {
            return DESCEncryption.Decrypt(text, skey, uppercase);
        }

        /// <summary>
        /// 密码（字符串） PBKDF2 加密
        /// </summary>
        /// <param name="text">需要加密的文本（字符串）</param>
        /// <returns></returns>
        public static string ToPBKDF2Encrypt(this string text)
        {
            return PBKDF2Encryption.Encrypt(text);
        }

        /// <summary>
        /// 使用 Pbkdf2 算法验证密码（字符串）是否正确
        /// </summary>
        /// <param name="text">待验证的原始字符串（字符串）</param>
        /// <param name="encryptText"></param>
        /// <returns></returns>
        public static bool ToPBKDF2Compare(this string text, string encryptText)
        {
            return PBKDF2Encryption.Compare(text, encryptText);
        }
    }
}