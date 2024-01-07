// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RemoteRequest;

/// <summary>
/// 配置查询参数
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Parameter)]
public class QueryStringAttribute : ParameterBaseAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public QueryStringAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="alias"></param>
    public QueryStringAttribute(string alias)
    {
        Alias = alias;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="alias"></param>
    /// <param name="format"></param>
    public QueryStringAttribute(string alias, string format)
        : this(alias)
    {
        Format = format;
    }

    /// <summary>
    /// 参数别名
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// 时间类型参数格式化
    /// </summary>
    public string Format { get; set; }
}