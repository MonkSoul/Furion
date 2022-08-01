// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
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

using System.Buffers.Text;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Furion.DataEncryption;

/// <summary>
/// AES 加解密
/// </summary>
[SuppressSniffer]
public class AESEncryption
{
    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="text">加密文本</param>
    /// <param name="skey">密钥</param>
    /// <returns></returns>
    public static string Encrypt(string text, string skey)
    {
        var encryptKey = Encoding.UTF8.GetBytes(skey);

        using var aesAlg = Aes.Create();
        using var encryptor = aesAlg.CreateEncryptor(encryptKey, aesAlg.IV);
        using var msEncrypt = new MemoryStream();
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write, true))

        using (var swEncrypt = new StreamWriter(csEncrypt, leaveOpen: true))
        {
            swEncrypt.Write(text);
        }

        var iv = aesAlg.IV;
        var dataLength = iv.Length + (int)msEncrypt.Length;
        var decryptedContent = msEncrypt.GetBuffer();
        var base64Length = Base64.GetMaxEncodedToUtf8Length(dataLength);
        var result = new byte[base64Length];

        Unsafe.CopyBlock(ref result[0], ref iv[0], (uint)iv.Length);
        Unsafe.CopyBlock(ref result[iv.Length], ref decryptedContent[0], (uint)msEncrypt.Length);

        Base64.EncodeToUtf8InPlace(result, dataLength, out base64Length);

        return Encoding.ASCII.GetString(result.AsSpan()[..base64Length]);
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="hash">加密后字符串</param>
    /// <param name="skey">密钥</param>
    /// <returns></returns>
    public static string Decrypt(string hash, string skey)
    {
        var fullCipher = Convert.FromBase64String(hash);

        var iv = new byte[16];
        var cipher = new byte[fullCipher.Length - iv.Length];

        Unsafe.CopyBlock(ref iv[0], ref fullCipher[0], (uint)iv.Length);
        Unsafe.CopyBlock(ref cipher[0], ref fullCipher[iv.Length], (uint)(fullCipher.Length - iv.Length));
        var decryptKey = Encoding.UTF8.GetBytes(skey);

        using var aesAlg = Aes.Create();
        using var decryptor = aesAlg.CreateDecryptor(decryptKey, iv);
        using var msDecrypt = new MemoryStream(cipher);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}