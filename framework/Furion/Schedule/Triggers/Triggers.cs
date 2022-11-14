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

using Furion.TimeCrontab;

namespace Furion.Schedule;

/// <summary>
/// 作业触发器静态类
/// </summary>
[SuppressSniffer]
public static class Triggers
{
    /// <summary>
    /// 创建作业周期（间隔）触发器构建器
    /// </summary>
    /// <param name="interval">间隔（毫秒）</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Period(int interval)
    {
        return TriggerBuilder.Period(interval);
    }

    /// <summary>
    /// 创建作业 Cron 触发器构建器
    /// </summary>
    /// <param name="schedule">Cron 表达式</param>
    /// <param name="format">Cron 表达式格式化类型，默认 <see cref="CronStringFormat.Default"/></param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Cron(string schedule, CronStringFormat format = CronStringFormat.Default)
    {
        return TriggerBuilder.Cron(schedule, format);
    }

    /// <summary>
    /// 创建作业每秒触发器构建器
    /// </summary>
    /// <returns></returns>
    public static TriggerBuilder Secondly()
    {
        return Cron("@secondly");
    }

    /// <summary>
    /// 创建作业每分钟的开始触发器构建器
    /// </summary>
    /// <returns></returns>
    public static TriggerBuilder Minutely()
    {
        return Cron("@minutely");
    }

    /// <summary>
    /// 创建作业每小时开始触发器构建器
    /// </summary>
    /// <returns></returns>
    public static TriggerBuilder Hourly()
    {
        return Cron("@hourly");
    }

    /// <summary>
    /// 创建作业每天（午夜）开始触发器构建器
    /// </summary>
    /// <returns></returns>
    public static TriggerBuilder Daily()
    {
        return Cron("@daily");
    }

    /// <summary>
    /// 创建作业每月1号（午夜）开始触发器构建器
    /// </summary>
    /// <returns></returns>
    public static TriggerBuilder Monthly()
    {
        return Cron("@monthly");
    }

    /// <summary>
    /// 创建作业每周日（午夜）开始触发器构建器
    /// </summary>
    /// <returns></returns>
    public static TriggerBuilder Weekly()
    {
        return Cron("@weekly");
    }

    /// <summary>
    /// 创建每年1月1号（午夜）开始触发器构建器
    /// </summary>
    /// <returns></returns>
    public static TriggerBuilder Yearly()
    {
        return Cron("@yearly");
    }
}