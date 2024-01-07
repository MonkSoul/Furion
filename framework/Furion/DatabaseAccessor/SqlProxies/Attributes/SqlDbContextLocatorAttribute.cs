// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 代理指定定位器特性
/// </summary>
[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
public sealed class SqlDbContextLocatorAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dbContextLocator"></param>
    public SqlDbContextLocatorAttribute(Type dbContextLocator)
    {
        Locator = dbContextLocator;
    }

    /// <summary>
    /// 定位器
    /// </summary>
    public Type Locator { get; set; } = typeof(MasterDbContextLocator);
}