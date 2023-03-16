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
/// Cron 字段值含 {0}W 字符解析器
/// </summary>
/// <remarks>
/// <para>表示离指定日期最近的工作日，即最后一个非周六周末日，仅在 <see cref="CrontabFieldKind.Day"/> 字段域中使用</para>
/// </remarks>
internal sealed class NearestWeekdayParser : ICronParser
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="specificValue">天数（具体值）</param>
    /// <param name="kind">Cron 字段种类</param>
    /// <exception cref="TimeCrontabException">Cron 字段种类</exception>
    public NearestWeekdayParser(int specificValue, CrontabFieldKind kind)
    {
        // 验证 {0}W 字符是否在 Day 字段域中使用
        if (kind != CrontabFieldKind.Day)
        {
            throw new TimeCrontabException(string.Format("The <{0}W> parser can only be used in the Day field.", specificValue));
        }

        // 判断天数是否在有效取值范围内
        var maximum = Constants.MaximumDateTimeValues[CrontabFieldKind.Day];
        if (specificValue <= 0 || specificValue > maximum)
        {
            throw new TimeCrontabException(string.Format("The <{0}W> is out of bounds for the Day field.", specificValue));
        }

        SpecificValue = specificValue;
        Kind = kind;
    }

    /// <summary>
    /// Cron 字段种类
    /// </summary>
    public CrontabFieldKind Kind { get; }

    /// <summary>
    /// 天数（具体值）
    /// </summary>
    public int SpecificValue { get; }

    /// <summary>
    /// 判断当前时间是否符合 Cron 字段种类解析规则
    /// </summary>
    /// <param name="datetime">当前时间</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsMatch(DateTime datetime)
    {
        /*
         * W：表示有效工作日(周一到周五),只能出现在 Day 域，系统将在离指定日期的最近的有效工作日触发事件
         * 例如：在 Day 使用 5W，如果 5 日是星期六，则将在最近的工作日：星期五，即 4 日触发
         * 如果 5 日是星期天，则在 6 日(周一)触发；如果 5 日在星期一到星期五中的一天，则就在 5 日触发
         * 另外一点，W 的最近寻找不会跨过月份
         */

        // 如果这个月没有足够的天数则跳过（例如，二月没有 30 和 31 日）
        if (DateTime.DaysInMonth(datetime.Year, datetime.Month) < SpecificValue)
        {
            return false;
        }

        // 获取当前时间特定天数时间
        var specificDay = new DateTime(datetime.Year, datetime.Month, SpecificValue);

        // 最靠近的工作日时间
        DateTime closestWeekday;

        // 处理当天的不同情况
        switch (specificDay.DayOfWeek)
        {
            // 如果当天是周六，则退一天
            case DayOfWeek.Saturday:
                closestWeekday = specificDay.AddDays(-1);

                // 如果退一天不在本月，则转到下周一
                if (closestWeekday.Month != specificDay.Month)
                {
                    closestWeekday = specificDay.AddDays(2);
                }

                break;

            // 如果当天是周天，则进一天
            case DayOfWeek.Sunday:
                closestWeekday = specificDay.AddDays(1);

                // 如果进一天不在本月，则退到上周五
                if (closestWeekday.Month != specificDay.Month)
                {
                    closestWeekday = specificDay.AddDays(-2);
                }

                break;

            // 处理恰好是工作日情况，直接使用
            default:
                closestWeekday = specificDay;
                break;
        }

        return datetime.Day == closestWeekday.Day;
    }

    /// <summary>
    /// 将解析器转换成字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return string.Format("{0}W", SpecificValue);
    }
}