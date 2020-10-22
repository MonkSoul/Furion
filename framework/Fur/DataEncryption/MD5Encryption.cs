using Fur.DependencyInjection;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Fur.DataEncryption
{
    /// <summary>
    /// MD5 加密
    /// </summary>
    [SkipScan]
    public class MD5Encryption
    {
        /// <summary>
        /// 字符串 MD5 比较
        /// </summary>
        /// <param name="text">加密文本</param>
        /// <param name="hash">MD5 字符串</param>
        /// <returns>bool</returns>
        public static bool Compare(string text, string hash)
        {
            using var md5Hash = MD5.Create();
            var hashOfInput = Encrypt(text);
            var comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash)) return true;
            else return false;
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="text">加密文本</param>
        /// <returns></returns>
        public static string Encrypt(string text)
        {
            using var md5Hash = MD5.Create();
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}