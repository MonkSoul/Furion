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

namespace Furion.TimeCrontab;

/// <summary>
/// TimeCrontab 模块常量
/// </summary>
internal static class Constants
{
    /// <summary>
    /// Cron 字段种类最大值
    /// </summary>
    internal static readonly Dictionary<CrontabFieldKind, int> MaximumDateTimeValues = new()
    {
        { CrontabFieldKind.Second, 59 },
        { CrontabFieldKind.Minute, 59 },
        { CrontabFieldKind.Hour, 23 },
        { CrontabFieldKind.DayOfWeek, 7 },
        { CrontabFieldKind.Day, 31 },
        { CrontabFieldKind.Month, 12 },
        { CrontabFieldKind.Year, 9999 },
    };

    /// <summary>
    /// Cron 字段种类最大值
    /// </summary>
    internal static readonly Dictionary<CrontabFieldKind, int> MinimumDateTimeValues = new()
    {
        { CrontabFieldKind.Second, 0 },
        { CrontabFieldKind.Minute, 0 },
        { CrontabFieldKind.Hour, 0 },
        { CrontabFieldKind.DayOfWeek, 0 },
        { CrontabFieldKind.Day, 1 },
        { CrontabFieldKind.Month, 1 },
        { CrontabFieldKind.Year, 1 },
    };

    /// <summary>
    /// Cron 不同格式化类型字段数量
    /// </summary>
    internal static readonly Dictionary<CronStringFormat, int> ExpectedFieldCounts = new()
    {
        { CronStringFormat.Default, 5 },
        { CronStringFormat.WithYears, 6 },
        { CronStringFormat.WithSeconds, 6 },
        { CronStringFormat.WithSecondsAndYears, 7 },
    };

    /// <summary>
    /// 配置 C# 中 <see cref="DayOfWeek"/> 枚举元素值
    /// </summary>
    /// <remarks>主要解决 C# 中该类型和 Cron 星期字段域不对应问题</remarks>
    internal static readonly Dictionary<DayOfWeek, int> CronDays = new()
    {
        { DayOfWeek.Sunday, 0 },
        { DayOfWeek.Monday, 1 },
        { DayOfWeek.Tuesday, 2 },
        { DayOfWeek.Wednesday, 3 },
        { DayOfWeek.Thursday, 4 },
        { DayOfWeek.Friday, 5 },
        { DayOfWeek.Saturday, 6 },
    };

    /// <summary>
    /// 定义 Cron 星期字段域值支持的星期英文缩写
    /// </summary>
    internal static readonly Dictionary<string, int> Days = new()
    {
        { "SUN", 0 },
        { "MON", 1 },
        { "TUE", 2 },
        { "WED", 3 },
        { "THU", 4 },
        { "FRI", 5 },
        { "SAT", 6 },
    };

    /// <summary>
    /// 定义 Cron 月字段域值支持的星期英文缩写
    /// </summary>
    internal static readonly Dictionary<string, int> Months = new()
    {
        { "JAN", 1 },
        { "FEB", 2 },
        { "MAR", 3 },
        { "APR", 4 },
        { "MAY", 5 },
        { "JUN", 6 },
        { "JUL", 7 },
        { "AUG", 8 },
        { "SEP", 9 },
        { "OCT", 10 },
        { "NOV", 11 },
        { "DEC", 12 },
    };
}