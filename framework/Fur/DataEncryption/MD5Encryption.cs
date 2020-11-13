using Fur.DependencyInjection;
using System;
using System.Runtime.CompilerServices;
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
        private static readonly char[] Digitals = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

        /// <summary>
        /// 字符串 MD5 比较
        /// </summary>
        /// <param name="text">加密文本</param>
        /// <param name="hash">MD5 字符串</param>
        /// <returns>bool</returns>
        public static bool Compare(string text, string hash)
        {
            var hashOfInput = Encrypt(text);
            return hash.Equals(hashOfInput, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="text">加密文本</param>
        /// <returns></returns>
        public static string Encrypt(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);

            return ByteToString(MD5Instances.Instance.ComputeHash(bytes));
        }

        /// <summary>
        /// 创建MD5实例
        /// </summary>
        private static class MD5Instances
        {
            /// <summary>
            /// 线程静态变量
            /// </summary>
            [ThreadStatic]
            private static MD5 instance;

            /// <summary>
            /// MD5实例
            /// </summary>
            public static MD5 Instance => instance ?? Create();

            [MethodImpl(MethodImplOptions.NoInlining)]
            private static MD5 Create()
            {
                instance = MD5.Create() ?? Activator.CreateInstance<MD5>();

                return instance;
            }
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static string ByteToString(byte[] bytes)
        {
            var chars = new char[bytes.Length * 2];
            var index = 0;
            foreach (var item in bytes)
            {
                chars[index] = Digitals[item >> 4]; ++index;
                chars[index] = Digitals[item & 15]; ++index;
            }
            return new string(chars, 0, chars.Length);
        }
    }
}