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
/// RSA 加密
/// </summary>
[SuppressSniffer]
public static class RSAEncryption
{
    /// <summary>
    /// 生成 RSA 秘钥
    /// </summary>
    /// <param name="keySize">大小必须为 2048 到 16384 之间，且必须能被 8 整除</param>
    /// <returns></returns>
    public static (string publicKey, string privateKey) GenerateSecretKey(int keySize = 2048)
    {
        CheckRSAKeySize(keySize);

        using var rsa = new RSACryptoServiceProvider(keySize);
        return (rsa.ToXmlString(false), rsa.ToXmlString(true));
    }

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="text">明文内容</param>
    /// <param name="publicKey">公钥</param>
    /// <param name="keySize"></param>
    /// <returns></returns>
    public static string Encrypt(string text, string publicKey, int keySize = 2048)
    {
        CheckRSAKeySize(keySize);

        using var rsa = new RSACryptoServiceProvider(keySize);
        rsa.FromXmlString(publicKey);

        var encryptedData = rsa.Encrypt(Encoding.Default.GetBytes(text), false);
        return Convert.ToBase64String(encryptedData);
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="text">密文内容</param>
    /// <param name="privateKey">私钥</param>
    /// <param name="keySize"></param>
    /// <returns></returns>
    public static string Decrypt(string text, string privateKey, int keySize = 2048)
    {
        CheckRSAKeySize(keySize);

        using var rsa = new RSACryptoServiceProvider(keySize);
        rsa.FromXmlString(privateKey);

        var decryptedData = rsa.Decrypt(Convert.FromBase64String(text), false);
        return Encoding.Default.GetString(decryptedData);
    }

    /// <summary>
    /// 检查 RSA 长度
    /// </summary>
    /// <param name="keySize"></param>
    private static void CheckRSAKeySize(int keySize)
    {
        if (keySize < 2048 || keySize > 16384 || keySize % 8 != 0)
            throw new ArgumentException("The keySize must be between 2048 and 16384 in size and must be divisible by 8.", nameof(keySize));
    }
}