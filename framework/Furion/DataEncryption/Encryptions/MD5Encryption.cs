// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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
    /// MD5 比较
    /// </summary>
    /// <param name="text">加密文本</param>
    /// <param name="hash">MD5 字符串</param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <param name="is16">是否输出 16 位</param>
    /// <returns>bool</returns>
    public static bool Compare(string text, string hash, bool uppercase = false, bool is16 = false)
    {
        return Compare(Encoding.UTF8.GetBytes(text), hash, uppercase, is16);
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
        return Encrypt(Encoding.UTF8.GetBytes(text), uppercase, is16);
    }

    /// <summary>
    /// MD5 加密
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <param name="is16">是否输出 16 位</param>
    /// <returns></returns>
    public static string Encrypt(byte[] bytes, bool uppercase = false, bool is16 = false)
    {
        var data = MD5.HashData(bytes);

        var stringBuilder = new StringBuilder();
        for (var i = 0; i < data.Length; i++)
        {
            stringBuilder.Append(data[i].ToString("x2"));
        }

        var md5String = stringBuilder.ToString();
        var hash = !is16 ? md5String : md5String.Substring(8, 16);
        return !uppercase ? hash : hash.ToUpper();
    }

    /// <summary>
    /// MD5 比较
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="hash">MD5 字符串</param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <param name="is16">是否输出 16 位</param>
    /// <returns>bool</returns>
    public static bool Compare(byte[] bytes, string hash, bool uppercase = false, bool is16 = false)
    {
        var hashOfInput = Encrypt(bytes, uppercase, is16);
        return hash.Equals(hashOfInput, StringComparison.OrdinalIgnoreCase);
    }
}