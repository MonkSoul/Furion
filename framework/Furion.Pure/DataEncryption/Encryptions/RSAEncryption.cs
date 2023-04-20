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