// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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
        var bKey = Encoding.UTF8.GetBytes(skey);

        using var aesAlg = Aes.Create();
        using var encryptor = aesAlg.CreateEncryptor(bKey, aesAlg.IV);
        using var msEncrypt = new MemoryStream();
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write, true))
        using (var swEncrypt = new StreamWriter(csEncrypt, leaveOpen: true))
        {
            swEncrypt.Write(text);
        }

        var bVector = aesAlg.IV;
        var dataLength = bVector.Length + (int)msEncrypt.Length;
        var decryptedContent = msEncrypt.GetBuffer();
        var base64Length = Base64.GetMaxEncodedToUtf8Length(dataLength);
        var result = new byte[base64Length];

        Unsafe.CopyBlock(ref result[0], ref bVector[0], (uint)bVector.Length);
        Unsafe.CopyBlock(ref result[bVector.Length], ref decryptedContent[0], (uint)msEncrypt.Length);
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

        var bVector = new byte[16];
        var cipher = new byte[fullCipher.Length - bVector.Length];

        Unsafe.CopyBlock(ref bVector[0], ref fullCipher[0], (uint)bVector.Length);
        Unsafe.CopyBlock(ref cipher[0], ref fullCipher[bVector.Length], (uint)(fullCipher.Length - bVector.Length));
        var bKey = Encoding.UTF8.GetBytes(skey);

        using var aesAlg = Aes.Create();
        using var decryptor = aesAlg.CreateDecryptor(bKey, bVector);
        using var msDecrypt = new MemoryStream(cipher);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);

        return srDecrypt.ReadToEnd();
    }

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="bytes">源文件 字节数组</param>
    /// <param name="skey">密钥</param>
    /// <returns>加密后的字节数组</returns>
    public static byte[] Encrypt(byte[] bytes, string skey)
    {
        var bKey = new byte[32];
        Array.Copy(Encoding.UTF8.GetBytes(skey.PadRight(bKey.Length)), bKey, bKey.Length);

        var vector = MD5Encryption.Encrypt(skey, false, is16: true);
        var bVector = new byte[16];
        Array.Copy(Encoding.UTF8.GetBytes(vector.PadRight(bVector.Length)), bVector, bVector.Length);

        using var aesAlg = Aes.Create();
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateEncryptor(bKey, bVector), CryptoStreamMode.Write);

        cryptoStream.Write(bytes, 0, bytes.Length);
        cryptoStream.FlushFinalBlock();

        return memoryStream.ToArray();
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="bytes">加密后文件 字节数组</param>
    /// <param name="skey">密钥</param>
    /// <returns></returns>
    public static byte[] Decrypt(byte[] bytes, string skey)
    {
        var bKey = new byte[32];
        Array.Copy(Encoding.UTF8.GetBytes(skey.PadRight(bKey.Length)), bKey, bKey.Length);

        var vector = MD5Encryption.Encrypt(skey, false, is16: true);
        var bVector = new byte[16];
        Array.Copy(Encoding.UTF8.GetBytes(vector.PadRight(bVector.Length)), bVector, bVector.Length);

        using var aesAlg = Aes.Create();
        using var memoryStream = new MemoryStream(bytes);
        using var cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateDecryptor(bKey, bVector), CryptoStreamMode.Read);
        using var originalStream = new MemoryStream();

        var buffer = new byte[1024];
        var readBytes = 0;

        while ((readBytes = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
        {
            originalStream.Write(buffer, 0, readBytes);
        }

        return originalStream.ToArray();
    }
}