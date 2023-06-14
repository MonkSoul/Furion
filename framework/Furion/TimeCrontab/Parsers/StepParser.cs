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
/// Cron 字段值含 / 字符解析器
/// </summary>
/// <remarks>
/// <para>表示从某值开始，每隔固定值触发，该字符支持在 Cron 所有字段域中设置</para>
/// </remarks>
internal sealed class StepParser : ICronParser, ITimeParser
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="start">起始值</param>
    /// <param name="steps">步长</param>
    /// <param name="kind">Cron 字段种类</param>
    /// <exception cref="TimeCrontabException"></exception>
    public StepParser(int start, int steps, CrontabFieldKind kind)
    {
        // 验证步长有效性：不能小于或等于0，且不能大于 Cron 字段种类取值最大值
        var minimum = Constants.MinimumDateTimeValues[kind];
        var maximum = Constants.MaximumDateTimeValues[kind];
        if (steps <= 0 || steps > maximum)
        {
            throw new TimeCrontabException(string.Format("Steps = {0} is out of bounds for <{1}> field.", steps, Enum.GetName(typeof(CrontabFieldKind), kind)));
        }

        Start = start;
        Steps = steps;
        Kind = kind;

        // 控制循环起始值，并不一定从 Start 开始
        var loopStart = Math.Max(start, minimum);

        // 计算所有满足间隔步长计算的解析器
        var parsers = new List<SpecificParser>();
        for (var evalValue = loopStart; evalValue <= maximum; evalValue++)
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
    /// 步长
    /// </summary>
    public int Steps { get; }

    /// <summary>
    /// 所有满足间隔步长计算的解析器
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
        return string.Format("{0}/{1}", Start == 0 ? "*" : Start.ToString(), Steps);
    }

    /// <summary>
    /// 判断是否符合间隔或带步长间隔解析规则
    /// </summary>
    /// <param name="evalValue">当前值</param>
    /// <returns><see cref="bool"/></returns>
    private bool IsMatch(int evalValue)
    {
        return evalValue >= Start && (evalValue - Start) % Steps == 0;
    }
}