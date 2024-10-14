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

namespace Furion.Extensions;

/// <summary>
///     数值类型拓展类
/// </summary>
public static class NumberExtensions
{
    /// <summary>
    ///     根据指定的单位将字节数进行转换
    /// </summary>
    /// <param name="byteSize">字节数</param>
    /// <param name="unit">单位。可选值为：<c>B</c>, <c>KB</c>, <c>MB</c>, <c>GB</c>, <c>TB</c>, <c>PB</c>, <c>EB</c>。</param>
    /// <returns>
    ///     <see cref="double" />
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static double ToSizeUnits(this double byteSize, string unit)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(unit);

        // 非负检查
        if (byteSize < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(byteSize),
                $"The `{nameof(byteSize)}` must be non-negative.");
        }

        return unit.ToUpperInvariant() switch
        {
            "B" => byteSize,
            "KB" => byteSize / 1024.0,
            "MB" => byteSize / (1024.0 * 1024),
            "GB" => byteSize / (1024.0 * 1024 * 1024),
            "TB" => byteSize / (1024.0 * 1024 * 1024 * 1024),
            "PB" => byteSize / (1024.0 * 1024 * 1024 * 1024 * 1024),
            "EB" => byteSize / (1024.0 * 1024 * 1024 * 1024 * 1024 * 1024),
            _ => throw new ArgumentOutOfRangeException(nameof(unit), $"Unsupported `{unit}` unit.")
        };
    }

    /// <summary>
    ///     根据指定的单位将字节数进行转换
    /// </summary>
    /// <param name="byteSize">字节数</param>
    /// <param name="unit">单位。可选值为：<c>B</c>, <c>KB</c>, <c>MB</c>, <c>GB</c>, <c>TB</c>, <c>PB</c>, <c>EB</c>。</param>
    /// <returns>
    ///     <see cref="double" />
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static double ToSizeUnits(this long byteSize, string unit) => ToSizeUnits((double)byteSize, unit);
}