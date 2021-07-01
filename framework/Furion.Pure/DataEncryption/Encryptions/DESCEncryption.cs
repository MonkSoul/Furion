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
    /// DESC 加解密
    /// </summary>
    [SuppressSniffer]
    public class DESCEncryption
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">加密文本</param>
        /// <param name="skey">密钥</param>
        /// <param name="uppercase">是否输出大写加密，默认 false</param>
        /// <returns></returns>
        public static string Encrypt(string text, string skey, bool uppercase = false)
        {
            using var des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(text);

            des.Key = Encoding.ASCII.GetBytes(MD5Encryption.Encrypt(skey, uppercase).Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(MD5Encryption.Encrypt(skey, uppercase).Substring(0, 8));

            using var ms = new System.IO.MemoryStream();
            using var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            var ret = new StringBuilder();
            foreach (var b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }

            return ret.ToString();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="hash">加密后字符串</param>
        /// <param name="skey">密钥</param>
        /// <param name="uppercase">是否输出大写加密，默认 false</param>
        /// <returns></returns>
        public static string Decrypt(string hash, string skey, bool uppercase = false)
        {
            using var des = new DESCryptoServiceProvider();
            int len;
            len = hash.Length / 2;
            var inputByteArray = new byte[len];
            int x, i;

            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(hash.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }

            des.Key = Encoding.ASCII.GetBytes(MD5Encryption.Encrypt(skey, uppercase).Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(MD5Encryption.Encrypt(skey, uppercase).Substring(0, 8));

            using var ms = new System.IO.MemoryStream();
            using var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return Encoding.Default.GetString(ms.ToArray());
        }
    }
}