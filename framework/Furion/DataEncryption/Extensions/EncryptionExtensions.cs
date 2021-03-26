using Furion.DataEncryption;
using Furion.DependencyInjection;
using Furion.JsonSerialization;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Furion.DataEncryption
{
    /// <summary>
    /// System.Text.Json 拓展
    /// </summary>
    [SkipScan]
    public static class EncryptionExtensions
    {
        /// <summary>
        /// 字符串的 Md5
        /// </summary>
        /// <param name="str"></param>
        /// <returns>string</returns>
        public static string ToMd5(this string str)
        {
            // return Md5Helper.Md5(str);
            return MD5Encryption.Encrypt(str);
        }

        #region AES
        private static string AesDefaultKey = "#@iST~&i+DaFGdd^544#$d^++2@#6@id";
        /// <summary>
        /// 字符串Aes 加密
        /// </summary>
        /// <param name="data">需要加密的字符串</param>
        /// <returns>string</returns>
        public static string ToAesEncry(this string data)
        {
            return AESEncryption.Encrypt(data, AesDefaultKey);
        }
        /// <summary>
        /// 字符串Aes 解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns>string</returns>
        public static string ToAesDecry(this string data)
        {
            return AESEncryption.Decrypt(data, AesDefaultKey);
        }

        /// <summary>
        /// 字符串Aes 加密
        /// </summary>
        /// <param name="data">需要加密的字符串</param>
        /// <returns>string</returns>
        public static string ToAesEncry(this string data, string key)
        {
            // 对key进行MD5 是防止输入错误密码而报错，但是使用了MD5后，密钥始终长度为32，都是正确的
            return AESEncryption.Encrypt(data, key.ToMd5().ToUpper());
        }
        /// <summary>
        /// 字符串Aes 解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns>string</returns>
        public static string ToAesDecry(this string data, string key)
        {
            // 对key进行MD5 是防止输入错误密码而报错，但是使用了MD5后，密钥始终长度为32，都是正确的
            return AESEncryption.Decrypt(data, key.ToMd5().ToUpper());
        }
        #endregion

        #region DESC
        private static string DescDefaultKey = "#@iST~&i+DaFGdd^544#$d^++2@#6@id";
        /// <summary>
        /// 字符串Aes 加密
        /// </summary>
        /// <param name="data">需要加密的字符串</param>
        /// <returns>string</returns>
        public static string ToDescEncry(this string data)
        {
            return DESCEncryption.Encrypt(data, DescDefaultKey);
        }
        /// <summary>
        /// 字符串Aes 解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns>string</returns>
        public static string ToDescDecry(this string data)
        {
            return DESCEncryption.Decrypt(data, DescDefaultKey);
        }

        /// <summary>
        /// 字符串Aes 加密
        /// </summary>
        /// <param name="data">需要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>string</returns>
        public static string ToDescEncry(this string data, string key)
        {
            // 对key进行MD5 是防止输入错误密码而报错，但是使用了MD5后，密钥始终长度为32，都是正确的
            return DESCEncryption.Encrypt(data, key.ToMd5().ToUpper());
        }

        /// <summary>
        /// 字符串Aes 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key">密钥</param>
        /// <returns>string</returns>
        public static string ToDescDecry(this string data, string key)
        {
            // 对key进行MD5 是防止输入错误密码而报错，但是使用了MD5后，密钥始终长度为32，都是正确的
            return DESCEncryption.Decrypt(data, key.ToMd5().ToUpper());
        }
        #endregion
    }

}