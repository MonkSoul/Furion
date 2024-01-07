// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 执行代理基特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public class SqlProxyAttribute : Attribute
{
    /// <summary>
    /// 配置拦截唯一 ID（比方法名优先级高）
    /// </summary>
    public string InterceptorId { get; set; }
}