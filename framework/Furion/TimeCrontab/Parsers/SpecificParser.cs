// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.TimeCrontab;

/// <summary>
/// Cron 字段值含 数值 字符解析器
/// </summary>
/// <remarks>
/// <para>表示具体值，该字符支持在 Cron 所有字段域中设置</para>
/// </remarks>
internal class SpecificParser : ICronParser, ITimeParser
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="specificValue">具体值</param>
    /// <param name="kind">Cron 字段种类</param>
    public SpecificParser(int specificValue, CrontabFieldKind kind)
    {
        SpecificValue = specificValue;
        Kind = kind;

        // 验证值有效性
        ValidateBounds(specificValue);
    }

    /// <summary>
    /// Cron 字段种类
    /// </summary>
    public CrontabFieldKind Kind { get; }

    /// <summary>
    /// 具体值
    /// </summary>
    public int SpecificValue { get; private set; }

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

        return evalValue == SpecificValue;
    }

    /// <summary>
    /// 获取 Cron 字段种类当前值的下一个发生值
    /// </summary>
    /// <param name="currentValue">时间值</param>
    /// <returns><see cref="int"/></returns>
    /// <exception cref="TimeCrontabException"></exception>
    public virtual int? Next(int currentValue)
    {
        return SpecificValue;
    }

    /// <summary>
    /// 获取 Cron 字段种类字段起始值
    /// </summary>
    /// <returns><see cref="int"/></returns>
    /// <exception cref="TimeCrontabException"></exception>
    public int First()
    {
        return SpecificValue;
    }

    /// <summary>
    /// 将解析器转换成字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return SpecificValue.ToString();
    }

    /// <summary>
    /// 验证值有效性
    /// </summary>
    /// <param name="value">具体值</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void ValidateBounds(int value)
    {
        var minimum = Constants.MinimumDateTimeValues[Kind];
        var maximum = Constants.MaximumDateTimeValues[Kind];

        // 验证值有效性
        if (value < minimum || value > maximum)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"{nameof(value)} should be between {minimum} and {maximum} (was {SpecificValue}).");
        }

        // 兼容星期日可以同时用 0 或 7 表示
        if (Kind == CrontabFieldKind.DayOfWeek)
        {
            SpecificValue %= 7;
        }
    }
}