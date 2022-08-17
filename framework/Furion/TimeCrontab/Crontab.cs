// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
        return new Crontab
        {
            Format = format,
            Parsers = ParseToDictionary(expression, format)
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