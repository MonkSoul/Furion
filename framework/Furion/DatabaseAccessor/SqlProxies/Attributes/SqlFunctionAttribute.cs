// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 函数特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public class SqlFunctionAttribute : SqlObjectProxyAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">函数名</param>
    public SqlFunctionAttribute(string name) : base(name)
    {
    }
}