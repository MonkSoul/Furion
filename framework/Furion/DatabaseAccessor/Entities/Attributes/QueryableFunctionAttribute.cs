// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.EntityFrameworkCore;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 实体函数配置特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public class QueryableFunctionAttribute : DbFunctionAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">函数名</param>
    /// <param name="schema">架构名</param>
    public QueryableFunctionAttribute(string name, string schema = null) : base(name, schema)
    {
        DbContextLocators = Array.Empty<Type>();
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">函数名</param>
    /// <param name="schema">架构名</param>
    /// <param name="dbContextLocators">数据库上下文定位器</param>
    public QueryableFunctionAttribute(string name, string schema = null, params Type[] dbContextLocators) : base(name, schema)
    {
        DbContextLocators = dbContextLocators ?? Array.Empty<Type>();
    }

    /// <summary>
    /// 数据库上下文定位器
    /// </summary>
    public Type[] DbContextLocators { get; set; }
}