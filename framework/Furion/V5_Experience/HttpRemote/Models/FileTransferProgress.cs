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

using Furion.Extensions;
using Furion.Utilities;

namespace Furion.HttpRemote;

/// <summary>
///     文件传输的进度信息
/// </summary>
public sealed class FileTransferProgress
{
    /// <summary>
    ///     使用一个小的正值来防止除零错误
    /// </summary>
    internal const double _epsilon = double.Epsilon;

    /// <summary>
    ///     <inheritdoc cref="FileTransferProgress" />
    /// </summary>
    /// <param name="fileFullName">文件完整路径</param>
    /// <param name="totalFileSize">文件的总大小</param>
    internal FileTransferProgress(string fileFullName, long totalFileSize)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(fileFullName);

        TotalFileSize = totalFileSize;

        FileFullName = fileFullName;
        FileName = Path.GetFileName(fileFullName);
    }

    /// <summary>
    ///     文件完整路径
    /// </summary>
    public string FileFullName { get; }

    /// <summary>
    ///     文件的名称
    /// </summary>
    public string FileName { get; }

    /// <summary>
    ///     文件的总大小
    /// </summary>
    /// <remarks>以字节为单位。</remarks>
    public long TotalFileSize { get; }

    /// <summary>
    ///     已传输的数据量
    /// </summary>
    /// <remarks>以字节为单位。</remarks>
    public long Transferred { get; private set; }

    /// <summary>
    ///     已完成的传输百分比
    /// </summary>
    public double PercentageComplete { get; private set; }

    /// <summary>
    ///     获取当前的传输速率
    /// </summary>
    /// <remarks>以字节/秒为单位。</remarks>
    public double TransferRate { get; private set; }

    /// <summary>
    ///     从开始传输到现在的持续时间
    /// </summary>
    public TimeSpan TimeElapsed { get; private set; }

    /// <summary>
    ///     预估剩余传输时间
    /// </summary>
    public TimeSpan EstimatedTimeRemaining { get; private set; }

    /// <inheritdoc />
    public override string ToString() =>
        StringUtility.FormatKeyValuesSummary([
            new KeyValuePair<string, IEnumerable<string>>("File Name", [FileName]),
            new KeyValuePair<string, IEnumerable<string>>("File Full Name", [FileFullName]),
            new KeyValuePair<string, IEnumerable<string>>("Total File Size",
                [$"{TotalFileSize.ToSizeUnits("MB"):F2} MB"]),
            new KeyValuePair<string, IEnumerable<string>>("Transferred", [$"{Transferred.ToSizeUnits("MB"):F2} MB"]),
            new KeyValuePair<string, IEnumerable<string>>("Percentage Complete", [$"{PercentageComplete:F2}%"]),
            new KeyValuePair<string, IEnumerable<string>>("Transfer Rate",
                [$"{TransferRate.ToSizeUnits("MB"):F2} MB/s"]),
            new KeyValuePair<string, IEnumerable<string>>("Time Elapsed (s)", [$"{TimeElapsed.TotalSeconds:F2}"]),
            new KeyValuePair<string, IEnumerable<string>>("Estimated Time Remaining (s)",
                [$"{EstimatedTimeRemaining.TotalSeconds:F2}"])
        ], "Transfer Progress")!;

    /// <summary>
    ///     更新文件传输进度
    /// </summary>
    /// <param name="transferred">已传输的数据量</param>
    /// <param name="timeElapsed">从开始传输到现在的持续时间</param>
    internal void UpdateProgress(long transferred, TimeSpan timeElapsed)
    {
        // 计算已完成的传输百分比和当前的传输速率
        var percentageComplete = TotalFileSize > 0 ? 100.0 * transferred / TotalFileSize : -1;
        var transferRate = timeElapsed.TotalSeconds > _epsilon ? transferred / timeElapsed.TotalSeconds : 0;

        // 更新内部进度状态
        Transferred = transferred;
        TimeElapsed = timeElapsed;
        PercentageComplete = percentageComplete;
        TransferRate = transferRate;

        // 计算预估剩余传输时间
        EstimatedTimeRemaining = CalculateEstimatedTimeRemaining();
    }

    /// <summary>
    ///     计算预估剩余传输时间
    /// </summary>
    /// <returns>
    ///     <see cref="TimeSpan" />
    /// </returns>
    internal TimeSpan CalculateEstimatedTimeRemaining()
    {
        // 如果文件大小小于等于 0 或传输速率为 0 或接近 0，则认为无法预估
        if (TotalFileSize <= 0 || TransferRate <= _epsilon)
        {
            return TimeSpan.MaxValue;
        }

        // 计算剩余时间
        var secondsRemaining = (TotalFileSize - Transferred) / TransferRate;

        // 如果剩余时间超过最大值，则返回最大值
        return secondsRemaining > TimeSpan.MaxValue.TotalSeconds
            ? TimeSpan.MaxValue
            : TimeSpan.FromSeconds(secondsRemaining);
    }
}