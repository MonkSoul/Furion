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

namespace Furion.DataEncryption;

/// <summary>
/// PBKDF2 加密
/// </summary>
[SuppressSniffer]
public class PBKDF2Encryption
{
    private const string SaltHashSeparator = ":";

    /// <summary>
    /// PBKDF2 加密
    /// </summary>
    /// <param name="text">加密文本</param>
    /// <param name="saltSize">随机 salt 大小</param>
    /// <param name="iterationCount">迭代次数</param>
    /// <param name="derivedKeyLength">密钥长度</param>
    /// <returns></returns>
    public static string Encrypt(string text, int saltSize = 16, int iterationCount = 10000, int derivedKeyLength = 32)
    {
        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[saltSize];
        rng.GetBytes(salt);

        using var pbkdf2 = new Rfc2898DeriveBytes(text, salt, iterationCount, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(derivedKeyLength);

        // 分别编码盐和哈希，并用分隔符拼接
        return Convert.ToBase64String(salt) + SaltHashSeparator + Convert.ToBase64String(hash);
    }

    /// <summary>
    /// PBKDF2 比较
    /// </summary>
    /// <param name="text">加密文本</param>
    /// <param name="hash">PBKDF2 字符串</param>
    /// <param name="saltSize">随机 salt 大小</param>
    /// <param name="iterationCount">迭代次数</param>
    /// <param name="derivedKeyLength">密钥长度</param>
    /// <returns></returns>
    public static bool Compare(string text, string hash, int saltSize = 16, int iterationCount = 10000, int derivedKeyLength = 32)
    {
        try
        {
            var parts = hash.Split(new[] { SaltHashSeparator }, StringSplitOptions.None);
            if (parts.Length != 2)
                return false;

            var saltBytes = Convert.FromBase64String(parts[0]);
            var storedHashBytes = Convert.FromBase64String(parts[1]);

            if (saltBytes.Length != saltSize || storedHashBytes.Length != derivedKeyLength)
                return false;

            using var pbkdf2 = new Rfc2898DeriveBytes(text, saltBytes, iterationCount, HashAlgorithmName.SHA256);
            var computedHash = pbkdf2.GetBytes(derivedKeyLength);

            return computedHash.SequenceEqual(storedHashBytes);
        }
        catch
        {
            return false;
        }
    }
}