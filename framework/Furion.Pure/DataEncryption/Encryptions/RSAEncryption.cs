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

        var originalData = Encoding.Default.GetBytes(text);
        byte[] encryptedData;

        // 密钥可加密数据长度
        var bufferSize = (rsa.KeySize / 8) - 11;

        // RSA 算法规定：https://gitee.com/dotnetchina/Furion/pulls/788
        // 待加密的字节数不能超过密钥的长度值除以 8 再减去 11（即：RSACryptoServiceProvider.KeySize / 8 - 11），
        // 而加密后得到密文的字节数，正好是密钥的长度值除以 8（即：RSACryptoServiceProvider.KeySize / 8）
        if (originalData.Length > bufferSize)
        {
            // 分段加密
            var buffer = new byte[bufferSize];
            using var input = new MemoryStream(originalData);
            using var output = new MemoryStream();

            while (true)
            {
                var readLine = input.Read(buffer, 0, bufferSize);
                if (readLine <= 0)
                {
                    break;
                }

                var temp = new byte[readLine];
                Array.Copy(buffer, 0, temp, 0, readLine);

                var encrypt = rsa.Encrypt(temp, false);
                output.Write(encrypt, 0, encrypt.Length);
            }
            encryptedData = output.ToArray();
        }
        else encryptedData = rsa.Encrypt(originalData, false);

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

        var encryptData = Convert.FromBase64String(text);
        byte[] decryptedData;

        // 可解密密文最大长度
        var bufferSize = rsa.KeySize / 8;

        // RSA 算法规定：https://gitee.com/dotnetchina/Furion/pulls/788
        // 待加密的字节数不能超过密钥的长度值除以 8 再减去 11（即：RSACryptoServiceProvider.KeySize / 8 - 11），
        // 而加密后得到密文的字节数，正好是密钥的长度值除以 8（即：RSACryptoServiceProvider.KeySize / 8）
        if (encryptData.Length > bufferSize)
        {
            // 分段解密
            var buffer = new byte[bufferSize];
            using var input = new MemoryStream(encryptData);
            using var output = new MemoryStream();

            while (true)
            {
                var readLine = input.Read(buffer, 0, bufferSize);
                if (readLine <= 0)
                {
                    break;
                }

                var temp = new byte[readLine];
                Array.Copy(buffer, 0, temp, 0, readLine);

                var decrypt = rsa.Decrypt(temp, false);
                output.Write(decrypt, 0, decrypt.Length);
            }
            decryptedData = output.ToArray();
        }
        else decryptedData = rsa.Decrypt(encryptData, false);

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