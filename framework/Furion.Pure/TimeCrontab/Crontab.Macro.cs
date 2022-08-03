// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
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
public sealed partial class Crontab
{
    /// <summary>
    /// 表示每秒的 <see cref="Crontab"/> 对象
    /// </summary>
    public static readonly Crontab Secondly = Parse("* * * * * *", CronStringFormat.WithSeconds);

    /// <summary>
    /// 表示每分钟的 <see cref="Crontab"/> 对象
    /// </summary>
    public static readonly Crontab Minutely = Parse("* * * * *", CronStringFormat.Default);

    /// <summary>
    /// 表示每小时开始 的 <see cref="Crontab"/> 对象
    /// </summary>
    public static readonly Crontab Hourly = Parse("0 * * * *", CronStringFormat.Default);

    /// <summary>
    /// 表示每天（午夜）开始的 <see cref="Crontab"/> 对象
    /// </summary>
    public static readonly Crontab Daily = Parse("0 0 * * *", CronStringFormat.Default);

    /// <summary>
    /// 表示每月1号（午夜）开始的 <see cref="Crontab"/> 对象
    /// </summary>
    public static readonly Crontab Monthly = Parse("0 0 1 * *", CronStringFormat.Default);

    /// <summary>
    /// 表示每周日（午夜）开始的 <see cref="Crontab"/> 对象
    /// </summary>
    public static readonly Crontab Weekly = Parse("0 0 * * 0", CronStringFormat.Default);

    /// <summary>
    /// 表示每年1月1号（午夜）开始的 <see cref="Crontab"/> 对象
    /// </summary>
    public static readonly Crontab Yearly = Parse("0 0 1 1 *", CronStringFormat.Default);
}