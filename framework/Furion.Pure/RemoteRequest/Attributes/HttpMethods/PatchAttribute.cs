// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RemoteRequest;

/// <summary>
/// HttpPatch 请求
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public class PatchAttribute : HttpMethodBaseAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public PatchAttribute() : base(HttpMethod.Patch)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="requestUrl"></param>
    public PatchAttribute(string requestUrl) : base(requestUrl, HttpMethod.Patch)
    {
    }
}