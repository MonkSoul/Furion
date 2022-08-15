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

namespace Furion.DataEncryption.Extensions;

/// <summary>
/// DataEncryption 字符串加密拓展
/// </summary>
[SuppressSniffer]
public static class StringEncryptionExtensions
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
    /// 字符串 RSA 加密
    /// </summary>
    /// <param name="text">需要加密的文本</param>
    /// <param name="publicKey">公钥</param>
    /// <returns></returns>
    public static string ToRSAEncrpyt(this string text, string publicKey)
    {
        return RSAEncryption.Encrypt(text, publicKey);
    }

    /// <summary>
    /// 字符串 RSA 解密
    /// </summary>
    /// <param name="text">需要解密的文本</param>
    /// <param name="privateKey">私钥</param>
    /// <returns></returns>
    public static string ToRSADecrypt(this string text, string privateKey)
    {
        return RSAEncryption.Decrypt(text, privateKey);
    }
}