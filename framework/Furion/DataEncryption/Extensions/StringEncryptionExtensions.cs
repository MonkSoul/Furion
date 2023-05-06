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
    /// 字节数组（文件） AES 加密
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="skey"></param>
    /// <returns>string</returns>
    public static byte[] ToAESEncrypt(this byte[] bytes, string skey)
    {
        return AESEncryption.Encrypt(bytes, skey);
    }

    /// <summary>
    /// 字节数组（文件） AES 解密
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="skey"></param>
    /// <returns>string</returns>
    public static byte[] ToAESDecrypt(this byte[] bytes, string skey)
    {
        return AESEncryption.Decrypt(bytes, skey);
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