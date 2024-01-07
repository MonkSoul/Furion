// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion;

namespace Microsoft.Extensions.Hosting;

/// <summary>
/// 监听泛型主机启动事件
/// </summary>
internal class GenericHostLifetimeEventsHostedService : IHostedService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="host"></param>
    public GenericHostLifetimeEventsHostedService(IHost host)
    {
        // 存储根服务
        InternalApp.RootServices = host.Services;
    }

    /// <summary>
    /// 监听主机启动
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 监听主机停止
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}