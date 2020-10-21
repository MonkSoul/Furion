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