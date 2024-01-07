// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Security.Cryptography;
using System.Text;

namespace Furion.DataEncryption;

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
        using var des = DES.Create();
        var inputByteArray = Encoding.Default.GetBytes(text);

        var md5Bytes = Encoding.ASCII.GetBytes(MD5Encryption.Encrypt(skey, uppercase)[..8]);
        des.Key = md5Bytes;
        des.IV = md5Bytes;

        using var ms = new MemoryStream();
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
        using var des = DES.Create();
        var len = hash.Length / 2;
        var inputByteArray = new byte[len];
        int x, i;

        for (x = 0; x < len; x++)
        {
            i = Convert.ToInt32(hash.Substring(x * 2, 2), 16);
            inputByteArray[x] = (byte)i;
        }

        var md5Bytes = Encoding.ASCII.GetBytes(MD5Encryption.Encrypt(skey, uppercase)[..8]);
        des.Key = md5Bytes;
        des.IV = md5Bytes;

        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();

        return Encoding.Default.GetString(ms.ToArray());
    }
}