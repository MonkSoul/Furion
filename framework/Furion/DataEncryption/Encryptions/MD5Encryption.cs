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
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Furion.DataEncryption
{
    /// <summary>
    /// MD5 加密
    /// </summary>
    [SuppressSniffer]
    public static unsafe class MD5Encryption
    {
        private const uint LOWERCASING = 0x2020U;

        private const uint UPPERCASING = 0;

        private static readonly delegate* managed<ReadOnlySpan<byte>, Span<char>, uint, void> _EncodeToUtf16Ptr;

        [ThreadStatic]
        private static MD5 instance;

        /// <summary>
        /// MD5实例
        /// </summary>
        public static MD5 Instance => instance ??= MD5.Create();

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static MD5Encryption()
        {
            _EncodeToUtf16Ptr = (delegate* managed<ReadOnlySpan<byte>, Span<char>, uint, void>)typeof(uint).Assembly.GetType("System.HexConverter").GetMethod("EncodeToUtf16", BindingFlags.Static | BindingFlags.Public).MethodHandle.GetFunctionPointer();
        }

        /// <summary>
        /// 字符串 MD5 比较
        /// </summary>
        /// <param name="text">加密文本</param>
        /// <param name="hash">MD5 字符串</param>
        /// <param name="uppercase">是否输出大写加密，默认 false</param>
        /// <returns>bool</returns>
        public static bool Compare(string text, string hash, bool uppercase = false)
        {
            var hashOfInput = Encrypt(text, uppercase);
            return hash.Equals(hashOfInput, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="text">加密文本</param>
        /// <param name="uppercase">是否输出大写加密，默认 false</param>
        /// <returns></returns>
        public static string Encrypt(string text, bool uppercase = false)
        {
            return string.Create(32, text, (p, q) =>
           {
               var buffer = Encoding.UTF8.GetBytes(q);
               Span<byte> signed = stackalloc byte[16];
               Instance.TryComputeHash(buffer, signed, out _);
               _EncodeToUtf16Ptr(signed, p, !uppercase ? LOWERCASING : UPPERCASING);
           });
        }
    }
}