using Fur.DependencyInjection;
using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Fur.DataEncryption
{
    /// <summary>
    /// MD5 加密
    /// </summary>
    [SkipScan]
    public unsafe class MD5Encryption
    {
        private const uint LOWERCASING = 0x2020U;

        private static readonly delegate* managed<ReadOnlySpan<byte>, Span<char>, uint, void> _EncodeToUtf16Ptr;

        static MD5Encryption()
        {
            _EncodeToUtf16Ptr = (delegate* managed<ReadOnlySpan<byte>, Span<char>, uint, void>)typeof(uint).Assembly.GetType("System.HexConverter").GetMethod("EncodeToUtf16", BindingFlags.Static | BindingFlags.Public).MethodHandle.GetFunctionPointer();
        }

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
            return 0 == comparer.Compare(hashOfInput, hash);
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
            Span<char> output = stackalloc char[32];
            _EncodeToUtf16Ptr(data, output, LOWERCASING);
            return new string(output);
        }
    }
}