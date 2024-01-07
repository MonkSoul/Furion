// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Furion.HostingStartup))]

namespace Furion;

/// <summary>
/// 配置程序启动时自动注入
/// </summary>
[SuppressSniffer]
public sealed class HostingStartup : IHostingStartup
{
    /// <summary>
    /// 配置应用启动
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(IWebHostBuilder builder)
    {
        InternalApp.ConfigureApplication(builder);
    }
}