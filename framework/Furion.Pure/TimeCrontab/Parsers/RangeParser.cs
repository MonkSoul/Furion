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

namespace Furion.TimeCrontab;

/// <summary>
/// Cron 字段值含 - 字符解析器
/// </summary>
/// <remarks>
/// <para>表示特定取值范围，如 1-5 或 1-5/2，该字符支持在 Cron 所有字段域中设置</para>
/// </remarks>
internal sealed class RangeParser : ICronParser, ITimeParser
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="start">起始值</param>
    /// <param name="end">终止值</param>
    /// <param name="steps">步长</param>
    /// <param name="kind">Cron 字段种类</param>
    /// <exception cref="TimeCrontabException"></exception>
    public RangeParser(int start, int end, int? steps, CrontabFieldKind kind)
    {
        var maximum = Constants.MaximumDateTimeValues[kind];

        // 验证起始值有效性
        if (start < 0 || start > maximum)
        {
            throw new TimeCrontabException(string.Format("Start = {0} is out of bounds for <{1}> field.", start, Enum.GetName(typeof(CrontabFieldKind), kind)));
        }

        // 验证终止值有效性
        if (end < 0 || end > maximum)
        {
            throw new TimeCrontabException(string.Format("End = {0} is out of bounds for <{1}> field.", end, Enum.GetName(typeof(CrontabFieldKind), kind)));
        }

        // 验证步长有效性
        if (steps != null && (steps <= 0 || steps > maximum))
        {
            throw new TimeCrontabException(string.Format("Steps = {0} is out of bounds for <{1}> field.", steps, Enum.GetName(typeof(CrontabFieldKind), kind)));
        }

        Start = start;
        End = end;
        Kind = kind;
        Steps = steps;

        // 计算所有满足范围计算的解析器
        var parsers = new List<SpecificParser>();
        for (var evalValue = Start; evalValue <= End; evalValue++)
        {
            if (IsMatch(evalValue))
            {
                parsers.Add(new SpecificParser(evalValue, Kind));
            }
        }

        SpecificParsers = parsers;
    }

    /// <summary>
    /// Cron 字段种类
    /// </summary>
    public CrontabFieldKind Kind { get; }

    /// <summary>
    /// 起始值
    /// </summary>
    public int Start { get; }

    /// <summary>
    /// 终止值
    /// </summary>
    public int End { get; }

    /// <summary>
    /// 步长
    /// </summary>
    public int? Steps { get; }

    /// <summary>
    /// 所有满足范围计算的解析器
    /// </summary>
    public IEnumerable<SpecificParser> SpecificParsers { get; }

    /// <summary>
    /// 判断当前时间是否符合 Cron 字段种类解析规则
    /// </summary>
    /// <param name="datetime">当前时间</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsMatch(DateTime datetime)
    {
        // 获取不同 Cron 字段种类对应时间值
        var evalValue = Kind switch
        {
            CrontabFieldKind.Second => datetime.Second,
            CrontabFieldKind.Minute => datetime.Minute,
            CrontabFieldKind.Hour => datetime.Hour,
            CrontabFieldKind.Day => datetime.Day,
            CrontabFieldKind.Month => datetime.Month,
            CrontabFieldKind.DayOfWeek => datetime.DayOfWeek.ToCronDayOfWeek(),
            CrontabFieldKind.Year => datetime.Year,
            _ => throw new ArgumentOutOfRangeException(nameof(datetime), Kind, null),
        };

        return IsMatch(evalValue);
    }

    /// <summary>
    /// 获取 Cron 字段种类当前值的下一个发生值
    /// </summary>
    /// <param name="currentValue">时间值</param>
    /// <returns><see cref="int"/></returns>
    /// <exception cref="TimeCrontabException"></exception>
    public int? Next(int currentValue)
    {
        // 由于天、月、周计算复杂，所以这里排除对它们的处理
        if (Kind == CrontabFieldKind.Day
            || Kind == CrontabFieldKind.Month
            || Kind == CrontabFieldKind.DayOfWeek)
        {
            throw new TimeCrontabException("Cannot call Next for Day, Month or DayOfWeek types.");
        }

        // 默认递增步长为 1
        int? newValue = currentValue + 1;

        // 获取下一个匹配的发生值
        var maximum = Constants.MaximumDateTimeValues[Kind];
        while (newValue < maximum && !IsMatch(newValue.Value))
        {
            newValue++;
        }

        return newValue > maximum ? null : newValue;
    }

    /// <summary>
    /// 存储起始值，避免重复计算
    /// </summary>
    private int? FirstCache { get; set; }

    /// <summary>
    /// 获取 Cron 字段种类字段起始值
    /// </summary>
    /// <returns><see cref="int"/></returns>
    /// <exception cref="TimeCrontabException"></exception>
    public int First()
    {
        // 判断是否缓存过起始值，如果有则跳过
        if (FirstCache.HasValue)
        {
            return FirstCache.Value;
        }

        // 由于天、月、周计算复杂，所以这里排除对它们的处理
        if (Kind == CrontabFieldKind.Day
            || Kind == CrontabFieldKind.Month
            || Kind == CrontabFieldKind.DayOfWeek)
        {
            throw new TimeCrontabException("Cannot call First for Day, Month or DayOfWeek types.");
        }

        var maximum = Constants.MaximumDateTimeValues[Kind];
        var newValue = 0;

        // 获取首个符合的起始值
        while (newValue < maximum && !IsMatch(newValue))
        {
            newValue++;
        }

        // 验证起始值有效性
        if (newValue > maximum)
        {
            throw new TimeCrontabException(
                string.Format("Next value for {0} on field {1} could not be found!",
                ToString(),
                Enum.GetName(typeof(CrontabFieldKind), Kind))
            );
        }

        // 缓存起始值
        FirstCache = newValue;
        return newValue;
    }

    /// <summary>
    /// 将解析器转换成字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return Steps.HasValue
                 ? string.Format("{0}-{1}/{2}", Start, End, Steps)
                 : string.Format("{0}-{1}", Start, End);
    }

    /// <summary>
    /// 判断是否符合范围或带步长范围解析规则
    /// </summary>
    /// <param name="evalValue">当前值</param>
    /// <returns><see cref="bool"/></returns>
    private bool IsMatch(int evalValue)
    {
        return evalValue >= Start && evalValue <= End
            && (!Steps.HasValue || ((evalValue - Start) % Steps) == 0);
    }
}