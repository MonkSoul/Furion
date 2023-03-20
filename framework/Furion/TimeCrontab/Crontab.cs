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
/// Cron 表达式抽象类
/// </summary>
/// <remarks>主要将 Cron 表达式转换成 OOP 类进行操作</remarks>
[SuppressSniffer]
public sealed partial class Crontab
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <remarks>禁止外部 new 实例化</remarks>
    private Crontab()
    {
        Parsers = new Dictionary<CrontabFieldKind, List<ICronParser>>();
        Format = CronStringFormat.Default;
    }

    /// <summary>
    /// Cron 字段解析器字典集合
    /// </summary>
    private Dictionary<CrontabFieldKind, List<ICronParser>> Parsers { get; set; }

    /// <summary>
    /// Cron 表达式格式化类型
    /// </summary>
    /// <remarks>禁止运行时更改</remarks>
    public CronStringFormat Format { get; private set; }

    /// <summary>
    /// 解析 Cron 表达式并转换成 <see cref="Crontab"/> 对象
    /// </summary>
    /// <param name="expression">Cron 表达式</param>
    /// <param name="format">Cron 表达式格式化类型</param>
    /// <returns><see cref="Crontab"/></returns>
    /// <exception cref="TimeCrontabException"></exception>
    public static Crontab Parse(string expression, CronStringFormat format = CronStringFormat.Default)
    {
        // 处理 Macro 表达式
        if (expression.StartsWith('@'))
        {
            return expression switch
            {
                "@secondly" => Secondly,
                "@minutely" => Minutely,
                "@hourly" => Hourly,
                "@daily" => Daily,
                "@monthly" => Monthly,
                "@weekly" => Weekly,
                "@yearly" => Yearly,
                "@workday" => Workday,
                _ => throw new NotImplementedException(),
            };
        }

        return new Crontab
        {
            Format = format,
            Parsers = ParseToDictionary(expression, format)
        };
    }

    /// <summary>
    /// 解析 Cron Macro 符号并转换成 <see cref="Crontab"/> 对象
    /// </summary>
    /// <param name="macro">Macro 符号</param>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    /// <exception cref="TimeCrontabException"></exception>
    public static Crontab ParseAt(string macro, params object[] fields)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(macro)) throw new ArgumentNullException(nameof(macro));

        return macro switch
        {
            "@secondly" => SecondlyAt(fields),
            "@minutely" => MinutelyAt(fields),
            "@hourly" => HourlyAt(fields),
            "@daily" => DailyAt(fields),
            "@monthly" => MonthlyAt(fields),
            "@weekly" => WeeklyAt(fields),
            "@yearly" => YearlyAt(fields),
            _ => throw new NotImplementedException(),
        };
    }

    /// <summary>
    /// 解析 Cron 表达式并转换成 <see cref="Crontab"/> 对象
    /// </summary>
    /// <remarks>解析失败返回 default</remarks>
    /// <param name="expression">Cron 表达式</param>
    /// <param name="format">Cron 表达式格式化类型</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab TryParse(string expression, CronStringFormat format = CronStringFormat.Default)
    {
        try
        {
            return Parse(expression, format);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 判断 Cron 表达式是否有效
    /// </summary>
    /// <param name="expression">Cron 表达式</param>
    /// <param name="format">Cron 表达式格式化类型</param>
    /// <returns><see cref="Crontab"/></returns>
    public static bool IsValid(string expression, CronStringFormat format = CronStringFormat.Default)
    {
        return TryParse(expression, format) != null;
    }

    /// <summary>
    /// 获取起始时间下一个发生时间
    /// </summary>
    /// <param name="baseTime">起始时间</param>
    /// <returns><see cref="DateTime"/></returns>
    public DateTime GetNextOccurrence(DateTime baseTime)
    {
        return GetNextOccurrence(baseTime, DateTime.MaxValue);
    }

    /// <summary>
    /// 获取特定时间范围下一个发生时间
    /// </summary>
    /// <param name="baseTime">起始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <returns><see cref="DateTime"/></returns>
    public DateTime GetNextOccurrence(DateTime baseTime, DateTime endTime)
    {
        return InternalGetNextOccurence(baseTime, endTime);
    }

    /// <summary>
    /// 获取特定时间范围所有发生时间
    /// </summary>
    /// <param name="baseTime">起始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <returns><see cref="IEnumerable{T}"/></returns>
    public IEnumerable<DateTime> GetNextOccurrences(DateTime baseTime, DateTime endTime)
    {
        for (var occurrence = GetNextOccurrence(baseTime, endTime);
             occurrence < endTime;
             occurrence = GetNextOccurrence(occurrence, endTime))
        {
            yield return occurrence;
        }
    }

    /// <summary>
    /// 计算距离下一个发生时间相差毫秒数
    /// </summary>
    /// <param name="baseTime">起始时间</param>
    /// <returns></returns>
    public double GetSleepMilliseconds(DateTime baseTime)
    {
        // 采用 DateTimeKind.Unspecified 转换当前时间并忽略毫秒之后部分
        var startAt = new DateTime(baseTime.Year
            , baseTime.Month
            , baseTime.Day
            , baseTime.Hour
            , baseTime.Minute
            , baseTime.Second
            , baseTime.Millisecond);

        // 计算总休眠时间
        return (GetNextOccurrence(startAt) - startAt).TotalMilliseconds;
    }

    /// <summary>
    /// 计算距离下一个发生时间相差时间戳
    /// </summary>
    /// <param name="baseTime">起始时间</param>
    /// <returns></returns>
    public TimeSpan GetSleepTimeSpan(DateTime baseTime)
    {
        return TimeSpan.FromMilliseconds(GetSleepMilliseconds(baseTime));
    }

    /// <summary>
    /// 将 <see cref="Crontab"/> 对象转换成 Cron 表达式字符串
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var paramList = new List<string>();

        // 判断当前 Cron 格式化类型是否包含秒字段域
        if (Format == CronStringFormat.WithSeconds || Format == CronStringFormat.WithSecondsAndYears)
        {
            JoinParsers(paramList, CrontabFieldKind.Second);
        }

        // Cron 常规字段域
        JoinParsers(paramList, CrontabFieldKind.Minute);
        JoinParsers(paramList, CrontabFieldKind.Hour);
        JoinParsers(paramList, CrontabFieldKind.Day);
        JoinParsers(paramList, CrontabFieldKind.Month);
        JoinParsers(paramList, CrontabFieldKind.DayOfWeek);

        // 判断当前 Cron 格式化类型是否包含年字段域
        if (Format == CronStringFormat.WithYears || Format == CronStringFormat.WithSecondsAndYears)
        {
            JoinParsers(paramList, CrontabFieldKind.Year);
        }

        // 空格分割并输出
        return string.Join(" ", paramList.ToArray());
    }
}