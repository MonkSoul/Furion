// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.TimeCrontab;

/// <summary>
/// Cron 字段字符解析器依赖接口
/// </summary>
internal interface ICronParser
{
    /// <summary>
    /// Cron 字段种类
    /// </summary>
    CrontabFieldKind Kind { get; }

    /// <summary>
    /// 判断当前时间是否符合 Cron 字段种类解析规则
    /// </summary>
    /// <param name="datetime">当前时间</param>
    /// <returns><see cref="bool"/></returns>
    bool IsMatch(DateTime datetime);
}