// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业信息持久化上下文
/// </summary>
[SuppressSniffer]
public class PersistenceContext
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="behavior">作业持久化行为</param>
    internal PersistenceContext(JobDetail jobDetail
        , PersistenceBehavior behavior)
    {
        JobId = jobDetail.JobId;
        JobDetail = jobDetail;
        Behavior = behavior;
    }

    /// <summary>
    /// 作业 Id
    /// </summary>
    public string JobId { get; }

    /// <summary>
    /// 作业信息
    /// </summary>
    public JobDetail JobDetail { get; }

    /// <summary>
    /// 作业持久化行为
    /// </summary>
    public PersistenceBehavior Behavior { get; }

    /// <summary>
    /// 转换成 Sql 语句
    /// </summary>
    /// <param name="tableName">数据库表名</param>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="string"/></returns>
    public string ConvertToSQL(string tableName, NamingConventions naming = NamingConventions.CamelCase)
    {
        return JobDetail.ConvertToSQL(tableName, Behavior, naming);
    }

    /// <summary>
    /// 转换成 JSON 语句
    /// </summary>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="string"/></returns>
    public string ConvertToJSON(NamingConventions naming = NamingConventions.CamelCase)
    {
        return JobDetail.ConvertToJSON(naming);
    }

    /// <summary>
    /// 转换成 Monitor 字符串
    /// </summary>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="string"/></returns>
    public string ConvertToMonitor(NamingConventions naming = NamingConventions.CamelCase)
    {
        return JobDetail.ConvertToMonitor(naming);
    }

    /// <summary>
    /// 根据不同的命名法返回属性名
    /// </summary>
    /// <param name="propertyName">属性名</param>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="string"/></returns>
    public string GetNaming(string propertyName, NamingConventions naming = NamingConventions.CamelCase)
    {
        // 空检查
        if (!string.IsNullOrWhiteSpace(propertyName)) return propertyName;

        return Penetrates.GetNaming(propertyName, naming);
    }

    /// <summary>
    /// 作业信息持久化上下文转字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return $"{JobDetail} [{Behavior}]";
    }
}