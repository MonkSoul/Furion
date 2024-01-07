// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 对象类型执行代理
/// </summary>
[SuppressSniffer]
public class SqlObjectProxyAttribute : SqlProxyAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">对象名</param>
    public SqlObjectProxyAttribute(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 对象名
    /// </summary>
    public string Name { get; set; }
}