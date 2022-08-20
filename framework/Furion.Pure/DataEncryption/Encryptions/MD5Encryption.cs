// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Security.Cryptography;
using System.Text;

namespace Furion.DataEncryption;

/// <summary>
/// MD5 加密
/// </summary>
[SuppressSniffer]
public static unsafe class MD5Encryption
{
    /// <summary>
    /// 字符串 MD5 比较
    /// </summary>
    /// <param name="text">加密文本</param>
    /// <param name="hash">MD5 字符串</param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <param name="is16">是否输出 16 位</param>
    /// <returns>bool</returns>
    public static bool Compare(string text, string hash, bool uppercase = false, bool is16 = false)
    {
        var hashOfInput = Encrypt(text, uppercase, is16);
        return hash.Equals(hashOfInput, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// MD5 加密
    /// </summary>
    /// <param name="text">加密文本</param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <param name="is16">是否输出 16 位</param>
    /// <returns></returns>
    public static string Encrypt(string text, bool uppercase = false, bool is16 = false)
    {
        using var md5Hash = MD5.Create();
        var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

        var stringBuilder = new StringBuilder();
        for (var i = 0; i < data.Length; i++)
        {
            stringBuilder.Append(data[i].ToString("x2"));
        }

        var md5String = stringBuilder.ToString();
        var hash = !is16 ? md5String : md5String.Substring(8, 16);
        return !uppercase ? hash : hash.ToUpper();
    }
}