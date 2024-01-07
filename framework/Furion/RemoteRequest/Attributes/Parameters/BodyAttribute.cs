// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RemoteRequest;

/// <summary>
/// 配置Body参数
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Parameter)]
public class BodyAttribute : ParameterBaseAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public BodyAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="contentType"></param>
    public BodyAttribute(string contentType)
    {
        ContentType = contentType;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="contentType"></param>
    /// <param name="encoding"></param>
    public BodyAttribute(string contentType, string encoding)
    {
        ContentType = contentType;
        Encoding = encoding;
    }

    /// <summary>
    /// 内容类型
    /// </summary>
    public string ContentType { get; set; } = "application/json";

    /// <summary>
    /// 内容编码
    /// </summary>
    public string Encoding { get; set; } = "UTF-8";
}