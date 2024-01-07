// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RemoteRequest;

/// <summary>
/// HttpGet 请求
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public class GetAttribute : HttpMethodBaseAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public GetAttribute() : base(HttpMethod.Get)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="requestUrl"></param>
    public GetAttribute(string requestUrl) : base(requestUrl, HttpMethod.Get)
    {
    }
}