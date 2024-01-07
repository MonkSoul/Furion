// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DynamicApiController;

/// <summary>
/// 将 Action 所有参数 [FromQuery] 化
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public sealed class QueryParametersAttribute : Attribute
{
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public QueryParametersAttribute()
    {
    }
}