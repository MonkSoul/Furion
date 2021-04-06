using Furion.DependencyInjection;

namespace Furion.DataEncryption.Extensions
{
    /// <summary>
    /// DataEncryption 字符串加密拓展
    /// </summary>
    [SkipScan]
    public static class DataEncryptionStringExtensions
    {
        /// <summary>
        /// 字符串的 MD5
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string</returns>
        public static string ToMD5Encrypt(this string text)
        {
            return MD5Encryption.Encrypt(text);
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
        /// <returns>string</returns>
        public static string ToDESCEncrypt(this string text, string skey)
        {
            return DESCEncryption.Encrypt(text, skey);
        }

        /// <summary>
        /// 字符串 DESC 解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="skey">密钥</param>
        /// <returns>string</returns>
        public static string ToDESCDecrypt(this string text, string skey)
        {
            return DESCEncryption.Decrypt(text, skey);
        }

        /// <summary>
        /// 密码（字符串） Pbkdf2 加密
        /// </summary>
        /// <param name="passWord">需要加密的密码（字符串）</param>
        /// <returns></returns>
        public static string ToPbkdf2(this string passWord)
        {
            return Pbkdf2Encryption.Pbkdf2(passWord);
        }

        /// <summary>
        /// 使用 Pbkdf2 算法验证密码（字符串）是否正确
        /// </summary>
        /// <param name="encryptPassWord"></param>
        /// <param name="passWord">待验证的原始密码（字符串）</param>
        /// <returns></returns>
        public static bool Pbkdf2Verify(this string encryptPassWord, string passWord)
        {
            return Pbkdf2Encryption.Pbkdf2Verify(encryptPassWord, passWord);
        }
    }
}