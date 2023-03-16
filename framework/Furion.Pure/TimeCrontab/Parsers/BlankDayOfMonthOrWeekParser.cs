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
/// Cron 字段值含 ? 字符解析器
/// </summary>
/// <remarks>
/// <para>只能用在 Day 和 DayOfWeek 两个域使用。它也匹配域的任意值，但实际不会。因为 Day 和 DayOfWeek 会相互影响</para>
/// <para>例如想在每月的 20 日触发调度，不管 20 日到底是星期几，则只能使用如下写法：13 15 20 * ?</para>
/// <para>其中最后一位只能用 ?，而不能使用 *，如果使用 * 表示不管星期几都会触发，实际上并不是这样</para>
/// <para>所以 ? 起着 Day 和 DayOfWeek 互斥性作用</para>
/// <para>仅在 <see cref="CrontabFieldKind.Day"/> 或 <see cref="CrontabFieldKind.DayOfWeek"/> 字段域中使用</para>
/// </remarks>
internal sealed class BlankDayOfMonthOrWeekParser : ICronParser
{
    /// <summary>
    ///  构造函数
    /// </summary>
    /// <param name="kind">Cron 字段种类</param>
    /// <exception cref="TimeCrontabException"></exception>
    public BlankDayOfMonthOrWeekParser(CrontabFieldKind kind)
    {
        // 验证 ? 字符是否在 DayOfWeek 和 Day 字段域中使用
        if (kind != CrontabFieldKind.DayOfWeek && kind != CrontabFieldKind.Day)
        {
            throw new TimeCrontabException("The <?> parser can only be used in the Day-of-Week or Day-of-Month fields.");
        }

        Kind = kind;
    }

    /// <summary>
    /// Cron 字段种类
    /// </summary>
    public CrontabFieldKind Kind { get; }

    /// <summary>
    /// 判断当前时间是否符合 Cron 字段种类解析规则
    /// </summary>
    /// <param name="datetime">当前时间</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsMatch(DateTime datetime)
    {
        return true;
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

        // 验证最大值
        var maximum = Constants.MaximumDateTimeValues[Kind];
        return newValue >= maximum ? null : newValue;
    }

    /// <summary>
    /// 获取 Cron 字段种类字段起始值
    /// </summary>
    /// <returns><see cref="int"/></returns>
    /// <exception cref="TimeCrontabException"></exception>
    public int First()
    {
        // 由于天、月、周计算复杂，所以这里排除对它们的处理
        if (Kind == CrontabFieldKind.Day
            || Kind == CrontabFieldKind.Month
            || Kind == CrontabFieldKind.DayOfWeek)
        {
            throw new TimeCrontabException("Cannot call First for Day, Month or DayOfWeek types.");
        }

        return 0;
    }

    /// <summary>
    /// 将解析器转换成字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return "?";
    }
}