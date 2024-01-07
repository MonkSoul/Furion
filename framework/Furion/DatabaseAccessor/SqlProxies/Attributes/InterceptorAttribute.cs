// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 代理拦截器
/// </summary>
/// <remarks>如果贴在静态方法中且 InterceptorId/MethodName 为空，则为全局拦截</remarks>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public class InterceptorAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public InterceptorAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="interceptorIds"></param>
    public InterceptorAttribute(params string[] interceptorIds)
    {
        InterceptorIds = interceptorIds;
    }

    /// <summary>
    /// 方法名称
    /// </summary>
    public string[] InterceptorIds { get; set; }
}