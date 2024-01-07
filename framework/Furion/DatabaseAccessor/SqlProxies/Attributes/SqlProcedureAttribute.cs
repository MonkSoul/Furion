// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库存储过程特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public class SqlProcedureAttribute : SqlObjectProxyAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">存储过程名</param>
    public SqlProcedureAttribute(string name) : base(name)
    {
    }
}