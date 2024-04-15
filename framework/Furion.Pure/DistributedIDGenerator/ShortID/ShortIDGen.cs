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

using System.Text;

namespace Furion.DistributedIDGenerator;

/// <summary>
/// 短 ID 生成核心代码
/// <para>代码参考自：https://github.com/bolorundurowb/shortid </para>
/// </summary>
[SuppressSniffer]
public static class ShortIDGen
{
    /// <summary>
    /// 短 ID 生成器期初数据
    /// </summary>
    private static Random _random = new();

    private const string Bigs = "ABCDEFGHIJKLMNPQRSTUVWXY";
    private const string Smalls = "abcdefghjklmnopqrstuvwxyz";
    private const string Numbers = "0123456789";
    private const string Specials = "_-";
    private static string _pool = $"{Smalls}{Bigs}";

    /// <summary>
    /// 线程安全锁
    /// </summary>
    private static readonly object ThreadLock = new();

    /// <summary>
    /// 生成目前比较主流的短 ID
    /// <para>包含字母、数字，不包含特殊字符</para>
    /// <para>默认生成 8 位</para>
    /// </summary>
    /// <returns></returns>
    public static string NextID()
    {
        return NextID(new GenerationOptions
        {
            UseNumbers = true,
            UseSpecialCharacters = false,
            Length = 8
        });
    }

    /// <summary>
    /// 生成短 ID
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static string NextID(GenerationOptions options)
    {
        // 配置必填
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        // 判断生成的长度是否小于规定的长度，规定为 8
        if (options.Length < Constants.MinimumAutoLength)
        {
            throw new ArgumentException(
                $"The specified length of {options.Length} is less than the lower limit of {Constants.MinimumAutoLength} to avoid conflicts.");
        }

        var characterPool = _pool;
        var poolBuilder = new StringBuilder(characterPool);

        // 是否包含数字
        if (options.UseNumbers)
        {
            poolBuilder.Append(Numbers);
        }

        // 是否包含特殊字符
        if (options.UseSpecialCharacters)
        {
            poolBuilder.Append(Specials);
        }

        var pool = poolBuilder.ToString();

        // 生成拼接
        var output = new char[options.Length];
        for (var i = 0; i < options.Length; i++)
        {
            lock (ThreadLock)
            {
                var charIndex = _random.Next(0, pool.Length);
                output[i] = pool[charIndex];
            }
        }

        return new string(output);
    }

    /// <summary>
    /// 设置参与运算的字符，最少 50 位
    /// </summary>
    /// <param name="characters"></param>
    public static void SetCharacters(string characters)
    {
        if (string.IsNullOrWhiteSpace(characters))
        {
            throw new ArgumentException("The replacement characters must not be null or empty.");
        }

        var charSet = characters
            .ToCharArray()
            .Where(x => !char.IsWhiteSpace(x))
            .Distinct()
            .ToArray();

        if (charSet.Length < Constants.MinimumCharacterSetLength)
        {
            throw new InvalidOperationException(
                $"The replacement characters must be at least {Constants.MinimumCharacterSetLength} letters in length and without whitespace.");
        }

        lock (ThreadLock)
        {
            _pool = new string(charSet);
        }
    }

    /// <summary>
    /// 设置种子步长
    /// </summary>
    /// <param name="seed"></param>
    public static void SetSeed(int seed)
    {
        lock (ThreadLock)
        {
            _random = new Random(seed);
        }
    }

    /// <summary>
    /// 重置所有配置
    /// </summary>
    public static void Reset()
    {
        lock (ThreadLock)
        {
            _random = new Random();
            _pool = $"{Smalls}{Bigs}";
        }
    }
}