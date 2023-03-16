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