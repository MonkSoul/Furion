// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Furion;

/// <summary>
/// 模拟 Startup，解决 .NET5 下不设置 UseStartup 时出现异常问题
/// </summary>
[SuppressSniffer]
public sealed class FakeStartup
{
    /// <summary>
    /// 配置服务
    /// </summary>
    public void ConfigureServices(IServiceCollection _)
    {
    }

    /// <summary>
    /// 配置请求
    /// </summary>
    public void Configure(IApplicationBuilder _)
    {
    }
}