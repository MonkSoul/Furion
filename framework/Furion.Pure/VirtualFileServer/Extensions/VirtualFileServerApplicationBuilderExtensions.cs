// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// 虚拟文件服务中间件
/// </summary>
[SuppressSniffer]
public static class VirtualFileServerApplicationBuilderExtensions
{
    /// <summary>
    /// 虚拟文件系统中间件
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseVirtualFileServer(this IApplicationBuilder app)
    {
        return app;
    }
}