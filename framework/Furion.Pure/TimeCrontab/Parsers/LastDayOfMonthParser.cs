// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.TimeCrontab;

/// <summary>
/// Cron 字段值含 L 字符解析器
/// </summary>
/// <remarks>
/// <para>L 表示月中最后一天，仅在 <see cref="CrontabFieldKind.Day"/> 字段域中使用</para>
/// </remarks>
internal sealed class LastDayOfMonthParser : ICronParser
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="kind">Cron 字段种类</param>
    /// <exception cref="TimeCrontabException"></exception>
    public LastDayOfMonthParser(CrontabFieldKind kind)
    {
        // 验证 L 字符是否在 Day 字段域中使用
        if (kind != CrontabFieldKind.Day)
        {
            throw new TimeCrontabException("The <L> parser can only be used with the Day field.");
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
        return DateTime.DaysInMonth(datetime.Year, datetime.Month) == datetime.Day;
    }

    /// <summary>
    /// 将解析器转换成字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return "L";
    }
}