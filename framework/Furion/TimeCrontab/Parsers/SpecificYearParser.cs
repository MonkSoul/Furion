// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.TimeCrontab;

/// <summary>
/// Cron 字段值含 数值 字符解析器
/// </summary>
/// <remarks>
/// <para>表示具体值，这里仅处理 <see cref="CrontabFieldKind.Year"/> 字段域</para>
/// </remarks>
internal sealed class SpecificYearParser : SpecificParser
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="specificValue">年（具体值)</param>
    /// <param name="kind">Cron 字段种类</param>
    public SpecificYearParser(int specificValue, CrontabFieldKind kind)
        : base(specificValue, kind)
    {
    }

    /// <summary>
    /// 获取 Cron 字段种类当前值的下一个发生值
    /// </summary>
    /// <param name="currentValue">时间值</param>
    /// <returns><see cref="int"/></returns>
    /// <exception cref="TimeCrontabException"></exception>
    public override int? Next(int currentValue)
    {
        // 如果当前年份小于具体值，则返回具体值，否则返回 null
        // 因为一旦指定了年份，那么就必须等到那一年才触发
        return currentValue < SpecificValue ? SpecificValue : null;
    }
}