// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 执行特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public class SqlExecuteAttribute : SqlSentenceProxyAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sql">sql 语句</param>
    public SqlExecuteAttribute(string sql) : base(sql)
    {
    }

    /// <summary>
    /// 返回受影响行数
    /// </summary>
    /// <remarks>只有非查询类操作有效</remarks>
    public bool RowEffects { get; set; } = false;
}