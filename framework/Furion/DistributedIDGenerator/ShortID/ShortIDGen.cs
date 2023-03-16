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