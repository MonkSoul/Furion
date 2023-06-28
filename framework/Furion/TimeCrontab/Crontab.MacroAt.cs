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
/// Cron 表达式抽象类
/// </summary>
/// <remarks>主要将 Cron 表达式转换成 OOP 类进行操作</remarks>
public sealed partial class Crontab
{
    /// <summary>
    /// 创建指定特定秒开始作业触发器构建器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab SecondlyAt(params object[] fields)
    {
        // 检查字段合法性
        CheckFieldsNotNullOrEmpty(fields);

        return Parse($"{FieldsToString(fields)} * * * * *", CronStringFormat.WithSeconds);
    }

    /// <summary>
    /// 创建每分钟特定秒开始作业触发器构建器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab MinutelyAt(params object[] fields)
    {
        // 检查字段合法性
        CheckFieldsNotNullOrEmpty(fields);

        return Parse($"{FieldsToString(fields)} * * * * *", CronStringFormat.WithSeconds);
    }

    /// <summary>
    /// 创建每小时特定分钟开始作业触发器构建器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab HourlyAt(params object[] fields)
    {
        // 检查字段合法性
        CheckFieldsNotNullOrEmpty(fields);

        return Parse($"{FieldsToString(fields)} * * * *", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每天特定小时开始作业触发器构建器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab DailyAt(params object[] fields)
    {
        // 检查字段合法性
        CheckFieldsNotNullOrEmpty(fields);

        return Parse($"0 {FieldsToString(fields)} * * *", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每月特定天（午夜）开始作业触发器构建器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab MonthlyAt(params object[] fields)
    {
        // 检查字段合法性
        CheckFieldsNotNullOrEmpty(fields);

        return Parse($"0 0 {FieldsToString(fields)} * *", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每周特定星期几（午夜）开始作业触发器构建器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab WeeklyAt(params object[] fields)
    {
        // 检查字段合法性
        CheckFieldsNotNullOrEmpty(fields);

        return Parse($"0 0 * * {FieldsToString(fields)}", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每年特定月1号（午夜）开始作业触发器构建器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab YearlyAt(params object[] fields)
    {
        // 检查字段合法性
        CheckFieldsNotNullOrEmpty(fields);

        return Parse($"0 0 1 {FieldsToString(fields)} *", CronStringFormat.Default);
    }

    /// <summary>
    /// 检查字段域 非 Null 非空数组
    /// </summary>
    /// <param name="fields">字段值</param>
    private static void CheckFieldsNotNullOrEmpty(params object[] fields)
    {
        // 空检查
        if (fields == null || fields.Length == 0) throw new ArgumentNullException(nameof(fields));

        // 检查 fields 只能是 int, long，string 和非 null 类型
        if (fields.Any(f => f == null || (f.GetType() != typeof(int) && f.GetType() != typeof(long) && f.GetType() != typeof(string)))) throw new InvalidOperationException("Invalid Cron expression.");
    }

    /// <summary>
    /// 将字段域转换成 string
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="string"/></returns>
    private static string FieldsToString(params object[] fields)
    {
        return string.Join(",", fields);
    }
}