// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Data;

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 语句执行代理
/// </summary>
[SuppressSniffer]
public class SqlSentenceProxyAttribute : SqlProxyAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sql"></param>
    public SqlSentenceProxyAttribute(string sql)
    {
        Sql = sql;
        CommandType = CommandType.Text;
    }

    /// <summary>
    /// Sql 语句
    /// </summary>
    public string Sql { get; set; }

    /// <summary>
    /// 命令类型
    /// </summary>
    public CommandType CommandType { get; set; }
}