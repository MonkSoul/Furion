// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using System.Security.Cryptography;

namespace Furion.DataEncryption.Extensions;

/// <summary>
/// DataEncryption 字符串加密拓展
/// </summary>
[SuppressSniffer]
public static class StringEncryptionExtensions
{
    /// <summary>
    /// 字符串的 MD5 加密
    /// </summary>
    /// <param name="text"></param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <param name="is16">是否输出 16 位</param>
    /// <returns>string</returns>
    public static string ToMD5Encrypt(this string text, bool uppercase = false, bool is16 = false)
    {
        return MD5Encryption.Encrypt(text, uppercase, is16);
    }

    /// <summary>
    /// 字符串的 MD5 对比
    /// </summary>
    /// <param name="text"></param>
    /// <param name="hash"></param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <param name="is16">是否输出 16 位</param>
    /// <returns>string</returns>
    public static bool ToMD5Compare(this string text, string hash, bool uppercase = false, bool is16 = false)
    {
        return MD5Encryption.Compare(text, hash, uppercase, is16);
    }

    /// <summary>
    /// 字节数组的 MD5 加密
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <param name="is16">是否输出 16 位</param>
    /// <returns>string</returns>
    public static string ToMD5Encrypt(this byte[] bytes, bool uppercase = false, bool is16 = false)
    {
        return MD5Encryption.Encrypt(bytes, uppercase, is16);
    }

    /// <summary>
    /// 字节数组的 MD5 对比
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="hash"></param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <param name="is16">是否输出 16 位</param>
    /// <returns>string</returns>
    public static bool ToMD5Compare(this byte[] bytes, string hash, bool uppercase = false, bool is16 = false)
    {
        return MD5Encryption.Compare(bytes, hash, uppercase, is16);
    }

    /// <summary>
    /// 字符串 AES 加密
    /// </summary>
    /// <param name="text">加密文本</param>
    /// <param name="skey">密钥</param>
    /// <param name="iv">偏移量</param>
    /// <param name="mode">模式</param>
    /// <param name="padding">填充</param>
    /// <returns>string</returns>
    public static string ToAESEncrypt(this string text, string skey, byte[] iv = null, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
    {
        return AESEncryption.Encrypt(text, skey, iv, mode, padding);
    }

    /// <summary>
    /// 字符串 AES 解密
    /// </summary>
    /// <param name="text">加密文本</param>
    /// <param name="skey">密钥</param>
    /// <param name="iv">偏移量</param>
    /// <param name="mode">模式</param>
    /// <param name="padding">填充</param>
    /// <returns>string</returns>
    public static string ToAESDecrypt(this string text, string skey, byte[] iv = null, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
    {
        return AESEncryption.Decrypt(text, skey, iv, mode, padding);
    }

    /// <summary>
    /// 字节数组（文件） AES 加密
    /// </summary>
    /// <param name="bytes">源文件 字节数组</param>
    /// <param name="skey">密钥</param>
    /// <param name="iv">偏移量</param>
    /// <param name="mode">模式</param>
    /// <param name="padding">填充</param>
    /// <returns>string</returns>
    public static byte[] ToAESEncrypt(this byte[] bytes, string skey, byte[] iv = null, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
    {
        return AESEncryption.Encrypt(bytes, skey, iv, mode, padding);
    }

    /// <summary>
    /// 字节数组（文件） AES 解密
    /// </summary>
    /// <param name="bytes">加密后文件 字节数组</param>
    /// <param name="skey">密钥</param>
    /// <param name="iv">偏移量</param>
    /// <param name="mode">模式</param>
    /// <param name="padding">填充</param>
    /// <returns>string</returns>
    public static byte[] ToAESDecrypt(this byte[] bytes, string skey, byte[] iv = null, CipherMode mode = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7)
    {
        return AESEncryption.Decrypt(bytes, skey, iv, mode, padding);
    }

    /// <summary>
    /// 字符串 DES 加密
    /// </summary>
    /// <param name="text">需要加密的字符串</param>
    /// <param name="skey">密钥</param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <returns>string</returns>
    public static string ToDESEncrypt(this string text, string skey, bool uppercase = false)
    {
        return DESEncryption.Encrypt(text, skey, uppercase);
    }

    /// <summary>
    /// 字符串 DES 解密
    /// </summary>
    /// <param name="text"></param>
    /// <param name="skey">密钥</param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <returns>string</returns>
    public static string ToDESDecrypt(this string text, string skey, bool uppercase = false)
    {
        return DESEncryption.Decrypt(text, skey, uppercase);
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

    /// <summary>
    /// 字符串 SHA1 加密
    /// </summary>
    /// <param name="text">需要加密的文本</param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <returns></returns>
    public static string ToSHA1Encrypt(this string text, bool uppercase = false)
    {
        return SHA1Encryption.Encrypt(text, uppercase);
    }

    /// <summary>
    /// 字节数组的 SHA1 加密
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <returns></returns>
    public static string ToSHA1Encrypt(this byte[] bytes, bool uppercase = false)
    {
        return SHA1Encryption.Encrypt(bytes, uppercase);
    }

    /// <summary>
    /// 字符串的 SHA1 对比
    /// </summary>
    /// <param name="text"></param>
    /// <param name="hash"></param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <returns>string</returns>
    public static bool ToSHA1Compare(this string text, string hash, bool uppercase = false)
    {
        return SHA1Encryption.Compare(text, hash, uppercase);
    }

    /// <summary>
    /// 字节数组的 SHA1 对比
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="hash"></param>
    /// <param name="uppercase">是否输出大写加密，默认 false</param>
    /// <returns>string</returns>
    public static bool ToSHA1Compare(this byte[] bytes, string hash, bool uppercase = false)
    {
        return SHA1Encryption.Compare(bytes, hash, uppercase);
    }

    /// <summary>
    /// 字符串的 PBKDF2 加密
    /// </summary>
    /// <param name="text">加密文本</param>
    /// <param name="saltSize">随机 salt 大小</param>
    /// <param name="iterationCount">迭代次数</param>
    /// <param name="derivedKeyLength">密钥长度</param>
    /// <returns></returns>
    public static string ToPBKDF2Encrypt(this string text, int saltSize = 16, int iterationCount = 10000, int derivedKeyLength = 32)
    {
        return PBKDF2Encryption.Encrypt(text, saltSize, iterationCount, derivedKeyLength);
    }

    /// <summary>
    /// 字符串的 PBKDF2 比较
    /// </summary>
    /// <param name="text">加密文本</param>
    /// <param name="hash">PBKDF2 字符串</param>
    /// <param name="saltSize">随机 salt 大小</param>
    /// <param name="iterationCount">迭代次数</param>
    /// <param name="derivedKeyLength">密钥长度</param>
    /// <returns></returns>
    public static bool ToPBKDF2Compare(this string text, string hash, int saltSize = 16, int iterationCount = 10000, int derivedKeyLength = 32)
    {
        return PBKDF2Encryption.Compare(text, hash, saltSize, iterationCount, derivedKeyLength);
    }
}