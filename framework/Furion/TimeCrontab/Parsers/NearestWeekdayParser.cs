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